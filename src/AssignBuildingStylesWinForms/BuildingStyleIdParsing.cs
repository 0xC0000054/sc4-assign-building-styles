// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Globalization;
using System.Text.RegularExpressions;

namespace AssignBuildingStylesWinForms
{
    internal static partial class BuildingStyleIdParsing
    {
        /// <summary>
        /// Determines whether the specified text is a is valid style list.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> if the specified text is a is valid style list; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The style list is a comma separated list of hexadecimal styles ids with the 0x prefix.
        /// </remarks>
        internal static bool IsValidStyleList(ReadOnlySpan<char> text)
        {
            return !text.IsEmpty
                && CommaSeparatedHexRegex().IsMatch(text);
        }

        internal static List<uint> ParseStyleList(ReadOnlySpan<char> text)
        {
            if (!IsValidStyleList(text))
            {
                throw new InvalidOperationException("The style id text must be comma separated hexadecimal numbers.");
            }

            var result = new List<uint>();

            foreach (var range in text.Split(','))
            {
                var segment = text[range];

                // Remove the 0x prefix as uint.Parse thrown an exception if it is present.
                result.Add(uint.Parse(segment[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture));
            }

            return result;
        }

        [GeneratedRegex("^0x[0-9a-f]+(?:,0x[0-9a-f]+)*$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
        private static partial Regex CommaSeparatedHexRegex();
    }
}
