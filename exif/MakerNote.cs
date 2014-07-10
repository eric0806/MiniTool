using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace exif
{
    public struct CameraCustomSpec
    {
        public string ModelName;
        public string DisplayName;
        public Dictionary<string, string> Values;
    }

    class MakerNote
    {
        protected byte[] _MakerNoteData;
        protected int _MakerNoteOffset;
        protected bool _IsLittleEndian;
        protected Dictionary<string, string> _MakerNoteList = new Dictionary<string, string>();
        protected List<IFDEntry> IFDList = new List<IFDEntry>();
        protected string _Msg = string.Empty;
        protected CameraCustomSpec _Custom = new CameraCustomSpec();

        /// <summary>
        /// 整個MakerNote資料
        /// </summary>
        public byte[] MakerNoteData {
            //get { return _MakerNoteData; }
            set { _MakerNoteData = value; }
        }

        /// <summary>
        /// MakerNote資料內Offset必須減掉的值
        /// </summary>
        public int MakerNoteOffset {
            //get { return _MakerNoteOffset; }
            set { _MakerNoteOffset = value; }
        }

        /// <summary>
        /// 是否低位數在前
        /// </summary>
        public bool IsLittleEndian {
            //get { return _IsLittleEndian; }
            set { _IsLittleEndian = value; }
        }

        /// <summary>
        /// 相機專屬功能區
        /// </summary>
        public CameraCustomSpec Custom {
            get { return _Custom; }
        }

        /// <summary>
        /// MakerNote資料
        /// </summary>
        public Dictionary<string, string> MakerNoteList {
            get { return _MakerNoteList; }
        }

        public string Msg {
            get {
                //return "List容量:" + IFDList.Count.ToString() + Environment.NewLine + ExifFunc.BytesToOriString((int[])IFDList[0].val);
                //return "List[0] = " + ExifFunc.DebugEntry(IFDList[2]);
                return _Msg;
            }
            protected set {
                _Msg = value;
            }
        }


        public virtual void Run() { }

        /// <summary>
        /// 動態取得真正處理每個牌子內容的物件
        /// </summary>
        /// <param name="Make">廠牌</param>
        /// <param name="Model">型號</param>
        /// <returns></returns>
        public static MakerNote GetMakerObject(string Make, string Model) {
            switch (Make.ToLower()) {
                case "canon":
                    return new Canon.MakerNoteCanon(Make, Model);
                default:
                    return null;
            }
        }

        #region ParseItem相關

        protected string GetItemValueString(IFDEntry item) {
            string TrueValue = string.Empty;
            switch (item.type) {
                case 1:
                    TrueValue = GetValueOfByte(item); break;
                case 2:
                    TrueValue = GetValueOfAscii(item); break;
                case 3:
                case 4:
                case 9:
                    TrueValue = GetValueOfNum(item);
                    break;
                case 5:
                case 10:
                    TrueValue = GetValueOfRational(item);
                    break;
                case 7:
                    TrueValue = GetValueOfUndefined(item); break;
            }
            return TrueValue;
        }

        /// <summary>
        /// 處理BYTE資料
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetValueOfByte(IFDEntry item) {
            string ret = string.Empty;
            byte[] val = (byte[])item.val;
            ret = GetSpecialTagValue(item.tag, val);
            if (ret == string.Empty) {
                int temp = 0;
                for (int i = val.Length - 1; i >= 0; i--) {
                    temp += val[i] << val.Length - i - 1;
                }
                ret = temp.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 處理ASCII資料
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetValueOfAscii(IFDEntry item) {
            string ret = string.Empty;
            ret = GetSpecialTagValue(item.tag, item.val);
            if (ret == string.Empty) {
                ret = (string)item.val;
            }
            return ret;
        }

        /// <summary>
        /// 處理整數資料
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetValueOfNum(IFDEntry item) {
            string ret = string.Empty;
            int[] val = (int[])item.val;
            ret = GetSpecialTagValue(item.tag, val);
            if (ret == string.Empty) {
                //int temp = 0;
                for (int i = val.Length - 1; i >= 0; i--) {
                    //temp += val[i] << val.Length - i - 1;
                    ret += val[i].ToString() + ", ";
                }
                ret = ret.Substring(0, ret.Length - 2);
                //ret = temp.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 處理UNDEFINED資料
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetValueOfUndefined(IFDEntry item) {
            string ret = string.Empty;
            byte[] val = (byte[])item.val;
            ret = GetSpecialTagValue(item.tag, val);
            if (ret == string.Empty) {
                ret = Encoding.ASCII.GetString(val);
            }
            return ret;
        }

        /// <summary>
        /// 處理分數資料
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetValueOfRational(IFDEntry item) {
            string ret = string.Empty;
            int[][] val = (int[][])item.val;
            ret = GetSpecialTagValue(item.tag, val);
            if (ret == string.Empty) {
                for (int i = 0; i < val.Length; i++) {
                    ret += string.Format("{0}/{1}", val[i][0], val[i][1]) + ", ";
                }
                ret = ret.Substring(0, ret.Length - 2);
            }
            return ret;
        }


        /// <summary>
        /// 取得需要特殊處理顯示的Tag值
        /// </summary>
        /// <param name="tag">16進位的Int，表示Tag ID</param>
        /// <param name="args">參數陣列，可自由定義</param>
        /// <returns></returns>
        protected virtual string GetSpecialTagValue(int tag, object args) {
            string ret = string.Empty;
            switch (tag) {
                default:
                    break;
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// int32轉ushort(unsigned)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected ushort IntToUShort(int value) {
            return ushort.Parse(string.Format("{0:X}", value), System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// int32轉short(signed)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected short IntToShort(int value) {
            return short.Parse(string.Format("{0:X}", value), System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// int32轉ulong(unsigned)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected ulong IntToULong(int value) {
            return ulong.Parse(string.Format("{0:X}", value), System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// int32轉long(signed)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected long IntToLong(int value) {
            return long.Parse(string.Format("{0:X}", value), System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// 浮點數轉x/y形式的分數
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected string FloatToFraction(double a) {
            /*
             * 思考: xx.xx的分數，就是 xx.xx/1
             * 然後分子分母依據分子的小數位數乘以正確的10的次方
             * 最後分子分母做約分即可
             */
            double top; //分子
            double bottom; //分母
            bottom = 1 * Math.Pow(10, DotNumber(a));
            top = a * bottom;
            long cd = gcd((long)top, (long)bottom);
            top = top / cd;
            bottom = bottom / cd;
            return top.ToString() + "/" + bottom.ToString();
        }

        /// <summary>
        /// 取得小數點位數
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private int DotNumber(double a) {
            int offset = a.ToString().IndexOf(".");
            return a.ToString().Substring(offset + 1, a.ToString().Length - offset - 1).Length;
        }

        /// <summary>
        /// 取得兩數最大公約數
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected long gcd(long a, long b) {
            if (0 == b) return a;
            return gcd(b, a % b);
        }

    }
}
