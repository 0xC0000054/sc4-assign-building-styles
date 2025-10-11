// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    internal sealed class BuildingStyleManager : IBuildingStyleManager
    {
        private readonly Lock sync;
        private Dictionary<uint, BuildingStyleInfo>? buildingStyles;

        public BuildingStyleManager()
        {
            buildingStyles = null;
            sync = new Lock();
            Dirty = false;
        }

        public bool Dirty { get; private set; }

        public void Save()
        {
            if (buildingStyles is not null && Dirty)
            {
                lock (sync)
                {
                    BuildingStylesCacheFile.Save(buildingStyles);
                    Dirty = false;
                }
            }
        }

        public Dictionary<uint, BuildingStyleInfo> GetBuildingStyles()
        {
            Dictionary<uint, BuildingStyleInfo> items;

            lock (sync)
            {
                if (buildingStyles is null)
                {
                    try
                    {
                        buildingStyles = BuildingStylesCacheFile.Load();
                        Dirty = false;
                    }
                    catch (FileNotFoundException)
                    {
                        buildingStyles = BuildingStylesOnlineSpreadsheet.Load();
                        Dirty = true;
                    }
                }

                items = new Dictionary<uint, BuildingStyleInfo>(buildingStyles);
            }

            return items;
        }

        public void SetBuildingStyles(Dictionary<uint, BuildingStyleInfo> value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            lock (sync)
            {
                buildingStyles = new Dictionary<uint, BuildingStyleInfo>(value);
                Dirty = true;
            }
        }
    }
}
