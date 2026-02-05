// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            okButton = new Button();
            cancelButton = new Button();
            intallFolderGroupBox = new GroupBox();
            installFolderPathBrowseButton = new Button();
            installFolderPathTextBox = new TextBox();
            installFolderPathDescriptionLabel2 = new Label();
            installFolderPathDescription1 = new Label();
            pluginFolderPathGroupBox = new GroupBox();
            pluginFolderPathBrowseButton = new Button();
            pluginFolderPathTextBox = new TextBox();
            pluginFolderPathDescription2 = new Label();
            pluginFolderPathDescription1 = new Label();
            folderBrowserDialog = new FolderBrowserDialog();
            intallFolderGroupBox.SuspendLayout();
            pluginFolderPathGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(362, 224);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(443, 224);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // intallFolderGroupBox
            // 
            intallFolderGroupBox.Controls.Add(installFolderPathBrowseButton);
            intallFolderGroupBox.Controls.Add(installFolderPathTextBox);
            intallFolderGroupBox.Controls.Add(installFolderPathDescriptionLabel2);
            intallFolderGroupBox.Controls.Add(installFolderPathDescription1);
            intallFolderGroupBox.Location = new Point(12, 12);
            intallFolderGroupBox.Name = "intallFolderGroupBox";
            intallFolderGroupBox.Size = new Size(506, 100);
            intallFolderGroupBox.TabIndex = 2;
            intallFolderGroupBox.TabStop = false;
            intallFolderGroupBox.Text = "Installtion folder path";
            // 
            // installFolderPathBrowseButton
            // 
            installFolderPathBrowseButton.Location = new Point(425, 60);
            installFolderPathBrowseButton.Name = "installFolderPathBrowseButton";
            installFolderPathBrowseButton.Size = new Size(75, 23);
            installFolderPathBrowseButton.TabIndex = 3;
            installFolderPathBrowseButton.Text = "Browse...";
            installFolderPathBrowseButton.UseVisualStyleBackColor = true;
            installFolderPathBrowseButton.Click += installFolderPathBrowseButton_Click;
            // 
            // installFolderPathTextBox
            // 
            installFolderPathTextBox.Location = new Point(5, 61);
            installFolderPathTextBox.Name = "installFolderPathTextBox";
            installFolderPathTextBox.Size = new Size(414, 23);
            installFolderPathTextBox.TabIndex = 2;
            // 
            // installFolderPathDescriptionLabel2
            // 
            installFolderPathDescriptionLabel2.AutoSize = true;
            installFolderPathDescriptionLabel2.Location = new Point(6, 34);
            installFolderPathDescriptionLabel2.Name = "installFolderPathDescriptionLabel2";
            installFolderPathDescriptionLabel2.Size = new Size(202, 15);
            installFolderPathDescriptionLabel2.TabIndex = 1;
            installFolderPathDescriptionLabel2.Text = "This is used to resolve parent cohorts";
            // 
            // installFolderPathDescription1
            // 
            installFolderPathDescription1.AutoSize = true;
            installFolderPathDescription1.Location = new Point(6, 19);
            installFolderPathDescription1.Name = "installFolderPathDescription1";
            installFolderPathDescription1.Size = new Size(252, 15);
            installFolderPathDescription1.TabIndex = 0;
            installFolderPathDescription1.Text = "The path the your SimCity 4 installation folder.";
            // 
            // pluginFolderPathGroupBox
            // 
            pluginFolderPathGroupBox.Controls.Add(pluginFolderPathBrowseButton);
            pluginFolderPathGroupBox.Controls.Add(pluginFolderPathTextBox);
            pluginFolderPathGroupBox.Controls.Add(pluginFolderPathDescription2);
            pluginFolderPathGroupBox.Controls.Add(pluginFolderPathDescription1);
            pluginFolderPathGroupBox.Location = new Point(12, 118);
            pluginFolderPathGroupBox.Name = "pluginFolderPathGroupBox";
            pluginFolderPathGroupBox.Size = new Size(506, 100);
            pluginFolderPathGroupBox.TabIndex = 3;
            pluginFolderPathGroupBox.TabStop = false;
            pluginFolderPathGroupBox.Text = "Plugin folder path";
            // 
            // pluginFolderPathBrowseButton
            // 
            pluginFolderPathBrowseButton.Location = new Point(425, 60);
            pluginFolderPathBrowseButton.Name = "pluginFolderPathBrowseButton";
            pluginFolderPathBrowseButton.Size = new Size(75, 23);
            pluginFolderPathBrowseButton.TabIndex = 3;
            pluginFolderPathBrowseButton.Text = "Browse...";
            pluginFolderPathBrowseButton.UseVisualStyleBackColor = true;
            pluginFolderPathBrowseButton.Click += pluginFolderPathBrowseButton_Click;
            // 
            // pluginFolderPathTextBox
            // 
            pluginFolderPathTextBox.Location = new Point(5, 61);
            pluginFolderPathTextBox.Name = "pluginFolderPathTextBox";
            pluginFolderPathTextBox.Size = new Size(414, 23);
            pluginFolderPathTextBox.TabIndex = 2;
            // 
            // pluginFolderPathDescription2
            // 
            pluginFolderPathDescription2.AutoSize = true;
            pluginFolderPathDescription2.Location = new Point(6, 34);
            pluginFolderPathDescription2.Name = "pluginFolderPathDescription2";
            pluginFolderPathDescription2.Size = new Size(202, 15);
            pluginFolderPathDescription2.TabIndex = 1;
            pluginFolderPathDescription2.Text = "This is used to resolve parent cohorts";
            // 
            // pluginFolderPathDescription1
            // 
            pluginFolderPathDescription1.AutoSize = true;
            pluginFolderPathDescription1.Location = new Point(6, 19);
            pluginFolderPathDescription1.Name = "pluginFolderPathDescription1";
            pluginFolderPathDescription1.Size = new Size(258, 15);
            pluginFolderPathDescription1.TabIndex = 0;
            pluginFolderPathDescription1.Text = "The path the your SimCity 4 user plugins folder.";
            // 
            // SettingsDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(530, 259);
            Controls.Add(pluginFolderPathGroupBox);
            Controls.Add(intallFolderGroupBox);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsDialog";
            ShowInTaskbar = false;
            Text = "Settings";
            intallFolderGroupBox.ResumeLayout(false);
            intallFolderGroupBox.PerformLayout();
            pluginFolderPathGroupBox.ResumeLayout(false);
            pluginFolderPathGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private GroupBox intallFolderGroupBox;
        private Label installFolderPathDescription1;
        private Label installFolderPathDescriptionLabel2;
        private Button installFolderPathBrowseButton;
        private TextBox installFolderPathTextBox;
        private GroupBox pluginFolderPathGroupBox;
        private Button pluginFolderPathBrowseButton;
        private TextBox pluginFolderPathTextBox;
        private Label pluginFolderPathDescription2;
        private Label pluginFolderPathDescription1;
        private FolderBrowserDialog folderBrowserDialog;
    }
}