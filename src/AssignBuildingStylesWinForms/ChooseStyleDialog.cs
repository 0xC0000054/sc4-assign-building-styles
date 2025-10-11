
using AssignBuildingStylesWinForms.Properties;
using LinkDotNet.StringBuilder;

namespace AssignBuildingStylesWinForms
{
    public partial class ChooseStyleDialog : Form
    {
        private readonly ListViewColumnSorter listViewColumnSorter;
        private readonly List<ListViewItem> listViewItems;
        private readonly IBuildingStyleManager buildingStyleManager;
        private bool stylesModified;

        internal ChooseStyleDialog(IBuildingStyleManager buildingStyleManager)
        {
            InitializeComponent();
            listViewColumnSorter = new ListViewColumnSorter();
            listViewItems = [];
            this.buildingStyleManager = buildingStyleManager;
            stylesModified = false;
            CheckedStyleIds = string.Empty;
            ErrorProviderHelper.SetIconFromOS(errorProvider);
            if (!DesignMode && !addManualStylePanel.Visible)
            {
                Height -= addManualStylePanel.Height;
            }
        }

        internal string CheckedStyleIds { get; private set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LoadBuildingStyles();
        }

        private static ListViewItem CreateStyleListViewItem(uint styleID, string name, string author, string description)
        {
            return new ListViewItem(
            [
                BuildingStyleIdParsing.StyleNumberToString(styleID),
                name,
                author,
                description
            ])
            { 
                Tag = new Box<uint>(styleID)
            };
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == listViewColumnSorter.Column)
            {
                if (listViewColumnSorter.SortOrder == SortOrder.Ascending)
                {
                    listViewColumnSorter.SortOrder = SortOrder.Descending;
                }
                else
                {
                    listViewColumnSorter.SortOrder = SortOrder.Ascending;
                }
            }
            else
            {
                listViewColumnSorter.Column = e.Column;
                listViewColumnSorter.SortOrder = SortOrder.Ascending;
            }

            listViewItems.Sort(listViewColumnSorter);

            if (listView.SelectedIndices.Count > 0)
            {
                listView.SelectedIndices.Clear();
                listView.SelectedIndices.Add(0);
            }

            listView.Refresh();
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;

            // HACK: Toggle the item's checked state for unchecked items.
            // This is required due to a bug related to check box rendering in virtual mode.
            // See https://www.codeproject.com/articles/ListView-in-VirtualMode-and-checkboxes

            if (!e.Item.Checked)
            {
                e.Item.Checked = true;
                e.Item.Checked = false;
            }
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            // HACK: Toggle the item's checked state if the click was within the checkbox area.
            // This is required due to a bug related to check box rendering in virtual mode.
            // See https://www.codeproject.com/articles/ListView-in-VirtualMode-and-checkboxes

            ListView lv = (ListView)sender;
            ListViewItem? lvi = lv.GetItemAt(e.X, e.Y);

            if (lvi is not null)
            {
                lvi.Checked = !lvi.Checked;
                lv.Invalidate(lvi.Bounds);
            }
        }

        private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = listViewItems[e.ItemIndex];
        }

        private void LoadBuildingStyles()
        {
            Cursor = Cursors.WaitCursor;
            listView.VirtualListSize = 0;
            listViewItems.Clear();
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = buildingStyleManager.GetBuildingStyles();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;

            if (e.Error != null)
            {
                TaskDialogUtil.ShowErrorMessageBox(this, Text, e.Error);
                return;
            }

            var buildingStyles = (Dictionary<uint, BuildingStyleInfo>?)e.Result!;

            if (buildingStyles != null && buildingStyles.Count > 0)
            {
                listViewItems.Capacity = buildingStyles.Count;

                foreach (var pair in buildingStyles)
                {
                    uint id = pair.Key;
                    BuildingStyleInfo buildingStyleInfo = pair.Value;

                    listViewItems.Add(CreateStyleListViewItem(id,
                                                              buildingStyleInfo.Name,
                                                              buildingStyleInfo.Author,
                                                              buildingStyleInfo.Description));
                }

                listViewItems.Sort(new ListViewColumnSorter(0, SortOrder.Ascending));
                listView.VirtualListSize = listViewItems.Count;
            }
        }

        private void SaveModifiedStyles()
        {
            if (listViewItems.Count > 0)
            {
                try
                {
                    Dictionary<uint, BuildingStyleInfo> styles = new(listViewItems.Count);

                    foreach (var item in listViewItems)
                    {
                        var subItems = item.SubItems;

                        styles.TryAdd(
                            BuildingStyleIdParsing.ParseStyleNumber(subItems[0].Text),
                            new BuildingStyleInfo(subItems[1].Text, subItems[2].Text, subItems[3].Text));
                    }

                    buildingStyleManager.SetBuildingStyles(styles);
                }
                catch (Exception ex)
                {
                    TaskDialogUtil.ShowErrorMessageBox(this, Text, ex);
                }
            }
        }

        private void UpdateCheckedStyleString()
        {
            CheckedStyleIds = string.Empty;

            using (ValueStringBuilder stringBuilder = new(stackalloc char[512]))
            {
                bool addedFirstItem = false;

                foreach (var item in listViewItems)
                {
                    if (item.Checked)
                    {
                        if (addedFirstItem)
                        {
                            // The output string is a comma-separated list of styles ids.
                            stringBuilder.Append(',');
                        }
                        else
                        {
                            addedFirstItem = true;
                        }

                        stringBuilder.Append(item.SubItems[0].Text);
                    }
                }

                CheckedStyleIds = stringBuilder.ToString();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (stylesModified)
            {
                SaveModifiedStyles();
            }

            UpdateCheckedStyleString();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void addManualStyleButton_Click(object sender, EventArgs e)
        {
            if (addManualStylePanel.Visible)
            {
                addManualStylePanel.Visible = false;
                Height -= addManualStylePanel.Height;
                addManualStyleButton.Text = Resources.ChooseStyleShowAddStyleText;
            }
            else
            {
                Height += addManualStylePanel.Height;
                addManualStylePanel.Visible = true;
                addManualStyleButton.Text = Resources.ChooseStyleHideAddStyleText;
            }
        }

        private void styleIdTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;

            if (!string.IsNullOrEmpty(text))
            {
                if (BuildingStyleIdParsing.IsValidSingleStyle(text))
                {
                    errorProvider.SetError(textBox, null);
                }
                else
                {
                    errorProvider.SetError(textBox, Resources.ChooseStyleInvalidStyleId);
                }
            }
        }

        private int FindExistingStyleIndex(uint styleID)
        {
            for (int i = 0; i < listViewItems.Count; i++)
            {
                Box<uint> tag = (Box<uint>)listViewItems[i].Tag!;

                if (tag.Value == styleID)
                {
                    return i;
                }
            }

            return -1;
        }

        private bool ManualStyleTextBoxesAreValid()
        {
            bool valid = true;

            if (BuildingStyleIdParsing.IsValidSingleStyle(styleIdTextBox.Text))
            {
                errorProvider.SetError(styleIdTextBox, null);
            }
            else
            {
                errorProvider.SetError(styleIdTextBox, Resources.ChooseStyleInvalidStyleId);
                valid = false;
            }

            if (!string.IsNullOrWhiteSpace(styleNameTextBox.Text))
            {
                errorProvider.SetError(styleNameTextBox, null);
            }
            else
            {
                errorProvider.SetError(styleNameTextBox, Resources.ChooseStyleAddEmptyTextBox);
                valid = false;
            }

            if (!string.IsNullOrWhiteSpace(styleAuthorTextBox.Text))
            {
                errorProvider.SetError(styleAuthorTextBox, null);
            }
            else
            {
                errorProvider.SetError(styleAuthorTextBox, Resources.ChooseStyleAddEmptyTextBox);
                valid = false;
            }

            if (!string.IsNullOrWhiteSpace(styleDescriptionTextBox.Text))
            {
                errorProvider.SetError(styleDescriptionTextBox, null);
            }
            else
            {
                errorProvider.SetError(styleDescriptionTextBox, Resources.ChooseStyleAddEmptyTextBox);
                valid = false;
            }

            return valid;
        }

        private void submitManualStyleButton_Click(object sender, EventArgs e)
        {
            if (ManualStyleTextBoxesAreValid())
            {
                uint styleID = BuildingStyleIdParsing.ParseStyleNumber(styleIdTextBox.Text);
                string name = styleNameTextBox.Text;
                string author = styleAuthorTextBox.Text;
                string description = styleDescriptionTextBox.Text;

                int existingStyleIndex = FindExistingStyleIndex(styleID);

                if (existingStyleIndex != -1)
                {
                    listViewItems[existingStyleIndex] = CreateStyleListViewItem(styleID,
                                                                                name,
                                                                                author,
                                                                                description);
                    listView.RedrawItems(existingStyleIndex, existingStyleIndex, false);
                }
                else
                {
                    listViewItems.Add(CreateStyleListViewItem(styleID,
                                                              name,
                                                              author,
                                                              description));
                    listViewItems.Sort(new ListViewColumnSorter(0, SortOrder.Ascending));
                    listView.VirtualListSize++;
                    listView.Invalidate();
                }
                stylesModified = true;
            }
        }

        private void removeStyleButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int index = listView.SelectedIndices[0];

                listViewItems.RemoveAt(index);
                listView.VirtualListSize--;
                listView.Invalidate();
                listView.SelectedIndices.Clear();
                stylesModified = true;
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeStyleButton.Enabled = listView.SelectedIndices.Count > 0;
        }
    }
}
