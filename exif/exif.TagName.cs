using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace exif
{
    public partial class exif
    {
        /// <summary>
        /// 從二進位的Tag值取得文字名稱
        /// </summary>
        /// <param name="tag">取得正確順序後的二進位整數資料</param>
        /// <returns></returns>
        private string GetIFDTagName(int tag) {
            string name = string.Empty;
            switch (tag) {
                case 0x100:
                    name = "ImageWidth"; break;
                    //name = "圖片寬度"; break;
                case 0x101:
                    name = "ImageHeight"; break;
                    //name = "圖片高度"; break;
                case 0x102:
                    name = "BitsPerSample"; break;
                case 0x103:
                    name = "Compression"; break;
                case 0x106:
                    name = "PhotometricInterpretation"; break;
                case 0x112:
                    name = "Orientation"; break;
                    //name = "方向"; break;
                case 0x115:
                    name = "SamplesPerPixel"; break;
                case 0x11C:
                    name = "PlanarConfiguration"; break;
                case 0x212:
                    name = "YCbCrSubSampling"; break;
                case 0x213:
                    name = "YCbCrPositioning"; break;
                case 0x11A:
                    name = "XResolution"; break;
                    //name = "X解析度"; break;
                case 0x11B:
                    name = "YResolution"; break;
                    //name = "Y解析度"; break;
                case 0x128:
                    name = "ResolutionUnit"; break;
                    //name = "解析度單位"; break;
                case 0x111:
                    name = "StripOffsets"; break;
                case 0x116:
                    name = "RowsPerStrip"; break;
                case 0x117:
                    name = "StripByteCounts"; break;
                case 0x201:
                    name = "JPEGInterchangeFormat"; break;
                case 0x202:
                    name = "JPEGInterchangeFormatLength"; break;
                case 0x12D:
                    name = "TransferFunction"; break;
                case 0x13E:
                    name = "WhitePoint"; break;
                case 0x13F:
                    name = "PrimaryChromaticities"; break;
                case 0x211:
                    name = "YCbCrCoefficients"; break;
                case 0x214:
                    name = "ReferenceBlackWhite"; break;
                case 0x132:
                    name = "ModifyDate"; break;
                    //name = "修改日期"; break;
                case 0x10E:
                    name = "ImageDescription"; break;
                case 0x10F:
                    name = "Make"; break;
                    //name = "相機廠牌"; break;
                case 0x110:
                    name = "Model"; break;
                    //name = "相機型號"; break;
                case 0x131:
                    name = "Software"; break;
                    //name = "使用軟體"; break;
                case 0x13B:
                    name = "Artist"; break;
                    //name = "作者"; break;
                case 0x8298:
                    name = "Copyright"; break;
                    //name = "版權"; break;
                case 0x9000:
                    name = "ExifVersion"; break;
                    //name = "Exif版本"; break;
                case 0xA000:
                    name = "FlashpixVersion"; break;
                    //name = "Flashpix版本"; break;
                case 0xA001:
                    name = "ColorSpace"; break;
                    //name = "色彩空間"; break;
                case 0xA500:
                    name = "Gamma"; break;
                case 0x9101:
                    name = "ComponentsConfiguration"; break;
                case 0x9102:
                    name = "CompressedBitsPerPixel"; break;
                case 0xA002:
                    name = "ExifImageWidth"; break;
                    //name = "Exif圖片寬度"; break;
                case 0xA003:
                    name = "ExifImageHeight"; break;
                    //name = "Exif圖片高度"; break;
                case 0xA004:
                    name = "RelatedSoundFile"; break;
                    //name = "聲音記錄檔"; break;
                case 0x9286:
                    name = "UserComment"; break;
                case 0x9003:
                    name = "DateTimeOriginal"; break;
                case 0x9004:
                    name = "DateTimeDigitized"; break;
                case 0x9290:
                    name = "SubSecTime"; break;
                case 0x9291:
                    name = "SubSecTimeOriginal"; break;
                case 0x9292:
                    name = "SubSecTimeDigitized"; break;
                case 0xA420:
                    name = "ImageUniqueID"; break;
                case 0x829A:
                    name = "ExposureTime"; break;
                    //name = "快門速度"; break;
                case 0x829D:
                    name = "FNumber"; break;
                    //name = "光圈大小"; break;
                case 0x8822:
                    name = "ExposureProgram"; break;
                    //name = "操作模式"; break;
                case 0x8824:
                    name = "SpectralSensitivity"; break;
                case 0x8827:
                    name = "ISO"; break;
                case 0x8828:
                    name = "OECF"; break;
                case 0x8830:
                    name = "SensitivityType"; break;
                case 0x8831:
                    name = "StandardOutputSensitivity"; break;
                case 0x8832:
                    name = "RecommendedExposureIndex"; break;
                case 0x8833:
                    name = "ISOSpeed"; break;
                case 0x8834:
                    name = "ISOSpeedLatitudeyyy"; break;
                case 0x8835:
                    name = "ISOSpeedLatitudezzz"; break;
                case 0x9201:
                    name = "ShutterSpeedValue(APEX)"; break;
                case 0x9202:
                    name = "ApertureValue(APEX)"; break;
                case 0x9203:
                    name = "BrightnessValue(APEX)"; break;
                case 0x9204:
                    name = "ExposureBiasValue(APEX)"; break;
                case 0x9205:
                    name = "MaxApertureValue(APEX)"; break;
                case 0x9206:
                    name = "SubjectDistance"; break;
                    //name = "目標距離"; break;
                case 0x9207:
                    name = "MeteringMode"; break;
                    //name = "測光模式"; break;
                case 0x9208:
                    name = "LightSource"; break;
                    //name = "光源"; break;
                case 0x9209:
                    name = "Flash"; break;
                    //name = "閃光燈"; break;
                case 0x920A:
                    name = "FocalLength"; break;
                    //name = "焦距"; break;
                case 0x9214:
                    name = "SubjectArea"; break;
                case 0x927C:
                    //name = "MakerNote(debug用)"; break;
                    break;
                case 0xA20B:
                    name = "FlashEnergy"; break;
                case 0xA20C:
                    name = "SpatialFrequencyResponse"; break;
                case 0xA20E:
                    name = "FocalPlaneXResolution"; break;
                case 0xA20F:
                    name = "FocalPlaneYResolution"; break;
                case 0xA210:
                    name = "FocalPlaneResolutionUnit"; break;
                case 0xA214:
                    name = "SubjectLocation"; break;
                case 0xA215:
                    name = "ExposureIndex"; break;
                case 0xA217:
                    name = "SensingMethod"; break;
                case 0xA300:
                    name = "FileSource"; break;
                case 0xA301:
                    name = "SceneType"; break;
                case 0xA302:
                    name = "CFAPattern"; break;
                case 0xA401:
                    name = "CustomRendered"; break;
                case 0xA402:
                    name = "ExposureMode"; break;
                    //name = "曝光模式"; break;
                case 0xA403:
                    name = "WhiteBalance"; break;
                    //name = "白平衡"; break;
                case 0xA404:
                    name = "DigitalZoomRatio"; break;
                case 0xA405:
                    name = "FocalLengthIn35mmFilm"; break;
                case 0xA406:
                    name = "SceneCaptureType"; break;
                case 0xA407:
                    name = "GainControl"; break;
                case 0xA408:
                    name = "Contrast"; break;
                    //name = "對比"; break;
                case 0xA409:
                    name = "Saturation"; break;
                    //name = "飽和"; break;
                case 0xA40A:
                    name = "Sharpness"; break;
                    //name = "銳利"; break;
                case 0xA40B:
                    name = "DeviceSettingDescription"; break;
                case 0xA40C:
                    name = "SubjectDistanceRange"; break;
                case 0xA430:
                    name = "CameraOwnerName"; break;
                case 0xA431:
                    name = "BodySerialNumber"; break;
                    //name = "機身序號"; break;
                case 0xA432:
                    name = "LensSpecification"; break;
                    //name = "鏡頭規格"; break;
                case 0xA433:
                    name = "LensMake"; break;
                    //name = "鏡頭廠牌"; break;
                case 0xA434:
                    name = "LensModel"; break;
                    //name = "鏡頭型號"; break;
                case 0xA435:
                    name = "LensSerialNumber"; break;
                    //name = "鏡頭序號"; break;
                //GPS
                case 0x0:
                    name = "GPSVersionID"; break;
                case 0x1:
                    name = "GPSLatitudeRef"; break;
                case 0x2:
                    name = "GPSLatitude"; break;
                case 0x3:
                    name = "GPSLongitudeRef"; break;
                case 0x4:
                    name = "GPSLongitude"; break;
                case 0x5:
                    name = "GPSAltitudeRef"; break;
                case 0x6:
                    name = "GPSAltitude"; break;
                case 0x7:
                    name = "GPSTimeStamp"; break;
                case 0x8:
                    name = "GPSSatellites"; break;
                case 0x9:
                    name = "GPSStatus"; break;
                case 0xA:
                    name = "GPSMeasureMode"; break;
                case 0xB:
                    name = "GPSDOP"; break;
                case 0xC:
                    name = "GPSSpeedRef"; break;
                case 0xD:
                    name = "GPSSpeed"; break;
                case 0xE:
                    name = "GPSTrackRef"; break;
                case 0xF:
                    name = "GPSTrack"; break;
                case 0x10:
                    name = "GPSImgDirectionRef"; break;
                case 0x11:
                    name = "GPSImgDirection"; break;
                case 0x12:
                    name = "GPSMapDatum"; break;
                case 0x13:
                    name = "GPSDestLatitudeRef"; break;
                case 0x14:
                    name = "GPSDestLatitude"; break;
                case 0x15:
                    name = "GPSDestLongitudeRef"; break;
                case 0x16:
                    name = "GPSDestLongitude"; break;
                case 0x17:
                    name = "GPSDestBearingRef"; break;
                case 0x18:
                    name = "GPSDestBearing"; break;
                case 0x19:
                    name = "GPSDestDistanceRef"; break;
                case 0x1A:
                    name = "GPSDestDistance"; break;
                case 0x1B:
                    name = "GPSProcessingMethod"; break;
                case 0x1C:
                    name = "GPSAreaInformation"; break;
                case 0x1D:
                    name = "GPSDateStamp"; break;
                case 0x1E:
                    name = "GPSDifferential"; break;
                default:
                    //name = tag.ToString();
                    name = string.Empty;
                    break;
            }
            return name;
        }

    }
}
