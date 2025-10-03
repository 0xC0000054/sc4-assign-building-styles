// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    internal sealed class BackgroundWorkerArgs
    {
        public BackgroundWorkerArgs(IReadOnlyCollection<string> inputFiles,
                                    bool recurseSubdirectories,
                                    string styleText,
                                    bool wallToWall,
                                    string exemplarPatchPath)
        {
            InputFiles = inputFiles ?? throw new ArgumentNullException(nameof(inputFiles));
            RecurseSubdirectories = recurseSubdirectories;
            StyleIdText = styleText ?? throw new ArgumentNullException(nameof(styleText));
            WallToWall = wallToWall;
            ExemplarPatchPath = exemplarPatchPath ?? throw new ArgumentNullException(nameof(exemplarPatchPath));
        }

        public IReadOnlyCollection<string> InputFiles { get; }

        public bool RecurseSubdirectories { get; }

        public string StyleIdText { get; }

        public bool WallToWall { get; }

        public string ExemplarPatchPath { get; }
    }
}
