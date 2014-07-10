using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace extensions
{
    /// <summary>
    /// An instance of the SortWrapper class is created for each item and added to the ArrayList for sorting.
    /// </summary>
    public class SortWrapper
    {
        internal ListViewItem sortItem;
        internal int sortColumn;


        /// <summary>
        /// A SortWrapper requires the item and the index of the clicked column.
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="iColumn"></param>
        public SortWrapper(ListViewItem Item, int iColumn) {
            sortItem = Item;
            sortColumn = iColumn;
        }

        /// <summary>
        /// Text property for getting the text of an item.
        /// </summary>
        public string Text {
            get {
                return sortItem.SubItems[sortColumn].Text;
            }
        }

        /// <summary>
        /// Implementation of the IComparer interface for sorting ArrayList items.
        /// </summary>
        public class SortComparer : IComparer
        {
            bool ascending;

            // Constructor requires the sort order;
            // true if ascending, otherwise descending.
            /// <summary>
            /// Constructor requires the sort order; true if ascending, otherwise descending.
            /// </summary>
            /// <param name="asc"></param>
            public SortComparer(bool asc) {
                this.ascending = asc;
            }

            /// <summary>
            /// Implemnentation of the IComparer:Compare. method for comparing two objects.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(object x, object y) {
                SortWrapper xItem = (SortWrapper)x;
                SortWrapper yItem = (SortWrapper)y;

                string xText = xItem.sortItem.SubItems[xItem.sortColumn].Text;
                string yText = yItem.sortItem.SubItems[yItem.sortColumn].Text;
                return xText.CompareTo(yText) * (this.ascending ? 1 : -1);
            }
        }
    }
}
