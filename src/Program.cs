// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;
using Mono.Options;
using System.Globalization;

namespace SC4AssignBuildingStyles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string buildingStylesOption = string.Empty;
                string wallToWallOption = string.Empty;
                bool recurseSubdirectories = false;
                string installFolderPath = string.Empty;
                string pluginFolderPath = string.Empty;

                OptionSet options = new()
                {
                    {
                        "s|building-styles=",
                        "The hexadecimal building style ids. Multiple values must be separated by commas.",
                        value => buildingStylesOption = value
                    },
                    {
                        "w|wall-to-wall=",
                        "Indicates if the building are wall to wall (W2W). Must be true or false.",
                        value => wallToWallOption = value
                    },
                    {
                        "r|recurse-subdirectories",
                        "Search subdirectories of the input folder for DBPF files.",
                        value => recurseSubdirectories = value != null
                    },
                    {
                        "i|install-folder-path=",
                        "The path of your SimCity 4 installation folder. Used to find parent cohorts.",
                        value => installFolderPath = value
                    },
                    {
                        "p|plugin-folder-path=",
                        "The path of your SimCity 4 plugin folder. Used to find parent cohorts.",
                        value => pluginFolderPath = value
                    },
                };

                // The arguments must include the file name and at least one command.
                if (args.Length < 2)
                {
                    ShowUsage(options);
                    return;
                }

                List<string> remainingArgs = options.Parse(args);

                if (remainingArgs.Count != 1)
                {
                    // Unknown or invalid option.
                    ShowUsage(options);
                    return;
                }

                IReadOnlyList<uint>? buildingStyleIds = CommandLineArgs.ParseBuildingStylesOption(buildingStylesOption);
                bool? isWallToWall = CommandLineArgs.ParseWallToWallOption(wallToWallOption);

                ExemplarUtil.InitializeCohortCollection(installFolderPath, pluginFolderPath);

                string input = remainingArgs[0];

                if (Directory.Exists(input))
                {
                    // The path is a directory, update all files in the folder.
                    foreach (string path in DBPFDirectoryEnumerator.Create(input, recurseSubdirectories))
                    {
                        ProcessDBPFFile(path, buildingStyleIds, isWallToWall, input);
                    }
                }
                else
                {
                    ProcessDBPFFile(input, buildingStyleIds, isWallToWall);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ProcessDBPFFile(string path,
                                            IReadOnlyList<uint>? styles,
                                            bool? isWallToWall,
                                            string rootDirectory = "")
        {
            string fileName;

            if (!string.IsNullOrEmpty(rootDirectory))
            {
                fileName = Path.GetRelativePath(rootDirectory, path);
            }
            else
            {
                fileName = Path.GetFileName(path);
            }

            Console.WriteLine("Processing {0}:", fileName);

            try
            {
                ProcessExemplarResult processExemplarResult;

                using (DBPFFile file = new(path))
                {
                    processExemplarResult = ProcessBuildingExemplars(file, styles, isWallToWall);
                }

                if (processExemplarResult.errors.Count > 0)
                {
                    Console.WriteLine("  Processed {0} building exemplar(s) with {1} error(s).",
                                      processExemplarResult.totalPatchedBuildingExemplars,
                                      processExemplarResult.errors.Count);
                    Console.WriteLine("    Errors:");

                    foreach (var error in processExemplarResult.errors)
                    {
                        Console.WriteLine("      {0}: {1}", error.Item1, error.Item2.Message);
                    }
                }
                else
                {
                    Console.WriteLine("  Processed {0} building exemplar(s).",
                                      processExemplarResult.totalPatchedBuildingExemplars);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("  {0}", ex.Message);
            }
        }

        private sealed class ProcessExemplarResult
        {
            public readonly ulong totalPatchedBuildingExemplars;
            public readonly IReadOnlyList<Tuple<string, Exception>> errors;

            public ProcessExemplarResult(ulong totalPatchedBuildingExemplars,
                                         IReadOnlyList<Tuple<string, Exception>> errors)
            {
                this.totalPatchedBuildingExemplars = totalPatchedBuildingExemplars;
                this.errors = errors ?? throw new ArgumentNullException(nameof(errors));
            }
        }

        private static ProcessExemplarResult ProcessBuildingExemplars(DBPFFile file,
                                                                      IReadOnlyList<uint>? styles,
                                                                      bool? isWallToWall)
        {
            const uint BuildingStylesPropertyID = 0xAA1DD400;
            const uint BuildingIsWallToWallPropertyID = 0xAA1DD401;

            ulong modifiedExemplarCount = 0;
            List<Tuple<string, Exception>> errors = [];

            var exemplarIndices = file.Index.Where(e => ExemplarUtil.IsExemplar(e.Type));

            foreach (var index in exemplarIndices)
            {
                DBPFEntry entry = file.GetEntry(index);

                Exemplar? exemplar = null;

                try
                {
                    byte[] data = entry.GetUncompressedData();
                    exemplar = new(data);
                }
                catch (Exception ex)
                {
                    string message = string.Format(CultureInfo.InvariantCulture,
                                                   "Error when parsing exemplar 0x{0:X8}, 0x{1:X8}, 0x{2:X8}",
                                                   index.Type,
                                                   index.Group,
                                                   index.Instance);

                    errors.Add(new Tuple<string, Exception>(message, ex));
                }

                if (exemplar != null && IsBuildingExemplar(exemplar))
                {
                    bool exemplarModified = false;

                    if (styles != null)
                    {
                        exemplar.Properties[BuildingStylesPropertyID] = new ExemplarPropertyUInt32(BuildingStylesPropertyID, styles);
                        exemplarModified = true;
                    }

                    if (isWallToWall.HasValue)
                    {
                        exemplar.Properties[BuildingIsWallToWallPropertyID] = new ExemplarPropertyBoolean(BuildingIsWallToWallPropertyID,
                                                                                                          isWallToWall.Value);
                        exemplarModified = true;
                    }

                    if (exemplarModified)
                    {
                        byte[] data = exemplar.Encode();

                        file.Update(index.Type, index.Group, index.Instance, data, entry.IsCompressed);
                        modifiedExemplarCount++;
                    }
                }
            }

            if (modifiedExemplarCount > 0)
            {
                file.Save();
            }

            return new ProcessExemplarResult(modifiedExemplarCount, errors);

            static bool IsBuildingExemplar(Exemplar exemplar)
            {
                const uint ExemplarTypePropertyID = 0x00000010;
                const uint BuildingExemplarType = 2;

                bool result = false;

                if (!exemplar.IsCohort)
                {
                    if (ExemplarUtil.TryGetProperty(exemplar, ExemplarTypePropertyID, out ExemplarPropertyUInt32? property))
                    {
                        var values = property.Values;

                        if (values.Count == 1)
                        {
                            result = values[0] == BuildingExemplarType;
                        }
                    }
                }

                return result;
            }
        }

        private static void ShowUsage(OptionSet options)
        {
            Console.WriteLine("SC4AssignBuildingStyles OPTIONS <file or directory>");
            Console.WriteLine("If the input path is a directory, the options will be applied to all files within it.");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
