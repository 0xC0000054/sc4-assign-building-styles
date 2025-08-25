// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using CommandLiners;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using System.CodeDom.Compiler;

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

                if (remainingArgs.Count != 1 && remainingArgs.Count != 2)
                {
                    // Unknown or invalid option.
                    ShowUsage(optionSet);
                    return;
                }

                var builder = new ConfigurationBuilder()
                    .AddIniFile("SC4AssignBuildingStyles.ini")
                    .AddCommandLineOptions(map)
                    .Build();

                ProgramOptions programOptions = new(builder);

                IReadOnlyList<uint>? buildingStyleIds = programOptions.BuildingStyles;
                bool? isWallToWall = programOptions.IsWallToWall;
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

                using (IndentedTextWriter statusWriter = new(Console.Out, " "))
                {
                    BuildingStyleProcessingBase buildingStyleProcessing;

                    if (remainingArgs.Count == 2)
                    {
                        string exemplarPatchPath = remainingArgs[1];

                        buildingStyleProcessing = new ExemplarPatchBuildingStyleProcessing(exemplarPatchPath,
                                                                                           buildingStyleIds,
                                                                                           isWallToWall,
                                                                                           statusWriter);
                    }
                    else
                    {
                        buildingStyleProcessing = new InPlaceBuildingStyleProcessing(buildingStyleIds,
                                                                                     isWallToWall,
                                                                                     statusWriter);
                    }

                    if (Directory.Exists(input))
                    {
                        // The path is a directory, update all files in the folder.
                        buildingStyleProcessing.ProcessDirectory(input, recurseSubdirectories);
                    }
                    else
                    {
                        buildingStyleProcessing.ProcessFile(input);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ShowUsage(OptionSet options)
        {
            Console.WriteLine("SC4AssignBuildingStyles OPTIONS <file or directory> [exemplar patch output path]");
            Console.WriteLine("If the input path is a directory, the options will be applied to all files within it.");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
