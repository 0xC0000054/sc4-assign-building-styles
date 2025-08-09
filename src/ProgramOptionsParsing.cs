// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Globalization;

namespace SC4AssignBuildingStyles
{
    internal static class ProgramOptionsParsing
    {
        public static IReadOnlyList<uint>? ParseBuildingStylesOption(ReadOnlySpan<char> data)
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
        }

        public static bool? ParseWallToWallOption(ReadOnlySpan<char> data)
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

        private static bool TryParseHexNumber(ReadOnlySpan<char> chars, out uint value)
        {
            // TryParse returns false if the hexadecimal number starts with a 0x or 0X prefix.
            if (chars.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                chars = chars[2..];
            }

            return uint.TryParse(chars, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
        }
    }
}
