// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Runtime.Serialization;

namespace AssignBuildingStylesWinForms
{
    [DataContract(Name = "Settings", Namespace = "")]
    internal sealed class Settings
    {
        private string installFolderPath;
        private string pluginFolderPath;

        public Settings()
        {
            installFolderPath = string.Empty;
            pluginFolderPath = string.Empty;
            Dirty = false;
        }

        [DataMember(Name = nameof(InstallFolderPath))]
        public string InstallFolderPath
        {
            get => installFolderPath;
            set
            {
                if (!string.Equals(installFolderPath, value, StringComparison.OrdinalIgnoreCase))
                {
                    installFolderPath = value;
                    Dirty = true;
                }
            }
        }

        [DataMember(Name = nameof(PluginFolderPath))]
        public string PluginFolderPath
        {
            get => pluginFolderPath;
            set
            {
                if (!string.Equals(pluginFolderPath, value, StringComparison.OrdinalIgnoreCase))
                {
                    pluginFolderPath = value;
                    Dirty = true;
                }
            }
        }

        public bool Dirty { get; set; }
    }
}
