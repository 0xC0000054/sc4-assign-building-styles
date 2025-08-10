// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using CommandLiners;
using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;
using Microsoft.Extensions.Configuration;
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
                MapOptions<ProgramOptions> map = new();

                OptionSet optionSet = new()
                {
                    {
                        "s|building-styles=",
                        "The hexadecimal building style ids. Multiple values must be separated by commas.",
                        value => map.Add(value, m => m.BuildingStyles)
                    },
                    {
                        "w|wall-to-wall=",
                        "Indicates if the building are wall to wall (W2W). Must be true or false.",
                        value => map.Add(value, m => m.IsWallToWall)
                    },
                    {
                        "r|recurse-subdirectories",
                        "Search subdirectories of the input folder for DBPF files.",
                        value => map.Add(value != null ? bool.TrueString : bool.FalseString, m => m.RecurseSubdirectories)
                    },
                    {
                        "i|install-folder-path=",
                        "The path of your SimCity 4 installation folder. Used to find parent cohorts.",
                        value => map.Add(value, m => m.InstallFolderPath)
                    },
                    {
                        "p|plugin-folder-path=",
                        "The path of your SimCity 4 plugin folder. Used to find parent cohorts.",
                        value => map.Add(value, m => m.PluginFolderPath)
                    },
                };

                List<string> remainingArgs = optionSet.Parse(args);

                if (remainingArgs.Count != 1)
                {
                    // Unknown or invalid option.
                    ShowUsage(optionSet);
                    return;
                }

                var builder = new ConfigurationBuilder()
                    .AddIniFile("SC4AssignBuildingStyles.ini")
                    .AddCommandLineOptions(map)
                    .Build();

                ProgramOptions programOptions = new();
                builder.Bind(programOptions);

                IReadOnlyList<uint>? buildingStyleIds = ProgramOptionsParsing.ParseBuildingStylesOption(programOptions.BuildingStyles);
                bool? isWallToWall = ProgramOptionsParsing.ParseWallToWallOption(programOptions.IsWallToWall);
                bool recurseSubdirectories = programOptions.RecurseSubdirectories;
                ExemplarUtil.InitializeCohortCollection(programOptions.InstallFolderPath, programOptions.PluginFolderPath);

                if (buildingStyleIds != null)
                {
                    if (isWallToWall.HasValue)
                    {
                        Console.WriteLine("Building Styles: {0}, Is Wall to Wall: {1}", programOptions.BuildingStyles, isWallToWall.Value);
                    }
                    else
                    {
                        Console.WriteLine("Building Styles: {0}", programOptions.BuildingStyles);
                    }
                }
                else if (isWallToWall.HasValue)
                {
                    Console.WriteLine("Is Wall to Wall: {0}", isWallToWall.Value);
                }
                else
                {
                    Console.WriteLine("The building style and/or wall to wall options must be set.");
                    Console.WriteLine("Exiting due to the program having nothing to modify.");
                    return;
                }

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
            const uint BuildingStylesPIMXTemplateMarkerPropertyID = 0xAA1DD402;
            const uint ExemplarCategoryPropertyID = 0x2C8F8746;

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
                        // Add the Building Styles PIMX Template Marker when the ExemplarCategory property is present.
                        // This is done to prevent the Building Style DLL from detecting style id 0x2004 as a PIMX placeholder.
                        if (exemplar.Properties.Contains(ExemplarCategoryPropertyID))
                        {
                            exemplar.Properties[BuildingStylesPIMXTemplateMarkerPropertyID] = new ExemplarPropertyBoolean(BuildingStylesPIMXTemplateMarkerPropertyID,
                                                                                                                          false);
                        }

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
