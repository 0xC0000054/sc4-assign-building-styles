// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    partial class ChooseStyleDialog
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
            components = new System.ComponentModel.Container();
            listView = new ListView();
            styleIdColumnHeader = new ColumnHeader();
            styleNameColumnHeader = new ColumnHeader();
            authorColumnHeader = new ColumnHeader();
            styleDescriptionColumnHeader = new ColumnHeader();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            addManualStylePanel = new Panel();
            styleAddDescription2 = new Label();
            styleAddDescription1 = new Label();
            submitManualStyleButton = new Button();
            styleAuthorTextBox = new TextBox();
            styleAuthorLabel = new Label();
            styleNameTextBox = new TextBox();
            styleNameLabel = new Label();
            styleDescriptionTextBox = new TextBox();
            styleDescriptionLabel = new Label();
            styleIdTextBox = new TextBox();
            styleAddManualIdHeader = new Label();
            addManualStyleButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            errorProvider = new ErrorProvider(components);
            removeStyleButton = new Button();
            addManualStylePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listView.CheckBoxes = true;
            listView.Columns.AddRange(new ColumnHeader[] { styleIdColumnHeader, styleNameColumnHeader, authorColumnHeader, styleDescriptionColumnHeader });
            listView.FullRowSelect = true;
            listView.Location = new Point(12, 12);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.OwnerDraw = true;
            listView.ShowItemToolTips = true;
            listView.Size = new Size(779, 217);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.VirtualMode = true;
            listView.ColumnClick += listView_ColumnClick;
            listView.DrawColumnHeader += listView_DrawColumnHeader;
            listView.DrawItem += listView_DrawItem;
            listView.DrawSubItem += listView_DrawSubItem;
            listView.RetrieveVirtualItem += listView_RetrieveVirtualItem;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            listView.MouseClick += listView_MouseClick;
            // 
            // styleIdColumnHeader
            // 
            styleIdColumnHeader.Text = "Style ID";
            styleIdColumnHeader.Width = 80;
            // 
            // styleNameColumnHeader
            // 
            styleNameColumnHeader.Text = "Name";
            styleNameColumnHeader.Width = 220;
            // 
            // authorColumnHeader
            // 
            authorColumnHeader.Text = "Author";
            authorColumnHeader.Width = 120;
            // 
            // styleDescriptionColumnHeader
            // 
            styleDescriptionColumnHeader.Text = "Description";
            styleDescriptionColumnHeader.Width = 330;
            // 
            // backgroundWorker
            // 
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // addManualStylePanel
            // 
            addManualStylePanel.Controls.Add(styleAddDescription2);
            addManualStylePanel.Controls.Add(styleAddDescription1);
            addManualStylePanel.Controls.Add(submitManualStyleButton);
            addManualStylePanel.Controls.Add(styleAuthorTextBox);
            addManualStylePanel.Controls.Add(styleAuthorLabel);
            addManualStylePanel.Controls.Add(styleNameTextBox);
            addManualStylePanel.Controls.Add(styleNameLabel);
            addManualStylePanel.Controls.Add(styleDescriptionTextBox);
            addManualStylePanel.Controls.Add(styleDescriptionLabel);
            addManualStylePanel.Controls.Add(styleIdTextBox);
            addManualStylePanel.Controls.Add(styleAddManualIdHeader);
            addManualStylePanel.Location = new Point(12, 264);
            addManualStylePanel.Name = "addManualStylePanel";
            addManualStylePanel.Size = new Size(779, 197);
            addManualStylePanel.TabIndex = 2;
            addManualStylePanel.Visible = false;
            // 
            // styleAddDescription2
            // 
            styleAddDescription2.AutoSize = true;
            styleAddDescription2.Location = new Point(6, 17);
            styleAddDescription2.Name = "styleAddDescription2";
            styleAddDescription2.Size = new Size(312, 15);
            styleAddDescription2.TabIndex = 10;
            styleAddDescription2.Text = "The style name, author, and description cannot be empty.";
            // 
            // styleAddDescription1
            // 
            styleAddDescription1.AutoSize = true;
            styleAddDescription1.Location = new Point(6, 2);
            styleAddDescription1.Name = "styleAddDescription1";
            styleAddDescription1.Size = new Size(349, 15);
            styleAddDescription1.TabIndex = 9;
            styleAddDescription1.Text = "The building style id must be a hexadecimal number, e.g. 0x2000.";
            // 
            // submitManualStyleButton
            // 
            submitManualStyleButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            submitManualStyleButton.Location = new Point(623, 163);
            submitManualStyleButton.Name = "submitManualStyleButton";
            submitManualStyleButton.Size = new Size(75, 23);
            submitManualStyleButton.TabIndex = 7;
            submitManualStyleButton.Text = "Submit";
            submitManualStyleButton.UseVisualStyleBackColor = true;
            submitManualStyleButton.Click += submitManualStyleButton_Click;
            // 
            // styleAuthorTextBox
            // 
            styleAuthorTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleAuthorTextBox.Location = new Point(82, 102);
            styleAuthorTextBox.Name = "styleAuthorTextBox";
            styleAuthorTextBox.Size = new Size(248, 23);
            styleAuthorTextBox.TabIndex = 5;
            // 
            // styleAuthorLabel
            // 
            styleAuthorLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleAuthorLabel.AutoSize = true;
            styleAuthorLabel.Location = new Point(6, 107);
            styleAuthorLabel.Name = "styleAuthorLabel";
            styleAuthorLabel.Size = new Size(47, 15);
            styleAuthorLabel.TabIndex = 6;
            styleAuthorLabel.Text = "Author:";
            // 
            // styleNameTextBox
            // 
            styleNameTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleNameTextBox.Location = new Point(82, 71);
            styleNameTextBox.Name = "styleNameTextBox";
            styleNameTextBox.Size = new Size(248, 23);
            styleNameTextBox.TabIndex = 4;
            // 
            // styleNameLabel
            // 
            styleNameLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleNameLabel.AutoSize = true;
            styleNameLabel.Location = new Point(6, 76);
            styleNameLabel.Name = "styleNameLabel";
            styleNameLabel.Size = new Size(42, 15);
            styleNameLabel.TabIndex = 4;
            styleNameLabel.Text = "Name:";
            // 
            // styleDescriptionTextBox
            // 
            styleDescriptionTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            styleDescriptionTextBox.Location = new Point(82, 134);
            styleDescriptionTextBox.Name = "styleDescriptionTextBox";
            styleDescriptionTextBox.Size = new Size(616, 23);
            styleDescriptionTextBox.TabIndex = 6;
            // 
            // styleDescriptionLabel
            // 
            styleDescriptionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleDescriptionLabel.AutoSize = true;
            styleDescriptionLabel.Location = new Point(6, 137);
            styleDescriptionLabel.Name = "styleDescriptionLabel";
            styleDescriptionLabel.Size = new Size(70, 15);
            styleDescriptionLabel.TabIndex = 2;
            styleDescriptionLabel.Text = "Description:";
            // 
            // styleIdTextBox
            // 
            styleIdTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleIdTextBox.Location = new Point(82, 42);
            styleIdTextBox.Name = "styleIdTextBox";
            styleIdTextBox.Size = new Size(100, 23);
            styleIdTextBox.TabIndex = 3;
            styleIdTextBox.Validating += styleIdTextBox_Validating;
            // 
            // styleAddManualIdHeader
            // 
            styleAddManualIdHeader.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            styleAddManualIdHeader.AutoSize = true;
            styleAddManualIdHeader.Location = new Point(6, 47);
            styleAddManualIdHeader.Name = "styleAddManualIdHeader";
            styleAddManualIdHeader.Size = new Size(48, 15);
            styleAddManualIdHeader.TabIndex = 0;
            styleAddManualIdHeader.Text = "Style Id:";
            // 
            // addManualStyleButton
            // 
            addManualStyleButton.Location = new Point(551, 235);
            addManualStyleButton.Name = "addManualStyleButton";
            addManualStyleButton.Size = new Size(159, 23);
            addManualStyleButton.TabIndex = 1;
            addManualStyleButton.Text = "Show Add Style Controls";
            addManualStyleButton.UseVisualStyleBackColor = true;
            addManualStyleButton.Click += addManualStyleButton_Click;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(635, 468);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 8;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(716, 468);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // removeStyleButton
            // 
            removeStyleButton.Enabled = false;
            removeStyleButton.Location = new Point(716, 235);
            removeStyleButton.Name = "removeStyleButton";
            removeStyleButton.Size = new Size(75, 23);
            removeStyleButton.TabIndex = 2;
            removeStyleButton.Text = "Remove";
            removeStyleButton.UseVisualStyleBackColor = true;
            removeStyleButton.Click += removeStyleButton_Click;
            // 
            // ChooseStyleDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(803, 502);
            Controls.Add(removeStyleButton);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(addManualStyleButton);
            Controls.Add(addManualStylePanel);
            Controls.Add(listView);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChooseStyleDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Choose Styles";
            addManualStylePanel.ResumeLayout(false);
            addManualStylePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listView;
        private ColumnHeader styleIdColumnHeader;
        private ColumnHeader styleNameColumnHeader;
        private ColumnHeader styleDescriptionColumnHeader;
        private ColumnHeader authorColumnHeader;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Panel addManualStylePanel;
        private Label styleAddManualIdHeader;
        private TextBox styleIdTextBox;
        private TextBox styleAuthorTextBox;
        private Label styleAuthorLabel;
        private TextBox styleNameTextBox;
        private Label styleNameLabel;
        private TextBox styleDescriptionTextBox;
        private Label styleDescriptionLabel;
        private Button addManualStyleButton;
        private Button okButton;
        private Button cancelButton;
        private Button submitManualStyleButton;
        private ErrorProvider errorProvider;
        private Button removeStyleButton;
        private Label styleAddDescription1;
        private Label styleAddDescription2;
    }
}