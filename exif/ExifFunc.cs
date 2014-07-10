using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace exif
{
    public static class ExifFunc
    {
        /// <summary>
        /// 取指定偏移量的byte資料
        /// </summary>
        /// <param name="bys">原始byte</param>
        /// <param name="offset">起始位置，從0開始</param>
        /// <param name="length">長度</param>
        /// <returns></returns>
        public static byte[] GetBytes(byte[] bys, int offset, int length) {
            byte[] retBytes = new byte[length];
            for (int i = 0; i < length; i++) {
                retBytes[i] = bys[i + offset];
            }
            return retBytes;
        }

        /// <summary>
        /// 2byte資料轉不帶負號ushort
        /// </summary>
        /// <param name="bstr"></param>
        /// <returns></returns>
        public static int BytesToUShort(byte[] bstr, bool IsLittleEndian) {
            if (IsLittleEndian) {
                return (bstr[1] << 8) + bstr[0];
            }
            else {
                return (bstr[0] << 8) + bstr[1];
            }
        }


        /// <summary>
        /// 4byte資料轉不帶負號Uint
        /// </summary>
        /// <param name="bstr"></param>
        /// <returns></returns>
        public static int BytesToInt(byte[] bstr, bool IsLittleEndian) {
            if (IsLittleEndian) {
                return ((bstr[3] << 24) + (bstr[2] << 16) + (bstr[1] << 8) + bstr[0]);
            }
            else {
                return ((bstr[0] << 24) + (bstr[1] << 16) + (bstr[2] << 8) + bstr[3]);
            }
        }

        /// <summary>
        /// 取得整段IFD原始資料
        /// </summary>
        /// <param name="StartPos">IFD起始位址</param>
        /// <param name="IFDCount">IFD元素數量</param>
        /// <returns></returns>
        public static byte[] GetIFDByteData(ref FileStream fs, long StartPos, bool IsLittleEndian, out int IFDCount) {
            fs.Seek(StartPos, SeekOrigin.Begin);
            byte[] len = new byte[2];
            fs.Read(len, 0, 2);
            IFDCount = ExifFunc.BytesToUShort(len, IsLittleEndian);
            byte[] rtn = new byte[(IFDCount * 12) + 4];
            fs.Seek(fs.Position, SeekOrigin.Begin);
            fs.Read(rtn, 0, rtn.Length);
            return rtn;
        }

        /// <summary>
        /// 將原始二進位資料放入IFDEntry中
        /// </summary>
        /// <param name="entry">12byte IFD資料</param>
        /// <param name="OriData">原始二進位資料陣列</param>
        /// <param name="Offset">從原始二進位資料抓資料的起始位址</param>
        /// <param name="IsLittleEndian">是否低字位在前</param>
        /// <returns></returns>
        public static IFDEntry CreateIFDItem(byte[] entry, ref byte[] OriData, int Offset, bool IsLittleEndian) {
            /*-------------------------------------------------------------------------------------------------------------
             * 資料型態:
             * 1 = BYTE An 8-bit unsigned integer., 
             * 2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL., 
             * 3 = SHORT A 16-bit (2-byte) unsigned integer, 
             * 4 = LONG A 32-bit (4-byte) unsigned integer, 
             * 5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the denominator., 
             * 7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition, 
             * 9 = SLONG A 32-bit (4-byte) signed integer (2's complement notation), 
             * 10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the denominator. 
             * ------------------------------------------------------------------------------------------------------------
             */
            IFDEntry item = new IFDEntry();
            item.tag = ExifFunc.BytesToUShort(ExifFunc.GetBytes(entry, 0, 2), IsLittleEndian);
            item.type = ExifFunc.BytesToUShort(ExifFunc.GetBytes(entry, 2, 2), IsLittleEndian);
            item.count = ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 4, 4), IsLittleEndian);

            byte[] data;
            switch (item.type) {
                case 1: {
                        /*---------------------------------------
                         * BYTE
                         * byte排列方式是照順序，所以不用判斷II或MM
                         * item.val存放類型是byte[]
                         * --------------------------------------
                         */
                        if (item.count * exif.BYTE > 4) { //紀錄的是位址，要從該位址+TiffHead取Count個byte的資料
                            data = ExifFunc.GetBytes(OriData, Offset, item.count);
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count);
                        }
                        item.val = data;
                        break;
                    }
                case 2: {
                        /*---------------------------------------------------
                         * ASCII 因為字元最後會以00(null)結尾，所以要取值的數量要減1
                         * ASCII排列方式是照順序，所以不用判斷II或MM
                         * 存放類型是string
                         * --------------------------------------------------
                         */
                        if (item.count > 4) { //紀錄的是位址，要從該位址+TiffHead取Count個byte的資料
                            data = ExifFunc.GetBytes(OriData, Offset, item.count);
                            /*
                            data = new byte[item.count - 1];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count - 1);
                            fs.Position = Pos;
                            */
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count);
                        }
                        item.val = Encoding.ASCII.GetString(data).Replace("\0", "");
                        break;
                    }
                case 3: {
                        /*------------------------------------------
                         * SHORT (unsigned)，排列順序要根據II或MM來判斷
                         * 存放類型是int[]
                         * -----------------------------------------
                         */
                        if (item.count * exif.SHORT > 4) { //紀錄的是位址，所以要從該位址+TiffHead取Count * SHORT個byte的資料
                            data = ExifFunc.GetBytes(OriData, Offset, item.count * exif.SHORT);
                            /*
                            data = new byte[item.count * exif.SHORT];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count * exif.SHORT);
                            fs.Position = Pos;
                            */
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count * exif.SHORT);
                        }
                        /*
                         * 處理資料:
                         * 假設count = 3，則要抓6個byte的資料
                         * 以II為例，假設抓到的資料是20001001F013
                         * 要先分開成2000 1001 F013
                         * 再重新排序成0020 0110 13F0
                         */
                        int[] val = new int[item.count];
                        for (int i = 0; i < item.count; i++) {
                            val[i] = ExifFunc.BytesToUShort(ExifFunc.GetBytes(data, i * exif.SHORT, exif.SHORT), IsLittleEndian);
                        }
                        item.val = val;
                        break;
                    }
                case 4:
                case 9: {
                        /*-----------------------------------------------------------
                         * LONG (unsigned) or SLONG (signed)，排列順序要根據II或MM來判斷
                         * 存放類型是int[]
                         * ----------------------------------------------------------
                         */
                        if (item.count * exif.LONG > 4) { //紀錄的是位址，所以要從該位址+TiffHead取Count * LONG個byte的資料
                            data = ExifFunc.GetBytes(OriData, Offset, item.count * exif.LONG);
                            /*
                            data = new byte[item.count * exif.LONG];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count * exif.LONG);
                            fs.Position = Pos;
                            */
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count * exif.LONG);
                        }
                        //處理資料，方法請參考SHORT
                        item.val = ProcessLong(data, item.count, IsLittleEndian);
                        break;
                    }
                case 5:
                case 10: {
                        /*--------------------------------------------------------------------------
                         * RATIONAL (2 long) or SRATIONAL (2 slong)
                         * 不管II或MM都是第一組long當分子，第二組long當分母;每組long的排序方式要依據II或MM改變
                         * 存放類型是int[count][2]
                         * -------------------------------------------------------------------------
                         */
                        data = ExifFunc.GetBytes(OriData, Offset, item.count * exif.RATIONAL);
                        /*
                        data = new byte[item.count * exif.RATIONAL];
                        Pos = fs.Position;
                        fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                        fs.Read(data, 0, item.count * exif.RATIONAL);
                        fs.Position = Pos;
                        */
                        item.val = ProcessRational(data, item.count, IsLittleEndian);
                        break;
                    }
                case 7: {
                        /*----------------------------------------------------------
                         * UNDEFINED
                         * 因為類型不固定，一個byte有可能是一個數字或文字，所以存原始byte資料
                         * 存放類型是byte[]
                         * ---------------------------------------------------------
                         */

                        //存取方式與byte相同，所以用相同方式
                        goto case 1;
                    }
            }
            return item;

        }

        /// <summary>
        /// 將原始二進位資料放入IFDEntry中
        /// </summary>
        /// <param name="entry">12byte IFD資料</param>
        /// <param name="fs">整個檔案的二進位串流</param>
        /// <param name="TiffHead">Tiff標頭到檔案開頭的距離</param>
        /// <param name="IsLittleEndian">是否為低字位在前</param>
        /// <returns></returns>
        public static IFDEntry CreateIFDItem(byte[] entry, ref FileStream fs, int TiffHead, bool IsLittleEndian) {


            IFDEntry item = new IFDEntry();
            item.tag = ExifFunc.BytesToUShort(ExifFunc.GetBytes(entry, 0, 2), IsLittleEndian);
            item.type = ExifFunc.BytesToUShort(ExifFunc.GetBytes(entry, 2, 2), IsLittleEndian);
            item.count = ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 4, 4), IsLittleEndian);

            long Pos;
            byte[] data;
            switch (item.type) {
                case 1: {
                        /*---------------------------------------
                         * BYTE
                         * byte排列方式是照順序，所以不用判斷II或MM
                         * item.val存放類型是byte[]
                         * --------------------------------------
                         */
                        if (item.count * exif.BYTE > 4) { //紀錄的是位址，要從該位址+TiffHead取Count個byte的資料
                            data = new byte[item.count];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count);
                            fs.Position = Pos;
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count);
                        }
                        item.val = data;
                        break;
                    }
                case 2: {
                        /*---------------------------------------------------
                         * ASCII 因為字元最後會以00(null)結尾，所以要取值的數量要減1
                         * ASCII排列方式是照順序，所以不用判斷II或MM
                         * 存放類型是string
                         * --------------------------------------------------
                         */
                        if (item.count > 4) { //紀錄的是位址，要從該位址+TiffHead取Count個byte的資料
                            data = new byte[item.count - 1];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count - 1);
                            fs.Position = Pos;
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count - 1);
                        }
                        item.val = Encoding.ASCII.GetString(data);
                        break;
                    }
                case 3: {
                        /*------------------------------------------
                         * SHORT (unsigned)，排列順序要根據II或MM來判斷
                         * 存放類型是int[]
                         * -----------------------------------------
                         */
                        if (item.count * exif.SHORT > 4) { //紀錄的是位址，所以要從該位址+TiffHead取Count * SHORT個byte的資料
                            data = new byte[item.count * exif.SHORT];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count * exif.SHORT);
                            fs.Position = Pos;
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count * exif.SHORT);
                        }
                        /*
                         * 處理資料:
                         * 假設count = 3，則要抓6個byte的資料
                         * 以II為例，假設抓到的資料是20001001F013
                         * 要先分開成2000 1001 F013
                         * 再重新排序成0020 0110 13F0
                         */
                        int[] val = new int[item.count];
                        for (int i = 0; i < item.count; i++) {
                            val[i] = ExifFunc.BytesToUShort(ExifFunc.GetBytes(data, i * exif.SHORT, exif.SHORT), IsLittleEndian);
                        }
                        item.val = val;
                        break;
                    }
                case 4:
                case 9: {
                        /*-----------------------------------------------------------
                         * LONG (unsigned) or SLONG (signed)，排列順序要根據II或MM來判斷
                         * 存放類型是int[]
                         * ----------------------------------------------------------
                         */
                        if (item.count * exif.LONG > 4) { //紀錄的是位址，所以要從該位址+TiffHead取Count * LONG個byte的資料
                            data = new byte[item.count * exif.LONG];
                            Pos = fs.Position;
                            fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                            fs.Read(data, 0, item.count * exif.LONG);
                            fs.Position = Pos;
                        }
                        else {
                            data = ExifFunc.GetBytes(entry, 8, item.count * exif.LONG);
                        }
                        //處理資料，方法請參考SHORT
                        item.val = ProcessLong(data, item.count, IsLittleEndian);
                        break;
                    }
                case 5:
                case 10: {
                        /*--------------------------------------------------------------------------
                         * RATIONAL (2 long) or SRATIONAL (2 slong)
                         * 不管II或MM都是第一組long當分子，第二組long當分母;每組long的排序方式要依據II或MM改變
                         * 存放類型是int[count][2]
                         * -------------------------------------------------------------------------
                         */
                        data = new byte[item.count * exif.RATIONAL];
                        Pos = fs.Position;
                        fs.Seek(ExifFunc.BytesToInt(ExifFunc.GetBytes(entry, 8, 4), IsLittleEndian) + TiffHead, SeekOrigin.Begin);
                        fs.Read(data, 0, item.count * exif.RATIONAL);
                        fs.Position = Pos;

                        item.val = ProcessRational(data, item.count, IsLittleEndian);
                        break;
                    }
                case 7: {
                        /*----------------------------------------------------------
                         * UNDEFINED
                         * 因為類型不固定，一個byte有可能是一個數字或文字，所以存原始byte資料
                         * 存放類型是byte[]
                         * ---------------------------------------------------------
                         */

                        //存取方式與byte相同，所以用相同方式
                        goto case 1;
                    }
            }
            return item;
        }

        /// <summary>
        /// 取得LONG與SLONG的資料
        /// </summary>
        /// <param name="val"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static int[] ProcessLong(byte[] data, int count, bool IsLittleEndian) {
            int[] ret = new int[count];
            for (int i = 0; i < count; i++) {
                ret[i] = ExifFunc.BytesToInt(ExifFunc.GetBytes(data, i * exif.LONG, exif.LONG), IsLittleEndian);
            }
            return ret;
        }

        /// <summary>
        /// 取得RATIONAL與SRATIONAL的資料
        /// </summary>
        /// <param name="val"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static int[][] ProcessRational(byte[] data, int count, bool IsLittleEndian) {
            /*
             * 處理資料:
             * 假設count = 2,則要抓 2 * 8 = 16個byte的資料
             * 以II為例，假設抓到的是18000021F0FF81003179003005E10F81
             * 則要先拆成兩組18000021F0FF8100 與 3179003005E10F81
             * 然後第一組再拆成18000021 F0FF8100
             * 再轉換成 21000018 0018FFF0
             * 將21000018放入int[0][0], 0018FFF0放入int[0][1]
             * 第二組拆換的就如上放入int[1][0]與int[1][1]
             */
            int[][] ret = new int[count][];
            for (int i = 0; i < count; i++) {
                ret[i] = ProcessLong(ExifFunc.GetBytes(data, i * exif.RATIONAL, exif.RATIONAL), 2, IsLittleEndian);
            }
            return ret;
        }

        /// <summary>
        /// byte陣列轉成文字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BytesToOriString(byte[] input) {
            string str = string.Empty;
            for (int i = 0; i < input.Length; i++) {
                str += extensions.Functions.Func.FillChar(string.Format("{0:X}", input[i]), 2, "0") + " ";
                if (i + 1 % 12 == 0) { str += Environment.NewLine; }
            }
            return str;
        }

        /// <summary>
        /// byte陣列轉成文字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BytesToOriString(int[] input) {
            string str = string.Empty;
            for (int i = 0; i < input.Length; i++) {
                str += extensions.Functions.Func.FillChar(string.Format("{0:X}", input[i]), 2, "0") + " ";
                if (i + 1 % 12 == 0) { str += Environment.NewLine; }
            }
            return str;
        }

        public static string DebugEntry(IFDEntry entry) {
            return string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", entry.tag, entry.type, entry.count, entry.val);

        }

        /// <summary>
        /// 取得APEX格式的光圈值
        /// </summary>
        /// <param name="a">分子</param>
        /// <param name="b">分母</param>
        /// <returns></returns>
        public static string GetAPEXFNumber(int a, int b) {
            return GetAPEXFNumber((double)a, (double)b);
        }
        public static string GetAPEXFNumber(long a, long b) {
            return GetAPEXFNumber((double)a, (double)b);
        }
        public static string GetAPEXFNumber(double a, double b) {
            /*
             * 如果值是2970854/1000000，算出來是2.970854，
             * 然後用算出根號2的該次方(根號二用1.4142來算)，
             * 也就是1.4142^2.970854，等於2.8
             */
            string ret;
            ret = string.Format("{0:0.0}", Math.Pow(Math.Sqrt(2), (a / b)));
            if (ret.Substring(ret.Length - 2, 2) == ".0") { ret = ret.Split(new char[1] { '.' })[0]; }
            return ret;
        }

        /// <summary>
        /// 取得APEX格式的快門速度
        /// </summary>
        /// <param name="a">分子</param>
        /// <param name="b">分母</param>
        /// <returns></returns>
        public static string GetAPEXExposureTime(int a, int b) {
            return GetAPEXExposureTime((double)a, (double)b);
        }
        public static string GetAPEXExposureTime(long a, long b) {
            return GetAPEXExposureTime((double)a, (double)b);
        }
        public static string GetAPEXExposureTime(double a, double b) {
            /*
             * 如果值是12287712/1000000，算出來是12.287712，
             * 然後算出2的該次方，也就是 2^12.287712，
             * 再倒數，最後就是 1/(2^12.287712)也就是1/5000
             */
            if (a < 0 || b < 0) {
                return string.Format("{0:0}", (double)1 / Math.Pow((double)2, (a / b)));
            }
            else {
                return "1/" + ((int)Math.Pow((double)2, (a / b))).ToString();
            }
        }

        /// <summary>
        /// 取得白平衡名稱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetWhiteBalanceName(short value) {
            string ret = string.Empty;
            switch (value) {
                case 0: ret = "Auto"; break;
                case 1: ret = "Daylight"; break;
                case 2: ret = "Cloudy"; break;
                case 3: ret = "Tungsten"; break;
                case 4: ret = "Fluorescent"; break;
                case 5: ret = "Flash"; break;
                case 6: ret = "Custom"; break;
                case 7: ret = "Black & White"; break;
                case 8: ret = "Shade"; break;
                case 9: ret = "Manual Temperature (Kelvin)"; break;
                case 10: ret = "PC SET1"; break;
                case 11: ret = "PC SET2"; break;
                case 12: ret = "PC SET3"; break;
                case 14: ret = "Daylight Fluorescent"; break;
                case 15: ret = "Custom 1"; break;
                case 16: ret = "Custom 2"; break;
                case 17: ret = "Underwater"; break;
                case 18: ret = "Custom 3"; break;
                case 19: ret = "Custom 4"; break;
                case 20: ret = "PC SET4"; break;
                case 21: ret = "PC SET5"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得白平衡名稱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetWhiteBalanceName(ushort value) {
            string ret = GetWhiteBalanceName((short)value);
            if (ret == string.Empty) {
                switch (value) {
                    case 0x8000: ret = "Custom, Auto"; break;
                    case 0x8001: ret = "Custom, Daylight"; break;
                    case 0x8002: ret = "Custom, Cloudy"; break;
                    case 0x8003: ret = "Custom, Tungsten"; break;
                    case 0x8004: ret = "Custom, Fluorescent"; break;
                    case 0x8005: ret = "Custom, Flash"; break;
                    case 0x8006: ret = "Custom, Custom"; break;
                    case 0x8007: ret = "Custom, Black & White"; break;
                    case 0x8008: ret = "Custom, Shade"; break;
                    case 0x8009: ret = "Custom, Manual temperature"; break;
                    case 0x800a: ret = "Custom, PC Set 1"; break;
                    case 0x800b: ret = "Custom, PC Set 2"; break;
                    case 0x800c: ret = "Custom, PC Set 3"; break;
                    case 0x800e: ret = "Custom, Daylight Fluorescent"; break;
                    case 0x800f: ret = "Custom, Custom 1"; break;
                    case 0x8010: ret = "Custom, Custom 2"; break;
                    case 0x8011: ret = "Custom, Underwater"; break;
                    case 0x8012: ret = "Custom, Custom 3"; break;
                    case 0x8013: ret = "Custom, Custom 4"; break;
                    case 0x8014: ret = "Custom, PC Set 4"; break;
                    case 0x8015: ret = "Custom, PC Set 5"; break;
                    case 0xffff: ret = "n/a"; break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 取得PictureStyle名稱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetPictureStyleName(short value) {
            string ret = string.Empty;
            switch (value) {
                case 0x0: ret = "None"; break;
                case 0x1: ret = "Standard"; break;
                case 0x2: ret = "Portrait"; break;
                case 0x3: ret = "High Saturation"; break;
                case 0x4: ret = "Adobe RGB"; break;
                case 0x5: ret = "Low Saturation"; break;
                case 0x6: ret = "CM Set 1"; break;
                case 0x7: ret = "CM Set 2"; break;
                case 0x21: ret = "User Def. 1"; break;
                case 0x22: ret = "User Def. 2"; break;
                case 0x23: ret = "User Def. 3"; break;
                case 0x41: ret = "PC 1"; break;
                case 0x42: ret = "PC 2"; break;
                case 0x43: ret = "PC 3"; break;
                case 0x81: ret = "Standard"; break;
                case 0x82: ret = "Portrait"; break;
                case 0x83: ret = "Landscape"; break;
                case 0x84: ret = "Neutral"; break;
                case 0x85: ret = "Faithful"; break;
                case 0x86: ret = "Monochrome"; break;
                case 0x87: ret = "Auto"; break;
            }
            return ret;
        }
    }
}
