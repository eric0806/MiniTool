using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理ProcessingInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseProcessingInfoValue(int tag, int value) {
            string ret = string.Empty;
            //value = IntToShort(value);
            short v = IntToShort(value);
            switch (tag) {
                case 1: { //ToneCurve
                        switch (v) {
                            case 0: ret = "Standard"; break;
                            case 1: ret = "Manual"; break;
                            case 2: ret = "Custom"; break;
                        }
                        break;
                    }
                case 2: //Sharpness
                case 4: //SensorRedLevel
                case 5: //SensorBlueLevel
                case 6: //WhiteBalanceRed
                case 7: //WhiteBalanceBlue
                case 9: //ColorTemperature
                case 12: //WBShiftAB
                case 13: //WBShiftGM
                    ret = v.ToString();
                    break;
                case 3: { //SharpnessFrequency
                        switch (v) {
                            case 0: ret = "n/a"; break;
                            case 1: ret = "Lowest"; break;
                            case 2: ret = "Low"; break;
                            case 3: ret = "Standard"; break;
                            case 4: ret = "High"; break;
                            case 5: ret = "Highest"; break;
                        }
                        break;
                    }
                case 8: { //WhiteBalance
                        ret = ExifFunc.GetWhiteBalanceName(v);
                        break;
                    }
                case 10: {//PictureStyle
                        ret = ExifFunc.GetPictureStyleName(v);
                        break;
                    }
                case 11: { //DigitalGain
                        if (v == 0) { ret = "0x"; }
                        else { ret = string.Format("{0:0.0}", (float)v / 10) + "x"; }
                        break;
                    }
            }
            return ret;
        }
    }
}
