// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;
using System.CodeDom.Compiler;

namespace AssignBuildingStylesEngine
{
    public sealed class ExemplarPatchBuildingStyleProcessing : BuildingStyleProcessingBase
    {
        private readonly string exemplarPatchFilePath;
        private readonly List<TGI> exemplarsToPatch;
        private bool addBuildingStylesPIMXTemplateMarker;

        public ExemplarPatchBuildingStyleProcessing(string exemplarPatchFilePath,
                                                    IReadOnlyList<uint>? buildingStyleIds,
                                                    bool? isWallToWall,
                                                    IndentedTextWriter statusWriter)
            : base(buildingStyleIds, isWallToWall, statusWriter)
        {
            ArgumentNullException.ThrowIfNull(exemplarPatchFilePath);

            this.exemplarPatchFilePath = exemplarPatchFilePath;
            exemplarsToPatch = [];
        }

        protected override bool ProcessBuildingExemplar(DBPFFile file, TGI exemplarTGI, Exemplar exemplar)
        {
            exemplarsToPatch.Add(exemplarTGI);

            if (!addBuildingStylesPIMXTemplateMarker
                && exemplar.Properties.Contains(ExemplarCategoryPropertyID))
            {
                // Add the Building Styles PIMX Template Marker when the ExemplarCategory property is present.
                // This is done to prevent the Building Style DLL from detecting style id 0x2004 as a PIMX placeholder.
                addBuildingStylesPIMXTemplateMarker = true;
            }

            return true;
        }

        protected override void OnProcessingFilesComplete()
        {
            const uint ExemplarPatchGroupID = 0xB03697D1;
            const uint ExemplarPatchTargetsPropertyID = 0x0062E78A;

            if (exemplarsToPatch.Count == 0)
            {
                return;
            }

            using (DBPFFile file = new())
            {
                TGI patchTGI = new(ExemplarUtil.CohortTypeID,
                                   ExemplarPatchGroupID,
                                   TGI.RandomGroupOrInstanceId());

                Exemplar exemplar = new();

                // The exemplar patch property data is an array containing group/instance id pairs.
                // One pair for each exemplar to be patched.
                // See https://github.com/memo33/submenus-dll#exemplar-patching

                uint[] exemplarPatchPropertyData = new uint[checked(exemplarsToPatch.Count * 2)];

                for (int i = 0; i < exemplarsToPatch.Count; i++)
                {
                    TGI tgi = exemplarsToPatch[i];

                    int exemplarPatchIndex = i * 2;

                    exemplarPatchPropertyData[exemplarPatchIndex] = tgi.Group;
                    exemplarPatchPropertyData[exemplarPatchIndex + 1] = tgi.Instance;
                }

                exemplar.Properties.AddOrUpdate(new ExemplarPropertyUInt32(ExemplarPatchTargetsPropertyID,
                                                                           exemplarPatchPropertyData));

                if (buildingStyleIds != null)
                {
                    exemplar.Properties.AddOrUpdate(new ExemplarPropertyUInt32(BuildingStylesPropertyID,
                                                                               buildingStyleIds));
                }

                if (isWallToWall.HasValue)
                {
                    exemplar.Properties.AddOrUpdate(new ExemplarPropertyBoolean(BuildingIsWallToWallPropertyID,
                                                                                isWallToWall.Value));
                }

                if (addBuildingStylesPIMXTemplateMarker)
                {
                    exemplar.Properties.AddOrUpdate(new ExemplarPropertyBoolean(BuildingStylesPIMXTemplateMarkerPropertyID,
                                                                                false));
                }

                byte[] exemplarData = exemplar.Encode();

                file.AddOrUpdate(patchTGI, exemplarData, compress: true);
                file.Save(exemplarPatchFilePath);
            }

            WriteStatus(0, "Wrote exemplar patch to {0}", exemplarPatchFilePath);
        }
    }
}
