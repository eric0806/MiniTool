using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace MiniTool
{
    partial class AboutMe : Form
    {
        public AboutMe() {
            InitializeComponent();
            this.Text = String.Format("關於 {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct + "  " + String.Format("版本 {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelMemo1.Text = "若有疑問，歡迎來信:erika.tw@gmail.com";
            this.labelMemo1.Links.Add(10, labelMemo1.Text.Length - 10, "mailto:erika.tw@gmail.com?subject=對於MiniTool " + AssemblyVersion + " 版本的問題或意見");
            this.labelMemo2.Text = "程式首頁:https://github.com/eric0806/MiniTool";
            this.labelMemo2.Links.Add(5, labelMemo2.Text.Length - 5, "https://github.com/eric0806/MiniTool");
            this.textBoxDescription.Text = ChangeLog;
        }

        #region 組件屬性存取子

        public string ChangeLog {
            get {
                return
                    "2012-09-28" + Environment.NewLine + 
                    "1.3.0.0" + Environment.NewLine + 
                    "1. 增加對Canon相機自訂Exif的支援，若相片內有Canon的自訂資料，可顯示在詳細Exif列表內。" + 
                    Environment.NewLine + Environment.NewLine +
                    "2012-09-19" + Environment.NewLine + 
                    "1.2.0.0" + Environment.NewLine + 
                    "1. 增加顯示GPS地圖功能，若圖片內有GPS座標資訊，可顯示拍攝地點的地圖。" +
                    Environment.NewLine + Environment.NewLine +
                    "2012-09-18" + Environment.NewLine +
                    "1.1.0.0" + Environment.NewLine + 
                    "1. 回歸正常介面，增加功能表及快捷設定功能。" + Environment.NewLine + 
                    "2. 增加圖片格式支援，可選擇僅JPG或是所有圖片格式。" + Environment.NewLine +
                    "3. 增加檢視完整Exif功能，選取圖片按右鍵，功能表內選擇「檢視完整Exif」。Exif內容正在完善中。" +
                    Environment.NewLine + Environment.NewLine +
                    "2012-09-13" + Environment.NewLine +
                    "1.0.0.4" + Environment.NewLine +
                    "1. 修正縮圖尺寸，會依據原始圖片尺寸顯示，而非一律正方形縮圖導致變形。" +
                    Environment.NewLine + Environment.NewLine +
                    "2012-09-12" + Environment.NewLine + 
                    "1.0.0.2" + Environment.NewLine +
                    "＊本版開始不支援XP以下作業系統！＊" + Environment.NewLine +
                    "1. 增加顯示相片縮圖功能，會在列表顯示24x24的縮圖，更能方便選擇想要搬移的圖片。" +
                    Environment.NewLine + Environment.NewLine +
                    "1.0.0.1" + Environment.NewLine +
                    "1. 增加如果沒有Exif訊息的照片也會讀入，拍攝日期就以檔案產生日期為主。" +
                    Environment.NewLine + Environment.NewLine +
                    "2012-09-11" + Environment.NewLine +
                    "1.0.0.0" + Environment.NewLine +
                    "1. 讀取目錄內有Exif的JPG檔，根據拍照日期做照片的搬移或複製。" + Environment.NewLine +
                    "2. 預設搬移資料夾名稱為拍照日期，可單獨為某部分照片指定其後要加入的文字敘述。如指定 \"測試\"，則會移動(複製)至 \"YYYY-MM-DD 測試\" 資料夾內。"
                    ;
            }
        }

        public string AssemblyTitle {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutMe_Load(object sender, EventArgs e) {
            
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (e.Link.LinkData != null) {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
        }

    }
}
