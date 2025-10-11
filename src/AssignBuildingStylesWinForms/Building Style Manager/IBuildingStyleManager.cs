// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    internal interface IBuildingStyleManager
    {
        Dictionary<uint, BuildingStyleInfo> GetBuildingStyles();

        void SetBuildingStyles(Dictionary<uint, BuildingStyleInfo> value);
    }
}
