// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using AssignBuildingStylesEngine;
using AssignBuildingStylesWinForms.Properties;
using System.CodeDom.Compiler;
using System.ComponentModel;

namespace AssignBuildingStylesWinForms
{
    internal partial class Form1 : Form
    {        
        private readonly List<ListViewItem> inputFileListViewItems;
        private Settings settings;
        private bool cohortColectionInitialized;

        public Form1()
        {
            InitializeComponent();

            inputFileListViewItems = [];
            settings = new Settings();
            cohortColectionInitialized = false;
            UpdateErrorProviderIcon();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                try
                {
                    settings = SettingsFile.Load();
                }
                catch (FileNotFoundException)
                {
                    TaskDialogUtil.ShowMessageBox(this,
                                                  Text,
                                                  Resources.ConfigureOptionsText);
                    // Initialize with default values.
                    settings.InstallFolderPath = SC4Directories.GetInstallFolderPathFromRegistry();
                    settings.PluginFolderPath = SC4Directories.GetDefaultUserPluginsPath();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                e.Cancel = true;
            }
            else
            {
                if (settings.Dirty)
                {
                    try
                    {
                        SettingsFile.Save(settings);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex);
                    }
                }
            }

            base.OnClosing(e);
        }


        private bool ShowErrorMessage(Exception exception)
        {
            return TaskDialogUtil.ShowErrorMessageBox(this, Text, exception);
        }

        private void UpdateErrorProviderIcon()
        {
            int width = errorProvider.Icon.Width;
            errorProvider.Icon = SystemIcons.GetStockIcon(StockIconId.Error, width);
        }

        private void UpdateProcessingButtonEnabledState()
        {
            bool enabled = inputFileListViewItems.Count > 0 && buildingStyleIdTextBox.TextLength > 0;

            overwriteOriginalFilesButton.Enabled = enabled;
            saveExemplarPatchButton.Enabled = enabled;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;

            var args = (BackgroundWorkerArgs)e.Argument!;

            if (!cohortColectionInitialized)
            {
                cohortColectionInitialized = true;
                ExemplarUtil.InitializeCohortCollection(settings.InstallFolderPath, settings.PluginFolderPath);
            }

            var files = args.InputFiles;
            var recurseSubdirectories = args.RecurseSubdirectories;
            var exemplarPatchPath = args.ExemplarPatchPath;
            var styleIds = BuildingStyleIdParsing.ParseStyleList(args.StyleIdText);

            double progressDone = 0;
            double progressDelta = (1.0 / files.Count) * 100.0;

            using (IndentedTextWriter statusWriter = new(new TextBoxAppendWriter(outputTextBox), " "))
            {
                BuildingStyleProcessingBase buildingStyleProcessing;

                if (!string.IsNullOrWhiteSpace(exemplarPatchPath))
                {
                    buildingStyleProcessing = new ExemplarPatchBuildingStyleProcessing(exemplarPatchPath,
                                                                                       styleIds,
                                                                                       args.WallToWall,
                                                                                       statusWriter);
                }
                else
                {
                    buildingStyleProcessing = new InPlaceBuildingStyleProcessing(styleIds,
                                                                                 args.WallToWall,
                                                                                 statusWriter);
                }

                foreach (var file in files)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    worker.ReportProgress((int)progressDone);

                    if (Directory.Exists(file))
                    {
                        statusWriter.WriteLine(Resources.DirectoryFormat, Path.GetFileName(file));
                        statusWriter.Indent = 2;

                        buildingStyleProcessing.ProcessDirectory(file, recurseSubdirectories);

                        statusWriter.Indent = 0;
                    }
                    else
                    {
                        buildingStyleProcessing.ProcessFile(file);
                    }

                    progressDone += progressDelta;
                }

                buildingStyleProcessing.ProcessingFilesComplete();
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user requested to close the application
                // while the background worker was running.
                Close();
                return;
            }

            if (e.Error != null)
            {
                ShowErrorMessage(e.Error);
            }

            inputFileListView.VirtualListSize = 0;
            inputFileListViewItems.Clear();
            toolStripProgressBar.Value = 0;
            addInputFilesButton.Enabled = true;
            addInputFoldersButton.Enabled = true;
            optionsButton.Enabled = true;
            toolStripProgressBar.Value = 0;
            Cursor = Cursors.Default;
        }

        private void StartBackgroundWorker(string exemplarPatchPath = "")
        {
            addInputFilesButton.Enabled = false;
            addInputFoldersButton.Enabled = false;
            overwriteOriginalFilesButton.Enabled = false;
            saveExemplarPatchButton.Enabled = false;
            optionsButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
            outputTextBox.Text = string.Empty;

            string[] files = new string[inputFileListViewItems.Count];

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = inputFileListViewItems[i].Text;
            }

            BackgroundWorkerArgs args = new(files,
                                            includeSubdirectoriesCheckBox.Checked,
                                            buildingStyleIdTextBox.Text,
                                            wallToWallCheckBox.Checked,
                                            exemplarPatchPath);
            backgroundWorker.RunWorkerAsync(args);
        }

        private void inputFilesTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateProcessingButtonEnabledState();
        }

        private void AppendToInputFileList(string[] files)
        {
            foreach (var file in files)
            {
                inputFileListViewItems.Add(new ListViewItem(file));
            }
            inputFileListView.VirtualListSize = inputFileListViewItems.Count;
        }

        private void addInputFilesButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                AppendToInputFileList(openFileDialog.FileNames);
            }
        }

        private void addInputFoldersButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                AppendToInputFileList(folderBrowserDialog.SelectedPaths);
            }
        }

        private void overwriteOriginalFilesButton_Click(object sender, EventArgs e)
        {
            StartBackgroundWorker();
        }

        private void saveExemplarPatchButton_Click(object sender, EventArgs e)
        {
            if (exemplarPatchSaveDialog.ShowDialog(this) == DialogResult.OK)
            {
                StartBackgroundWorker(exemplarPatchSaveDialog.FileName);
            }
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            using (OptionsDialog dialog = new(settings))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Settings newSettings = dialog.Settings;

                    if (!string.Equals(settings.InstallFolderPath,
                                       newSettings.InstallFolderPath,
                                       StringComparison.OrdinalIgnoreCase)
                        || !string.Equals(settings.PluginFolderPath,
                                          newSettings.PluginFolderPath,
                                          StringComparison.OrdinalIgnoreCase))
                    {
                        settings.InstallFolderPath = newSettings.InstallFolderPath;
                        settings.PluginFolderPath = newSettings.PluginFolderPath;
                        cohortColectionInitialized = false;
                    }
                }
            }
        }

        private void buildingStyleIdTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (BuildingStyleIdParsing.IsValidStyleList(buildingStyleIdTextBox.Text))
            {
                errorProvider.SetError(buildingStyleIdTextBox, null);
                UpdateProcessingButtonEnabledState();
            }
            else
            {
                errorProvider.SetError(buildingStyleIdTextBox, Resources.StyleTextBoxError);
                e.Cancel = true;
            }
        }

        private void inputFileListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = inputFileListViewItems[e.ItemIndex];
        }
    }
}
