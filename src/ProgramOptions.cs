// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace SC4AssignBuildingStyles
{
    internal sealed class ProgramOptions
    {
        public ProgramOptions()
        {
            BuildingStyles = string.Empty;
            IsWallToWall = string.Empty;
            RecurseSubdirectories = false;
            InstallFolderPath = string.Empty;
            PluginFolderPath = string.Empty;
        }

        public string BuildingStyles { get; set; }

        public string IsWallToWall { get; set; }

        public bool RecurseSubdirectories { get; set; }

        public string InstallFolderPath { get; set; }

        public string PluginFolderPath { get; set; }
    }
}
