using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理FaceDetect1裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseFaceDetect1Value(int tag, int value) {
            string ret = string.Empty;
            switch (tag) {
                case 2:
                case 3:
                case 4:
                    ret = IntToUShort(value).ToString();
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 處理FaceDetect2裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseFaceDetect2Value(int tag, byte value) {
            string ret = string.Empty;
            switch (tag) {
                case 1:
                case 2:
                    //ret = IntToUShort(value).ToString();
                    ret = value.ToString();
                    break;
            }
            return ret;
        }
    }
}
