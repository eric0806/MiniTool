using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理FileInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseFileInfoValue(int tag, int value) {
            string ret = string.Empty;
            switch (tag) {
                case 3: { //BracketMode
                        switch (IntToUShort(value)) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "AEB"; break;
                            case 2: ret = "FEB"; break;
                            case 3: ret = "ISO"; break;
                            case 4: ret = "WB"; break;
                        }
                        break;
                    }
                case 4:  //BracketValue
                case 5:  //BracketShotNumber
                case 12: //WBBracketValueAB
                case 13: //WBBracketValueGM
                    ret = IntToShort(value).ToString();
                    break;
                case 6: { //RawJpgQuality
                        switch (IntToShort(value)) {
                            case 1: ret = "Economy"; break;
                            case 2: ret = "Normal"; break;
                            case 3: ret = "Fine"; break;
                            case 4: ret = "RAW"; break;
                            case 5: ret = "Superfine"; break;
                            case 130: ret = "Normal Movie"; break;
                        }
                        break;
                    }
                case 7: { //RawJpgSize
                        switch (IntToShort(value)) {
                            case 0: ret = "Large"; break;
                            case 1: ret = "Medium"; break;
                            case 2: ret = "Small"; break;
                            case 5: ret = "Medium 1"; break;
                            case 6: ret = "Medium 2"; break;
                            case 7: ret = "Medium 3"; break;
                            case 8: ret = "Postcard"; break;
                            case 9: ret = "Widescreen"; break;
                            case 10: ret = "Medium Widescreen"; break;
                            case 14: ret = "Small 1"; break;
                            case 15: ret = "Small 2"; break;
                            case 16: ret = "Small 3"; break;
                            case 128: ret = "640x480 Movie"; break;
                            case 129: ret = "Medium Movie"; break;
                            case 130: ret = "Small Movie"; break;
                            case 137: ret = "1280x720 Movie"; break;
                            case 142: ret = "1920x1080 Movie"; break;
                        }
                        break;
                    }
                case 8: { //LongExposureNoiseReduction2
                        switch (IntToShort(value)) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "On (1D)"; break;
                            case 3: ret = "On"; break;
                            case 4: ret = "Auto"; break;
                        }
                        break;
                    }
                case 9: { //WBBracketMode
                        switch (IntToShort(value)) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "On (shift AB)"; break;
                            case 2: ret = "On (shift GM)"; break;
                        }
                        break;
                    }
                case 14: { //FilterEffect
                        switch (IntToShort(value)) {
                            case 0: ret = "None"; break;
                            case 1: ret = "Yellow"; break;
                            case 2: ret = "Orange"; break;
                            case 3: ret = "Red"; break;
                            case 4: ret = "Green"; break;
                        }
                        break;
                    }
                case 15: { //ToningEffect
                        switch (IntToShort(value)) {
                            case 0: ret = "None"; break;
                            case 1: ret = "Sepia"; break;
                            case 2: ret = "Blue"; break;
                            case 3: ret = "Purple"; break;
                            case 4: ret = "Green"; break;
                        }
                        break;
                    }
                case 19: //LiveViewShooting
                case 25: { //FlashExposureLock
                        switch (IntToShort(value)) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "On"; break;
                        }
                        break;
                    }
            }
            return ret;
        }

        /// <summary>
        /// 處理AspectInfo裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseAspectInfoValue(int tag, int value) {
            string ret = string.Empty;
            switch (tag) {
                case 0: { //AspectRatio
                        switch (value) {
                            case 0: ret = "3:2"; break;
                            case 1: ret = "1:1"; break;
                            case 2: ret = "4:3"; break;
                            case 7: ret = "16:9"; break;
                            case 8: ret = "4:5"; break;
                        }
                        break;
                    }
                case 1: //CroppedImageWidth
                case 2: //CroppedImageHeight
                case 3: //CroppedImageLeft
                case 4: //CroppedImageTop
                    ret = value.ToString();
                    break;
            }
            return ret;
        }
    }
}
