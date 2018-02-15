using System.Windows.Forms;

namespace DGD.HubGovernor.ListViewSorters
{
    sealed class FloatColumnSorter: IColumnSorter
    {

        public FloatColumnSorter(int ndxColumn)
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
            double d1 = double.Parse(lvi1.SubItems[ColumnIndex].Text);
            double d2 = double.Parse(lvi2.SubItems[ColumnIndex].Text);

            if (SortDescending)
                return d2 < d1 ? -1 : d1 == d2 ? 0 : 1;

            return d1 < d2 ? -1 : d2 == d1 ? 0 : 1;
        }
    }
}
