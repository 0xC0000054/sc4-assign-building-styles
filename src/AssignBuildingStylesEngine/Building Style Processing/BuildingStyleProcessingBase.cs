// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using DBPFSharp;
using DBPFSharp.FileFormat.Exemplar;
using DBPFSharp.FileFormat.Exemplar.Properties;
using System.CodeDom.Compiler;
using System.Globalization;

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
        private readonly IndentedTextWriter statusWriter;

        protected BuildingStyleProcessingBase(IReadOnlyList<uint>? buildingStyleIds,
                                              bool? isWallToWall,
                                              TextWriter statusWriter)
        {
            this.buildingStyleIds = buildingStyleIds;
            this.isWallToWall = isWallToWall;
            this.statusWriter = new IndentedTextWriter(statusWriter, " ");
        }

        public void ProcessDirectory(string input, bool recurseSubdirectories)
        {
            // The path is a directory, update all files in the folder.
            foreach (string path in DBPFDirectoryEnumerator.Create(input, recurseSubdirectories))
            {
                ProcessDBPFFile(path, input);
            }

            OnProcessingFilesComplete();
        }

        public void ProcessFile(string path)
        {
            ProcessDBPFFile(path);

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

            WriteStatus(0, "Processing {0}:", fileName);

            try
            {
                ProcessExemplarResult processExemplarResult;

                using (DBPFFile file = new(path))
                {
                    processExemplarResult = ProcessBuildingExemplars(file);
                }

                if (processExemplarResult.errors.Count > 0)
                {
                    WriteStatus(2, "Processed {0} building exemplar(s) with {1} error(s).",
                                processExemplarResult.totalPatchedBuildingExemplars,
                                processExemplarResult.errors.Count);
                    WriteStatus(4, "Errors:");

                    foreach (var error in processExemplarResult.errors)
                    {
                        WriteStatus(6, "{0}: {1}", error.Item1, error.Item2.Message);
                    }
                }
                else
                {
                    WriteStatus(2, "Processed {0} building exemplar(s).",
                                processExemplarResult.totalPatchedBuildingExemplars);
                }
            }
            catch (Exception ex)
            {
                WriteStatus(2, ex.Message);
            }
        }

        private ProcessExemplarResult ProcessBuildingExemplars(DBPFFile file)
        {
            ulong modifiedExemplarCount = 0;
            List<Tuple<string, Exception>> errors = [];

            var exemplarIndices = file.Index.Where(e => ExemplarUtil.IsExemplar(e.Type));

            foreach (var index in exemplarIndices)
            {
                DBPFEntry entry = file.GetEntry(index);

                Exemplar? exemplar = null;

                try
                {
                    byte[] data = entry.GetUncompressedData();
                    exemplar = new(data);
                }
                catch (Exception ex)
                {
                    string message = string.Format(CultureInfo.InvariantCulture,
                                                   "Error when parsing exemplar 0x{0:X8}, 0x{1:X8}, 0x{2:X8}",
                                                   index.Type,
                                                   index.Group,
                                                   index.Instance);

                    errors.Add(new Tuple<string, Exception>(message, ex));
                }

                if (exemplar != null && IsBuildingExemplar(exemplar))
                {
                    if (ProcessBuildingExemplar(file, index.TGI, exemplar))
                    {
                        modifiedExemplarCount++;
                    }
                }
            }

            if (modifiedExemplarCount > 0)
            {
                OnFileModificationsComplete(file);
            }

            return new ProcessExemplarResult(modifiedExemplarCount, errors);

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

        private sealed class ProcessExemplarResult
        {
            public readonly ulong totalPatchedBuildingExemplars;
            public readonly IReadOnlyList<Tuple<string, Exception>> errors;

            public ProcessExemplarResult(ulong totalPatchedBuildingExemplars,
                                         IReadOnlyList<Tuple<string, Exception>> errors)
            {
                this.totalPatchedBuildingExemplars = totalPatchedBuildingExemplars;
                this.errors = errors ?? throw new ArgumentNullException(nameof(errors));
            }
        }
    }
}
