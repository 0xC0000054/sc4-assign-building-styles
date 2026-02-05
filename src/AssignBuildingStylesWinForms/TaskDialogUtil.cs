// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using AssignBuildingStylesWinForms.Properties;

namespace AssignBuildingStylesWinForms
{
    internal static class TaskDialogUtil
    {
        internal static bool ShowErrorMessageBox(IWin32Window owner, string caption, Exception exception)
        {
            TaskDialogExpander expander = new()
            {
                CollapsedButtonText = Resources.ErrorTaskDialog_ShowDetails,
                ExpandedButtonText = Resources.ErrorTaskDialog_HideDetails,
                Text = exception.ToString()
            };

            TaskDialogPage page = new()
            {
                Caption = caption,
                Heading = exception.Message,
                Buttons = [TaskDialogButton.OK],
                Expander = expander,
                SizeToContent = true,
                Icon = TaskDialogIcon.Error
            };

            return TaskDialog.ShowDialog(owner, page) == TaskDialogButton.OK;
        }

        internal static bool ShowMessageBox(IWin32Window owner, string caption, string message)
        {
            TaskDialogPage page = new()
            {
                Caption = caption,
                Heading = message,
                Buttons = [TaskDialogButton.OK],
                Expander = null,
                SizeToContent = true,
                Icon = TaskDialogIcon.None
            };

            return TaskDialog.ShowDialog(owner, page) == TaskDialogButton.OK;
        }
    }
}
