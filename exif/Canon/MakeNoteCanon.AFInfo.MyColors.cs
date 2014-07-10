using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理AFInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseAFInfoValue(int tag, int value) {
            string ret = string.Empty;
            switch (tag) {
                case 0:  //NumAFPoints
                case 1: //ValidAFPoints
                case 2: //Image Width
                case 3: //Image Height
                case 4: //Image Width As Shot
                case 5: //Image Height As Shot
                case 6: //AF Area Width
                case 7: //AF Area Height
                case 11: //Primary AF Point
                case 12: //Primary AF Point
                    ret = IntToUShort(value).ToString();
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 處理AFInfo2裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseAFInfo2Value(int tag, int value) {
            string ret = string.Empty;
            switch (tag) {
                case 0: { //AF Info Size
                        ushort v = IntToUShort(value);
                        ret = (v / 2).ToString() + " Fields";
                        break;
                    }
                case 1: { //AF Mode
                        ushort v = IntToUShort(value);
                        switch (v) {
                            case 0: ret = "Off (Manual Focus)"; break;
                            case 2: ret = "Single-point AF"; break;
                            case 4: ret = "Multi-point AF or AI AF"; break;
                            case 5: ret = "Face Detect AF"; break;
                            case 7: ret = "Zone AF"; break;
                            case 8: ret = "AF Point Expansion"; break;
                            case 9: ret = "Spot AF"; break;
                        }
                        break;
                    }
                case 2: //Num AF Points
                case 3: //Valid AF Points
                case 4: //Canon Image Width
                case 5: //Canon Image Height
                case 6: //AF Image Width
                case 7: //AF Image Height
                case 14: //Primary AF Point
                    ret = IntToUShort(value).ToString();
                    break;
            }
            return ret;
        }


        /// <summary>
        /// 處理MyColor裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseMyColorModeValue(int tag, int value) {
            string ret = string.Empty;
            switch (tag) {
                case 2: { //MyColorMode
                        switch (value) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "Positive Film"; break;
                            case 2: ret = "Light Skin Tone"; break;
                            case 3: ret = "Dark Skin Tone"; break;
                            case 4: ret = "Vivid Blue"; break;
                            case 5: ret = "Vivid Green"; break;
                            case 6: ret = "Vivid Red"; break;
                            case 7: ret = "Color Accent"; break;
                            case 8: ret = "Color Swap"; break;
                            case 9: ret = "Custom"; break;
                            case 12: ret = "Vivid"; break;
                            case 13: ret = "Neutral"; break;
                            case 14: ret = "Sepia"; break;
                            case 15: ret = "B&W"; break;
                        }
                        break;
                    }
            }
            return ret;
        }
    }
}
