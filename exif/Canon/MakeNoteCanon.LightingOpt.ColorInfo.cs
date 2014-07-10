using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理LightingOpt裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseLightingOptValue(int tag, int value) {
            string ret = string.Empty;
            short v = IntToShort(value);
            switch (tag) {
                case 2: { //AutoLightingOptimizer
                        switch (v) {
                            case 0: ret = "Standard"; break;
                            case 1: ret = "Low"; break;
                            case 2: ret = "Strong"; break;
                            case 3: ret = "Off"; break;
                        }
                        break;
                    }
            }
            return ret;
        }

        /// <summary>
        /// 處理ColorInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseColorInfoValue(int tag, int value) {
            string ret = string.Empty;
            ushort v = IntToUShort(value);
            switch (tag) {
                case 1: //Saturation
                case 2: //ColorTone
                    if (v == 0) { ret = "Normal"; }
                    else { ret = v.ToString(); }
                    break;
                case 3: { //ColorSpace
                        switch (v) {
                            case 1: ret = "sRGB"; break;
                            case 2: ret = "Adobe RGB"; break;
                        }
                        break;
                    }
            }
            return ret;
        }

    }
}
