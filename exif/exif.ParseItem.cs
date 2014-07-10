using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using extensions.Functions;

namespace exif
{
    public partial class exif
    {
        /// <summary>
        /// 處理每一個Item
        /// </summary>
        /// 
        private void ParseItem() {
            string TrueValue = string.Empty;
            foreach (IFDEntry item in IFDList) {
                TrueValue = GetItemValueString(item);
                if (GetIFDTagName(item.tag) != string.Empty && TrueValue != string.Empty) {
                    exifList.Add(GetIFDTagName(item.tag), TrueValue);
                }
            }
            if (this.RunCameraSpecial) {
                if (MakerNoteData.Length > 0) {
                    MN = MakerNote.GetMakerObject(exifList["Make"], exifList["Model"]);
                    MN.MakerNoteData = MakerNoteData;
                    MN.MakerNoteOffset = MakerNoteIFDOffset;
                    MN.IsLittleEndian = IsLittleEndian;
                    MN.Run();
                    //ErrMsg = MN.Msg + Environment.NewLine + "MakerNoteList.Count=" + MN.MakerNoteList.Count.ToString();
                    //ErrMsg = ExifFunc.BytesToOriString(MakerNoteData);
                    ErrMsg = MN.Msg;
                    makerExifList = MN.MakerNoteList;
                }
            }
        }

        private string GetItemValueString(IFDEntry item) {
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
        private string GetValueOfByte(IFDEntry item) {
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
        private string GetValueOfAscii(IFDEntry item) {
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
        private string GetValueOfNum(IFDEntry item) {
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
        private string GetValueOfUndefined(IFDEntry item) {
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
        private string GetValueOfRational(IFDEntry item) {
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
        private string GetSpecialTagValue(int tag, object args) {
            string ret = string.Empty;
            switch (tag) {
                case 0x102: { //BitsPerSample, SHORT(2byte) int[]
                        for (int i = 0; i < ((int[])args).Length; i++) {
                            ret += ((int[])args)[i].ToString() + ", ";
                        }
                        ret = ret.Substring(0, ret.Length - 2);
                        break;
                    }
                case 0x103: { //Compression
                        switch (((int[])args)[0]) {
                            case 1: ret = "Uncompressed"; break;
                            case 2: ret = "CCITT 1D"; break;
                            case 3: ret = "T4/Group 3 Fax"; break;
                            case 4: ret = "T6/Group 4 Fax"; break;
                            case 5: ret = "LZW"; break;
                            case 6: ret = "JPEG (old-style)"; break;
                            case 7: ret = "JPEG"; break;
                            case 8: ret = "Adobe Deflate"; break;
                            case 9: ret = "JBIG B&W"; break;
                            case 10: ret = "JBIG Color"; break;
                            case 99: ret = "JPEG"; break;
                            case 262: ret = "Kodak 262"; break;
                            case 32766: ret = "Next"; break;
                            case 32767: ret = "Sony ARW Compressed"; break;
                            case 32769: ret = "Packed RAW"; break;
                            case 32770: ret = "Samsung SRW Compressed"; break;
                            case 32771: ret = "CCIRLEW"; break;
                            case 32773: ret = "PackBits"; break;
                            case 32809: ret = "Thunderscan"; break;
                            case 32867: ret = "Kodak KDC Compressed"; break;
                            case 32895: ret = "IT8CTPAD"; break;
                            case 32896: ret = "IT8LW"; break;
                            case 32897: ret = "IT8MP"; break;
                            case 32898: ret = "IT8BL"; break;
                            case 32908: ret = "PixarFilm"; break;
                            case 32909: ret = "PixarLog"; break;
                            case 32946: ret = "Deflate"; break;
                            case 32947: ret = "DCS"; break;
                            case 34661: ret = "JBIG"; break;
                            case 34676: ret = "SGILog"; break;
                            case 34677: ret = "SGILog24"; break;
                            case 34712: ret = "JPEG 2000"; break;
                            case 34713: ret = "Nikon NEF Compressed"; break;
                            case 34715: ret = "JBIG2 TIFF FX"; break;
                            case 34718: ret = "Microsoft Document Imaging (MDI) Binary Level Codec"; break;
                            case 34719: ret = "Microsoft Document Imaging (MDI) Progressive Transform Codec"; break;
                            case 34720: ret = "Microsoft Document Imaging (MDI) Vector"; break;
                            case 65000: ret = "Kodak DCR Compressed"; break;
                            case 65535: ret = "Pentax PEF Compressed"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x106: { //PhotometricInterpretation
                        switch (((int[])args)[0]) {
                            case 0: ret = "WhiteIsZero"; break;
                            case 1: ret = "BlackIsZero"; break;
                            case 2: ret = "RGB"; break;
                            case 3: ret = "RGB Palette"; break;
                            case 4: ret = "Transparency Mask"; break;
                            case 5: ret = "CMYK"; break;
                            case 6: ret = "YCbCr"; break;
                            case 8: ret = "CIELab"; break;
                            case 9: ret = "ICCLab"; break;
                            case 10: ret = "ITULab"; break;
                            case 32803: ret = "Color Filter Array"; break;
                            case 32844: ret = "Pixar LogL"; break;
                            case 32845: ret = "Pixar LogLuv"; break;
                            case 34892: ret = "Linear Raw"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x107: { //Thresholding
                        switch (((int[])args)[0]) {
                            case 1: ret = "No dithering or halftoning"; break;
                            case 2: ret = "Ordered dither or halftone"; break;
                            case 3: ret = "Randomized dither"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x10A: { //FillOrder
                        switch (((int[])args)[0]) {
                            case 1: ret = "Normal"; break;
                            case 2: ret = "Reversed"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x112: { //Orientation
                        switch (((int[])args)[0]) {
                            case 1: ret = "Horizontal (normal)"; break;
                            case 2: ret = "Mirror horizontal"; break;
                            case 3: ret = "Rotate 180"; break;
                            case 4: ret = "Mirror vertical"; break;
                            case 5: ret = "Mirror horizontal and rotate 270 CW"; break;
                            case 6: ret = "Rotate 90 CW"; break;
                            case 7: ret = "Mirror horizontal and rotate 90 CW"; break;
                            case 8: ret = "Rotate 270 CW"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x11A:
                case 0x11B: { //XResolution & YResolution 
                        int[][] val = (int[][])args;
                        ret = ((val[0][0]) / (val[0][1])).ToString();
                        break;
                    }
                case 0x11C: { //PlanarConfiguration
                        switch (((int[])args)[0]) {
                            case 1: ret = "Chunky"; break;
                            case 2: ret = "Planar"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x122: { //GrayResponseUnit
                        switch (((int[])args)[0]) {
                            case 1: ret = "0.1"; break;
                            case 2: ret = "0.001"; break;
                            case 3: ret = "0.0001"; break;
                            case 4: ret = "1e-05"; break;
                            case 5: ret = "1e-06"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x128: { //ResolutionUnit
                        switch (((int[])args)[0]) {
                            case 1: ret = "None"; break;
                            case 2: ret = "inches"; break;
                            case 3: ret = "cm"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x13D: { //Predictor
                        switch (((int[])args)[0]) {
                            case 1: ret = "None"; break;
                            case 2: ret = "Horizontal differencing"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x14C: { //InkSet
                        switch (((int[])args)[0]) {
                            case 1: ret = "CMYK"; break;
                            case 2: ret = "Not CMYK"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x212: { //YCbCrSubSampling
                        int[] val = (int[])args;
                        string temp;
                        temp = val[0].ToString() + "-" + val[1].ToString();
                        switch (temp) {
                            case "1-1": ret = "YCbCr4:4:4"; break;
                            case "1-2": ret = "YCbCr4:4:0"; break;
                            case "1-4": ret = "YCbCr4:4:1"; break;
                            case "2-1": ret = "YCbCr4:2:2"; break;
                            case "2-2": ret = "YCbCr4:2:0"; break;
                            case "2-4": ret = "YCbCr4:2:1"; break;
                            case "4-1": ret = "YCbCr4:1:1"; break;
                            case "4-2": ret = "YCbCr4:1:0"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x213: { //YCbCrPositioning
                        switch (((int[])args)[0]) {
                            case 1: ret = "Centered"; break;
                            case 2: ret = "Co-sited"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x214: { //ReferenceBlackWhite
                        int[][] val = (int[][])args;
                        for (int i = 0; i < val.Length; i++) {
                            for (int j = 0; j < val[i].Length; j++) {
                                ret += (val[i][j]).ToString() + ", ";
                            }
                        }
                        ret = ret.Substring(0, ret.Length - 2);
                        break;
                    }
                case 0x8298: { //Copyright
                        string val = (string)args;
                        try {
                            if (val.Substring(0, 1) == " ") { //Only Editor copyright
                                ret = val.Replace(Encoding.ASCII.GetString(new byte[] { 0x20, 0x00 }), "");
                            }
                            else {
                                string[] cr = val.Split(Encoding.ASCII.GetChars(new byte[] { 0x00 }));
                                if (cr.Length > 1) {
                                    ret = cr[0] + Environment.NewLine;
                                    ret += cr[1];
                                }
                                else {
                                    ret = cr[0];
                                }
                            }
                        }
                        catch {
                            ret = string.Empty;
                        }
                        break;
                    }
                /*-----------------------------
                 *          Exif IFD
                 * ----------------------------
                 */
                case 0x829A: { //ExposureTime
                        int[][] val = (int[][])args;
                        if (val[0][0] / val[0][1] > 1) { ret = (val[0][0] / val[0][1]).ToString(); }
                        else { ret = string.Format("{0}/{1}", val[0][0], val[0][1]); }
                        break;
                    }
                case 0x829D: { //FNumber
                        int[][] val = (int[][])args;
                        ret = string.Format("{0:0.0}", (float)val[0][0] / (float)val[0][1]);
                        break;
                    }
                case 0x8822: { //ExposureProgram
                        switch (((int[])args)[0]) {
                            case 0: ret = "Not Defined"; break;
                            case 1: ret = "Manual"; break;
                            case 2: ret = "Program AE"; break;
                            case 3: ret = "Aperture-priority AE"; break;
                            case 4: ret = "Shutter speed priority AE"; break;
                            case 5: ret = "Creative (Slow speed)"; break;
                            case 6: ret = "Action (High speed)"; break;
                            case 7: ret = "Portrait"; break;
                            case 8: ret = "Landscape"; break;
                            case 9: ret = "Bulb"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x8830: { //SensitivityType
                        switch (((int[])args)[0]) {
                            case 0: ret = "Unknown"; break;
                            case 1: ret = "Standard Output Sensitivity"; break;
                            case 2: ret = "Recommended Exposure Index"; break;
                            case 3: ret = "ISO Speed"; break;
                            case 4: ret = "Standard Output Sensitivity and Recommended Exposure Index"; break;
                            case 5: ret = "Standard Output Sensitivity and ISO Speed"; break;
                            case 6: ret = "Recommended Exposure Index and ISO Speed"; break;
                            case 7: ret = "Standard Output Sensitivity, Recommended Exposure Index and ISO Speed"; break;
                            default: ret = "reserved"; break;
                        }
                        break;
                    }
                case 0x9101: { //ComponentsConfiguration
                        for (int i = 0; i < ((byte[])args).Length; i++) {
                            switch (((byte[])args)[i]) {
                                case 0: ret += " "; break;
                                case 1: ret += "Y"; break;
                                case 2: ret += "Cb"; break;
                                case 3: ret += "Cr"; break;
                                case 4: ret += "R"; break;
                                case 5: ret += "G"; break;
                                case 6: ret += "B"; break;
                                default: ret += "Unknown"; break;
                            }
                            ret += ", ";
                        }
                        ret = ret.Substring(0, ret.Length - 2);
                        break;
                    }
                case 0x9102: { //CompressedBitsPerPixel
                        int[][] val = (int[][])args;
                        ret = ((float)val[0][0] / (float)val[0][1]).ToString();
                        break;
                    }

                case 0x9201: { //ShutterSpeedValue
                        int[][] val = (int[][])args;
                        ret = ExifFunc.GetAPEXExposureTime(val[0][0], val[0][1]);
                        break;
                    }

                case 0x9202:
                case 0x9205: { //ApertureValue & MaxApertureValue
                        int[][] val = (int[][])args;
                        ret = ExifFunc.GetAPEXFNumber(val[0][0], val[0][1]);
                        break;
                    }
                case 0x9203:
                case 0x9204: { //BrightnessValue & ExposureBiasValue
                        /*
                         * 直接顯示即可，該值有分正負，表示+EV或-EV
                         */
                        int[][] val = (int[][])args;
                        if ((float)val[0][0] / (float)val[0][1] == 0) { ret = "0"; }
                        else { ret = string.Format("{0:0.0}", (float)val[0][0] / (float)val[0][1]); }
                        break;
                    }
                case 0x9206: { //SubjectDistance
                        int[][] val = (int[][])args;
                        if (val[0][0] == 0xFFFF && val[0][1] == 0xFFFF) { ret = "Infinity"; }
                        else if (val[0][0] == 0 && val[0][1] == 0) { ret = "Unknown"; }
                        else { ret = string.Format("{0:0.00}", (float)val[0][0] / (float)val[0][1]) + " m"; }
                        break;
                    }
                case 0x9207: { //MeteringMode
                        switch (((int[])args)[0]) {
                            case 0: ret = "Unknown"; break;
                            case 1: ret = "Average"; break;
                            case 2: ret = "Center-weighted average"; break;
                            case 3: ret = "Spot"; break;
                            case 4: ret = "Multi-spot"; break;
                            case 5: ret = "Multi-segment"; break;
                            case 6: ret = "Partial"; break;
                            case 255: ret = "Other"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x9208: { //LightSource
                        switch (((int[])args)[0]) {
                            case 0: ret = "Unknown"; break;
                            case 1: ret = "Daylight"; break;
                            case 2: ret = "Fluorescent"; break;
                            case 3: ret = "Tungsten (Incandescent)"; break;
                            case 4: ret = "Flash"; break;
                            case 9: ret = "Fine weather"; break;
                            case 10: ret = "Cloudy"; break;
                            case 11: ret = "Shade"; break;
                            case 12: ret = "Daylight Fluorescent"; break;
                            case 13: ret = "Day White Fluorescent"; break;
                            case 14: ret = "Cool White Fluorescent"; break;
                            case 15: ret = "White Fluorescent"; break;
                            case 16: ret = "Warm White Fluorescent"; break;
                            case 17: ret = "Standard light A"; break;
                            case 18: ret = "Standard light B"; break;
                            case 19: ret = "Standard light C"; break;
                            case 20: ret = "D55"; break;
                            case 21: ret = "D65"; break;
                            case 22: ret = "D75"; break;
                            case 23: ret = "D50"; break;
                            case 24: ret = "ISO Studio Tungsten"; break;
                            case 255: ret = "Other"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x9209: { //Flash
                        switch (((int[])args)[0]) {
                            case 0x0: ret = "No Flash"; break;
                            case 0x1: ret = "Fired"; break;
                            case 0x5: ret = "Fired, Return not detected"; break;
                            case 0x7: ret = "Fired, Return detected"; break;
                            case 0x8: ret = "On, Did not fire"; break;
                            case 0x9: ret = "On, Fired"; break;
                            case 0xD: ret = "On, Return not detected"; break;
                            case 0xF: ret = "On, Return detected"; break;
                            case 0x10: ret = "Off, Did not fire"; break;
                            case 0x14: ret = "Off, Did not fire, Return not detected"; break;
                            case 0x18: ret = "Auto, Did not fire"; break;
                            case 0x19: ret = "Auto, Fired"; break;
                            case 0x1D: ret = "Auto, Fired, Return not detected"; break;
                            case 0x1F: ret = "Auto, Fired, Return detected"; break;
                            case 0x20: ret = "No flash function"; break;
                            case 0x30: ret = "Off, No flash function"; break;
                            case 0x41: ret = "Fired, Red-eye reduction"; break;
                            case 0x45: ret = "Fired, Red-eye reduction, Return not detected"; break;
                            case 0x47: ret = "Fired, Red-eye reduction, Return detected"; break;
                            case 0x49: ret = "On, Red-eye reduction"; break;
                            case 0x4D: ret = "On, Red-eye reduction, Return not detected"; break;
                            case 0x4F: ret = "On, Red-eye reduction, Return detected"; break;
                            case 0x50: ret = "Off, Red-eye reduction"; break;
                            case 0x58: ret = "Auto, Did not fire, Red-eye reduction"; break;
                            case 0x59: ret = "Auto, Fired, Red-eye reduction"; break;
                            case 0x5D: ret = "Auto, Fired, Red-eye reduction, Return not detected"; break;
                            case 0x5F: ret = "Auto, Fired, Red-eye reduction, Return detected"; break;
                        }
                        break;
                    }
                case 0x920A: { //FocalLength
                        int[][] val = (int[][])args;
                        ret = string.Format("{0:0.0}", ((float)(val[0][0])) / ((float)(val[0][1]))) + " mm";
                        break;
                    }
                case 0x9210: { //FocalPlaneResolutionUnit
                        switch (((int[])args)[0]) {
                            case 1: ret = "None"; break;
                            case 2: ret = "inches"; break;
                            case 3: ret = "cm"; break;
                            case 4: ret = "mm"; break;
                            case 5: ret = "um"; break;
                            default: ret = "unknow"; break;
                        }
                        break;
                    }
                case 0x9212: { //SecurityClassification
                        switch (((string)args).ToUpper()) {
                            case "C": ret = "Confidential"; break;
                            case "R": ret = "Restricted"; break;
                            case "S": ret = "Secret"; break;
                            case "T": ret = "Top Secret"; break;
                            case "U": ret = "Unclassified"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x9217: { //SensingMethod
                        switch (((int[])args)[0]) {
                            case 1: ret = "Monochrome area"; break;
                            case 2: ret = "One-chip color area"; break;
                            case 3: ret = "Two-chip color area"; break;
                            case 4: ret = "Three-chip color area"; break;
                            case 5: ret = "Color sequential area"; break;
                            case 6: ret = "Monochrome linear"; break;
                            case 7: ret = "Trilinear"; break;
                            case 8: ret = "Color sequential linear"; break;
                            default: ret = "unknow"; break;
                        }
                        break;
                    }
                case 0xA001: { //ColorSpace
                        switch (((int[])args)[0]) {
                            case 0x1: ret = "sRGB"; break;
                            case 0x2: ret = "Adobe RGB"; break;
                            case 0xFFFD: ret = "Wide Gamut RGB"; break;
                            case 0xFFFE: ret = "ICC Profile"; break;
                            case 0xFFFF: ret = "Uncalibrated"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA20B: { //FlashEnergy
                        int[][] val = (int[][])args;
                        ret = string.Format("{0:0.0}", (float)val[0][0] / (float)val[0][1]);
                        break;
                    }
                case 0xA20E:
                case 0xA20F: { //FocalPlaneXResolution & FocalPlaneYResolution
                        int[][] val = (int[][])args;
                        ret = ((float)(val[0][0]) / (float)(val[0][1])).ToString();
                        break;
                    }
                case 0xA210: { //FocalPlaneResolutionUnit
                        switch (((int[])args)[0]) {
                            case 1: ret = "None"; break;
                            case 2: ret = "inches"; break;
                            case 3: ret = "cm"; break;
                            case 4: ret = "mm"; break;
                            case 5: ret = "um"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA217: { //SensingMethod
                        switch (((int[])args)[0]) {
                            case 1: ret = "Not defined"; break;
                            case 2: ret = "One-chip color area"; break;
                            case 3: ret = "Two-chip color area"; break;
                            case 4: ret = "Three-chip color area"; break;
                            case 5: ret = "Color sequential area"; break;
                            case 7: ret = "Trilinear"; break;
                            case 8: ret = "Color sequential linear"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA300: { //FileSource
                        switch (((byte[])args)[0]) {
                            case 1: ret = "Film Scanner"; break;
                            case 2: ret = "Reflection Print Scanner"; break;
                            case 3: ret = "Digital Camera"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA301: { //SceneType
                        switch (((byte[])args)[0]) {
                            case 1: ret = "Directly photographed"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA401: { //CustomRendered
                        switch (((int[])args)[0]) {
                            case 0: ret = "Normal"; break;
                            case 1: ret = "Custom"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA402: { //ExposureMode
                        switch (((int[])args)[0]) {
                            case 0: ret = "Auto"; break;
                            case 1: ret = "Manual"; break;
                            case 2: ret = "Auto bracket"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA403: { //WhiteBalance
                        switch (((int[])args)[0]) {
                            case 0: ret = "Auto"; break;
                            case 1: ret = "Manual"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA404: { //DigitalZoomRatio
                        int[][] val = (int[][])args;
                        ret = string.Format("{0:0.0}", ((float)val[0][0]) / ((float)val[0][1]));
                        break;
                    }
                case 0xA406: { //SceneCaptureType
                        switch (((int[])args)[0]) {
                            case 0: ret = "Standard"; break;
                            case 1: ret = "Landscape"; break;
                            case 2: ret = "Portrait"; break;
                            case 3: ret = "Night"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA407: { //GainControl
                        switch (((int[])args)[0]) {
                            case 0: ret = "None"; break;
                            case 1: ret = "Low gain up"; break;
                            case 2: ret = "High gain up"; break;
                            case 3: ret = "Low gain down"; break;
                            case 4: ret = "High gain down"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA408:
                case 0xA409: { //Contrast Saturation 
                        switch (((int[])args)[0]) {
                            case 0: ret = "Normal"; break;
                            case 1: ret = "Low"; break;
                            case 2: ret = "High"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA40A: { //Sharpness
                        switch (((int[])args)[0]) {
                            case 0: ret = "Normal"; break;
                            case 1: ret = "Soft"; break;
                            case 2: ret = "Hard"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA40C: { //SubjectDistanceRange
                        switch (((int[])args)[0]) {
                            case 0: ret = "Unknown"; break;
                            case 1: ret = "Macro"; break;
                            case 2: ret = "Close"; break;
                            case 3: ret = "Distant"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA431: { //body Serial Number 
                        //ulong val = ulong.Parse((string)args);
                        //ret = val.ToString();
                        break;
                    }
                case 0xA432: { //LensInfo
                        //前兩個為焦段，後兩個為最大光圈
                        //每組數據一樣則表示定焦鏡
                        int[][] val = (int[][])args;
                        if (val[0][0] / val[0][1] == val[1][0] / val[1][1]) {
                            ret = (val[0][0] / val[0][1]).ToString() + "mm, f:";
                            if (val[2][0] != 0 && val[2][1] != 0) {
                                ret += string.Format("{0:0.0}", ((float)val[2][0] / (float)val[2][1])) + ", ";
                            }
                            else { ret += "?, "; }
                        }
                        else {
                            ret = (val[0][0] / val[0][1]).ToString() + "-" + (val[1][0] / val[1][1]).ToString() + "mm, f:";
                            if (val[2][0] != 0 && val[2][1] != 0) {
                                ret += string.Format("{0:0.0}", ((float)val[2][0] / (float)val[2][1])) + " - ";
                            }
                            //else { ret += "? - "; }
                            if (val[3][0] != 0 && val[3][1] != 0) {
                                ret += string.Format("{0:0.0}", ((float)val[3][0] / (float)val[3][1])) + ", ";
                            }
                            else { ret += "?, "; }
                        }
                        ret = ret.Substring(0, ret.Length - 2);
                        break;
                    }

                /*
                 * GPS
                 * 
                 */
                case 0x0:
                    hadGPS = true;
                    break;
                case 0x1:
                case 0x13: { //GPSLatitudeRef, GPSDestLatitudeRef 
                        switch ((string)args) {
                            case "N":
                                ret = "North";
                                break;
                            case "S":
                                ret = "South";
                                latitude = "-";
                                break;
                            default:
                                ret = "Unknown";
                                break;
                        }
                        break;
                    }
                case 0x3:
                case 0x15: { //GPSLongitudeRef, GPSDestLongitudeRef 
                        switch ((string)args) {
                            case "E":
                                ret = "East";
                                break;
                            case "W":
                                ret = "West";
                                longitude = "-";
                                break;
                            default:
                                ret = "Unknown";
                                break;
                        }
                        break;
                    }
                case 0x2: {//Latitude
                        int[][] val = (int[][])args;
                        if (val[2][0] == 0) { //使用 度:分
                            latitude += ((val[0][0] / val[0][1]) +
                                         (float)((float)val[1][0] / (float)val[1][1]) / 60).ToString();
                        }
                        else { //使用度:分:秒
                            latitude += ((val[0][0] / val[0][1]) +
                                           (float)((float)val[1][0] / (float)val[1][1]) / 60 +
                                           (float)((float)val[2][0] / (float)val[2][1]) / 3600).ToString();

                        }
                        goto case 0x16;
                    }
                case 0x4: { //Longitude
                        int[][] val = (int[][])args;
                        if (val[2][0] == 0) { //使用 度:分
                            longitude += ((val[0][0] / val[0][1]) +
                                         (float)((float)val[1][0] / (float)val[1][1]) / 60).ToString();
                        }
                        else { //使用度:分:秒
                            longitude += ((val[0][0] / val[0][1]) +
                                           (float)((float)val[1][0] / (float)val[1][1]) / 60 +
                                           (float)((float)val[2][0] / (float)val[2][1]) / 3600).ToString();

                        }
                        goto case 0x16;
                    }
                case 0x14:
                case 0x16: { //GPSLatitude, GPSLongitude, GPSDestLatitude, GPSDestLongitude
                        int[][] val = (int[][])args;
                        if (val[2][0] == 0) { //使用 度:分
                            ret = Func.FillChar((val[0][0] / val[0][1]).ToString(), 2, "0")
                                + ":"
                                + Func.FillChar(((float)((float)val[1][0] / (float)val[1][1])).ToString(), 2, "0");
                        }
                        else { //使用度:分:秒
                            ret = Func.FillChar((val[0][0] / val[0][1]).ToString(), 2, "0")
                                + ":"
                                + Func.FillChar((val[1][0] / val[1][1]).ToString(), 2, "0")
                                + ":"
                                + Func.FillChar(((float)((float)val[2][0] / (float)val[2][1])).ToString(), 2, "0");

                        }
                        break;
                    }
                case 0x5: { //GPSAltitudeRef
                        switch (((byte[])args)[0]) {
                            case 0: ret = "Sea level"; break;
                            case 1: ret = "Sea level reference"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0x6: { //GPSAltitude
                        int[][] val = (int[][])args;
                        ret = (val[0][0] == 0) ? "0" : string.Format("{0:0.00}", (val[0][0] / val[0][1]));
                        ret += " m";
                        break;
                    }
                case 0x7: { //GPSTimeStamp
                        int[][] val = (int[][])args;
                        ret = Func.FillChar((val[0][0] / val[0][1]).ToString(), 2, "0")
                                    + ":"
                                    + Func.FillChar((val[1][0] / val[1][1]).ToString(), 2, "0")
                                    + ":"
                                    + Func.FillChar(((val[2][0] / val[2][1])).ToString(), 2, "0");
                        break;
                    }
                case 0x9: { //GPSStatus
                        switch (((string)args).ToUpper()) {
                            case "A": ret = "Measurement in progress"; break;
                            case "V": ret = "Measurement Interoperability"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xA: { //GPSMeasureMode
                        switch ((string)args) {
                            case "2": ret = "2-dimensional"; break;
                            case "3": ret = "3-dimensional"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xC:
                case 0x19: { //GPSSpeedRef, GPSDestDistanceRef
                        switch (((string)args).ToUpper()) {
                            case "K": ret = "Kilometers Per Hour"; break;
                            case "M": ret = "Miles Per Hour"; break;
                            case "N": ret = "Knots"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xE:
                case 0x10:
                case 0x17: { //GPSTrackRef, GPSImgDirectionRef, GPSDestBearingRef
                        switch (((string)args).ToUpper()) {
                            case "T": ret = "True direction"; break;
                            case "M": ret = "Magnetic direction"; break;
                            default: ret = "Unknown"; break;
                        }
                        break;
                    }
                case 0xF:
                case 0x11:
                case 0x18: { //GPSTrack, GPSImgDirection, GPSDestBearing
                        int[][] val = (int[][])args;
                        ret = string.Format("{0:0.00}", val[0][0] / val[0][1]);
                        break;
                    }
                case 0x1E: { //GPSDifferential 
                        switch (((int[])args)[0]) {
                            case 0: ret = "Measurement without differential correction"; break;
                            case 1: ret = "Differential correction applied"; break;
                        }
                        break;
                    }
                default:
                    break;
            }
            return ret;
        }

    }
}
