using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;

namespace extensions.Functions
{
    /// <summary>
    /// 靜態方法，提供常用的functions
    /// </summary>
    public static partial class Func
    {
        /*
         * 日期相關函數
         *
         */

        /// <summary>
        /// 將HHMM[SS]轉換為HH:mm:SS
        /// </summary>
        /// <param name="InTimeStr">四碼或六碼時間  </param>
        /// <returns></returns>
        public static string StrToTimeStr(string InTimeStr) {
            if (InTimeStr.Length == 4) {
                InTimeStr += "00";
            }
            if (InTimeStr.Length == 6) {
                return InTimeStr.Substring(0, 2) + ":" + InTimeStr.Substring(2, 2) + ":" + InTimeStr.Substring(4, 2);
            }
            else {
                return "00:00:00";
            }
        }

        /// <summary>
        /// 將時間轉換成HHmmSS格式
        /// </summary>
        /// <param name="InTime"></param>
        /// <returns></returns>
        public static string TimeFormat6(DateTime InTime) {
            try {
                return FillChar(InTime.Hour.ToString(), 2, "0") + FillChar(InTime.Minute.ToString(), 2, "0") + FillChar(InTime.Second.ToString(), 2, "0");
            }
            catch {
                return "000000";
            }
        }

        /// <summary>
        /// 將YYYYMMDD轉換為YYYY/MM/DD
        /// </summary>
        /// <param name="InDateStr">八碼日期</param>
        /// <returns></returns>
        public static DateTime StrToDate(string InDateStr) {
            if (InDateStr.Length == 8) {
                try {
                    return DateTime.Parse(InDateStr.Substring(0, 4) + "/" + InDateStr.Substring(4, 2) + "/" + InDateStr.Substring(6, 2)).Date;
                }
                catch {
                    return DateTime.Today;
                }
            }
            else {
                return DateTime.Today;
            }
        }

        /// <summary>
        /// 將日期轉換成YYYYMMDD格式
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string DateFormat8(DateTime InDate) {
            try {
                return InDate.Year.ToString() + FillChar(InDate.Month.ToString(), 2, "0") + FillChar(InDate.Day.ToString(), 2, "0");
            }
            catch {
                return "0000/00/00";
            }
        }

        /// <summary>
        /// 將YYYY/MM/DD轉換成YYYY-MM-DD
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string DateSlashToDash(DateTime InDate) {
            try {
                return InDate.Year + "-" + FillChar(InDate.Month.ToString(), 2, "0") + "-" + FillChar(InDate.Day.ToString(), 2, "0");
            }
            catch {
                return "0000-00-00";
            }
        }

        /// <summary>
        /// 將YYYY-MM-DD轉換成YYYY/MM/DD
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string DateDashToSlash(DateTime InDate) {
            try {
                return InDate.Year + "/" + FillChar(InDate.Month.ToString(), 2, "0") + "/" + FillChar(InDate.Day.ToString(), 2, "0");
            }
            catch {
                return "0000/00/00";
            }
        }

        /// <summary>
        /// 將西元日期轉換為七碼民國年月日YYYMMDD
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string DateFormat7(DateTime InDate) {
            return FillChar((InDate.Year - 1911).ToString(), 3, "0") + FillChar(InDate.Month.ToString(), 2, "0") + FillChar(InDate.Day.ToString(), 2, "0");
        }

        /// <summary>
        /// 取得民國年YYY-MM-DD
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string ChineseDateDash(DateTime InDate) {
            return (InDate.Year - 1911).ToString() + "-" + FillChar(InDate.Month.ToString(), 2, "0") + "-" + FillChar(InDate.Day.ToString(), 2, "0");
        }

        /// <summary>
        /// 將民國日期七碼字串轉換成西元日期
        /// </summary>
        /// <param name="InDateStr"></param>
        /// <returns></returns>
        public static DateTime Date7ToDate(string InDateStr) {
            DateTime OutDate;
            if (InDateStr.Substring(0, 1) == " ") {
                InDateStr = "0" + InDateStr.Trim();
            }
            if (InDateStr.Length != 7) {
                OutDate = DateTime.Today;
            }
            else {
                OutDate = DateTime.Parse((int.Parse(InDateStr.Substring(0, 3)) + 1911).ToString() + "/" + InDateStr.Substring(3, 2) + "/" + InDateStr.Substring(5, 2));
            }
            return OutDate;
        }

        /// <summary>
        /// 產生YYYYMMDDHHmmSS字串
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string MakeTimeStamp(DateTime InDate) {
            return InDate.Year.ToString()
                + FillChar(InDate.Month.ToString(), 2, "0")
                + FillChar(InDate.Day.ToString(), 2, "0")
                + FillChar(InDate.Hour.ToString(), 2, "0")
                + FillChar(InDate.Minute.ToString(), 2, "0")
                + FillChar(InDate.Second.ToString(), 2, "0");
        }

        /// <summary>
        /// 產生像Exif一樣YYYY:MM:DD HH:MM:SS格式的日期字串
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public static string MakeExifDateFormat(DateTime InDate) {
            return InDate.Year.ToString()
                + ":"
                + FillChar(InDate.Month.ToString(), 2, "0")
                + ":"
                + FillChar(InDate.Day.ToString(), 2, "0")
                + " "
                + FillChar(InDate.Hour.ToString(), 2, "0")
                + ":"
                + FillChar(InDate.Minute.ToString(), 2, "0")
                + ":"
                + FillChar(InDate.Second.ToString(), 2, "0");
        }
    }
}
