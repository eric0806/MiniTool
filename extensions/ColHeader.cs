using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace extensions
{
    // The ColHeader class is a ColumnHeader object with an
    // added property for determining an ascending or descending sort.
    // True specifies an ascending order, false specifies a descending order.
    public class ColHeader : ColumnHeader
    {
        public bool ascending;

        public ColHeader()
            : base() {
            this.ascending = false;
            this.TextAlign = HorizontalAlignment.Left;
        }
        public ColHeader(string text, int width, HorizontalAlignment align, bool asc) {
            this.Text = text;
            this.Width = width;
            this.TextAlign = align;
            this.ascending = asc;
        }


    }
}
