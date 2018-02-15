using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DGD.HubGovernor.ListViewSorters
{
    interface IColumnSorter: IComparer<ListViewItem>, IComparer
    {
        int ColumnIndex { get; }
        bool SortDescending { get; set; }
    }

}
