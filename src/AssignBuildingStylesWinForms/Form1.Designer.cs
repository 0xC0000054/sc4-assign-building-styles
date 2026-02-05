// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            includeSubdirectoriesCheckBox = new CheckBox();
            addInputFoldersButton = new Button();
            addInputFilesButton = new Button();
            openFileDialog = new OpenFileDialog();
            folderBrowserDialog = new FolderBrowserDialog();
            statusStrip = new StatusStrip();
            toolStripProgressBar = new ToolStripProgressBar();
            inputFilesGroupBox = new GroupBox();
            inputFileListView = new ListView();
            pathColumnHeader = new ColumnHeader();
            outputGroupBox = new GroupBox();
            outputTextBox = new RichTextBox();
            buildingStyleIdTextBox = new TextBox();
            buildingStylesTextBoxLabel = new Label();
            wallToWallCheckBox = new CheckBox();
            overwriteOriginalFilesButton = new Button();
            saveExemplarPatchButton = new Button();
            exemplarPatchSaveDialog = new SaveFileDialog();
            settingsButton = new Button();
            errorProvider = new ErrorProvider(components);
            buildingStyleIdDescription = new Label();
            chooseStylesButton = new Button();
            statusStrip.SuspendLayout();
            inputFilesGroupBox.SuspendLayout();
            outputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // includeSubdirectoriesCheckBox
            // 
            includeSubdirectoriesCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            includeSubdirectoriesCheckBox.Location = new Point(785, 22);
            includeSubdirectoriesCheckBox.Name = "includeSubdirectoriesCheckBox";
            includeSubdirectoriesCheckBox.Size = new Size(143, 19);
            includeSubdirectoriesCheckBox.TabIndex = 3;
            includeSubdirectoriesCheckBox.Text = "Include Subdirectories";
            includeSubdirectoriesCheckBox.UseVisualStyleBackColor = true;
            // 
            // addInputFoldersButton
            // 
            addInputFoldersButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addInputFoldersButton.Location = new Point(683, 19);
            addInputFoldersButton.Name = "addInputFoldersButton";
            addInputFoldersButton.Size = new Size(96, 23);
            addInputFoldersButton.TabIndex = 2;
            addInputFoldersButton.Text = "Add Folders...";
            addInputFoldersButton.UseVisualStyleBackColor = true;
            addInputFoldersButton.Click += addInputFoldersButton_Click;
            // 
            // addInputFilesButton
            // 
            addInputFilesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addInputFilesButton.Location = new Point(602, 19);
            addInputFilesButton.Name = "addInputFilesButton";
            addInputFilesButton.Size = new Size(75, 23);
            addInputFilesButton.TabIndex = 1;
            addInputFilesButton.Text = "Add Files...";
            addInputFilesButton.UseVisualStyleBackColor = true;
            addInputFilesButton.Click += addInputFilesButton_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.AddToRecent = false;
            openFileDialog.FileName = "openFileDialog";
            openFileDialog.Filter = "DBPF Files (*.DAT;*.SC4Desc;*.SC4Lot;*.SC4Model)|*.DAT;*.SC4Desc;*.SC4Lot;*.SC4Model|All Files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select Input DBPF Files";
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.AddToRecent = false;
            folderBrowserDialog.Description = "Select a folder containing DBPF files.";
            folderBrowserDialog.Multiselect = true;
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.UseDescriptionForTitle = true;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripProgressBar });
            statusStrip.Location = new Point(0, 555);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(953, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(100, 16);
            toolStripProgressBar.Step = 1;
            // 
            // inputFilesGroupBox
            // 
            inputFilesGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            inputFilesGroupBox.Controls.Add(inputFileListView);
            inputFilesGroupBox.Controls.Add(includeSubdirectoriesCheckBox);
            inputFilesGroupBox.Controls.Add(addInputFoldersButton);
            inputFilesGroupBox.Controls.Add(addInputFilesButton);
            inputFilesGroupBox.Location = new Point(13, 5);
            inputFilesGroupBox.Name = "inputFilesGroupBox";
            inputFilesGroupBox.Size = new Size(929, 231);
            inputFilesGroupBox.TabIndex = 1;
            inputFilesGroupBox.TabStop = false;
            inputFilesGroupBox.Text = "Input Files";
            // 
            // inputFileListView
            // 
            inputFileListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputFileListView.Columns.AddRange(new ColumnHeader[] { pathColumnHeader });
            inputFileListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            inputFileListView.HideSelection = true;
            inputFileListView.Location = new Point(6, 47);
            inputFileListView.MultiSelect = false;
            inputFileListView.Name = "inputFileListView";
            inputFileListView.Size = new Size(917, 121);
            inputFileListView.TabIndex = 4;
            inputFileListView.UseCompatibleStateImageBehavior = false;
            inputFileListView.View = View.Details;
            inputFileListView.VirtualMode = true;
            inputFileListView.RetrieveVirtualItem += inputFileListView_RetrieveVirtualItem;
            // 
            // pathColumnHeader
            // 
            pathColumnHeader.Text = "Path";
            pathColumnHeader.Width = 880;
            // 
            // outputGroupBox
            // 
            outputGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            outputGroupBox.Controls.Add(outputTextBox);
            outputGroupBox.Location = new Point(13, 179);
            outputGroupBox.Name = "outputGroupBox";
            outputGroupBox.Size = new Size(928, 303);
            outputGroupBox.TabIndex = 3;
            outputGroupBox.TabStop = false;
            outputGroupBox.Text = "Output";
            // 
            // outputTextBox
            // 
            outputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            outputTextBox.Location = new Point(6, 22);
            outputTextBox.Name = "outputTextBox";
            outputTextBox.ReadOnly = true;
            outputTextBox.Size = new Size(917, 275);
            outputTextBox.TabIndex = 1;
            outputTextBox.TabStop = false;
            outputTextBox.Text = "";
            outputTextBox.WordWrap = false;
            // 
            // buildingStyleIdTextBox
            // 
            buildingStyleIdTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buildingStyleIdTextBox.Location = new Point(119, 520);
            buildingStyleIdTextBox.Name = "buildingStyleIdTextBox";
            buildingStyleIdTextBox.Size = new Size(170, 23);
            buildingStyleIdTextBox.TabIndex = 5;
            buildingStyleIdTextBox.Validating += buildingStyleIdTextBox_Validating;
            // 
            // buildingStylesTextBoxLabel
            // 
            buildingStylesTextBoxLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buildingStylesTextBoxLabel.AutoSize = true;
            buildingStylesTextBoxLabel.Location = new Point(13, 525);
            buildingStylesTextBoxLabel.Name = "buildingStylesTextBoxLabel";
            buildingStylesTextBoxLabel.Size = new Size(100, 15);
            buildingStylesTextBoxLabel.TabIndex = 5;
            buildingStylesTextBoxLabel.Text = "Building Style Ids:";
            // 
            // wallToWallCheckBox
            // 
            wallToWallCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            wallToWallCheckBox.AutoSize = true;
            wallToWallCheckBox.Location = new Point(406, 525);
            wallToWallCheckBox.Name = "wallToWallCheckBox";
            wallToWallCheckBox.Size = new Size(91, 19);
            wallToWallCheckBox.TabIndex = 6;
            wallToWallCheckBox.Text = "Wall To Wall";
            wallToWallCheckBox.UseVisualStyleBackColor = true;
            // 
            // overwriteOriginalFilesButton
            // 
            overwriteOriginalFilesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            overwriteOriginalFilesButton.Enabled = false;
            overwriteOriginalFilesButton.Location = new Point(644, 521);
            overwriteOriginalFilesButton.Name = "overwriteOriginalFilesButton";
            overwriteOriginalFilesButton.Size = new Size(148, 23);
            overwriteOriginalFilesButton.TabIndex = 8;
            overwriteOriginalFilesButton.Text = "Overwrite Original Files";
            overwriteOriginalFilesButton.UseVisualStyleBackColor = true;
            overwriteOriginalFilesButton.Click += overwriteOriginalFilesButton_Click;
            // 
            // saveExemplarPatchButton
            // 
            saveExemplarPatchButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveExemplarPatchButton.Enabled = false;
            saveExemplarPatchButton.Location = new Point(798, 521);
            saveExemplarPatchButton.Name = "saveExemplarPatchButton";
            saveExemplarPatchButton.Size = new Size(143, 23);
            saveExemplarPatchButton.TabIndex = 9;
            saveExemplarPatchButton.Text = "Save Exemplar Patch...";
            saveExemplarPatchButton.UseVisualStyleBackColor = true;
            saveExemplarPatchButton.Click += saveExemplarPatchButton_Click;
            // 
            // exemplarPatchSaveDialog
            // 
            exemplarPatchSaveDialog.AddToRecent = false;
            exemplarPatchSaveDialog.Filter = "DAT Files (*.DAT) | *.DAT";
            // 
            // settingsButton
            // 
            settingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            settingsButton.Location = new Point(563, 521);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(75, 23);
            settingsButton.TabIndex = 7;
            settingsButton.Text = "Settings...";
            settingsButton.UseVisualStyleBackColor = true;
            settingsButton.Click += settingsButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // buildingStyleIdDescription
            // 
            buildingStyleIdDescription.AutoSize = true;
            buildingStyleIdDescription.Location = new Point(13, 485);
            buildingStyleIdDescription.Name = "buildingStyleIdDescription";
            buildingStyleIdDescription.Size = new Size(146, 15);
            buildingStyleIdDescription.TabIndex = 10;
            buildingStyleIdDescription.Text = "buildingStyleIdDescription";
            // 
            // chooseStylesButton
            // 
            chooseStylesButton.Location = new Point(295, 521);
            chooseStylesButton.Name = "chooseStylesButton";
            chooseStylesButton.Size = new Size(105, 23);
            chooseStylesButton.TabIndex = 11;
            chooseStylesButton.Text = "Choose Styles...";
            chooseStylesButton.UseVisualStyleBackColor = true;
            chooseStylesButton.Click += chooseStylesButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(953, 577);
            Controls.Add(chooseStylesButton);
            Controls.Add(buildingStyleIdDescription);
            Controls.Add(settingsButton);
            Controls.Add(saveExemplarPatchButton);
            Controls.Add(overwriteOriginalFilesButton);
            Controls.Add(wallToWallCheckBox);
            Controls.Add(buildingStylesTextBoxLabel);
            Controls.Add(buildingStyleIdTextBox);
            Controls.Add(outputGroupBox);
            Controls.Add(statusStrip);
            Controls.Add(inputFilesGroupBox);
            Name = "Form1";
            Text = "Assign Building Styles";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            inputFilesGroupBox.ResumeLayout(false);
            outputGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private CheckBox includeSubdirectoriesCheckBox;
        private Button addInputFoldersButton;
        private Button addInputFilesButton;
        private OpenFileDialog openFileDialog;
        private FolderBrowserDialog folderBrowserDialog;
        private StatusStrip statusStrip;
        private ToolStripProgressBar toolStripProgressBar;
        private GroupBox inputFilesGroupBox;
        private GroupBox outputGroupBox;
        private RichTextBox outputTextBox;
        private TextBox buildingStyleIdTextBox;
        private Label buildingStylesTextBoxLabel;
        private CheckBox wallToWallCheckBox;
        private Button overwriteOriginalFilesButton;
        private Button saveExemplarPatchButton;
        private SaveFileDialog exemplarPatchSaveDialog;
        private Button settingsButton;
        private ErrorProvider errorProvider;
        private ListView inputFileListView;
        private ColumnHeader pathColumnHeader;
        private Label buildingStyleIdDescription;
        private Button chooseStylesButton;
    }
}
