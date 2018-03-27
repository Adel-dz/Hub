using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.ListViewSorters
{
    sealed class VersionColumnSorter: IColumnSorter
    {
        public VersionColumnSorter(int ndxColumn)
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
            Version v1, v2;

            Version.TryParse(lvi1.SubItems[ColumnIndex].Text , out v1);
            Version.TryParse(lvi2.SubItems[ColumnIndex].Text , out v2);

            return SortDescending ? Compare(v2 , v1) : Compare(v1 , v2);
        }

        //private:
        static int Compare(Version v1 , Version v2)
        {
            if (v1 != null)
                return v1.CompareTo(v2);

            if (v2 == null)
                return 0;

            return 1;
        }
    }
}
