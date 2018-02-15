﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.Hub
{
    static class Extensions
    {
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
    }
}
