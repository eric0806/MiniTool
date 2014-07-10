using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace extensions
{
    /// <summary>
    /// ListView的擴充功能，Header按下去可顯示排序三角圖形
    /// </summary>
    public static class ListViewExtensions
    {
        const Int32 HDF_SORTDOWN = 0x200;
        const Int32 HDF_SORTUP = 0x400;
        const Int32 HDI_FORMAT = 0x4;
        const Int32 HDM_GETITEM = 0x120b;
        const Int32 HDM_SETITEM = 0x120c;
        const Int32 LVM_GETHEADER = 0x101f;

        private struct LVCOLUMN
        {
            public Int32 mask;
            public Int32 cx;
            public String pszText;
            public IntPtr hbm;
            public Int32 cchTextMax;
            public Int32 fmt;
            public Int32 iSubItem;
            public Int32 iImage;
            public Int32 iOrder;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, UInt32 msg, IntPtr wParam, ref LVCOLUMN lParam);

        public static void SetSortIcon(this ListView lstVw, int column, SortOrder sorting) {
            IntPtr clmHdr = SendMessage(lstVw.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            ///*
            for (int i = 0; i < lstVw.Columns.Count; i++) {
                IntPtr clmPtr = new IntPtr(i);
                LVCOLUMN lvColumn = new LVCOLUMN();

                lvColumn.mask = HDI_FORMAT;
                SendMessageLVCOLUMN(clmHdr, HDM_GETITEM, clmPtr, ref lvColumn);
                if (sorting != SortOrder.None && i == column) {
                    if (sorting == SortOrder.Ascending) {
                        lvColumn.fmt &= ~HDF_SORTDOWN;
                        lvColumn.fmt |= HDF_SORTUP;
                    }
                    else {
                        lvColumn.fmt &= ~HDF_SORTUP;
                        lvColumn.fmt |= HDF_SORTDOWN;
                    }
                }
                else {
                    lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP;
                }
                SendMessageLVCOLUMN(clmHdr, HDM_SETITEM, clmPtr, ref lvColumn);
            }
            // * */
        }
    }
}
