using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace extensions.Functions
{
    /// <summary>
    /// 靜態方法，提供常用的functions
    /// </summary>
    public static partial class Func
    {
        /*
         * IO相關
         */
        /// <summary>
        /// 讀取檔案內容
        /// </summary>
        /// <param name="FilePath">檔案實際路徑</param>
        /// <returns></returns>
        public static string LoadFile(string FilePath) {
            StreamReader sr;
            string content = "";
            try {
                sr = new StreamReader(FilePath, Encoding.Default);
                content = sr.ReadToEnd();
                sr.Close();
                return content;
            }
            catch {
                return "";
            }
        }

        /// <summary>
        /// 將內容附加或取代指定檔案，並以指定編碼方式儲存
        /// </summary>
        /// <param name="FilePath">檔案完整路徑</param>
        /// <param name="IsAppend">是否附加原檔後</param>
        /// <param name="FileContent">檔案內文</param>
        /// <param name="EncodingStr">編碼方式，big5 or utf-8</param>
        /// <returns></returns>
        public static bool SaveFile(string FilePath, bool IsAppend, string FileContent, string EncodingStr) {
            try {
                StreamWriter sw = new StreamWriter(FilePath, IsAppend, Encoding.GetEncoding(EncodingStr));
                sw.WriteLine(FileContent);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 將內容附加或取代指定檔案，並以系統預設編碼方式儲存
        /// </summary>
        /// <param name="FilePath">檔案完整路徑</param>
        /// <param name="IsAppend">是否附加原檔後</param>
        /// <param name="FileContent">檔案內文</param>
        /// <returns></returns>
        public static bool SaveFile(string FilePath, bool IsAppend, string FileContent) {
            try {
                StreamWriter sw = new StreamWriter(FilePath, IsAppend, Encoding.Default);
                sw.WriteLine(FileContent);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 將檔案內特殊字元以空白取代
        /// </summary>
        /// <param name="FilePath"></param>
        public static void ReplaceSpecialChar(string FilePath) {
            try {
                Regex reg = new Regex(@"[\u0000-\u0008|\u000b-\u000c|\u000e-\u001f]", RegexOptions.Multiline);
                string Content = LoadFile(FilePath);
                if (reg.IsMatch(Content)) {
                    Content = reg.Replace(Content, " ");
                }
                SaveFile(FilePath, false, Content);
            }
            catch {
            }
        }


    }
}
