﻿using System.Windows.Forms;

namespace DGD.HubGovernor.ListViewSorters
{
    sealed class IntegerColumnSorter: IColumnSorter
    {

        public IntegerColumnSorter(int ndxColumn)
        {
            ColumnIndex = ndxColumn;
        }

        public int ColumnIndex { get; }
        public bool SortDescending { get; set; }

        public int Compare(ListViewItem lvi1 , ListViewItem lvi2)
        {
            long i1, i2;
            long.TryParse(lvi1.SubItems[ColumnIndex].Text, out i1);
            long.TryParse(lvi2.SubItems[ColumnIndex].Text, out i2);

            if (SortDescending)
                return i2 < i1 ? -1 : i2 == i1 ? 0 : 1;

            return i2 < i1 ? 1 : i2 == i1 ? 0 : -1;
        }

        public int Compare(object x , object y)
        {
            return Compare(x as ListViewItem , y as ListViewItem);
        }
    }
}
