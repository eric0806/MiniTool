using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon._60D
{
    public static class TagName
    {
        public static string GetCameraInfoTagName(int tag) {
            switch (tag) {
                case 3: return "FNumber";
                case 4: return "ExposureTime";
                case 6: return "ISO";
                case 25: return "CameraTemperature";
                case 30: return "FocalLength";
                case 54: return "CameraOrientation";
                case 5: return "FocusDistanceUpper";
                case 87: return "FocusDistanceLower";
                case 125: return "ColorTemperature";
                case 232: return "LensType";
                case 234: return "MinFocalLength";
                case 236: return "MaxFocalLength";
                case 409: return "FirmwareVersion";
                case 473: return "FileIndex";
                case 485: return "DirectoryIndex";
                default: return string.Empty;
            }
        }
    }
}
