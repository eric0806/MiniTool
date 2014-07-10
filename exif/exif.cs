using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace exif
{
    public struct IFDEntry
    {
        public int tag;         // TAG名稱可查詢Exif Spec
        public int type;        // 資料類型
        public int count;       // 大小
        public object val;         // 取值, 根據type定義會改變
    };

    /// <summary>
    /// 取得圖片exif元件，僅限Jpeg格式圖檔
    /// </summary>
    public partial class exif
    {
        public const int MAKER_PREFIX = 0xFF;
        public const int SOI = 0xD8;
        public const int APP0 = 0xE0;
        public const int APP1 = 0xE1;
        public const int EXIF_IFD_POINTER = 0x8769;
        public const int GPS_IFD_POINTER = 0x8825;
        public const int MAKER_NOTE_POINTER = 0x927C;
        public const int II = 0x4949;

        //資料長度(byte)
        public const int BYTE = 1;
        public const int SHORT = 2;
        public const int LONG = 4;
        public const int RATIONAL = 8;

        private int ExifIFDOffset = 0;
        private int GPSIFDOffset = 0;
        private int NextIFDOffset = 0;
        private int MakerNoteIFDOffset = 0;
        int TiffHead;
        byte[] MakerNoteData;
        private MakerNote MN;

        bool IsJpg = true;
        bool hadGPS = false;
        bool RunCameraSpecial = false;
        string latitude = string.Empty;
        string longitude = string.Empty;

        private string ErrMsg = string.Empty;

        /// <summary>
        /// 目前Stream位址
        /// </summary>
        long Pos = 0;

        //long APP1Length;

        /// <summary>
        /// IsLittleEndian表示低位數在前，必須高低位反轉才是正確數值；反之則不須反轉位數
        /// </summary>
        bool IsLittleEndian = true;
        FileStream fs = null;
        /// <summary>
        /// 儲存圖片內包含的Exif資料，沒有的就不寫進去
        /// </summary>
        private Dictionary<string, string> exifList;

        /// <summary>
        /// 儲存MakerNote裡面相機廠商的自訂資訊Exif
        /// </summary>
        private Dictionary<string, string> makerExifList;

        List<IFDEntry> IFDList = new List<IFDEntry>();
        //List<IFDEntry> MakerNoteIFDList = new List<IFDEntry>();

        /// <summary>
        /// 建構子
        /// </summary>
        public exif() {
            exifList = new Dictionary<string, string>();
            makerExifList = new Dictionary<string, string>();
        }

        /// <summary>
        /// 取得Exif列表
        /// </summary>
        public Dictionary<string, string> ExifList {
            get {
                return exifList;
            }
        }

        /// <summary>
        /// 取得相機廠商自訂Exif資訊
        /// </summary>
        public Dictionary<string, string> MakerExifList {
            get {
                return makerExifList;
            }
        }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMsg {
            get {
                return ErrMsg;
            }
        }

        public bool GetIsLittleEndian {
            get {
                return IsLittleEndian;
            }
        }

        public int MakerNoteCount {
            get { return makerExifList.Count; }
        }

        /// <summary>
        /// 是否有GPS資料
        /// </summary>
        public bool HadGPS {
            get { return hadGPS; }
        }

        /// <summary>
        /// 緯度字串，以度為單位，正表示北緯，負表示南緯
        /// </summary>
        public string Latitude {
            get { return latitude; }
        }

        /// <summary>
        /// 經度字串，以度為單位，正表示東經，負表示西經
        /// </summary>
        public string Longitude {
            get { return longitude; }
        }

        /// <summary>
        /// 相機專屬功能
        /// </summary>
        public CameraCustomSpec CameraCustom {
            get {
                try {
                    return MN.Custom;
                }
                catch { return new CameraCustomSpec(); }
            }
        }

        /// <summary>
        /// 處理Exif資料
        /// </summary>
        /// <param name="FilePath">檔案路徑</param>
        /// <param name="RunCameraSpecial">指出是否只處理基本Exif，速度考量</param>
        public bool ProcessExif(string FilePath, bool RunCameraSpecial) {
            this.RunCameraSpecial = RunCameraSpecial;
            ErrMsg = string.Empty;
            exifList.Clear();
            makerExifList.Clear();
            IFDList.Clear();
            Pos = 0;
            IsLittleEndian = true;
            NextIFDOffset = 0;
            ExifIFDOffset = 0;
            GPSIFDOffset = 0;
            //MakerNoteIFDOffset = 0;
            MakerNoteData = new byte[0];
            hadGPS = false;
            latitude = string.Empty;
            longitude = string.Empty;

            try {
                fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                IsJpg = ReadSOI();
                
                PassAPP0();
                if (IsJpg) { ReadAPP1(); }
                else { ReadIFD(); }
            }
            catch (Exception e) {
                
                ErrMsg += "讀取錯誤:" + e.Message + Environment.NewLine + "錯誤行:" + e.StackTrace;
                return false;
            }
            finally {
                fs.Close();
                fs.Dispose();
            }


            return true;
        }

        /// <summary>
        /// 判斷jpg檔頭
        /// </summary>
        /// <returns></returns>
        private bool ReadSOI() {
            byte[] soi = new byte[2];
            fs.Seek(Pos, SeekOrigin.Begin);
            fs.Read(soi, 0, 2);
            Pos = fs.Position;
            if (soi[0] != MAKER_PREFIX || soi[1] != SOI) { Pos -= 2; return false; }
            else { return true; }
        }

        /// <summary>
        /// 處理並略過APP0
        /// </summary>
        /// <returns></returns>
        private void PassAPP0() {
            byte[] title = new byte[2];
            fs.Seek(Pos, SeekOrigin.Begin);
            fs.Read(title, 0, 2);
            Pos = fs.Position;
            if (title[0] != MAKER_PREFIX || title[1] != APP0) {
                Pos -= 2;
                return;
            }
            byte[] len = new byte[2];
            fs.Seek(Pos, SeekOrigin.Begin);
            fs.Read(len, 0, 2);
            Pos = fs.Position;

            // APP0段長度，左移8位表示若原本為8C(1000 1100)，左移後變成8C00(1000 1100 0000 0000)，這樣再加上低位數len[1]就是正確數字
            int length = (len[0] << 8) + len[1];

            byte[] data = new byte[length - 2];
            fs.Seek(Pos, SeekOrigin.Begin);
            fs.Read(data, 0, data.Length);
            Pos = fs.Position;
        }



    }
}
