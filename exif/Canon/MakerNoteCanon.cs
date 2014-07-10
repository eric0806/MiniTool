using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /*
         * 底下是實際MakerNote的內容範例
         *
         * 02BC: 1D 00  =NumRecord
         *       -ID-- -Type  ---Cnt---	 --Ofst---     #
         * 02BE: 01 00 03 00 2E 00 00 00 1E 04 00 00   1 - CameraSettings1
         * 02CA: 02 00 03 00 04 00 00 00 7A 04 00 00   2
         * 02D6: 03 00 03 00 04 00 00 00 82 04 00 00   3
         * 02E2: 04 00 03 00 22 00 00 00 8A 04 00 00   4 - CameraSettings2
         * 02EE: 06 00 02 00 0D 00 00 00 CE 04 00 00   5 - "Canon EOS 5D"
         * 02FA: 07 00 02 00 18 00 00 00 EE 04 00 00   6 - "Firmware Version 1.1.0"
         * 0306: 09 00 02 00 20 00 00 00 06 05 00 00   7 - OwnerName
         * 0312: 0C 00 04 00 01 00 00 00 1F 15 CE 42   8 - CameraSerialNum = 0x42CE151F
         * 031E: 0D 00 07 00 00 04 00 00 26 05 00 00   9
         * 032A: 0F 00 03 00 17 00 00 00 76 09 00 00   a - CustomFunctions
         * 0336: 10 00 04 00 01 00 00 00 13 02 00 80   b
         * 0342: 12 00 03 00 28 00 00 00 26 09 00 00   c
         * 034E: 13 00 03 00 04 00 00 00 A4 09 00 00   d
         * 035A: 15 00 04 00 01 00 00 00 00 00 00 A0   e
         * 0366: 19 00 03 00 01 00 00 00 01 00 00 00   f
         * 0372: 83 00 04 00 01 00 00 00 00 00 00 00  10
         * 037E: 93 00 03 00 10 00 00 00 AC 09 00 00  11
         * 038A: 95 00 02 00 40 00 00 00 CC 09 00 00  12 = "EF24-105mm f/4L IS USM"
         * 0396: 96 00 02 00 10 00 00 00 0C 0A 00 00  13
         * 03A2: A0 00 03 00 0E 00 00 00 1C 0A 00 00  14
         * 03AE: AA 00 03 00 05 00 00 00 38 0A 00 00  15
         * 03BA: B4 00 03 00 01 00 00 00 01 00 00 00  16
         * 03C6: E0 00 03 00 11 00 00 00 42 0A 00 00  17
         * 03D2: D0 00 04 00 01 00 00 00 00 00 00 00  18
         * 03DE: 01 40 03 00 1C 03 00 00 64 0A 00 00  19
         * 03EA: 02 40 03 00 66 2B 00 00 9C 10 00 00  1a
         * 03F6: 05 40 07 00 88 C0 00 00 68 67 00 00  1b
         * 0402: 08 40 03 00 03 00 00 00 F0 27 01 00  1c
         * 040E: 09 40 03 00 03 00 00 00 F6 27 01 00  1d
         */
        private int FocalUnits = 0;

        public MakerNoteCanon(string Make, string Model) {
            _IsLittleEndian = true;
            _Custom.Values = new Dictionary<string, string>();
            _Custom.ModelName = Model;
            _Custom.DisplayName = Model + "專屬內容";
            //_Custom.Values.Add("123:@@:1", "456");
        }

        public override void Run() {
            _IsLittleEndian = true;
            //前2byte為長度
            int len = ExifFunc.BytesToUShort(ExifFunc.GetBytes(_MakerNoteData, 0, 2), _IsLittleEndian);
            FindIFDItem(_MakerNoteData, 2, len);
            ParseItem();
        }

        private void FindIFDItem(byte[] Data, int index, int count) {
            byte[] item;
            for (int i = 0; i < count; i++) {
                item = ExifFunc.GetBytes(Data, index, 12);
                IFDList.Add(
                    ExifFunc.CreateIFDItem(
                        item,
                        ref _MakerNoteData,
                        ExifFunc.BytesToInt(ExifFunc.GetBytes(item, 8, 4), _IsLittleEndian) - _MakerNoteOffset,
                        _IsLittleEndian)
                    );
                index += 12;
            }
        }

        private void ParseItem() {
            foreach (IFDEntry item in IFDList) {
                //某些Tag的值是由一連串的值組成的，要特別處理
                switch (item.tag) {
                    case 0x0001:  //CanonCameraSettings
                    case 0x0002:  //CanonFocalLength
                    case 0x0004:  //CanonShotInfo
                    case 0x0012: //CanonAFInfo
                    case 0x001d: //CanonMyColors
                    case 0x0024: //FaceDetect1
                    case 0x0026: //CanonAFInfo2
                    case 0x0093: //CanonFileInfo
                    case 0x009A: //AspectInfo
                    case 0x00A0: //ProcessingInfo
                    case 0x00B1: //ModifiedInfo
                    case 0x00E0: //SensorInfo
                    case 0x4003: //ColorInfo
                    case 0x4018: //LightingOpt
                        ParseSpecial((int[])item.val, item.tag);
                        break;
                    case 0x0025: //FaceDetect2，是byte[]
                        ParseSpecial((byte[])item.val, item.tag);
                        break;
                    case 0x0005: //Panorama
                    case 0x000a: //UnknownD30
                    case 0x0011: //Movie Info
                    case 0x0027: //ContrastInfo
                    case 0x002f: //FaceDetect3
                    case 0x0035: //TimeInfo
                        //這些都不管他，去死
                        break;
                    case 0x000d: //CanonCameraInfo
                    case 0x000f: //CustomFunctions
                    case 0x0090: //CustomFunctions1D
                    case 0x0091: //PersonalFunctions
                    case 0x0092: //PersonalFunctionValues
                    case 0x0099: //CustomFunctions2
                    case 0x4001: //ColorData
                        //每個型號有不同的資訊，先跳過
                        break;
                    default: {
                            string TrueValue;
                            TrueValue = GetItemValueString(item);
                            if (GetDefaultTagName(item.tag) != string.Empty && TrueValue != string.Empty) {
                                _MakerNoteList.Add(GetDefaultTagName(item.tag) + ":@@:" + _MakerNoteList.Count.ToString(), TrueValue);
                            }
                            break;
                        }
                }
            }
        }


        #region 連續的子項目處理
        /// <summary>
        /// 處理值為連續的子項目組合成的Item
        /// </summary>
        /// <param name="val"></param>
        /// <param name="ItemTag"></param>
        private void ParseSpecial(int[] val, int ItemTag) {
            string tag = string.Empty, value = string.Empty;
            for (int i = 0; i < val.Length; i++) {
                switch (ItemTag) {
                    case 0x0001:
                        tag = GetCameraSettingsTagName(i);
                        value = ParseCameraSettings1Value(i, val[i]);
                        break;
                    case 0x0002:
                        tag = GetFocalLengthTagName(i);
                        value = ParseFocalLengthValue(i, val[i]);
                        break;
                    case 0x0004:
                        tag = GetShotInfoTagName(i);
                        value = ParseShotInfoValue(i, val[i]);
                        break;
                    case 0x0012:
                        tag = GetAFInfoTagName(i);
                        value = ParseAFInfoValue(i, val[i]);
                        break;
                    case 0x001d:
                        tag = GetMyColorsTagName(i);
                        value = ParseMyColorModeValue(i, val[i]);
                        break;
                    case 0x0024:
                        tag = GetFaceDetect1TagName(i);
                        value = ParseFaceDetect1Value(i, val[i]);
                        break;
                    case 0x0026:
                        tag = GetAFInfo2TagName(i);
                        value = ParseAFInfo2Value(i, val[i]);
                        break;
                    case 0x0093:
                        tag = GetFileInfoTagName(i);
                        value = ParseFileInfoValue(i, val[i]);
                        break;
                    case 0x009A:
                        tag = GetAspectInfoTagName(i);
                        value = ParseAspectInfoValue(i, val[i]);
                        break;
                    case 0x00A0:
                        tag = GetProcessingInfoTagName(i);
                        value = ParseProcessingInfoValue(i, val[i]);
                        break;
                    case 0x00B1:
                        tag = GetModifiedInfoTagName(i);
                        value = ParseModifiedInfoValue(i, val[i]);
                        break;
                    case 0x00E0:
                        tag = GetSensorInfoTagName(i);
                        value = ParseSensorInfoValue(i, val[i]);
                        break;
                    case 0x4003:
                        tag = GetColorInfoTagName(i);
                        value = ParseColorInfoValue(i, val[i]);
                        break;
                    case 0x4018:
                        tag = GetLightingOptTagName(i);
                        value = ParseLightingOptValue(i, val[i]);
                        break;
                    default:
                        tag = string.Empty;
                        value = string.Empty;
                        break;
                }
                if (tag != string.Empty && value != string.Empty) {
                    _MakerNoteList.Add(tag + ":@@:" + _MakerNoteList.Count.ToString(), value);
                }
            }
        }
        /// <summary>
        /// 處理值為連續的子項目組合成的Item
        /// </summary>
        /// <param name="val"></param>
        /// <param name="ItemTag"></param>
        private void ParseSpecial(byte[] val, int ItemTag) {
            string tag = string.Empty, value = string.Empty;
            for (int i = 0; i < val.Length; i++) {
                switch (ItemTag) {
                    case 0x0025:
                        tag = GetFaceDetect2TagName(i);
                        value = ParseFaceDetect2Value(i, val[i]);
                        break;
                }
                if (tag != string.Empty && value != string.Empty) {
                    _MakerNoteList.Add(tag + ":@@:" + _MakerNoteList.Count.ToString(), value);
                }
            }
        }
        /// <summary>
        /// 處理值為連續的子項目組合成的Item
        /// </summary>
        /// <param name="val"></param>
        /// <param name="ItemTag"></param>
        private void ParseSpecial(long[] val, int ItemTag) {
            string tag = string.Empty, value = string.Empty;
            for (int i = 0; i < val.Length; i++) {
                switch (ItemTag) {
                    default: break;
                }
                if (tag != string.Empty && value != string.Empty) {
                    _MakerNoteList.Add(tag + ":@@:" + _MakerNoteList.Count.ToString(), value);
                }
            }
        }
        #endregion

        #region 需要特殊處理的Tag
        /// <summary>
        /// 需要特殊處理的Tag在此處理
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override string GetSpecialTagValue(int tag, object args) {
            string ret = string.Empty;
            switch (tag) {
                case 0x0008: { //FileNumber
                        //string val = (string)args;
                        string val = ((int[])args)[0].ToString();
                        ret = val.Substring(0, 3) + "-" + val.Substring(3, val.Length - 3);
                        break;
                    }
                case 0x000c: { //Camera Serial Number
                        ulong val = IntToULong(((int[])args)[0]);
                        ret = val.ToString();
                        break;
                    }
                case 0x0010: { //CanonModelID
                        #region CanonModelID
                        uint val = uint.Parse(string.Format("{0:X}", ((int[])args)[0]), System.Globalization.NumberStyles.AllowHexSpecifier);
                        switch (val) {
                            case 0x1010000: ret = "PowerShot A30"; break;
                            case 0x1040000: ret = "PowerShot S300 / Digital IXUS 300 / IXY Digital 300"; break;
                            case 0x1060000: ret = "PowerShot A20"; break;
                            case 0x1080000: ret = "PowerShot A10"; break;
                            case 0x1090000: ret = "PowerShot S110 / Digital IXUS v / IXY Digital 200"; break;
                            case 0x1100000: ret = "PowerShot G2"; break;
                            case 0x1110000: ret = "PowerShot S40"; break;
                            case 0x1120000: ret = "PowerShot S30"; break;
                            case 0x1130000: ret = "PowerShot A40"; break;
                            case 0x1140000: ret = "EOS D30"; break;
                            case 0x1150000: ret = "PowerShot A100"; break;
                            case 0x1160000: ret = "PowerShot S200 / Digital IXUS v2 / IXY Digital 200a"; break;
                            case 0x1170000: ret = "PowerShot A200"; break;
                            case 0x1180000: ret = "PowerShot S330 / Digital IXUS 330 / IXY Digital 300a"; break;
                            case 0x1190000: ret = "PowerShot G3"; break;
                            case 0x1210000: ret = "PowerShot S45"; break;
                            case 0x1230000: ret = "PowerShot SD100 / Digital IXUS II / IXY Digital 30"; break;
                            case 0x1240000: ret = "PowerShot S230 / Digital IXUS v3 / IXY Digital 320"; break;
                            case 0x1250000: ret = "PowerShot A70"; break;
                            case 0x1260000: ret = "PowerShot A60"; break;
                            case 0x1270000: ret = "PowerShot S400 / Digital IXUS 400 / IXY Digital 400"; break;
                            case 0x1290000: ret = "PowerShot G5"; break;
                            case 0x1300000: ret = "PowerShot A300"; break;
                            case 0x1310000: ret = "PowerShot S50"; break;
                            case 0x1340000: ret = "PowerShot A80"; break;
                            case 0x1350000: ret = "PowerShot SD10 / Digital IXUS i / IXY Digital L"; break;
                            case 0x1360000: ret = "PowerShot S1 IS"; break;
                            case 0x1370000: ret = "PowerShot Pro1"; break;
                            case 0x1380000: ret = "PowerShot S70"; break;
                            case 0x1390000: ret = "PowerShot S60"; break;
                            case 0x1400000: ret = "PowerShot G6"; break;
                            case 0x1410000: ret = "PowerShot S500 / Digital IXUS 500 / IXY Digital 500"; break;
                            case 0x1420000: ret = "PowerShot A75"; break;
                            case 0x1440000: ret = "PowerShot SD110 / Digital IXUS IIs / IXY Digital 30a"; break;
                            case 0x1450000: ret = "PowerShot A400"; break;
                            case 0x1470000: ret = "PowerShot A310"; break;
                            case 0x1490000: ret = "PowerShot A85"; break;
                            case 0x1520000: ret = "PowerShot S410 / Digital IXUS 430 / IXY Digital 450"; break;
                            case 0x1530000: ret = "PowerShot A95"; break;
                            case 0x1540000: ret = "PowerShot SD300 / Digital IXUS 40 / IXY Digital 50"; break;
                            case 0x1550000: ret = "PowerShot SD200 / Digital IXUS 30 / IXY Digital 40"; break;
                            case 0x1560000: ret = "PowerShot A520"; break;
                            case 0x1570000: ret = "PowerShot A510"; break;
                            case 0x1590000: ret = "PowerShot SD20 / Digital IXUS i5 / IXY Digital L2"; break;
                            case 0x1640000: ret = "PowerShot S2 IS"; break;
                            case 0x1650000: ret = "PowerShot SD430 / Digital IXUS Wireless / IXY Digital Wireless"; break;
                            case 0x1660000: ret = "PowerShot SD500 / Digital IXUS 700 / IXY Digital 600"; break;
                            case 0x1668000: ret = "EOS D60"; break;
                            case 0x1700000: ret = "PowerShot SD30 / Digital IXUS i Zoom / IXY Digital L3"; break;
                            case 0x1740000: ret = "PowerShot A430"; break;
                            case 0x1750000: ret = "PowerShot A410"; break;
                            case 0x1760000: ret = "PowerShot S80"; break;
                            case 0x1780000: ret = "PowerShot A620"; break;
                            case 0x1790000: ret = "PowerShot A610"; break;
                            case 0x1800000: ret = "PowerShot SD630 / Digital IXUS 65 / IXY Digital 80"; break;
                            case 0x1810000: ret = "PowerShot SD450 / Digital IXUS 55 / IXY Digital 60"; break;
                            case 0x1820000: ret = "PowerShot TX1"; break;
                            case 0x1870000: ret = "PowerShot SD400 / Digital IXUS 50 / IXY Digital 55"; break;
                            case 0x1880000: ret = "PowerShot A420"; break;
                            case 0x1890000: ret = "PowerShot SD900 / Digital IXUS 900 Ti / IXY Digital 1000"; break;
                            case 0x1900000: ret = "PowerShot SD550 / Digital IXUS 750 / IXY Digital 700"; break;
                            case 0x1920000: ret = "PowerShot A700"; break;
                            case 0x1940000: ret = "PowerShot SD700 IS / Digital IXUS 800 IS / IXY Digital 800 IS"; break;
                            case 0x1950000: ret = "PowerShot S3 IS"; break;
                            case 0x1960000: ret = "PowerShot A540"; break;
                            case 0x1970000: ret = "PowerShot SD600 / Digital IXUS 60 / IXY Digital 70"; break;
                            case 0x1980000: ret = "PowerShot G7"; break;
                            case 0x1990000: ret = "PowerShot A530"; break;
                            case 0x2000000: ret = "PowerShot SD800 IS / Digital IXUS 850 IS / IXY Digital 900 IS"; break;
                            case 0x2010000: ret = "PowerShot SD40 / Digital IXUS i7 / IXY Digital L4"; break;
                            case 0x2020000: ret = "PowerShot A710 IS"; break;
                            case 0x2030000: ret = "PowerShot A640"; break;
                            case 0x2040000: ret = "PowerShot A630"; break;
                            case 0x2090000: ret = "PowerShot S5 IS"; break;
                            case 0x2100000: ret = "PowerShot A460"; break;
                            case 0x2120000: ret = "PowerShot SD850 IS / Digital IXUS 950 IS / IXY Digital 810 IS"; break;
                            case 0x2130000: ret = "PowerShot A570 IS"; break;
                            case 0x2140000: ret = "PowerShot A560"; break;
                            case 0x2150000: ret = "PowerShot SD750 / Digital IXUS 75 / IXY Digital 90"; break;
                            case 0x2160000: ret = "PowerShot SD1000 / Digital IXUS 70 / IXY Digital 10"; break;
                            case 0x2180000: ret = "PowerShot A550"; break;
                            case 0x2190000: ret = "PowerShot A450"; break;
                            case 0x2230000: ret = "PowerShot G9"; break;
                            case 0x2240000: ret = "PowerShot A650 IS"; break;
                            case 0x2260000: ret = "PowerShot A720 IS"; break;
                            case 0x2290000: ret = "PowerShot SX100 IS"; break;
                            case 0x2300000: ret = "PowerShot SD950 IS / Digital IXUS 960 IS / IXY Digital 2000 IS"; break;
                            case 0x2310000: ret = "PowerShot SD870 IS / Digital IXUS 860 IS / IXY Digital 910 IS"; break;
                            case 0x2320000: ret = "PowerShot SD890 IS / Digital IXUS 970 IS / IXY Digital 820 IS"; break;
                            case 0x2360000: ret = "PowerShot SD790 IS / Digital IXUS 90 IS / IXY Digital 95 IS"; break;
                            case 0x2370000: ret = "PowerShot SD770 IS / Digital IXUS 85 IS / IXY Digital 25 IS"; break;
                            case 0x2380000: ret = "PowerShot A590 IS"; break;
                            case 0x2390000: ret = "PowerShot A580"; break;
                            case 0x2420000: ret = "PowerShot A470"; break;
                            case 0x2430000: ret = "PowerShot SD1100 IS / Digital IXUS 80 IS / IXY Digital 20 IS"; break;
                            case 0x2460000: ret = "PowerShot SX1 IS"; break;
                            case 0x2470000: ret = "PowerShot SX10 IS"; break;
                            case 0x2480000: ret = "PowerShot A1000 IS"; break;
                            case 0x2490000: ret = "PowerShot G10"; break;
                            case 0x2510000: ret = "PowerShot A2000 IS"; break;
                            case 0x2520000: ret = "PowerShot SX110 IS"; break;
                            case 0x2530000: ret = "PowerShot SD990 IS / Digital IXUS 980 IS / IXY Digital 3000 IS"; break;
                            case 0x2540000: ret = "PowerShot SD880 IS / Digital IXUS 870 IS / IXY Digital 920 IS"; break;
                            case 0x2550000: ret = "PowerShot E1"; break;
                            case 0x2560000: ret = "PowerShot D10"; break;
                            case 0x2570000: ret = "PowerShot SD960 IS / Digital IXUS 110 IS / IXY Digital 510 IS"; break;
                            case 0x2580000: ret = "PowerShot A2100 IS"; break;
                            case 0x2590000: ret = "PowerShot A480"; break;
                            case 0x2600000: ret = "PowerShot SX200 IS"; break;
                            case 0x2610000: ret = "PowerShot SD970 IS / Digital IXUS 990 IS / IXY Digital 830 IS"; break;
                            case 0x2620000: ret = "PowerShot SD780 IS / Digital IXUS 100 IS / IXY Digital 210 IS"; break;
                            case 0x2630000: ret = "PowerShot A1100 IS"; break;
                            case 0x2640000: ret = "PowerShot SD1200 IS / Digital IXUS 95 IS / IXY Digital 110 IS"; break;
                            case 0x2700000: ret = "PowerShot G11"; break;
                            case 0x2710000: ret = "PowerShot SX120 IS"; break;
                            case 0x2720000: ret = "PowerShot S90"; break;
                            case 0x2750000: ret = "PowerShot SX20 IS"; break;
                            case 0x2760000: ret = "PowerShot SD980 IS / Digital IXUS 200 IS / IXY Digital 930 IS"; break;
                            case 0x2770000: ret = "PowerShot SD940 IS / Digital IXUS 120 IS / IXY Digital 220 IS"; break;
                            case 0x2800000: ret = "PowerShot A495"; break;
                            case 0x2810000: ret = "PowerShot A490"; break;
                            case 0x2820000: ret = "PowerShot A3100 IS / A3150 IS"; break;
                            case 0x2830000: ret = "PowerShot A3000 IS"; break;
                            case 0x2840000: ret = "PowerShot SD1400 IS / IXUS 130 / IXY 400F"; break;
                            case 0x2850000: ret = "PowerShot SD1300 IS / IXUS 105 / IXY 200F"; break;
                            case 0x2860000: ret = "PowerShot SD3500 IS / IXUS 210 / IXY 10S"; break;
                            case 0x2870000: ret = "PowerShot SX210 IS"; break;
                            case 0x2880000: ret = "PowerShot SD4000 IS / IXUS 300 HS / IXY 30S"; break;
                            case 0x2890000: ret = "PowerShot SD4500 IS / IXUS 1000 HS / IXY 50S"; break;
                            case 0x2920000: ret = "PowerShot G12"; break;
                            case 0x2930000: ret = "PowerShot SX30 IS"; break;
                            case 0x2940000: ret = "PowerShot SX130 IS"; break;
                            case 0x2950000: ret = "PowerShot S95"; break;
                            case 0x2980000: ret = "PowerShot A3300 IS"; break;
                            case 0x2990000: ret = "PowerShot A3200 IS"; break;
                            case 0x3000000: ret = "PowerShot ELPH 500 HS / IXUS 310 HS / IXY 31S"; break;
                            case 0x3010000: ret = "PowerShot Pro90 IS"; break;
                            case 0x3010001: ret = "PowerShot A800"; break;
                            case 0x3020000: ret = "PowerShot ELPH 100 HS / IXUS 115 HS / IXY 210F"; break;
                            case 0x3030000: ret = "PowerShot SX230 HS"; break;
                            case 0x3040000: ret = "PowerShot ELPH 300 HS / IXUS 220 HS / IXY 410F"; break;
                            case 0x3050000: ret = "PowerShot A2200"; break;
                            case 0x3060000: ret = "PowerShot A1200"; break;
                            case 0x3070000: ret = "PowerShot SX220 HS"; break;
                            case 0x3080000: ret = "PowerShot G1 X"; break;
                            case 0x3090000: ret = "PowerShot SX150 IS"; break;
                            case 0x3100000: ret = "PowerShot ELPH 510 HS / IXUS 1100 HS / IXY 51S"; break;
                            case 0x3110000: ret = "PowerShot S100 (new)"; break;
                            case 0x3120000: ret = "PowerShot ELPH 310 HS / IXUS 230 HS / IXY 600F"; break;
                            case 0x3130000: ret = "PowerShot SX40 HS"; break;
                            case 0x3140000: ret = "PowerShot ELPH 500 HS / IXUS 320 HS / IXY 32S"; break;
                            case 0x3160000: ret = "PowerShot A1300"; break;
                            case 0x3170000: ret = "PowerShot A810"; break;
                            case 0x3180000: ret = "PowerShot ELPH 320 HS / IXUS 240 HS / IXY 420F"; break;
                            case 0x3190000: ret = "PowerShot ELPH 110 HS / IXUS 125 HS / IXY 220F"; break;
                            case 0x3200000: ret = "PowerShot D20"; break;
                            case 0x3210000: ret = "PowerShot A4000 IS"; break;
                            case 0x3220000: ret = "PowerShot SX260 HS"; break;
                            case 0x3230000: ret = "PowerShot SX240 HS"; break;
                            case 0x3240000: ret = "PowerShot ELPH 530 HS / IXUS 510 HS / IXY 1"; break;
                            case 0x3250000: ret = "PowerShot ELPH 520 HS / IXUS 500 HS / IXY 3"; break;
                            case 0x3260000: ret = "PowerShot A3400 IS"; break;
                            case 0x3270000: ret = "PowerShot A2400 IS"; break;
                            case 0x3280000: ret = "PowerShot A2300"; break;
                            case 0x3350000: ret = "PowerShot SX160 IS"; break;
                            case 0x3370000: ret = "PowerShot SX500 IS"; break;
                            case 0x4040000: ret = "PowerShot G1"; break;
                            case 0x6040000: ret = "PowerShot S100 / Digital IXUS / IXY Digital"; break;
                            case 0x4007d673: ret = "DC19/DC21/DC22"; break;
                            case 0x4007d674: ret = "XH A1"; break;
                            case 0x4007d675: ret = "HV10"; break;
                            case 0x4007d676: ret = "MD130/MD140/MD150/MD160/ZR850"; break;
                            case 0x4007d777: ret = "DC50"; break;
                            case 0x4007d778: ret = "HV20"; break;
                            case 0x4007d779: ret = "DC211"; break;
                            case 0x4007d77a: ret = "HG10"; break;
                            case 0x4007d77b: ret = "HR10"; break;
                            case 0x4007d77d: ret = "MD255/ZR950"; break;
                            case 0x4007d81c: ret = "HF11"; break;
                            case 0x4007d878: ret = "HV30"; break;
                            case 0x4007d87c: ret = "XH A1S"; break;
                            case 0x4007d87e: ret = "DC301/DC310/DC311/DC320/DC330"; break;
                            case 0x4007d87f: ret = "FS100"; break;
                            case 0x4007d880: ret = "HF10"; break;
                            case 0x4007d882: ret = "HG20/HG21"; break;
                            case 0x4007d925: ret = "HF21"; break;
                            case 0x4007d926: ret = "HF S11"; break;
                            case 0x4007d978: ret = "HV40"; break;
                            case 0x4007d987: ret = "DC410/DC411/DC420"; break;
                            case 0x4007d988: ret = "FS19/FS20/FS21/FS22/FS200"; break;
                            case 0x4007d989: ret = "HF20/HF200"; break;
                            case 0x4007d98a: ret = "HF S10/S100"; break;
                            case 0x4007da8e: ret = "HF R10/R16/R17/R18/R100/R106"; break;
                            case 0x4007da8f: ret = "HF M30/M31/M36/M300/M306"; break;
                            case 0x4007da90: ret = "HF S20/S21/S200"; break;
                            case 0x4007da92: ret = "FS31/FS36/FS37/FS300/FS305/FS306/FS307"; break;
                            case 0x80000001: ret = "EOS-1D"; break;
                            case 0x80000167: ret = "EOS-1DS"; break;
                            case 0x80000168: ret = "EOS 10D"; break;
                            case 0x80000169: ret = "EOS-1D Mark III"; break;
                            case 0x80000170: ret = "EOS Digital Rebel / 300D / Kiss Digital"; break;
                            case 0x80000174: ret = "EOS-1D Mark II"; break;
                            case 0x80000175: ret = "EOS 20D"; break;
                            case 0x80000176: ret = "EOS Digital Rebel XSi / 450D / Kiss X2"; break;
                            case 0x80000188: ret = "EOS-1Ds Mark II"; break;
                            case 0x80000189: ret = "EOS Digital Rebel XT / 350D / Kiss Digital N"; break;
                            case 0x80000190: ret = "EOS 40D"; break;
                            case 0x80000213: ret = "EOS 5D"; break;
                            case 0x80000215: ret = "EOS-1Ds Mark III"; break;
                            case 0x80000218: ret = "EOS 5D Mark II"; break;
                            case 0x80000219: ret = "WFT-E1"; break;
                            case 0x80000232: ret = "EOS-1D Mark II N"; break;
                            case 0x80000234: ret = "EOS 30D"; break;
                            case 0x80000236: ret = "EOS Digital Rebel XTi / 400D / Kiss Digital X"; break;
                            case 0x80000241: ret = "WFT-E2"; break;
                            case 0x80000246: ret = "WFT-E3"; break;
                            case 0x80000250: ret = "EOS 7D"; break;
                            case 0x80000252: ret = "EOS Rebel T1i / 500D / Kiss X3"; break;
                            case 0x80000254: ret = "EOS Rebel XS / 1000D / Kiss F"; break;
                            case 0x80000261: ret = "EOS 50D"; break;
                            case 0x80000269: ret = "EOS-1D X"; break;
                            case 0x80000270: ret = "EOS Rebel T2i / 550D / Kiss X4"; break;
                            case 0x80000271: ret = "WFT-E4"; break;
                            case 0x80000273: ret = "WFT-E5"; break;
                            case 0x80000281: ret = "EOS-1D Mark IV"; break;
                            case 0x80000285: ret = "EOS 5D Mark III"; break;
                            case 0x80000286: ret = "EOS Rebel T3i / 600D / Kiss X5"; break;
                            case 0x80000287: ret = "EOS 60D"; break;
                            case 0x80000288: ret = "EOS Rebel T3 / 1100D / Kiss X50"; break;
                            case 0x80000297: ret = "WFT-E2 II"; break;
                            case 0x80000298: ret = "WFT-E4 II"; break;
                            case 0x80000301: ret = "EOS Rebel T4i / 650D / Kiss X6i"; break;
                            case 0x80000331: ret = "EOS M"; break;
                        }
                        break;
                        #endregion
                    }
                case 0x0015: { //SerialNumberFormat
                        ulong val = IntToULong(((int[])args)[0]);
                        switch (val) {
                            case 0x90000000: ret = "Format 1"; break;
                            case 0xa0000000: ret = "Format 2"; break;
                        }
                        break;
                    }
                case 0x001A: { //SuperMacro
                        switch (((int[])args)[0]) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "On (1)"; break;
                            case 2: ret = "On (2)"; break;
                        }
                        break;
                    }
                case 0x001C: { //DateStampMode
                        switch (((int[])args)[0]) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "Date"; break;
                            case 2: ret = "Date & Time"; break;
                        }
                        break;
                    }
                case 0x001e: { //FirmwareRevision
                        string val = extensions.Functions.Func.FillChar(string.Format("{0:X}", ((int[])args)[0]), 8, "0");
                        if (val.Substring(0, 1).ToUpper() == "A") { ret = "Alpha "; }
                        if (val.Substring(0, 1).ToUpper() == "B") { ret = "Beta "; }
                        ret += val.Substring(1, 1) + "." + val.Substring(2, 2);
                        ret += " rev " + val.Substring(4, 2) + "." + val.Substring(6, 2);
                        break;
                    }
                case 0x0023: { //Categories
                        int[] val = (int[])args;
                        string by = extensions.Functions.Func.FillChar(Convert.ToString(val[1], 2), 8, "0");
                        int idx = 0;
                        for (int i = by.Length - 1; i >= 0; i--) {
                            if (by.Substring(i, 1) == "1") {
                                switch (idx) {
                                    case 0: ret += "People,"; break;
                                    case 1: ret += "Scenery,"; break;
                                    case 2: ret += "Events,"; break;
                                    case 3: ret += "User 1,"; break;
                                    case 4: ret += "User 2,"; break;
                                    case 5: ret += "User 3,"; break;
                                    case 6: ret += "To Do,"; break;
                                }
                            }
                            idx++;
                        }
                        if (ret.Length > 2) ret = ret.Substring(0, ret.Length - 1);
                        else ret = "(none)";
                        break;
                    }
                case 0x0028: { //ImageUniqueID
                        byte[] val = (byte[])args;
                        for (int i = 0; i < val.Length; i++) {
                            ret += string.Format("{0:X}", val[i]);
                        }
                        break;
                    }
                case 0x00B4: { //ColorSpace
                        switch (((int[])args)[0]) {
                            case 1: ret = "sRGB"; break;
                            case 2: ret = "Adobe RGB"; break;
                        }
                        break;
                    }
                case 0x0096: { //InternalSerialNumber
                        ret = ((string)args).Replace("\0", "");
                        break;
                    }
                case 0x0098: { //CropInfo
                        int[] val = (int[])args;
                        try {
                            _MakerNoteList.Add("CropLeftMargin" + ":@@:" + _MakerNoteList.Count, val[0].ToString());
                            _MakerNoteList.Add("CropRightMargin" + ":@@:" + _MakerNoteList.Count, val[1].ToString());
                            _MakerNoteList.Add("CropTopMargin" + ":@@:" + _MakerNoteList.Count, val[2].ToString());
                            _MakerNoteList.Add("CropBottomMargin" + ":@@:" + _MakerNoteList.Count, val[3].ToString());
                        }
                        catch { }
                        break;
                    }
                default:
                    break;
            }
            return ret;
        }
        #endregion

        #region 相機各型號專屬區
        private void ParseCustom(int tag, int[] val) {
            string TagName = string.Empty, Value = string.Empty;
            for (int i = 0; i < val.Length; i++) {
                switch (tag) {
                    case 0x000D: {
                            switch (_Custom.ModelName) {
                                case "Canon EOS 60D":
                                    TagName = _60D.TagName.GetCameraInfoTagName(tag);
                                    break;
                            }
                            break;
                        }
                }
            }
            
        }
        #endregion
    }
}
