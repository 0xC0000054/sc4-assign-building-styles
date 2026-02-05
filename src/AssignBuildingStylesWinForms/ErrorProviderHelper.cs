// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    internal static class ErrorProviderHelper
    {
        internal static void SetIconFromOS(ErrorProvider errorProvider)
        {
            int width = errorProvider.Icon.Width;
            errorProvider.Icon = SystemIcons.GetStockIcon(StockIconId.Error, width);
        }
    }
}
