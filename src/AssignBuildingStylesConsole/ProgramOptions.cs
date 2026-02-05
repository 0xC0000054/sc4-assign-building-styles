// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace AssignBuildingStylesConsole
{
    internal sealed class ProgramOptions
    {
        public ProgramOptions(IConfiguration configuration)
        {
            BuildingStyles = ParseBuildingStylesOption(configuration[nameof(BuildingStyles)]);
            BuildingStylesString = configuration[nameof(BuildingStyles)];
            IsWallToWall = ParseWallToWallOption(configuration[nameof(IsWallToWall)]);
            RecurseSubdirectories = ParseBoolean(configuration[nameof(RecurseSubdirectories)]) ?? false;
            InstallFolderPath = configuration[nameof(InstallFolderPath)] ?? string.Empty;
            PluginFolderPath = configuration[nameof(PluginFolderPath)] ?? string.Empty;
            ExemplarPatchPath = configuration[nameof(ExemplarPatchPath)] ?? string.Empty;
        }

        public IReadOnlyList<uint>? BuildingStyles { get; }

        public string? BuildingStylesString { get; }

        public bool? IsWallToWall { get; }

        public bool RecurseSubdirectories { get; }

        public string InstallFolderPath { get; }

        public string PluginFolderPath { get; }

        public string ExemplarPatchPath { get; }

        private static List<uint>? ParseBuildingStylesOption(ReadOnlySpan<char> data)
        {
            List<uint>? styles = null;

            if (!data.IsEmpty)
            {
                styles = [];

                foreach (var range in data.Split(','))
                {
                    var segment = data[range];

                    if (TryParseHexNumber(segment, out uint style))
                    {
                        styles.Add(style);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("The style id '{0}' must be a hexadecimal number.", segment.ToString()));
                    }
                }

                if (styles.Count == 0)
                {
                    styles = null;
                }
            }

            return styles;

            static bool TryParseHexNumber(ReadOnlySpan<char> chars, out uint value)
            {
                // TryParse returns false if the hexadecimal number starts with a 0x or 0X prefix.
                if (chars.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    chars = chars[2..];
                }

                return uint.TryParse(chars, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
            }
        }

        private static bool? ParseWallToWallOption(ReadOnlySpan<char> data)
        {
            bool? result = null;

            if (!data.IsEmpty)
            {
                if (bool.TryParse(data, out bool value))
                {
                    result = value;
                }
                else
                {
                    throw new ArgumentException("The wall to wall option must be true or false.");
                }
            }

            return result;
        }

        private static bool? ParseBoolean(ReadOnlySpan<char> data)
        {
            return !data.IsEmpty && bool.TryParse(data, out bool value) ? value : null;
        }
    }
}
