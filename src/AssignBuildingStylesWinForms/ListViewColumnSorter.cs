// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    internal sealed class ListViewColumnSorter : Comparer<ListViewItem>
    {
        public ListViewColumnSorter() : this(0, SortOrder.None)
        {
        }

        public ListViewColumnSorter(int column, SortOrder sortOrder)
        {
            Column = column;
            SortOrder = sortOrder;
        }

        public int Column { get; set; }

        public SortOrder SortOrder { get; set; }

        public override int Compare(ListViewItem? x, ListViewItem? y)
        {
            if (x is null)
            {
                return y is null ? 0 : -1;
            }
            else if (y is null)
            {
                return 1;
            }

            int column = Column;

            int result = string.Compare(x.SubItems[column].Text, y.SubItems[column].Text);

            if (SortOrder == SortOrder.Descending)
            {
                result = -result;
            }

            return result;
        }
    }
}
