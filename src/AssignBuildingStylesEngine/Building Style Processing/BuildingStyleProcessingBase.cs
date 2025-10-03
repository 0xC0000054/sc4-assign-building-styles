// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using AssignBuildingStylesEngine.Properties;
using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;
using System.CodeDom.Compiler;

namespace AssignBuildingStylesEngine
{
    public abstract class BuildingStyleProcessingBase
    {
        protected const uint BuildingStylesPropertyID = 0xAA1DD400;
        protected const uint BuildingIsWallToWallPropertyID = 0xAA1DD401;
        protected const uint BuildingStylesPIMXTemplateMarkerPropertyID = 0xAA1DD402;
        protected const uint ExemplarCategoryPropertyID = 0x2C8F8746;

        protected readonly IReadOnlyList<uint>? buildingStyleIds;
        protected readonly bool? isWallToWall;
        private readonly IndentedTextWriter? statusWriter;

        protected BuildingStyleProcessingBase(IReadOnlyList<uint>? buildingStyleIds,
                                              bool? isWallToWall,
                                              IndentedTextWriter statusWriter)
        {
            this.buildingStyleIds = buildingStyleIds;
            this.isWallToWall = isWallToWall;
            this.statusWriter = statusWriter;
        }

        public void ProcessDirectory(string input, bool recurseSubdirectories)
        {
            // The path is a directory, update all files in the folder.
            foreach (string path in DBPFDirectoryEnumerator.Create(input, recurseSubdirectories))
            {
                ProcessDBPFFile(path, input);
            }
        }

        public void ProcessFile(string path)
        {
            ProcessDBPFFile(path);
        }

        public void ProcessingFilesComplete()
        {
            OnProcessingFilesComplete();
        }

        /// <summary>
        /// Called when modifications are complete for the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        protected virtual void OnFileModificationsComplete(DBPFFile file)
        {
        }

        /// <summary>
        /// Called after finishing processing of the files.
        /// </summary>
        protected virtual void OnProcessingFilesComplete()
        {
        }

        protected abstract bool ProcessBuildingExemplar(DBPFFile file, TGI exemplarTGI, Exemplar exemplar);

        private void ProcessDBPFFile(string path, string parentDirectoryRoot = "")
        {
            string fileName;

            if (!string.IsNullOrEmpty(parentDirectoryRoot))
            {
                fileName = Path.GetRelativePath(parentDirectoryRoot, path);
            }
            else
            {
                fileName = Path.GetFileName(path);
            }

            WriteStatus(0, Resources.ProcessingFormat, fileName);

            try
            {
                using (DBPFFile file = new(path))
                {
                    ProcessBuildingExemplars(file);
                }
            }
            catch (Exception ex)
            {
                WriteStatus(2, ex.Message);
            }
        }

        private void ProcessBuildingExemplars(DBPFFile file)
        {
            bool processedBuildingExemplar = false;

            var exemplarIndices = file.Index.Where(e => ExemplarUtil.IsExemplar(e.Type));

            foreach (var index in exemplarIndices)
            {
                DBPFEntry entry = file.GetEntry(index);

                try
                {
                    byte[] data = entry.GetUncompressedData();
                    Exemplar exemplar = new(data);

                    if (IsBuildingExemplar(exemplar))
                    {
                        if (ProcessBuildingExemplar(file, index.TGI, exemplar))
                        {
                            processedBuildingExemplar = true;
                            WriteStatus(2,
                                        Resources.ProcessedBuildingExemplarFormat,
                                        index.Type,
                                        index.Group,
                                        index.Instance);
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteStatus(2,
                                Resources.ExemplarProcessingErrorFormat,
                                index.Type,
                                index.Group,
                                index.Instance,
                                ex.Message);
                }
            }

            if (processedBuildingExemplar)
            {
                OnFileModificationsComplete(file);
            }
            else
            {
                WriteStatus(2, Resources.FileDoesNotContainBuildingExemplars);
            }

            static bool IsBuildingExemplar(Exemplar exemplar)
            {
                const uint ExemplarTypePropertyID = 0x00000010;
                const uint BuildingExemplarType = 2;

                bool result = false;

                if (!exemplar.IsCohort)
                {
                    if (ExemplarUtil.TryGetProperty(exemplar, ExemplarTypePropertyID, out ExemplarPropertyUInt32? property))
                    {
                        var values = property.Values;

                        if (values.Count == 1)
                        {
                            result = values[0] == BuildingExemplarType;
                        }
                    }
                }

                return result;
            }
        }

        protected void WriteStatus(byte indent, string format, params object[] args)
        {
            if (statusWriter != null)
            {
                statusWriter.Indent += indent;

                statusWriter.WriteLine(format, args);

                statusWriter.Indent -= indent;
            }
        }
    }
}
