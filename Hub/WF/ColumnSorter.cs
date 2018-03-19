using easyLib.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DGD.Hub.WF
{
    interface IColumnSorter: IComparer<ListViewItem>, IComparer
    {
        int ColumnIndex { get; set; }
        bool SortDescending { get; set; }
        ColumnDataType_t DataType { get; }
    }



    sealed class FloatColumnSorter: IColumnSorter
    {

        public FloatColumnSorter(int ndxColumn = 0)
        {
            ColumnIndex = ndxColumn;

            DataType = ColumnDataType_t.Float;
        }


        public ColumnDataType_t DataType { get; }
        public int ColumnIndex { get; set; }
        public bool SortDescending { get; set; }


        public int Compare(object x , object y)
        {
            return Compare(x as ListViewItem , y as ListViewItem);
        }

        public int Compare(ListViewItem lvi1 , ListViewItem lvi2)
        {
            double d1, d2;

            double.TryParse(lvi1.SubItems[ColumnIndex].Text, out d1);
            double.TryParse(lvi2.SubItems[ColumnIndex].Text, out d2);

            if (SortDescending)
                return d2 < d1 ? -1 : d1 == d2 ? 0 : 1;

            return d1 < d2 ? -1 : d2 == d1 ? 0 : 1;
        }
    }



    sealed class IntegerColumnSorter: IColumnSorter
    {

        public IntegerColumnSorter(int ndxColumn = 0)
        {
            ColumnIndex = ndxColumn;
            DataType = ColumnDataType_t.Integer;
        }


        public ColumnDataType_t DataType { get; }
        public int ColumnIndex { get; set; }
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



    sealed class TextColumnSorter: IColumnSorter
    {
        public TextColumnSorter(int ndxColumn = 0)
        {
            ColumnIndex = ndxColumn;

            DataType = ColumnDataType_t.Text;
        }


        public ColumnDataType_t DataType { get; }
        public int ColumnIndex { get; set; }
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



    sealed class TimeColumnSorter: IColumnSorter
    {
        public TimeColumnSorter(int ndxColumn = 0)
        {
            ColumnIndex = ndxColumn;
            DataType = ColumnDataType_t.Time;
        }


        public ColumnDataType_t DataType { get; }
        public int ColumnIndex { get; set; }
        public bool SortDescending { get; set; }

        public int Compare(object x , object y)
        {
            return Compare(x as ListViewItem , y as ListViewItem);
        }

        public int Compare(ListViewItem lvi1 , ListViewItem lvi2)
        {
            DateTime dt1, dt2;
            DateTime.TryParse(lvi1.SubItems[ColumnIndex].Text, out dt1);
            DateTime.TryParse(lvi2.SubItems[ColumnIndex].Text, out dt2);

            return SortDescending ? DateTime.Compare(dt2 , dt1) : DateTime.Compare(dt1 , dt2);
        }
    }
}
