using System;
using System.Windows.Forms;


namespace DGD.HubGovernor.ListViewSorters
{
    sealed class TimeColumnSorter: IColumnSorter
    {
        public TimeColumnSorter(int ndxColumn)
        {
            ColumnIndex = ndxColumn;
        }

        public int ColumnIndex { get; }
        public bool SortDescending { get; set; }

        public int Compare(object x , object y)
        {
            return Compare(x as ListViewItem , y as ListViewItem);
        }

        public int Compare(ListViewItem lvi1 , ListViewItem lvi2)
        {
            DateTime dt1, dt2;
            DateTime.TryParse(lvi1.SubItems[ColumnIndex].Text , out dt1);
            DateTime.TryParse(lvi2.SubItems[ColumnIndex].Text , out dt2);

            return SortDescending ? DateTime.Compare(dt2 , dt1) : DateTime.Compare(dt1 , dt2);
        }
    }
}
