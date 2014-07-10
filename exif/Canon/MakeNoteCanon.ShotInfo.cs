using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理ShotInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseShotInfoValue(int tag, int value) {
            string ret = "Unknown";
            switch (tag) {
                case 1: { //AutoISO
                        //Formula is exp( value / 32 * log(2) ) * 100)
                        ret = string.Format("{0:0}", Math.Exp((double)value / 32 * Math.Log10(2)) * 100);
                        break;
                    }
                case 2: { //BaseISO
                        if (value == 0) {
                            ret = "Auto";
                        }
                        else {
                            //exp(( Value / 32 ) * ln(2) ) * 100 / 32
                            /*
                             * 在C#的Math類別中，並沒有ln(x)的函式。
                             * 其實ln(x)就是以指數為底的log()函式啦。
                             * 所以要求ln(x)是多少的話，可以用Math.Log( x, Math.E) 來表示。
                             */
                            ret = string.Format("{0:0}", Math.Exp(((double)value / 32) * Math.Log(2, Math.E)) * 100 / 32);
                        }
                        break;
                    }
                case 3: { //Measured EV，帶正負號
                        //ret = string.Format("{0}", value / 32);
                        short v = IntToShort(value);
                        //ret = ((float)v / 32).ToString();
                        switch (v) {
                            case -52: ret = "-1.63 EV"; break;
                            case -48: ret = "-1.50 EV"; break;
                            case -44: ret = "-1.38 EV"; break;
                            case -40: ret = "-1.25 EV"; break;
                            case -2: ret = "-0.06 EV"; break;
                            case -1: ret = "-0.03 EV"; break;
                            case 0: ret = "0 EV"; break;
                            case 1: ret = "0.03 EV"; break;
                            case 8: ret = "0.25 EV"; break;
                            case 20: ret = "0.63 EV"; break;
                            case 52: ret = "1.63 EV"; break;
                            case 60: ret = "1.88 EV"; break;
                            case 254: ret = "7.94 EV"; break;
                            case 255: ret = "7.97 EV"; break;
                            case 256: ret = "8 EV"; break;
                            default: ret = string.Format("{0:0.00}", (float)((float)v / 32)) + " EV"; break;
                        }
                        //ret = "(" + v.ToString() + ")" + ret;
                        break;
                    }
                case 4: { //Target Aperture
                        #region TargetAperture過長所以摺疊
                        short v = IntToShort(value);
                        switch (v) {
                            case -32768: ret = "0"; break;
                            case -32: ret = "0.7"; break;
                            case 0: ret = "1"; break;
                            case 20: ret = "1.2"; break;
                            case 32: ret = "1.4"; break;
                            case 43:
                            case 44: ret = "1.6"; break;
                            case 52:
                            case 54: ret = "1.8"; break;
                            case 64: ret = "2"; break;
                            case 73: ret = "2.2"; break;
                            case 76: ret = "2.3"; break;
                            case 81: ret = "2.4"; break;
                            case 84:
                            case 85: ret = "2.5"; break;
                            case 88: ret = "2.6"; break;
                            case 90:
                            case 91:
                            case 92: ret = "2.7"; break;
                            case 95:
                            case 96: ret = "2.8"; break;
                            case 98: ret = "2.9"; break;
                            case 100:
                            case 101: ret = "3"; break;
                            case 104: ret = "3.1"; break;
                            //case 105:
                            case 106:
                            case 107:
                            case 108: ret = "3.2"; break;
                            case 109: ret = "3.3"; break;
                            case 113:
                            case 114: ret = "3.4"; break;
                            case 116: ret = "3.5"; break;
                            case 118: ret = "3.6"; break;
                            case 121: ret = "3.7"; break;
                            case 123:
                            case 124: ret = "3.8"; break;
                            case 125:
                            case 126: ret = "3.9"; break;
                            case 127:
                            case 128: ret = "4"; break;
                            case 130:
                            case 131: ret = "4.1"; break;
                            case 132: ret = "4.2"; break;
                            case 135: ret = "4.3"; break;
                            case 139: ret = "4.5"; break;
                            case 140: ret = "4.6"; break;
                            case 143: ret = "4.7"; break;
                            case 145: ret = "4.8"; break;
                            case 147: ret = "4.9"; break;
                            case 148:
                            case 149: ret = "5"; break;
                            case 154: ret = "5.3"; break;
                            case 156: ret = "5.4"; break;
                            case 157: ret = "5.5"; break;
                            case 159: ret = "5.6"; break;
                            case 160:
                            case 161: ret = "5.7"; break;
                            case 162: ret = "5.8"; break;
                            case 164: ret = "5.9"; break;
                            case 167: ret = "6.1"; break;
                            case 170: ret = "6.3"; break;
                            case 172: ret = "6.4"; break;
                            case 174: ret = "6.6"; break;
                            case 176: ret = "6.7"; break;
                            case 177: ret = "6.8"; break;
                            case 178:
                            case 179: ret = "6.9"; break;
                            case 180: ret = "7"; break;
                            case 181: ret = "7.1"; break;
                            case 182: ret = "7.2"; break;
                            case 183:
                            case 184: ret = "7.3"; break;
                            case 185: ret = "7.4"; break;
                            case 186: ret = "7.5"; break;
                            case 188:
                            case 189: ret = "7.7"; break;
                            case 190: ret = "7.8"; break;
                            case 191: ret = "7.9"; break;
                            case 192: ret = "8"; break;
                            case 193: ret = "8.1"; break;
                            case 195: ret = "8.3"; break;
                            case 199: ret = "8.6"; break;
                            case 203: ret = "9"; break;
                            case 204: ret = "9.1"; break;
                            case 206: ret = "9.3"; break;
                            case 208: ret = "9.5"; break;
                            case 212: ret = "9.9"; break;
                            case 213: ret = "10"; break;
                            case 214: ret = "10.2"; break;
                            case 215: ret = "10.3"; break;
                            case 221: ret = "11"; break;
                            case 222: ret = "11.1"; break;
                            case 224: ret = "11.3"; break;
                            case 236: ret = "12.9"; break;
                            case 237: ret = "13"; break;
                            case 244: ret = "14.1"; break;
                            case 256: ret = "16"; break;
                            case 267: ret = "18"; break;
                            case 268: ret = "18.2"; break;
                            case 276: ret = "19.9"; break;
                            case 285: ret = "21.9"; break;
                            case 288: ret = "22.6"; break;
                            case 297: ret = "24.9"; break;
                            case 308: ret = "28.1"; break;
                            case 311: ret = "29"; break;
                            case 320: ret = "32"; break;
                            case 331: ret = "36"; break;
                            case 32767: ret = "n/a"; break;
                            default:
                                ret = ExifFunc.GetAPEXFNumber(v, 32);
                                break;
                        }
                        //ret = "(" + v.ToString() + ")" + ret;
                        break;
                        #endregion
                    }
                case 5: { //Target Exposure Time
                        #region 快門速度，太長所以摺疊
                        short v = IntToShort(value);
                        switch (v) {
                            case -32768: ret = "n/a"; break;
                            case -160:
                            case -157: ret = "30s"; break;
                            case -149: ret = "25s"; break;
                            case -140:
                            case -138: ret = "20s"; break;
                            case -128:
                            case -125: ret = "15s"; break;
                            case -118: ret = "13s"; break;
                            case -116: ret = "12s"; break;
                            case -108:
                            case -106: ret = "10s"; break;
                            case -96: ret = "8s"; break;
                            case -83: ret = "6s"; break;
                            case -76:
                            case -74: ret = "5s"; break;
                            case -64: ret = "4s"; break;
                            case -54: ret = "3.2s"; break;
                            case -52: ret = "3.1s"; break;
                            case -51: ret = "3s"; break;
                            case -44: ret = "2.6s"; break;
                            case -42: ret = "2.5s"; break;
                            case -32: ret = "2s"; break;
                            case -22: ret = "1.6s"; break;
                            case -20:
                            case -19: ret = "1.5s"; break;
                            case -12: ret = "1.3s"; break;
                            case -1:
                            case 0: ret = "1s"; break;
                            case 10:
                            case 12: ret = "0.8s"; break;
                            case 16: ret = "0.7s"; break;
                            case 20:
                            case 24: ret = "0.6s"; break;
                            case 32: ret = "0.5s"; break;
                            case 42:
                            case 44: ret = "0.4s"; break;
                            case 52:
                            case 54:
                            case 56: ret = "0.3s"; break;
                            case 64:
                            case 65: ret = "1/4 s"; break;
                            case 72:
                            case 74:
                            case 75:
                            case 76: ret = "1/5 s"; break;
                            case 82:
                            case 83:
                            case 84: ret = "1/6 s"; break;
                            case 87:
                            case 89: ret = "1/7 s"; break;
                            case 95:
                            case 96:
                            case 97:
                            case 98: ret = "1/8 s"; break;
                            case 100:
                            case 102:
                            case 103: ret = "1/9 s"; break;
                            case 106:
                            case 107:
                            case 108: ret = "1/10 s"; break;
                            case 116:
                            case 118:
                            case 119:
                            case 120: ret = "1/13 s"; break;
                            case 122:
                            case 123:
                            case 125:
                            case 126:
                            case 128:
                            case 129:
                            case 130: ret = "1/15 s"; break;
                            case 135:
                            case 137:
                            case 138:
                            case 140: ret = "1/20 s"; break;
                            case 144:
                            case 148:
                            case 149:
                            case 151: ret = "1/25 s"; break;
                            case 154:
                            case 157:
                            case 160: ret = "1/30 s"; break;
                            case 163: ret = "1/35 s"; break;
                            case 170:
                            case 171:
                            case 172: ret = "1/40 s"; break;
                            case 175: ret = "1/45 s"; break;
                            case 179:
                            case 180:
                            case 181:
                            case 182:
                            case 184: ret = "1/50 s"; break;
                            case 188:
                            case 189:
                            case 192: ret = "1/60 s"; break;
                            case 193:
                            case 196:
                            case 198: ret = "1/70 s"; break;
                            case 200:
                            case 201:
                            case 202:
                            case 203:
                            case 204:
                            case 205: ret = "1/80 s"; break;
                            case 208:
                            case 210: ret = "1/90 s"; break;
                            case 212:
                            case 213: ret = "1/100 s"; break;
                            case 215:
                            case 216: ret = "1/110 s"; break;
                            case 220:
                            case 221: ret = "1/120 s"; break;
                            case 222:
                            case 223:
                            case 224: ret = "1/125 s"; break;
                            case 226: ret = "1/130 s"; break;
                            case 227:
                            case 229: ret = "1/140 s"; break;
                            case 230:
                            case 231:
                            case 232: ret = "1/150 s"; break;
                            case 233:
                            case 234: ret = "1/160 s"; break;
                            case 236: ret = "1/170 s"; break;
                            case 240: ret = "1/180 s"; break;
                            case 242:
                            case 243: ret = "1/190 s"; break;
                            case 244:
                            case 245:
                            case 246:
                            case 247:
                            case 248: ret = "1/200 s"; break;
                            case 252:
                            case 254:
                            case 255:
                            case 256:
                            case 257:
                            case 258: ret = "1/250 s"; break;
                            case 260:
                            case 261:
                            case 262:
                            case 263: ret = "1/300 s"; break;
                            case 266:
                            case 267:
                            case 268: ret = "1/320 s"; break;
                            case 272:
                            case 273: ret = "1/350 s"; break;
                            case 274:
                            case 275:
                            case 276:
                            case 277:
                            case 278: ret = "1/400 s"; break;
                            case 280:
                            case 281:
                            case 283:
                            case 284: ret = "1/450 s"; break;
                            case 285:
                            case 286:
                            case 287:
                            case 288:
                            case 289: ret = "1/500 s"; break;
                            case 290:
                            case 291:
                            case 292:
                            case 293: ret = "1/550 s"; break;
                            case 294:
                            case 295:
                            case 296: ret = "1/600 s"; break;
                            case 298: ret = "1/640 s"; break;
                            case 300: ret = "1/650 s"; break;
                            case 301:
                            case 303:
                            case 304: ret = "1/700 s"; break;
                            case 305:
                            case 307: ret = "1/750 s"; break;
                            case 308:
                            case 309: ret = "1/800 s"; break;
                            case 319:
                            case 320: ret = "1/1000 s"; break;
                            case 329: ret = "1/1250 s"; break;
                            case 332: ret = "1/1300 s"; break;
                            case 338: ret = "1/1500 s"; break;
                            case 340:
                            case 341: ret = "1/1600 s"; break;
                            case 351:
                            case 352: ret = "1/2000 s"; break;
                            case 361: ret = "1/2500 s"; break;
                            case 364: ret = "1/2600 s"; break;
                            case 372:
                            case 373: ret = "1/3200 s"; break;
                            case 383:
                            case 384: ret = "1/4000 s"; break;
                            case 396: ret = "1/5400 s"; break;
                            case 402: ret = "1/6000 s"; break;
                            case 404: ret = "1/6400 s"; break;
                            case 415:
                            case 416: ret = "1/8000 s"; break;
                            default: {
                                    ret = ExifFunc.GetAPEXExposureTime(v, 32) + "s";
                                    break;
                                }
                        }
                        #endregion
                        //ret = "(" + v.ToString() + ")" + ret;
                        break;
                    }
                case 6: { //Exposure Compensation
                        //Formula is " value / 32 "
                        short v = IntToShort(value);
                        switch (v) {
                            case -64: ret = "-2 EV"; break;
                            case -52: ret = "-1 2/3 EV"; break;
                            case -44:
                            case -43: ret = "-1 1/3 EV"; break;
                            case -40: ret = "-1 1/4 EV"; break;
                            case -32: ret = "-1 EV"; break;
                            case -21:
                            case -20: ret = "-2/3 EV"; break;
                            case -16: ret = "-1/2 EV"; break;
                            case -12: ret = "-1/3 EV"; break;
                            case -8: ret = "-1/4 EV"; break;
                            case 0: ret = "0 EV"; break;
                            case 10:
                            case 12: ret = "+1/3 EV"; break;
                            case 16: ret = "+1/2 EV"; break;
                            case 20:
                            case 21: ret = "+2/3 EV"; break;
                            case 32: ret = "+1 EV"; break;
                            case 44: ret = "+1 1/3 EV"; break;
                            case 52: ret = "+1 2/3 EV"; break;
                            case 64: ret = "+2 EV"; break;
                            default:
                                ret = FloatToFraction((double)v / 32) + " EV";
                                break;
                        }
                        break;
                    }
                case 7: { //WhiteBalance
                        ushort v = IntToUShort(value);
                        ret = ExifFunc.GetWhiteBalanceName(v);
                        break;
                    }
                case 8: { //SlowShutter
                        ushort v = IntToUShort(value);
                        switch (v) {
                            case 0x0000: ret = "Off"; break;
                            case 0x0001:
                            case 0x0100: ret = "Night Scene"; break;
                            case 0x0002:
                            case 0x0200: ret = "On"; break;
                            case 0x0003:
                            case 0x0300: ret = "None"; break;
                            case 0xffff: ret = "n/a"; break;
                        }
                        break;
                    }
                case 9: //Sequence Number
                case 10:  // OpticalZoomCode
                case 11: { //Optical Zoom Step
                        ret = value.ToString();
                        break;
                    }
                case 12: { //CameraTemperature
                        ushort v = IntToUShort(value);
                        ret = v.ToString();
                        break;
                    }
                case 13: { //FlashGuideNumber
                        ushort v = IntToUShort(value);
                        if (v == 0xffff) { ret = "n/a"; }
                        else { ret = string.Format("{0:0.00}", ((float)v / 32)); }
                        break;
                    }
                case 14: {//AFPointsInFocus
                        switch (value) {
                            case 0: ret = "n/a"; break;
                            case 0x3000: ret = "None (MF)"; break;
                            case 0x3001: ret = "Right"; break;
                            case 0x3002: ret = "Center"; break;
                            case 0x3003: ret = "Center+Right"; break;
                            case 0x3004: ret = "Left"; break;
                            case 0x3005: ret = "Left+Right"; break;
                            case 0x3006: ret = "Left+Center"; break;
                            case 0x3007: ret = "All"; break;
                        }
                        break;
                    }
                case 15: { //FlashExposureComp
                        short v = IntToShort(value);
                        switch (v) {
                            case 0: ret = "0 EV"; break;
                            case 10:
                            case 12: ret = "+1/3 EV"; break;
                            case 20:
                            case 21: ret = "+2/3 EV"; break;
                            case 32: ret = "+1 EV"; break;
                            case 42: ret = "+1 1/3 EV"; break;
                            case 52:
                            case 53: ret = "+1 2/3 EV"; break;
                            case 64: ret = "+2 EV"; break;
                            case -64: ret = "-2 EV"; break;
                            case -53:
                            case -52: ret = "-1 2/3 EV"; break;
                            case -42: ret = "-1 1/3 EV"; break;
                            case -32: ret = "-1 EV"; break;
                            case -21:
                            case -20: ret = "-2/3 EV"; break;
                            case -12:
                            case -10: ret = "-1/3 EV"; break;
                            default: ret = string.Format("{0:0.0}", (float)((float)v / 32)) + " EV"; break;
                        }
                        break;
                    }
                case 16: { //AutoExposureBracketing
                        ushort v = IntToUShort(value);
                        switch (v) {
                            case 0xffff: ret = "On"; break;
                            case 0: ret = "Off"; break;
                            case 1: ret = "On (shot 1)"; break;
                            case 2: ret = "On (shot 2)"; break;
                            case 3: ret = "On (shot 3)"; break;
                        }
                        break;
                    }
                case 17: { //AEB Bracket Value
                        short v = IntToShort(value);
                        switch (v) {
                            case 0: ret = "0 EV"; break;
                            case 8: ret = "+1/4 EV"; break;
                            case 12: ret = "+1/3 EV"; break;
                            case 16: ret = "+1/2 EV"; break;
                            case 20: ret = "+2/3 EV"; break;
                            case 32: ret = "+1 EV"; break;
                            case 44: ret = "+1 1/3 EV"; break;
                            case 52: ret = "+1 2/3 EV"; break;
                            case 64: ret = "+2 EV"; break;
                            case -64: ret = "-2 EV"; break;
                            case -52: ret = "-1 2/3 EV"; break;
                            case -44: ret = "-1 1/3 EV"; break;
                            case -32: ret = "-1 EV"; break;
                            case -20: ret = "-2/3 EV"; break;
                            case -16: ret = "-1/2 EV"; break;
                            case -12: ret = "-1/3 EV"; break;
                            case -8: ret = "-1/4 EV"; break;
                            default: ret = string.Format("{0:0.0}", (float)((float)v / 32)) + " EV"; break;
                        }
                        break;
                    }
                case 18: { //ControlMode
                        switch (value) {
                            case 0: ret = "n/a"; break;
                            case 1:
                            case 2: ret = "Camera Local Control"; break;
                            case 3:
                            case 4: ret = "Computer Remote Control"; break;
                        }
                        break;
                    }
                case 19: //FocusDistanceUpper
                case 20: { //FocusDistanceLower
                        ushort v = IntToUShort(value);
                        if (v >= 100) { //m
                            ret = string.Format("{0:0.00}", (float)v / 100) + " m";
                        }
                        else {
                            ret = v.ToString() + " cm";
                        }
                        break;
                    }
                case 21: {//FNumber
                        goto case 4;
                    }
                case 22: { //Exposure Time
                        goto case 5;
                    }
                case 23: { //MeasuredEV2
                        short v = IntToShort(value);
                        ret = v.ToString();
                        break;
                    }
                case 24: { //Bulb Duration
                        ushort v = IntToUShort(value);
                        ret = string.Format("{0:0}", (float)v / 10);
                        break;
                    }
                case 26: { //CameraType
                        switch (value) {
                            case 0: ret = "n/a"; break;
                            case 248: ret = "EOS High-end"; break;
                            case 250: ret = "Compact"; break;
                            case 252: ret = "EOS Mid-range"; break;
                            case 255: ret = "DV Camera"; break;
                        }
                        break;
                    }
                case 27: { //AutoRotate
                        short v = short.Parse(string.Format("{0:X}", value), System.Globalization.NumberStyles.AllowHexSpecifier);
                        switch (v) {
                            case -1: ret = "n/a"; break;
                            case 0: ret = "None"; break;
                            case 1: ret = "Rotate 90 CW"; break;
                            case 2: ret = "Rotate 180"; break;
                            case 3: ret = "Rotate 270 CW"; break;
                        }
                        break;
                    }
                case 28: { //NDFilter
                        short v = short.Parse(string.Format("{0:X}", value), System.Globalization.NumberStyles.AllowHexSpecifier);
                        switch (v) {
                            case -1: ret = "n/a"; break;
                            case 0: ret = "Off"; break;
                            case 1: ret = "On"; break;
                        }
                        break;
                    }
            }
            return ret;
        }

    }
}
