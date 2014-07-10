using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 取得基本名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetDefaultTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                //case 0x0003: ret = "CanonFlashInfo"; break;
                case 0x0006: ret = "CanonImageType"; break;
                case 0x0007: ret = "CanonFirmwareVersion"; break;
                case 0x0008: ret = "FileNumber"; break;
                case 0x0009: ret = "OwnerName"; break;
                case 0x000C: ret = "SerialNumber"; break;
                case 0x000e: ret = "CanonFileLength"; break;
                case 0x0010: ret = "CanonModelID"; break;
                case 0x0013: ret = "ThumbnailImageValidArea"; break;
                case 0x0015: ret = "SerialNumberFormat"; break;
                case 0x001a: ret = "SuperMacro"; break;
                case 0x001c: ret = "DateStampMode"; break;
                case 0x001e: ret = "FirmwareRevision"; break;
                case 0x0023: ret = "Categories"; break;
                case 0x0028: ret = "ImageUniqueID"; break;
                case 0x0081: ret = "RawDataOffset"; break;
                case 0x0083: ret = "OriginalDecisionDataOffset"; break;
                //case 0x0094: ret = "AFPointsInFocus1D"; break;
                case 0x0095: ret = "LensModel"; break;
                case 0x0096: ret = "InternalSerialNumber"; break;
                case 0x0097: ret = "DustRemovalData"; break;
                //case 0x0098: ret = "CropInfo"; break;
                //case 0x00a1: ret = "ToneCurveTable"; break;
                //case 0x00a2: ret = "SharpnessTable"; break;
                //case 0x00a3: ret = "SharpnessFreqTable"; break;
                //case 0x00a4: ret = "WhiteBalanceTable"; break;
                case 0x00ae: ret = "ColorTemperature"; break;
                case 0x00b2: ret = "ToneCurveMatching"; break;
                case 0x00b3: ret = "WhiteBalanceMatching"; break;
                case 0x00b4: ret = "ColorSpace"; break;
                case 0x00d0: ret = "VRDOffset"; break;
                //case 0x4002: ret = "CRWParam"; break;
                //case 0x4005: ret = "Flavor"; break;
                case 0x4008: ret = "BlackLevel"; break;
                case 0x4010: ret = "CustomPictureStyleFileName"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得CameraSettings的Tag顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetCameraSettingsTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "MacroMode"; break;
                case 2: ret = "SelfTimer"; break;
                case 3: ret = "Quality"; break;
                case 4: ret = "CanonFlashMode"; break;
                case 5: ret = "ContinuousDrive"; break;
                case 7: ret = "FocusMode"; break;
                case 9: ret = "RecordMode"; break;
                case 10: ret = "CanonImageSize"; break;
                case 11: ret = "EasyMode"; break;
                case 12: ret = "DigitalZoom"; break;
                case 13: ret = "Contrast"; break;
                case 14: ret = "Saturation"; break;
                case 15: ret = "Sharpness"; break;
                case 16: ret = "CameraISO"; break;
                case 17: ret = "MeteringMode"; break;
                case 18: ret = "FocusRange"; break;
                case 19: ret = "AFPoint"; break;
                case 20: ret = "CanonExposureMode"; break;
                case 22: ret = "LensType"; break;
                case 23: ret = "MaxFocalLength"; break;
                case 24: ret = "MinFocalLength"; break;
                case 25: ret = "FocalUnits"; break;
                case 26: ret = "MaxAperture"; break;
                case 27: ret = "MinAperture"; break;
                case 28: ret = "FlashActivity"; break;
                case 29: ret = "FlashBits"; break;
                case 32: ret = "FocusContinuous"; break;
                case 33: ret = "AESetting"; break;
                case 34: ret = "ImageStabilization"; break;
                case 35: ret = "DisplayAperture"; break;
                case 36: ret = "ZoomSourceWidth"; break;
                case 37: ret = "ZoomTargetWidth"; break;
                case 39: ret = "SpotMeteringMode"; break;
                case 40: ret = "PhotoEffect"; break;
                case 41: ret = "ManualFlashOutput"; break;
                case 42: ret = "ColorTone"; break;
                case 46: ret = "SRAWQuality"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得FocalLength的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetFocalLengthTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 0: ret = "FocalType"; break;
                case 1: ret = "FocalLength"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得ShotInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetShotInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "AutoISO"; break;
                case 2: ret = "BaseISO"; break;
                case 3: ret = "MeasuredEV"; break;
                case 4: ret = "TargetAperture"; break;
                case 5: ret = "TargetExposureTime"; break;
                case 6: ret = "ExposureCompensation"; break;
                case 7: ret = "WhiteBalance"; break;
                case 8: ret = "SlowShutter"; break;
                case 9: ret = "SequenceNumber"; break;
                case 10: ret = "OpticalZoomCode"; break;
                case 12: ret = "CameraTemperature"; break;
                case 13: ret = "FlashGuideNumber"; break;
                case 14: ret = "AFPointsInFocus"; break;
                case 15: ret = "FlashExposureComp"; break;
                case 16: ret = "AutoExposureBracketing"; break;
                case 17: ret = "AEBBracketValue"; break;
                case 18: ret = "ControlMode"; break;
                case 19: ret = "FocusDistanceUpper"; break;
                case 20: ret = "FocusDistanceLower"; break;
                case 21: ret = "FNumber"; break;
                case 22: ret = "ExposureTime"; break;
                //case 23: ret = "MeasuredEV2"; break;
                case 24: ret = "BulbDuration"; break;
                case 26: ret = "CameraType"; break;
                case 27: ret = "AutoRotate"; break;
                case 28: ret = "NDFilter"; break;
                //case 29: ret = "SelfTimer2"; break;
                //case 33: ret = "FlashOutput"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得AFInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetAFInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 0: ret = "NumAFPoints"; break;
                case 1: ret = "ValidAFPoints"; break;
                case 2: ret = "CanonImageWidth"; break;
                case 3: ret = "CanonImageHeight"; break;
                case 4: ret = "AFImageWidth"; break;
                case 5: ret = "AFImageHeight"; break;
                //case 6: ret = "AFAreaWidth1"; break;
                //case 7: ret = "AFAreaHeight1"; break;
                //case 8: ret = "AFAreaXPositions"; break;
                //case 9: ret = "AFAreaYPositions"; break;
                //case 10: ret = "AFPointsInFocus"; break;
                case 11: ret = "PrimaryAFPoint"; break;
                case 12: ret = "PrimaryAFPoint"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得AFInfo2的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetAFInfo2TagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 0: ret = "AFInfoSize"; break;
                case 1: ret = "AFAreaMode"; break;
                case 2: ret = "NumAFPoints"; break;
                case 3: ret = "ValidAFPoints"; break;
                case 4: ret = "CanonImageWidth"; break;
                case 5: ret = "CanonImageHeight"; break;
                case 6: ret = "AFImageWidth"; break;
                case 7: ret = "AFImageHeight"; break;
                //case 8: ret = "AFAreaWidths"; break;
                //case 9: ret = "AFAreaHeights"; break;
                //case 10: ret = "AFAreaXPositions"; break;
                //case 11: ret = "AFAreaYPositions"; break;
                //case 12: ret = "AFPointsInFocus"; break;
                //case 13: ret = "AFPointsSelected"; break;
                case 14: ret = "PrimaryAFPoint"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得MyColors的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetMyColorsTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 2: ret = "MyColorMode"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得FaceDetect1的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetFaceDetect1TagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 2: ret = "FacesDetected"; break;
                case 3: ret = "FaceDetectFrameWidth"; break;
                case 4: ret = "FaceDetectFrameHeight"; break;
                /*
                case 8: ret = "Face1Position"; break;
                case 10: ret = "Face2Position"; break;
                case 12: ret = "Face3Position"; break;
                case 14: ret = "Face4Position"; break;
                case 16: ret = "Face5Position"; break;
                case 18: ret = "Face6Position"; break;
                case 20: ret = "Face7Position"; break;
                case 22: ret = "Face8Position"; break;
                case 24: ret = "Face9Position"; break;
                */
            }
            return ret;
        }

        /// <summary>
        /// 取得FaceDetect2的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetFaceDetect2TagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "FaceWidth"; break;
                case 2: ret = "FacesDetected"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得FileInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetFileInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                //case 1: ret = "FileNumber/ShutterCount"; break;
                case 3: ret = "BracketMode"; break;
                case 4: ret = "BracketValue"; break;
                case 5: ret = "BracketShotNumber"; break;
                case 6: ret = "RawJpgQuality"; break;
                case 7: ret = "RawJpgSize"; break;
                case 8: ret = "LongExposureNoiseReduction2"; break;
                case 9: ret = "WBBracketMode"; break;
                case 12: ret = "WBBracketValueAB"; break;
                case 13: ret = "WBBracketValueGM"; break;
                case 14: ret = "FilterEffect"; break;
                case 15: ret = "ToningEffect"; break;
                //case 16: ret = "MacroMagnification"; break;
                case 19: ret = "LiveViewShooting"; break;
                case 25: ret = "FlashExposureLock"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得AspectInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetAspectInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 0: ret = "AspectRatio"; break;
                case 1: ret = "CroppedImageWidth"; break;
                case 2: ret = "CroppedImageHeight"; break;
                case 3: ret = "CroppedImageLeft"; break;
                case 4: ret = "CroppedImageTop"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得ProcessingInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetProcessingInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "ToneCurve"; break;
                case 2: ret = "Sharpness"; break;
                case 3: ret = "SharpnessFrequency"; break;
                case 4: ret = "SensorRedLevel"; break;
                case 5: ret = "SensorBlueLevel"; break;
                case 6: ret = "WhiteBalanceRed"; break;
                case 7: ret = "WhiteBalanceBlue"; break;
                case 8: ret = "WhiteBalance"; break;
                case 9: ret = "ColorTemperature"; break;
                case 10: ret = "PictureStyle"; break;
                case 11: ret = "DigitalGain"; break;
                case 12: ret = "WBShiftAB"; break;
                case 13: ret = "WBShiftGM"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得ModifiedInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetModifiedInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "ModifiedToneCurve"; break;
                case 2: ret = "ModifiedSharpness"; break;
                case 3: ret = "ModifiedSharpnessFreq"; break;
                case 4: ret = "ModifiedSensorRedLevel"; break;
                case 5: ret = "ModifiedSensorBlueLevel"; break;
                case 6: ret = "ModifiedWhiteBalanceRed"; break;
                case 7: ret = "ModifiedWhiteBalanceBlue"; break;
                case 8: ret = "ModifiedWhiteBalance"; break;
                case 9: ret = "ModifiedColorTemp"; break;
                case 10: ret = "ModifiedPictureStyle"; break;
                case 11: ret = "ModifiedDigitalGain"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得SensorInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetSensorInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "SensorWidth"; break;
                case 2: ret = "SensorHeight"; break;
                case 5: ret = "SensorLeftBorder"; break;
                case 6: ret = "SensorTopBorder"; break;
                case 7: ret = "SensorRightBorder"; break;
                case 8: ret = "SensorBottomBorder"; break;
                case 9: ret = "BlackMaskLeftBorder"; break;
                case 10: ret = "BlackMaskTopBorder"; break;
                case 11: ret = "BlackMaskRightBorder"; break;
                case 12: ret = "BlackMaskBottomBorder"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得ColorInfo的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetColorInfoTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 1: ret = "Saturation"; break;
                case 2: ret = "ColorTone"; break;
                case 3: ret = "ColorSpace"; break;
            }
            return ret;
        }

        /// <summary>
        /// 取得LightingOpt的顯示名稱
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetLightingOptTagName(int tag) {
            string ret = string.Empty;
            switch (tag) {
                case 2: ret = "AutoLightingOptimizer"; break;
            }
            return ret;
        }
    }
}
