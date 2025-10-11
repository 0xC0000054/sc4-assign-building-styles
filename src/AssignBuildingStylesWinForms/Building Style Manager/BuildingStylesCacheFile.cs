// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Text.Json;

namespace AssignBuildingStylesWinForms
{
    internal static class BuildingStylesCacheFile
    {
        private static readonly string CacheFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)!, "BuildingStyleCache.json");

        public static Dictionary<uint, BuildingStyleInfo> Load()
        {
            Dictionary<uint, BuildingStyleInfo> buildingStyles;

            using (FileStream stream = new(CacheFilePath, FileMode.Open, FileAccess.Read))
            {
                var collection = JsonSerializer.Deserialize<Dictionary<uint, BuildingStyleInfo>>(stream);

                if (collection is not null)
                {
                    buildingStyles = collection;
                }
                else
                {
                    buildingStyles = [];
                }
            }

            return buildingStyles;
        }

        public static void Save(Dictionary<uint, BuildingStyleInfo> buildingStyles)
        {
            using (FileStream stream = new(CacheFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                JsonSerializer.Serialize(stream, buildingStyles);
            }
        }
    }
}
