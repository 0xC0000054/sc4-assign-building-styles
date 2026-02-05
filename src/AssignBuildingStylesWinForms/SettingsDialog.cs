// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    internal partial class SettingsDialog : Form
    {
        public SettingsDialog(Settings settings)
        {
            InitializeComponent();
            Settings = settings;
            installFolderPathTextBox.Text = settings.InstallFolderPath;
            pluginFolderPathTextBox.Text = settings.PluginFolderPath;
        }

        public Settings Settings { get; private set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            Settings.InstallFolderPath = installFolderPathTextBox.Text;
            Settings.PluginFolderPath = pluginFolderPathTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void installFolderPathBrowseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                installFolderPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void pluginFolderPathBrowseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                pluginFolderPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
