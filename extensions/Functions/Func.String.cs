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
        private const string FONT_RED = "<span style=\"color:red;\">?</span>";
        private const string FONT_GREEN = "<span style=\"color:#167D13;\">?</span>";
        private const string FONT_BLUE = "<span style=\"color:#096AC4;\">?</span>";
        private const string FONT_YELLOW = "<span style=\"color:#A47B00;\">?</span>";
        private const string BG_GREEN = "<span style=\"background-color:#B0B779;\">?</span>";
        private const string BG_RED = "<span style=\"background-color:#FFBDBD;\">?</span>";
        private const string BG_BLUE = "<span style=\"background-color:#AAC6C6;\">?</span>";
        private const string BG_YELLOW = "<span style=\"background-color:#FCF2D3;\">?</span>";
        private const string FONT_INDENT = "<span style=\"padding-left: 10px;\">?</span>";
        private static Encoding enc = Encoding.Default;

        /* 
         * 字串相關
         */
        /// <summary>
        /// 將輸入文字背景色設為淡紅色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetBGRed(string InStr) {
            return BG_RED.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字背景色設為淡綠色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetBGGreen(string InStr) {
            return BG_GREEN.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字背景設為淡藍色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetBGBlue(string InStr) {
            return BG_BLUE.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字背景設為淡黃色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetBGYellow(string InStr) {
            return BG_YELLOW.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字設為紅色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetFontRed(string InStr) {
            return FONT_RED.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字設為綠色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetFontGreen(string InStr) {
            return FONT_GREEN.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字設為藍色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetFontBlue(string InStr) {
            return FONT_BLUE.Replace("?", InStr);
        }

        /// <summary>
        /// 將輸入文字設為黃色
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetFontYellow(string InStr) {
            return FONT_YELLOW.Replace("?", InStr);
        }

        /// <summary>
        /// 輸入文字縮排10px
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public static string SetIndent(string InStr) {
            return FONT_INDENT.Replace("?", InStr);
        }

        /// <summary>
        /// 將字串左邊填滿指定位數的固定字元
        /// </summary>
        /// <param name="InText">原始輸入字串</param>
        /// <param name="FillCount">要填滿的長度</param>
        /// <param name="FillStr">要填滿的字元</param>
        /// <returns></returns>
        public static string FillChar(string InText, int FillCount, string FillStr) {
            int Len = GetLength(InText);
            if (Len < FillCount) {
                for (int i = 0; i < FillCount - Len; i++) {
                    InText = FillStr + InText;
                }
            }
            return InText;
        }

        /// <summary>
        /// 將字串右邊填滿指定位數的固定字元
        /// </summary>
        /// <param name="InText">原始輸入字串</param>
        /// <param name="FillCount">要填滿的長度</param>
        /// <param name="FillStr">要填滿的字元</param>
        /// <returns></returns>
        public static string FillCharRight(string InText, int FillCount, string FillStr) {
            int Len = GetLength(InText);
            if (Len < FillCount) {
                for (int i = 0; i < FillCount - Len; i++) {
                    InText += FillStr;
                }
            }
            return InText;
        }

        /// <summary>
        /// 取得字串真實長度，全形字長度為2
        /// </summary>
        /// <param name="InText">原始輸入字串</param>
        /// <returns></returns>
        public static int GetLength(string InText) {
            int Count = 0;
            for (int i = 0; i < InText.Length; i++) {
                Count += GetCharCount(InText.Substring(i, 1));
            }
            return Count;
        }

        /// <summary>
        /// 從輸入字串中取得從StartIndex開始的CharCount個字元，中文字長度為2
        /// </summary>
        /// <param name="InStr">原始輸入字串</param>
        /// <param name="StartIndex">起始位置，從0開始</param>
        /// <param name="CharCount">要擷取字元的長度</param>
        /// <returns></returns>
        public static string GetMidStr(string InStr, int StartIndex, int CharCount) {
            string OutStr = "";
            int RealPos, DoCount, MoveOffset;
            DoCount = 0;
            RealPos = 0;
            MoveOffset = 0;
            if (InStr.Length > 0) {
                for (int i = 0; i < InStr.Length; i++) {
                    if (GetLength(InStr.Substring(i, 1)) == 1) {
                        RealPos += 1;
                        MoveOffset = 1;
                    }
                    else {
                        RealPos += 2;
                        MoveOffset = 2;
                    }
                    if (RealPos > StartIndex) {
                        OutStr += InStr.Substring(i, 1);
                        DoCount += MoveOffset;
                    }
                    if (DoCount >= CharCount) { break; }
                }
            }
            return OutStr;
        }

        /// <summary>
        /// 將ReceiverName內的換行或Tab字元取代掉
        /// </summary>
        /// <param name="ReceiverName"></param>
        /// <returns></returns>
        public static string TrimShopperReceiverName(string ReceiverName) {
            if (ReceiverName == string.Empty) {
                return "";
            }
            else {
                return GetMidStr(TrimOrderSpeStr(ReceiverName), 0, 10);
            }
        }

        /// <summary>
        /// 將Order XML內某些欄位的值移掉特殊字元
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string TrimOrderSpeStr(string value) {
            if (value == string.Empty) {
                return "";
            }
            else {
                try {
                    value = value.Trim();
                    value = value.Replace("\t", "");
                    value = value.Replace("\n", "");
                    value = value.Replace(" ", "");
                    return value;
                }
                catch {
                    return "";
                }
            }
        }

        /// <summary>
        /// 從檔案完整路徑中取得檔名
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Type">1:回傳檔案路徑 2:回傳檔案名稱(不含附檔名)</param>
        /// <returns></returns>
        public static string GetFileNameInPath(string FilePath, int Type) {
            Regex reg = new Regex(@"([a-zA-Z]:\\.{1,}\\)(.{1,})\.[a-zA-Z0-9]{3,}", RegexOptions.Singleline);
            var matches = reg.Matches(FilePath);
            Match m = matches[0];
            return m.Groups[Type].Value;
        }

        /// <summary>
        /// 從檔案內讀取Big5文字，轉換成UTF-8編碼字串回傳
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string Big5ToU8(string FilePath) {
            try {
                StringBuilder txt = new StringBuilder();
                using (StreamReader sr = new StreamReader(FilePath, Encoding.Default)) {
                    while (sr.Peek() > -1) {
                        txt.AppendLine(sr.ReadLine());
                    }
                }
                return Big5ToU8(txt);
            }
            catch {
                return "";
            }

        }

        /// <summary>
        /// 從Big5編碼的StringBuilder轉換成UTF-8字串回傳
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string Big5ToU8(StringBuilder Content) {
            try {
                Encoding B5 = Encoding.Default;
                Encoding U8 = Encoding.UTF8;

                byte[] bByte = B5.GetBytes(Content.ToString());
                byte[] uByte = Encoding.Convert(B5, U8, bByte);
                char[] UChars = new char[U8.GetCharCount(uByte, 0, uByte.Length)];
                U8.GetChars(uByte, 0, uByte.Length, UChars, 0);
                return new string(UChars);
            }
            catch {
                return "";
            }
        }


        /// <summary>
        /// 取得字串右邊指定長度字元，例: Right("0abcde", 3) 回傳 "cde"
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string Right(string Str, int Length) {
            return Str.Substring(Str.Length - Length, Length);
        }

        private static int GetCharCount(string InChar) {
            return enc.GetByteCount(InChar);
        }

        /// <summary>
        /// 取得以文字敘述的檔案大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetFileSize(long size) {
            if (size < 1024) { return size.ToString() + " 個位元組"; }
            if (size / 1024 < 1024) { return (size / 1024).ToString() + " KB"; }
            if (size / 1024 < 1024 * 1024) { return (size / 1024 / 1024).ToString() + " MB"; }
            return (size / 1024 / 1024 / 1024).ToString() + " GB";
        }

    }
}
