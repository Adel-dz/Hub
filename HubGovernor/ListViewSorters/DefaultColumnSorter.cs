using System.Windows.Forms;

namespace DGD.HubGovernor.ListViewSorters
{
    sealed class DefaultColumnSorter : IColumnSorter
    {
        int m_ndxCol;

        public int ColumnIndex
        {
            get { return m_ndxCol; }

            set
            {
                if (m_ndxCol != value)
                {
                    m_ndxCol = value;
                    SortDescending = false;
                }
            }
        }

        public bool SortDescending { get; set; }

        public int Compare(ListViewItem lvi1 , ListViewItem lvi2)
        {
            string str1 = lvi1.SubItems[m_ndxCol].Text;
            string str2 = lvi2.SubItems[m_ndxCol].Text;
            return SortDescending ? string.Compare(str2 , str1) : string.Compare(str1 , str2);
        }

        public int Compare(object x , object y)
        {
            return Compare(x as ListViewItem , y as ListViewItem);
        }
    }
}
