using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理ModifiedInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseModifiedInfoValue(int tag, int value) {
            string ret = string.Empty;
            short v = IntToShort(value);
            switch (tag) {
                case 1: { //ModifiedToneCurve
                        switch (v) {
                            case 0: ret = "Standard"; break;
                            case 1: ret = "Manual"; break;
                            case 2: ret = "Custom"; break;
                        }
                        break;
                    }
                case 2: //ModifiedSharpness
                case 4: //ModifiedSensorRedLevel
                case 5: //ModifiedSensorBlueLevel
                case 6: //ModifiedWhiteBalanceRed
                case 7: //ModifiedWhiteBalanceBlue
                case 9: //ModifiedColorTemp
                    ret = v.ToString();
                    break;
                case 3: {//ModifiedSharpnessFreq
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
                case 8: //ModifiedWhiteBalance
                    ret = ExifFunc.GetWhiteBalanceName(v);
                    break;
                case 10: //ModifiedPictureStyle
                    ret = ExifFunc.GetPictureStyleName(v);
                    break;
                case 11: //ModifiedDigitalGain
                    if (v == 0) { ret = "0x"; }
                    else { ret = string.Format("{0:0.0}", (float)v / 10) + "x"; }
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 處理SensorInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseSensorInfoValue(int tag, int value) {
            short v = IntToShort(value);
            return v.ToString();
        }
    }
}
