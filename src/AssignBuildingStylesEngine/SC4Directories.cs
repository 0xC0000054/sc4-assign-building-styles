// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using Microsoft.Win32;
using System.Runtime.Versioning;

namespace AssignBuildingStylesEngine
{
    [SupportedOSPlatform("windows")]
    public static class SC4Directories
    {
        public static string GetInstallFolderPathFromRegistry()
        {
            string path = string.Empty;

            // We need to open the 32-bit registry view due to SC4 being a 32-bit application.
            using (RegistryKey root = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using (RegistryKey? sc4SubKey = root.OpenSubKey(@"SOFTWARE\Maxis\SimCity 4"))
            {
                if (sc4SubKey != null)
                {
                    if (sc4SubKey.GetValue("Install Dir") is string installDir)
                    {
                        path = installDir;
                    }
                }
            }

            return path;
        }

        public static string GetDefaultUserPluginsPath()
        {
            string path = string.Empty;

            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (!string.IsNullOrWhiteSpace(documentsFolder))
            {
                path = Path.Combine(documentsFolder, "SimCity 4", "Plugins");
            }

            return path;
        }
    }
}
