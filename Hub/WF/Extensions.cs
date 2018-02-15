using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.Hub.WF
{
    static class Extensions
    {
        [StructLayout(LayoutKind.Sequential)]
        struct HDITEM
        {
            public int mask;
            public int cxy;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            private IntPtr hbm;
            public int cchTextMax;
            public int fmt;
            public int lParam;
            public int iImage;
            public int iOrder;

            public IntPtr Hbm
            {
                get
                {
                    return hbm;
                }

                set
                {
                    hbm = value;
                }
            }
        };

        // Parameters for ListView-Headers
        const int HDI_FORMAT = 0x0004;
        const int HDF_LEFT = 0x0000;
        const int HDF_STRING = 0x4000;
        const int HDF_SORTUP = 0x0400;
        const int HDF_SORTDOWN = 0x0200;
        const int LVM_GETHEADER = 0x1000 + 31;  // LVM_FIRST + 31
        const int HDM_GETITEM = 0x1200 + 11;  // HDM_FIRST + 11
        const int HDM_SETITEM = 0x1200 + 12;  // HDM_FIRST + 12



        public static void SetColumnHeaderSortIcon(this ListView listView , int colIndex , SortOrder order)
        {
            Assert(listView != null);
            Assert(!listView.InvokeRequired);


            IntPtr hColHeader = SendMessage(listView.Handle , LVM_GETHEADER , IntPtr.Zero , IntPtr.Zero);

            HDITEM hdItem = new HDITEM();
            IntPtr colHeader = new IntPtr(colIndex);

            hdItem.mask = HDI_FORMAT;
            IntPtr rtn = SendMessageITEM(hColHeader , HDM_GETITEM , colHeader , ref hdItem);

            if (order == SortOrder.Ascending)
            {
                hdItem.fmt &= ~HDF_SORTDOWN;
                hdItem.fmt |= HDF_SORTUP;
            }
            else if (order == SortOrder.Descending)
            {
                hdItem.fmt &= ~HDF_SORTUP;
                hdItem.fmt |= HDF_SORTDOWN;
            }
            else
            {
                hdItem.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP;
            }

            rtn = SendMessageITEM(hColHeader , HDM_SETITEM , colHeader , ref hdItem);
        }

        public static void AdjustColumnsSize(this ListView listView)
        {
            Assert(listView != null);
            Assert(!listView.InvokeRequired);
            Assert(listView.View == View.Details);


            listView.BeginUpdate();

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            var szColumns = new int[listView.Columns.Count];

            for (int i = 0; i < szColumns.Length; i++)
                szColumns[i] = listView.Columns[i].Width;

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            for (int i = 0; i < szColumns.Length; i++)
                if (listView.Columns[i].Width < szColumns[i])
                    listView.Columns[i].Width = szColumns[i];

            listView.EndUpdate();
        }

        //private:
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd , uint Msg , IntPtr wParam , IntPtr lParam);

        [DllImport("user32.dll" , EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageITEM(IntPtr Handle , int msg , IntPtr wParam , ref HDITEM lParam);
    }
}
