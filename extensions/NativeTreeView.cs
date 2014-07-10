using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace extensions
{
    /// <summary>
    /// 使用系統主題的Treeview
    /// </summary>
    public class NativeTreeView : System.Windows.Forms.TreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);

        protected override void CreateHandle() {
            base.CreateHandle();

            SetWindowTheme(this.Handle, "explorer", null);
        }
    }
}
