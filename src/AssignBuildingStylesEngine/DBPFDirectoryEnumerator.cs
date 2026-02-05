// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.IO.Enumeration;

namespace AssignBuildingStylesEngine
{
    internal static class DBPFDirectoryEnumerator
    {
        public static IEnumerable<string> Create(string path, bool recurseSubdirectories)
        {
            return new FileSystemEnumerable<string>(path,
                                                    TransformEntry,
                                                    new EnumerationOptions() { RecurseSubdirectories = recurseSubdirectories })
            {
                ShouldIncludePredicate = ShouldIncludeEntry
            };
        }

        private static bool ShouldIncludeEntry(ref FileSystemEntry entry)
        {
            if (entry.IsDirectory)
            {
                return false;
            }

            ReadOnlySpan<char> fileExtension = Path.GetExtension(entry.FileName);

            if (fileExtension.Length == 0)
            {
                // Files without an extension are treated as potential .SC4* files, there
                // are released plugins that don't have a file extension.
                // For example, Bosham Church by mintoes
                return true;
            }

            return fileExtension.Equals(".DAT", StringComparison.OrdinalIgnoreCase)
                   || fileExtension.Equals(".SC4Desc", StringComparison.OrdinalIgnoreCase)
                   || fileExtension.Equals(".SC4Lot", StringComparison.OrdinalIgnoreCase)
                   || fileExtension.Equals(".SC4Model", StringComparison.OrdinalIgnoreCase);
        }

        private static string TransformEntry(ref FileSystemEntry entry) => entry.ToFullPath();
    }
}
