using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;

namespace extensions.Functions
{
    /// <summary>
    /// 靜態方法，提供常用的functions
    /// </summary>
    public static partial class Func
    {
        /*
         * 網路相關函數
         * 
         */

        /// <summary>
        /// 將PostData以指定的編碼和Post方式送到Url
        /// </summary>
        /// <param name="Url">目的Url</param>
        /// <param name="PostData">要送的資料</param>
        /// <param name="EncodingStr">big5 or utf-8</param>
        /// <returns></returns>
        public static string PostUrl(string Url, string PostData, string EncodingStr) {
            byte[] PostByte = Encoding.GetEncoding(EncodingStr).GetBytes(PostData);
            HttpWebRequest WebRQ;
            HttpWebResponse WebRS;
            Stream DataStream;
            StreamReader sr;
            string ReceiveData = "";

            try {
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);
                WebRQ = (HttpWebRequest)WebRequest.Create(Url);
                WebRQ.Timeout = 6000;
                WebRQ.Credentials = CredentialCache.DefaultCredentials;
                WebRQ.CachePolicy = new System.Net.Cache.HttpRequestCachePolicy(DateTime.Now);
                WebRQ.Method = "POST";
                WebRQ.ContentType = "application/x-www-form-urlencoded";
                WebRQ.ContentLength = PostByte.Length;
                Stream RQStream = WebRQ.GetRequestStream();
                RQStream.Write(PostByte, 0, PostByte.Length);
                RQStream.Close();

                WebRS = (HttpWebResponse)WebRQ.GetResponse();
                DataStream = WebRS.GetResponseStream();
                sr = new StreamReader(DataStream, Encoding.GetEncoding(EncodingStr));

                ReceiveData = sr.ReadToEnd();
                sr.Close();
                DataStream.Close();
                WebRS.Close();
            }
            catch {
                ReceiveData = "";
            }
            return ReceiveData;
        }

        /// <summary>
        /// 以Get方式取得Url內容
        /// </summary>
        /// <param name="url">目的Url</param>
        /// <param name="EncodingStr">big5 or utf-8</param>
        /// <returns></returns>
        public static string GetUrl(string url, string EncodingStr) {
            string ReceiveData;
            HttpWebRequest WebRQ;
            HttpWebResponse WebRS;
            Stream DataStream;
            StreamReader sr;

            try {
                WebRQ = (HttpWebRequest)WebRequest.Create(url);
                WebRQ.Timeout = 6000;
                WebRQ.Credentials = CredentialCache.DefaultCredentials;
                WebRQ.CachePolicy = new System.Net.Cache.HttpRequestCachePolicy(DateTime.Now);

                WebRS = (HttpWebResponse)WebRQ.GetResponse();
                DataStream = WebRS.GetResponseStream();
                sr = new StreamReader(DataStream, Encoding.GetEncoding(EncodingStr));
                ReceiveData = sr.ReadToEnd();

                sr.Close();
                DataStream.Close();
                WebRS.Close();
            }
            catch {
                ReceiveData = "";
            }
            return ReceiveData;
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            return true;
        }
    }
}
