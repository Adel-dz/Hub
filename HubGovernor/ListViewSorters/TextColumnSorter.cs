using System.Windows.Forms;

namespace DGD.HubGovernor.ListViewSorters
{
    sealed class TextColumnSorter: IColumnSorter
    {
        public TextColumnSorter(int ndxColumn)
        {
            ColumnIndex = ndxColumn;
        }


        public int ColumnIndex { get; }
        public bool SortDescending { get; set; }

        public int Compare(ListViewItem lvi1 , ListViewItem lvi2)
        {
            string str1 = lvi1.SubItems[ColumnIndex].Text;
            string str2 = lvi2.SubItems[ColumnIndex].Text;
            return SortDescending ? string.Compare(str2 , str1) : string.Compare(str1 , str2);
        }

        public int Compare(object x , object y)
        {
            return Compare(x as ListViewItem , y as ListViewItem);
        }
    }

}
