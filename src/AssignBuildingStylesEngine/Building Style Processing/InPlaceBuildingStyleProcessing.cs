// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;

namespace AssignBuildingStylesEngine
{
    public sealed class InPlaceBuildingStyleProcessing : BuildingStyleProcessingBase
    {
        public InPlaceBuildingStyleProcessing(IReadOnlyList<uint>? buildingStyleIds,
                                              bool? isWallToWall,
                                              IStatusWriter? statusWriter)
            : base(buildingStyleIds, isWallToWall, statusWriter)
        {
        }

        protected override bool ProcessBuildingExemplar(DBPFFile file, TGI exemplarTGI, Exemplar exemplar)
        {
            bool exemplarModified = false;

            if (buildingStyleIds != null)
            {
                exemplar.Properties.AddOrUpdate(new ExemplarPropertyUInt32(BuildingStylesPropertyID,
                                                                           buildingStyleIds));
                exemplarModified = true;
            }

            if (isWallToWall.HasValue)
            {
                exemplar.Properties.AddOrUpdate(new ExemplarPropertyBoolean(BuildingIsWallToWallPropertyID,
                                                                            isWallToWall.Value));
                exemplarModified = true;
            }

            if (exemplarModified)
            {
                // Add the Building Styles PIMX Template Marker when the ExemplarCategory property is present.
                // This is done to prevent the Building Style DLL from detecting style id 0x2004 as a PIMX placeholder.
                if (exemplar.Properties.Contains(ExemplarCategoryPropertyID))
                {
                    exemplar.Properties.AddOrUpdate(new ExemplarPropertyBoolean(BuildingStylesPIMXTemplateMarkerPropertyID,
                                                                                false));
                }

                byte[] data = exemplar.Encode();

                file.Update(exemplarTGI, data, compress: true);
            }

            return exemplarModified;
        }

        protected override void OnFileModificationsComplete(DBPFFile file)
        {
            file.Save();
        }
    }
}
