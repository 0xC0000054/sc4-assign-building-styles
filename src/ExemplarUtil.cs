// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;
using System.Diagnostics.CodeAnalysis;

namespace SC4AssignBuildingStyles
{
    internal static class ExemplarUtil
    {
        private const uint CohortTypeID = 0x05342861;
        private const uint ExemplarTypeID = 0x6534284A;

        private static readonly Dictionary<TGI, Exemplar> cohorts = [];

        internal static bool IsCohort(uint typeId) => typeId == CohortTypeID;

        internal static bool IsExemplar(uint typeId) => typeId == ExemplarTypeID;

        internal static void InitializeCohortCollection(string installFolderPath, string pluginFolderPath)
        {
            if (!string.IsNullOrWhiteSpace(installFolderPath))
            {
                LoadCohortsFromDirectory(installFolderPath);
            }

            if (!string.IsNullOrWhiteSpace(pluginFolderPath))
            {
                LoadCohortsFromDirectory(pluginFolderPath, recurseSubdirectories: true);
            }
        }

        /// <summary>
        /// Attempts to find the specified property in the exemplar and any parent cohorts.
        /// </summary>
        /// <param name="exemplar">The exemplar to search.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="property">The output property.</param>
        /// <returns><c>true</c> is successful; otherwise, <c>false</c>.</returns>
        internal static bool TryGetProperty<TProperty>(Exemplar exemplar,
                                                       uint propertyId,
                                                       [NotNullWhen(true)] out TProperty? property) where TProperty : ExemplarProperty
        {
            property = null;
            bool result = false;

            if (exemplar.Properties.TryGetValue(propertyId, out ExemplarProperty? loadedProperty))
            {
                property = loadedProperty as TProperty;
                result = property != null;
            }
            else
            {
                TGI parentCohort = exemplar.ParentCohort;

                while (parentCohort != TGI.Empty)
                {
                    if (cohorts.TryGetValue(parentCohort, out Exemplar? cohort))
                    {
                        if (cohort.Properties.TryGetValue(propertyId, out loadedProperty))
                        {
                            property = loadedProperty as TProperty;
                            result = property != null;
                            break;
                        }

                        parentCohort = cohort.ParentCohort;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private static void LoadCohortsFromDirectory(string path, bool recurseSubdirectories = false)
        {
            EnumerationOptions options = new() { RecurseSubdirectories = recurseSubdirectories };

            foreach (var filePath in Directory.EnumerateFiles(path, "*.DAT", options))
            {
                try
                {
                    using (DBPFFile file = new(filePath))
                    {
                        var cohortIndices = file.Index.Where(i => IsCohort(i.Type));

                        foreach (DBPFIndexEntry index in cohortIndices)
                        {
                            DBPFEntry entry = file.GetEntry(index);
                            Exemplar exemplar = new(entry.GetUncompressedData());

                            cohorts[index.TGI] = exemplar;
                        }
                    }
                }
                catch (DBPFException)
                {
                    // Ignore it.
                }
            }
        }
    }
}
