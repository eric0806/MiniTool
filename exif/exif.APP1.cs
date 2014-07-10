using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace exif
{
    public partial class exif
    {
        /// <summary>
        /// 如果是JPG檔，讀取並處理APP1區段
        /// </summary>
        /// <returns></returns>
        private void ReadAPP1() {
            //int index;
            byte[] buffer = new byte[2];
            fs.Seek(Pos, SeekOrigin.Begin);
            fs.Read(buffer, 0, 2);
            //如果沒有這兩個Head表示沒APP1區段，返回
            if (buffer[0] != MAKER_PREFIX || buffer[1] != APP1) { return; }

            //pos位置目前在FF E1之後
            Pos = fs.Position;

            //接下來略過2byte長度、6byteExif標記
            Pos += 8;

            //交由處理Tiff的Function處理
            ReadIFD();
        }

        /// <summary>
        /// 讀取Tiff檔頭起始的資料
        /// </summary>
        private void ReadIFD() {
            //目前Pos，如果是未壓縮格式，位址是0，如果是Jpg，會有偏移量
            TiffHead = (int)Pos; // 0;
            //Pos = 0;
            fs.Seek(Pos, SeekOrigin.Begin);
            byte[] head = new byte[2];
            fs.Read(head, 0, 2);
            //判斷高低位順序
            IsLittleEndian = CheckIsLittleEndian(ExifFunc.GetBytes(head, 0, 2));
            Pos = fs.Position;
            //略過00 2A
            Pos += 2;
            fs.Seek(Pos, SeekOrigin.Begin);
            //4個byte為IFD0距離Header的偏移量，因為Header在00，所以直接取得位址。該偏移量+TiffHead位址就是IFD0的起始位址
            byte[] offset = new byte[4];
            fs.Read(offset, 0, 4);

            byte[] IFD;
            int IFDCount;
            //取得IFD0資料，要加上TiffHead的偏移量才是正確位址
            IFD = ExifFunc.GetIFDByteData(ref fs, ExifFunc.BytesToInt(offset, IsLittleEndian) + TiffHead, IsLittleEndian, out IFDCount);
            NextIFDOffset = FindIFDItem(IFD, 0, IFDCount);

            if (ExifIFDOffset != 0) {
                IFD = ExifFunc.GetIFDByteData(ref fs, ExifIFDOffset + TiffHead, IsLittleEndian, out IFDCount);
                FindIFDItem(IFD, 0, IFDCount);
            }
            if (GPSIFDOffset != 0) {
                IFD = ExifFunc.GetIFDByteData(ref fs, GPSIFDOffset + TiffHead, IsLittleEndian, out IFDCount);
                FindIFDItem(IFD, 0, IFDCount);
            }
            //if (MakerNoteData.Length > 0) { ErrMsg += BytesToOriString(MakerNoteData); }

            ParseItem();
        }

        /// <summary>
        /// 查找每一個Item
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private int FindIFDItem(byte[] Data, int index, int count) {
            byte[] item;
            for (int i = 0; i < count; i++) {
                item = ExifFunc.GetBytes(Data, index, 12);
                if (ExifFunc.BytesToUShort(ExifFunc.GetBytes(item, 0, 2), IsLittleEndian) == MAKER_NOTE_POINTER) {
                    /*
                     * MakerNoteIFDOffset代表的是
                     * 如果MakerNote某個IFD紀錄的是偏移量，必須減掉MakerNoteIFDOffset
                     * MakerNoteIFDOffset為MakerNote資料起始的實際位址減TiffHead
                     */

                    MakerNoteIFDOffset = ExifFunc.BytesToInt(ExifFunc.GetBytes(item, 8, 4), IsLittleEndian);// +TiffHead;
                    ErrMsg = "TiffHead:" + TiffHead.ToString() + Environment.NewLine + "MakerOffset:" + MakerNoteIFDOffset.ToString();
                }
                IFDList.Add(ExifFunc.CreateIFDItem(item, ref fs, TiffHead, IsLittleEndian));
                //如果Tag是Exif IFD Pointer的話就要記錄偏移量
                if (IFDList[i].tag == EXIF_IFD_POINTER) {
                    ExifIFDOffset = ((int[])IFDList[i].val)[0];
                }
                //如果Tag是GPS IFD Pointer的話也要記錄偏移量
                if (IFDList[i].tag == GPS_IFD_POINTER) {
                    GPSIFDOffset = ((int[])IFDList[i].val)[0];
                }
                //如果Tag是MakerNote IFD Pointer的話，取得的資料就是整個MakerNote[]
                if (IFDList[i].tag == MAKER_NOTE_POINTER) {
                    MakerNoteData = (byte[])IFDList[i].val;
                }
                index += 12;
            }
            return index;
        }



        /// <summary>
        /// 判斷TIFF Header前2 byte，作為後面資料排序狀態準則
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private bool CheckIsLittleEndian(byte[] buffer) {
            if (((buffer[0] << 8) + buffer[1]) == II) { return true; }
            else { return false; }
        }
    }
}
