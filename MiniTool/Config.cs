using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using extensions;
using extensions.Functions;

namespace MiniTool
{
    public static class Config
    {
        /// <summary>
        /// 狀態列目前文字
        /// </summary>
        public static string StatusBarText = string.Empty;

        private static Dictionary<string, string> ConfList = new Dictionary<string, string>();

        private static TINI oTINI = new TINI(Path.Combine(Application.StartupPath, "MiniTool.ini"));

        /// <summary>
        /// 載入INI設定檔
        /// </summary>
        public static void LoadConfig() {
            if (!File.Exists(Path.Combine(Application.StartupPath, "MiniTool.ini"))) {
                if (!MakeINI()) { return; }
            }
        }

        /// <summary>
        /// 取得設定內容
        /// </summary>
        /// <param name="ConfKey"></param>
        /// <returns></returns>
        public static string GetConfig(string ConfKey) {
            try {
                return oTINI.getKeyValue("Settings", ConfKey);
            }
            catch {
                return string.Empty;
            }
        }

        /// <summary>
        /// 設定並儲存設定檔
        /// </summary>
        /// <param name="ConfKey"></param>
        /// <param name="ConfValue"></param>
        public static void SetConfig(string ConfKey, string ConfValue) {
            try {
                oTINI.setKeyValue("Settings", ConfKey, ConfValue);
            }
            catch (Exception e) {
                StatusBarText = e.Message;
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 產生INI設定檔
        /// </summary>
        /// <returns></returns>
        private static bool MakeINI() {
            try {
                StringBuilder s = new StringBuilder();
                s.AppendLine("[Settings]");
                s.AppendLine("SourcePath=");
                s.AppendLine("TargetPath=");
                s.AppendLine("SyncName=1");
                s.AppendLine("MoveMethod=M");
                s.AppendLine("YearMode=4");
                s.AppendLine("WindowWidth=");
                s.AppendLine("WindowHeight=");
                s.AppendLine("IsMax=");
                s.AppendLine("ReadRAW=0");
                s.AppendLine("Zoom=15");
                Func.SaveFile(Path.Combine(Application.StartupPath, "MiniTool.ini"), false, s.ToString());
                return true;
            }
            catch (Exception e) {
                StatusBarText = e.Message;
                return false;
            }
        }
    }
}
