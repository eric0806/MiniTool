using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 解析FocalLength區段
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseFocalLengthValue(int tag, int value) {
            string ret = "Unknown";
            switch (tag) {
                case 0: { //FocalType
                        switch (value) {
                            case 1: ret = "Fixed"; break;
                            case 2: ret = "Zoom"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 1: //FocalLength
                    ret = string.Format("{0:0.0}", (float)value / (float)FocalUnits) + " mm"; break;
            }
            return ret;
        }
    }
}
