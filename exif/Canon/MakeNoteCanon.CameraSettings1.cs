using System;
using System.Collections.Generic;
using System.Text;

namespace exif.Canon
{
    partial class MakerNoteCanon : MakerNote
    {
        /// <summary>
        /// 處理CameraSettings裡面的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ParseCameraSettings1Value(int tag, int value) {
            string ret = "Unknown";
            string OrNewLine = Environment.NewLine + "or" + Environment.NewLine;
            switch (tag) {
                case 1: { //MacroMode
                        switch (value) {
                            case 0: ret = "EOS Macro"; break;
                            case 1: ret = "Macro"; break;
                            case 2: ret = "Normal/Off"; break;
                        }
                        break;
                    }
                case 2: { //SelfTimer
                        ushort v = IntToUShort(value);
                        if (v / 10 == 0) {
                            ret = "Off";
                        }
                        else {
                            switch (v) {
                                case 16384: ret = "0 sec, custom 4"; break;
                                case 16394: ret = "1 sec, custom 4"; break;
                                case 16404: ret = "2 sec, custom 4"; break;
                                case 16414: ret = "3 sec, custom 4"; break;
                                case 16424: ret = "4 sec, custom 4"; break;
                                case 16434: ret = "5 sec, custom 4"; break;
                                case 16474: ret = "9 sec, custom 4"; break;
                                case 16484: ret = "10 sec, custom 4"; break;
                                case 32768: ret = "0 sec, custom 8"; break;
                                default:
                                    ret = (v / 10) + " s";
                                    break;
                            }
                        }
                        break;
                    }
                case 3: { //Quality
                        switch (value) {
                            case 1: ret = "Economy"; break;
                            case 2: ret = "Normal"; break;
                            case 3: ret = "Fine"; break;
                            case 4: ret = "RAW"; break;
                            case 5: ret = "Superfine"; break;
                            case 130: ret = "Normal Movie"; break;
                        }
                        break;
                    }
                case 4: { //CanonFlashMode
                        switch (value) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "Auto"; break;
                            case 2: ret = "On"; break;
                            case 3: ret = "Red-eye reduction"; break;
                            case 4: ret = "Slow-sync"; break;
                            case 5: ret = "Red-eye reduction (Auto)"; break;
                            case 6: ret = "Red-eye reduction (On)"; break;
                            case 16: ret = "External flash"; break;
                        }
                        break;
                    }
                case 5: { //ContinuousDrive
                        switch (value) {
                            case 0: ret = "Single"; break;
                            case 1: ret = "Continuous"; break;
                            case 2: ret = "Movie"; break;
                            case 3: ret = "Continuous, Speed Priority"; break;
                            case 4: ret = "Continuous, Low"; break;
                            case 5: ret = "Continuous, High"; break;
                            case 6: ret = "Silent Single"; break;
                        }
                        break;
                    }
                case 7: { //FocusMode
                        switch (value) {
                            case 0: ret = "One-shot AF"; break;
                            case 1: ret = "AI Servo AF"; break;
                            case 2: ret = "AI Focus AF"; break;
                            case 3: ret = "Manual Focus (3)"; break;
                            case 4: ret = "Single"; break;
                            case 5: ret = "Continuous"; break;
                            case 6: ret = "Manual Focus (6)"; break;
                            case 16: ret = "Pan Focus"; break;
                        }
                        break;
                    }
                case 9: { //RecordMode
                        switch (value) {
                            case 1: ret = "JPEG"; break;
                            case 2: ret = "CRW+THM"; break;
                            case 3: ret = "AVI+THM"; break;
                            case 4: ret = "TIF"; break;
                            case 5: ret = "TIF+JPEG"; break;
                            case 6: ret = "CR2"; break;
                            case 7: ret = "CR2+JPEG"; break;
                            case 9: ret = "Video"; break;
                        }
                        break;
                    }
                case 10: { //CanonImageSize
                    short v = IntToShort(value);
                        switch (v) {
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
                            default: ret = "Unknown (" + v.ToString() + ")"; break;
                        }
                        break;
                    }
                case 11: { //EasyMode
                        switch (value) {
                            case 0: ret = "Full auto"; break;
                            case 1: ret = "Manual"; break;
                            case 2: ret = "Landscape"; break;
                            case 3: ret = "Fast shutter"; break;
                            case 4: ret = "Slow shutter"; break;
                            case 5: ret = "Night"; break;
                            case 6: ret = "Gray Scale"; break;
                            case 7: ret = "Sepia"; break;
                            case 8: ret = "Portrait"; break;
                            case 9: ret = "Sports"; break;
                            case 10: ret = "Macro"; break;
                            case 11: ret = "Black & White"; break;
                            case 12: ret = "Pan focus"; break;
                            case 13: ret = "Vivid"; break;
                            case 14: ret = "Neutral"; break;
                            case 15: ret = "Flash Off"; break;
                            case 16: ret = "Long Shutter"; break;
                            case 17: ret = "Super Macro"; break;
                            case 18: ret = "Foliage"; break;
                            case 19: ret = "Indoor"; break;
                            case 20: ret = "Fireworks"; break;
                            case 21: ret = "Beach"; break;
                            case 22: ret = "Underwater"; break;
                            case 23: ret = "Snow"; break;
                            case 24: ret = "Kids & Pets"; break;
                            case 25: ret = "Night Snapshot"; break;
                            case 26: ret = "Digital Macro"; break;
                            case 27: ret = "My Colors"; break;
                            case 28: ret = "Movie Snap"; break;
                            case 29: ret = "Super Macro 2"; break;
                            case 30: ret = "Color Accent"; break;
                            case 31: ret = "Color Swap"; break;
                            case 32: ret = "Aquarium"; break;
                            case 33: ret = "ISO 3200"; break;
                            case 34: ret = "ISO 6400"; break;
                            case 35: ret = "Creative Light Effect"; break;
                            case 36: ret = "Easy"; break;
                            case 37: ret = "Quick Shot"; break;
                            case 38: ret = "Creative Auto"; break;
                            case 39: ret = "Zoom Blur"; break;
                            case 40: ret = "Low Light"; break;
                            case 41: ret = "Nostalgic"; break;
                            case 42: ret = "Super Vivid"; break;
                            case 43: ret = "Poster Effect"; break;
                            case 44: ret = "Face Self-timer"; break;
                            case 45: ret = "Smile"; break;
                            case 46: ret = "Wink Self-timer"; break;
                            case 47: ret = "Fisheye Effect"; break;
                            case 48: ret = "Miniature Effect"; break;
                            case 49: ret = "High-speed Burst"; break;
                            case 50: ret = "Best Image Selection"; break;
                            case 51: ret = "High Dynamic Range"; break;
                            case 52: ret = "Handheld Night Scene"; break;
                            case 53: ret = "Movie Digest"; break;
                            case 54: ret = "Live View Control"; break;
                            case 55: ret = "Discreet"; break;
                            case 56: ret = "Blur Reduction"; break;
                            case 57: ret = "Monochrome"; break;
                            case 58: ret = "Toy Camera Effect"; break;
                            case 59: ret = "Scene Intelligent Auto"; break;
                            case 60: ret = "High-speed Burst HQ"; break;
                            case 61: ret = "Smooth Skin"; break;
                            case 62: ret = "Soft Focus"; break;
                            case 257: ret = "Spotlight"; break;
                            case 258: ret = "Night 2"; break;
                            case 259: ret = "Night+"; break;
                            case 260: ret = "Super Night"; break;
                            case 261: ret = "Sunset"; break;
                            case 263: ret = "Night Scene"; break;
                            case 264: ret = "Surface"; break;
                            case 265: ret = "Low Light 2"; break;
                        }
                        break;
                    }
                case 12: { //DigitalZoom
                        switch (value) {
                            case 0: ret = "None"; break;
                            case 1: ret = "2x"; break;
                            case 2: ret = "4x"; break;
                            case 3: ret = "Other"; break;
                        }
                        break;
                    }
                case 13: //Contrast
                case 14: //Saturation
                case 15: { //Sharpness
                        short v = IntToShort(value);
                        switch (v) {
                            case 0: ret = "Normal"; break;
                            case 32767: ret = "n/a"; break;
                            default: ret = value.ToString(); break;
                        }
                        break;
                    }
                case 16: { //CameraISO
                        ushort v = IntToUShort(value);
                        switch (v) {
                            case 0: ret = "Not used"; break;
                            case 14: ret = "Auto High"; break;
                            case 15: ret = "Auto"; break;
                            case 16: ret = "50"; break;
                            case 17: ret = "100"; break;
                            case 18: ret = "200"; break;
                            case 19: ret = "400"; break;
                            case 20: ret = "800"; break;
                            case 17184: ret = "800"; break;
                            case 16448: ret = "64"; break;
                            case 16704: ret = "320"; break;
                            case 17984: ret = "1600"; break;
                            case 16464: ret = "80"; break;
                            case 16484: ret = "100"; break;
                            case 16509: ret = "125"; break;
                            case 19584: ret = "3200"; break;
                            case 16784: ret = "400"; break;
                            case 16584: ret = "200"; break;
                            case 17384: ret = "1000"; break;
                            case 16634: ret = "250"; break;
                            case 32767: ret = string.Empty; break;// "See Exif-ISO"; break;
                            //default: ret = "與Exif相同"; break;
                            default: ret = string.Empty; break;
                        }
                        //ret = string.Format("{0:X}", value);
                        break;
                    }
                case 17: { //MeteringMode
                        switch (value) {
                            case 0: ret = "Default"; break;
                            case 1: ret = "Spot"; break;
                            case 2: ret = "Average"; break;
                            case 3: ret = "Evaluative"; break;
                            case 4: ret = "Partial"; break;
                            case 5: ret = "Center-weighted average"; break;
                            case 65535: ret = string.Empty; break;
                        }
                        break;
                    }
                case 18: { //FocusRange
                        switch (value) {
                            case 0: ret = "Manual"; break;
                            case 1: ret = "Auto"; break;
                            case 2: ret = "Not Known"; break;
                            case 3: ret = "Macro"; break;
                            case 4: ret = "Very Close"; break;
                            case 5: ret = "Close"; break;
                            case 6: ret = "Middle Range"; break;
                            case 7: ret = "Far Range"; break;
                            case 8: ret = "Pan Focus"; break;
                            case 9: ret = "Close-up"; break;
                            case 10: ret = "Infinity"; break;
                            case 11: ret = "Super Macro mode (PS-S2/S5)"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 19: { //AFPoint
                        switch (value) {
                            case 0x2005: ret = "Manual AF point selection"; break;
                            case 0x3000: ret = "None (MF)"; break;
                            case 0x3001: ret = "Auto AF point selection"; break;
                            case 0x3002: ret = "Right"; break;
                            case 0x3003: ret = "Center"; break;
                            case 0x3004: ret = "Left"; break;
                            case 0x4001: ret = "Auto AF point selection"; break;
                            case 0x4006: ret = "Face Detect"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 20: { //CanonExposureMode
                        switch (value) {
                            case 0: ret = "Easy"; break;
                            case 1: ret = "Program AE"; break;
                            case 2: ret = "Shutter speed priority AE"; break;
                            case 3: ret = "Aperture-priority AE"; break;
                            case 4: ret = "Manual"; break;
                            case 5: ret = "Depth-of-field AE"; break;
                            case 6: ret = "M-Dep"; break;
                            case 7: ret = "Bulb"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 22: { //LensType
                        #region 鏡頭型號，很多所以摺疊
                        switch (value) {
                            case 1: ret = "Canon EF 50mm f/1.8"; break;
                            case 2: ret = "Canon EF 28mm f/2.8"; break;
                            case 3: ret = "Canon EF 135mm f/2.8 Soft"; break;
                            case 4: ret =
                                "Canon EF 35-105mm f/3.5-4.5" + OrNewLine +
                                "Sigma UC Zoom 35-135mm f/4-5.6"; break;
                            case 5: ret = "Canon EF 35-70mm f/3.5-4.5"; break;
                            case 6: ret =
                                "Canon EF 28-70mm f/3.5-4.5" + OrNewLine +
                                "Sigma 18-50mm f/3.5-5.6 DC" + OrNewLine +
                                "Sigma 18-125mm f/3.5-5.6 DC IF ASP" + OrNewLine +
                                "Tokina AF 193-2 19-35mm f/3.5-4.5" + OrNewLine +
                                "Sigma 28-80mm f/3.5-5.6 II Macro"; break;
                            case 7: ret = "Canon EF 100-300mm f/5.6L"; break;
                            case 8: ret =
                                "Canon EF 100-300mm f/5.6" + OrNewLine +
                                "Sigma 70-300mm f/4-5.6 [APO] DG Macro" + OrNewLine +
                                "Tokina AT-X 242 AF 24-200mm f/3.5-5.6"; break;
                            case 9: ret =
                                "Canon EF 70-210mm f/4" + OrNewLine +
                                "Sigma 55-200mm f/4-5.6 DC"; break;
                            case 10: ret =
                                "Canon EF 50mm f/2.5 Macro" + OrNewLine +
                                "Sigma 50mm f/2.8 EX" + OrNewLine +
                                "Sigma 28mm f/1.8" + OrNewLine +
                                "Sigma 105mm f/2.8 Macro EX" + OrNewLine +
                                "Sigma 70mm f/2.8 EX DG Macro EF"; break;
                            case 11: ret = "Canon EF 35mm f/2"; break;
                            case 13: ret = "Canon EF 15mm f/2.8 Fisheye"; break;
                            case 14: ret = "Canon EF 50-200mm f/3.5-4.5L"; break;
                            case 15: ret = "Canon EF 50-200mm f/3.5-4.5"; break;
                            case 16: ret = "Canon EF 35-135mm f/3.5-4.5"; break;
                            case 17: ret = "Canon EF 35-70mm f/3.5-4.5A"; break;
                            case 18: ret = "Canon EF 28-70mm f/3.5-4.5"; break;
                            case 20: ret = "Canon EF 100-200mm f/4.5A"; break;
                            case 21: ret = "Canon EF 80-200mm f/2.8L"; break;
                            case 22: ret =
                                "Canon EF 20-35mm f/2.8L" + OrNewLine +
                                "Tokina AT-X 280 AF Pro 28-80mm f/2.8 Aspherical"; break;
                            case 23: ret = "Canon EF 35-105mm f/3.5-4.5"; break;
                            case 24: ret = "Canon EF 35-80mm f/4-5.6 Power Zoom"; break;
                            case 25: ret = "Canon EF 35-80mm f/4-5.6 Power Zoom"; break;
                            case 26: ret =
                                "Canon EF 100mm f/2.8 Macro" + OrNewLine +
                                "Cosina 100mm f/3.5 Macro AF" + OrNewLine +
                                "Tamron SP AF 90mm f/2.8 Di Macro" + OrNewLine +
                                "Tamron SP AF 180mm f/3.5 Di Macro" + OrNewLine +
                                "Carl Zeiss Planar T* 50mm f/1.4"; break;
                            case 27: ret = "Canon EF 35-80mm f/4-5.6"; break;
                            case 28: ret =
                                "Canon EF 80-200mm f/4.5-5.6" + OrNewLine +
                                "Tamron SP AF 28-105mm f/2.8 LD Aspherical IF" + OrNewLine +
                                "Tamron SP AF 28-75mm f/2.8 XR Di LD Aspherical [IF] Macro" + OrNewLine +
                                "Tamron AF 70-300mm f/4.5-5.6 Di LD 1:2 Macro Zoom" + OrNewLine +
                                "Tamron AF Aspherical 28-200mm f/3.8-5.6"; break;
                            case 29: ret = "Canon EF 50mm f/1.8 II"; break;
                            case 30: ret = "Canon EF 35-105mm f/4.5-5.6"; break;
                            case 31: ret =
                                "Canon EF 75-300mm f/4-5.6" + OrNewLine +
                                "Tamron SP AF 300mm f/2.8 LD IF"; break;
                            case 32: ret =
                                "Canon EF 24mm f/2.8" + OrNewLine +
                                "Sigma 15mm f/2.8 EX Fisheye"; break;
                            case 33: ret =
                                "Voigtlander" + OrNewLine +
                                "Voigtlander Ultron 40mm f/2 SLII Aspherical" + OrNewLine +
                                "Carl Zeiss Distagon T* 15mm f/2.8 ZE" + OrNewLine +
                                "Carl Zeiss Distagon T* 18mm f/3.5 ZE" + OrNewLine +
                                "Carl Zeiss Distagon T* 21mm f/2.8 ZE" + OrNewLine +
                                "Carl Zeiss Distagon T* 28mm f/2 ZE" + OrNewLine +
                                "Carl Zeiss Distagon T* 35mm f/2 ZE"; break;
                            case 35: ret = "Canon EF 35-80mm f/4-5.6"; break;
                            case 36: ret = "Canon EF 38-76mm f/4.5-5.6"; break;
                            case 37: ret =
                                "Canon EF 35-80mm f/4-5.6" + OrNewLine +
                                "Tamron 70-200mm f/2.8 Di LD IF Macro" + OrNewLine +
                                "Tamron AF 28-300mm f/3.5-6.3 XR Di VC LD Aspherical [IF] Macro Model A20" + OrNewLine +
                                "Tamron SP AF 17-50mm f/2.8 XR Di II VC LD Aspherical [IF]" + OrNewLine +
                                "Tamron AF 18-270mm f/3.5-6.3 Di II VC LD Aspherical [IF] Macro"; break;
                            case 38: ret = "Canon EF 80-200mm f/4.5-5.6"; break;
                            case 39: ret = "Canon EF 75-300mm f/4-5.6"; break;
                            case 40: ret = "Canon EF 28-80mm f/3.5-5.6"; break;
                            case 41: ret = "Canon EF 28-90mm f/4-5.6"; break;
                            case 42: ret =
                                "Canon EF 28-200mm f/3.5-5.6" + OrNewLine +
                                "Tamron AF 28-300mm f/3.5-6.3 XR Di VC LD Aspherical [IF] Macro Model A20"; break;
                            case 43: ret = "Canon EF 28-105mm f/4-5.6"; break;
                            case 44: ret = "Canon EF 90-300mm f/4.5-5.6"; break;
                            case 45: ret = "Canon EF-S 18-55mm f/3.5-5.6 [II]"; break;
                            case 46: ret = "Canon EF 28-90mm f/4-5.6"; break;
                            case 48: ret = "Canon EF-S 18-55mm f/3.5-5.6 IS"; break;
                            case 49: ret = "Canon EF-S 55-250mm f/4-5.6 IS"; break;
                            case 50: ret = "Canon EF-S 18-200mm f/3.5-5.6 IS"; break;
                            case 51: ret = "Canon EF-S 18-135mm f/3.5-5.6 IS"; break;
                            case 52: ret = "Canon EF-S 18-55mm f/3.5-5.6 IS II"; break;
                            case 53: ret = "Canon EF-S 18-55mm f/3.5-5.6 III"; break;
                            case 94: ret = "Canon TS-E 17mm f/4L"; break;
                            case 95: ret = "Canon TS-E 24.0mm f/3.5 L II"; break;
                            case 124: ret = "Canon MP-E 65mm f/2.8 1-5x Macro Photo"; break;
                            case 125: ret = "Canon TS-E 24mm f/3.5L"; break;
                            case 126: ret = "Canon TS-E 45mm f/2.8"; break;
                            case 127: ret = "Canon TS-E 90mm f/2.8"; break;
                            case 129: ret = "Canon EF 300mm f/2.8L"; break;
                            case 130: ret = "Canon EF 50mm f/1.0L"; break;
                            case 131: ret =
                                "Canon EF 28-80mm f/2.8-4L" + OrNewLine +
                                "Sigma 8mm f/3.5 EX DG Circular Fisheye" + OrNewLine +
                                "Sigma 17-35mm f/2.8-4 EX DG Aspherical HSM" + OrNewLine +
                                "Sigma 17-70mm f/2.8-4.5 DC Macro" + OrNewLine +
                                "Sigma APO 50-150mm f/2.8 [II] EX DC HSM" + OrNewLine +
                                "Sigma APO 120-300mm f/2.8 EX DG HSM" + OrNewLine +
                                "Sigma 4.5mm F2.8 EX DC HSM Circular Fisheye"; break;
                            case 132: ret = "Canon EF 1200mm f/5.6L"; break;
                            case 134: ret = "Canon EF 600mm f/4L IS"; break;
                            case 135: ret = "Canon EF 200mm f/1.8L"; break;
                            case 136: ret = "Canon EF 300mm f/2.8L"; break;
                            case 137: ret =
                                "Canon EF 85mm f/1.2L" + OrNewLine +
                                "Sigma 18-50mm f/2.8-4.5 DC OS HSM" + OrNewLine +
                                "Sigma 50-200mm f/4-5.6 DC OS HSM" + OrNewLine +
                                "Sigma 18-250mm f/3.5-6.3 DC OS HSM" + OrNewLine +
                                "Sigma 24-70mm f/2.8 IF EX DG HSM" + OrNewLine +
                                "Sigma 18-125mm f/3.8-5.6 DC OS HSM" + OrNewLine +
                                "Sigma 17-70mm f/2.8-4 DC Macro OS HSM" + OrNewLine +
                                "Sigma 17-50mm f/2.8 OS HSM" + OrNewLine +
                                "Sigma 18-200mm f/3.5-6.3 II DC OS HSM" + OrNewLine +
                                "Tamron AF 18-270mm f/3.5-6.3 Di II VC PZD" + OrNewLine +
                                "Sigma 8-16mm f/4.5-5.6 DC HSM" + OrNewLine +
                                "Tamron SP 17-50mm f/2.8 XR Di II VC" + OrNewLine +
                                "Tamron SP 60mm f/2 Macro Di II" + OrNewLine +
                                "Sigma 10-20mm f/3.5 EX DC HSM"; break;
                            case 138: ret = "Canon EF 28-80mm f/2.8-4L"; break;
                            case 139: ret = "Canon EF 400mm f/2.8L"; break;
                            case 140: ret = "Canon EF 500mm f/4.5L"; break;
                            case 141: ret = "Canon EF 500mm f/4.5L"; break;
                            case 142: ret = "Canon EF 300mm f/2.8L IS"; break;
                            case 143: ret = "Canon EF 500mm f/4L IS"; break;
                            case 144: ret = "Canon EF 35-135mm f/4-5.6 USM"; break;
                            case 145: ret = "Canon EF 100-300mm f/4.5-5.6 USM"; break;
                            case 146: ret = "Canon EF 70-210mm f/3.5-4.5 USM"; break;
                            case 147: ret = "Canon EF 35-135mm f/4-5.6 USM"; break;
                            case 148: ret = "Canon EF 28-80mm f/3.5-5.6 USM"; break;
                            case 149: ret = "Canon EF 100mm f/2 USM"; break;
                            case 150: ret =
                                "Canon EF 14mm f/2.8L" + OrNewLine +
                                "Sigma 20mm EX f/1.8" + OrNewLine +
                                "Sigma 30mm f/1.4 DC HSM" + OrNewLine +
                                "Sigma 24mm f/1.8 DG Macro EX"; break;
                            case 151: ret = "Canon EF 200mm f/2.8L"; break;
                            case 152: ret =
                                "Canon EF 300mm f/4L IS" + OrNewLine +
                                "Sigma 12-24mm f/4.5-5.6 EX DG ASPHERICAL HSM" + OrNewLine +
                                "Sigma 14mm f/2.8 EX Aspherical HSM" + OrNewLine +
                                "Sigma 10-20mm f/4-5.6" + OrNewLine +
                                "Sigma 100-300mm f/4"; break;
                            case 153: ret =
                                "Canon EF 35-350mm f/3.5-5.6L" + OrNewLine +
                                "Sigma 50-500mm f/4-6.3 APO HSM EX" + OrNewLine +
                                "Tamron AF 28-300mm f/3.5-6.3 XR LD Aspherical [IF] Macro" + OrNewLine +
                                "Tamron AF 18-200mm f/3.5-6.3 XR Di II LD Aspherical [IF] Macro Model A14" + OrNewLine +
                                "Tamron 18-250mm f/3.5-6.3 Di II LD Aspherical [IF] Macro"; break;
                            case 154: ret = "Canon EF 20mm f/2.8 USM"; break;
                            case 155: ret = "Canon EF 85mm f/1.8 USM"; break;
                            case 156: ret =
                                "Canon EF 28-105mm f/3.5-4.5 USM" + OrNewLine +
                                "Tamron SP 70-300mm f/4.0-5.6 Di VC USD"; break;
                            case 160: ret =
                                "Canon EF 20-35mm f/3.5-4.5 USM" + OrNewLine +
                                "Tamron AF 19-35mm f/3.5-4.5" + OrNewLine +
                                "Tokina AT-X 124 AF Pro DX 12-24mm f/4" + OrNewLine +
                                "Tokina AT-X 107 AF DX 10-17mm f/3.5-4.5 Fisheye" + OrNewLine +
                                "Tokina AT-X 116 AF Pro DX 11-16mm f/2.8"; break;
                            case 161: ret =
                                "Canon EF 28-70mm f/2.8L" + OrNewLine +
                                "Sigma 24-70mm f/2.8 EX" + OrNewLine +
                                "Sigma 28-70mm f/2.8 EX" + OrNewLine +
                                "Tamron AF 17-50mm f/2.8 Di-II LD Aspherical" + OrNewLine +
                                "Tamron 90mm f/2.8"; break;
                            case 162: ret = "Canon EF 200mm f/2.8L"; break;
                            case 163: ret = "Canon EF 300mm f/4L"; break;
                            case 164: ret = "Canon EF 400mm f/5.6L"; break;
                            case 165: ret = "Canon EF 70-200mm f/2.8 L"; break;
                            case 166: ret = "Canon EF 70-200mm f/2.8 L + 1.4x"; break;
                            case 167: ret = "Canon EF 70-200mm f/2.8 L + 2x"; break;
                            case 168: ret = "Canon EF 28mm f/1.8 USM"; break;
                            case 169: ret =
                                "Canon EF 17-35mm f/2.8L" + OrNewLine +
                                "Sigma 18-200mm f/3.5-6.3 DC OS" + OrNewLine +
                                "Sigma 15-30mm f/3.5-4.5 EX DG Aspherical" + OrNewLine +
                                "Sigma 18-50mm f/2.8 Macro" + OrNewLine +
                                "Sigma 50mm f/1.4 EX DG HSM" + OrNewLine +
                                "Sigma 85mm f/1.4 EX DG HSM" + OrNewLine +
                                "Sigma 30mm f/1.4 EX DC HSM"; break;
                            case 170: ret = "Canon EF 200mm f/2.8L II"; break;
                            case 171: ret = "Canon EF 300mm f/4L"; break;
                            case 172: ret = "Canon EF 400mm f/5.6L"; break;
                            case 173: ret =
                                "Canon EF 180mm Macro f/3.5L" + OrNewLine +
                                "Sigma 180mm EX HSM Macro f/3.5" + OrNewLine +
                                "Sigma APO Macro 150mm f/2.8 EX DG HSM"; break;
                            case 174: ret =
                                "Canon EF 135mm f/2L" + OrNewLine +
                                "Sigma 70-200mm f/2.8 EX DG APO OS HSM" + OrNewLine +
                                "Sigma 50-500mm f/4.5-6.3 APO DG OS HSM"; break;
                            case 175: ret = "Canon EF 400mm f/2.8L"; break;
                            case 176: ret = "Canon EF 24-85mm f/3.5-4.5 USM"; break;
                            case 177: ret = "Canon EF 300mm f/4L IS"; break;
                            case 178: ret = "Canon EF 28-135mm f/3.5-5.6 IS"; break;
                            case 179: ret = "Canon EF 24mm f/1.4L"; break;
                            case 180: ret = "Canon EF 35mm f/1.4L"; break;
                            case 181: ret = "Canon EF 100-400mm f/4.5-5.6L IS + 1.4x"; break;
                            case 182: ret = "Canon EF 100-400mm f/4.5-5.6L IS + 2x"; break;
                            case 183: ret =
                                 "Canon EF 100-400mm f/4.5-5.6L IS" + OrNewLine +
                                 "Sigma 150mm f/2.8 EX DG OS HSM APO Macro"; break;
                            case 184: ret = "Canon EF 400mm f/2.8L + 2x"; break;
                            case 185: ret = "Canon EF 600mm f/4L IS"; break;
                            case 186: ret = "Canon EF 70-200mm f/4L"; break;
                            case 187: ret = "Canon EF 70-200mm f/4L + 1.4x"; break;
                            case 188: ret = "Canon EF 70-200mm f/4L + 2x"; break;
                            case 189: ret = "Canon EF 70-200mm f/4L + 2.8x"; break;
                            case 190: ret = "Canon EF 100mm f/2.8 Macro"; break;
                            case 191: ret = "Canon EF 400mm f/4 DO IS"; break;
                            case 193: ret = "Canon EF 35-80mm f/4-5.6 USM"; break;
                            case 194: ret = "Canon EF 80-200mm f/4.5-5.6 USM"; break;
                            case 195: ret = "Canon EF 35-105mm f/4.5-5.6 USM"; break;
                            case 196: ret = "Canon EF 75-300mm f/4-5.6 USM"; break;
                            case 197: ret = "Canon EF 75-300mm f/4-5.6 IS USM"; break;
                            case 198: ret = "Canon EF 50mm f/1.4 USM"; break;
                            case 199: ret = "Canon EF 28-80mm f/3.5-5.6 USM"; break;
                            case 200: ret = "Canon EF 75-300mm f/4-5.6 USM"; break;
                            case 201: ret = "Canon EF 28-80mm f/3.5-5.6 USM"; break;
                            case 202: ret = "Canon EF 28-80mm f/3.5-5.6 USM IV"; break;
                            case 208: ret = "Canon EF 22-55mm f/4-5.6 USM"; break;
                            case 209: ret = "Canon EF 55-200mm f/4.5-5.6"; break;
                            case 210: ret = "Canon EF 28-90mm f/4-5.6 USM"; break;
                            case 211: ret = "Canon EF 28-200mm f/3.5-5.6 USM"; break;
                            case 212: ret = "Canon EF 28-105mm f/4-5.6 USM"; break;
                            case 213: ret = "Canon EF 90-300mm f/4.5-5.6 USM"; break;
                            case 214: ret = "Canon EF-S 18-55mm f/3.5-5.6 USM"; break;
                            case 215: ret = "Canon EF 55-200mm f/4.5-5.6 II USM"; break;
                            case 224: ret = "Canon EF 70-200mm f/2.8L IS"; break;
                            case 225: ret = "Canon EF 70-200mm f/2.8L IS + 1.4x"; break;
                            case 226: ret = "Canon EF 70-200mm f/2.8L IS + 2x"; break;
                            case 227: ret = "Canon EF 70-200mm f/2.8L IS + 2.8x"; break;
                            case 228: ret = "Canon EF 28-105mm f/3.5-4.5 USM"; break;
                            case 229: ret = "Canon EF 16-35mm f/2.8L"; break;
                            case 230: ret = "Canon EF 24-70mm f/2.8L"; break;
                            case 231: ret = "Canon EF 17-40mm f/4L"; break;
                            case 232: ret = "Canon EF 70-300mm f/4.5-5.6 DO IS USM"; break;
                            case 233: ret = "Canon EF 28-300mm f/3.5-5.6L IS"; break;
                            case 234: ret = "Canon EF-S 17-85mm f4-5.6 IS USM"; break;
                            case 235: ret = "Canon EF-S 10-22mm f/3.5-4.5 USM"; break;
                            case 236: ret = "Canon EF-S 60mm f/2.8 Macro USM"; break;
                            case 237: ret = "Canon EF 24-105mm f/4L IS"; break;
                            case 238: ret = "Canon EF 70-300mm f/4-5.6 IS USM"; break;
                            case 239: ret = "Canon EF 85mm f/1.2L II"; break;
                            case 240: ret = "Canon EF-S 17-55mm f/2.8 IS USM"; break;
                            case 241: ret = "Canon EF 50mm f/1.2L"; break;
                            case 242: ret = "Canon EF 70-200mm f/4L IS"; break;
                            case 243: ret = "Canon EF 70-200mm f/4L IS + 1.4x"; break;
                            case 244: ret = "Canon EF 70-200mm f/4L IS + 2x"; break;
                            case 245: ret = "Canon EF 70-200mm f/4L IS + 2.8x"; break;
                            case 246: ret = "Canon EF 16-35mm f/2.8L II"; break;
                            case 247: ret = "Canon EF 14mm f/2.8L II USM"; break;
                            case 248: ret = "Canon EF 200mm f/2L IS"; break;
                            case 249: ret = "Canon EF 800mm f/5.6L IS"; break;
                            case 250: ret = "Canon EF 24 f/1.4L II"; break;
                            case 251: ret = "Canon EF 70-200mm f/2.8L IS II USM"; break;
                            case 252: ret = "Canon EF 70-200mm f/2.8L IS II USM + 1.4x"; break;
                            case 253: ret = "Canon EF 70-200mm f/2.8L IS II USM + 2x"; break;
                            case 254: ret = "Canon EF 100mm f/2.8L Macro IS USM"; break;
                            case 488: ret = "Canon EF-S 15-85mm f/3.5-5.6 IS USM"; break;
                            case 489: ret = "Canon EF 70-300mm f/4-5.6L IS USM"; break;
                            case 490: ret = "Canon EF 8-15mm f/4L USM"; break;
                            case 491: ret = "Canon EF 300mm f/2.8L IS II USM"; break;
                            case 494: ret = "Canon EF 600mm f/4.0L IS II USM"; break;
                            case 495: ret = "Canon EF 24-70mm f/2.8L II USM"; break;
                            case 4142: ret = "Canon EF-S 18-135mm f/3.5-5.6 IS STM"; break;
                            case 4143: ret = "Canon EF-M 18-55mm f/3.5-5.6 IS STM"; break;
                            case 4144: ret = "Canon EF 40mm f/2.8 STM"; break;
                            case 4145: ret = "Canon EF-M 22mm f/2 STM"; break;
                        }
                        //ret = string.Format("{0:X}", value);
                        break;
                        #endregion
                    }
                case 26: //MaxAperture
                case 27: {//MinAperture
                        ret = ExifFunc.GetAPEXFNumber(value, 32);// string.Format("{0:0.0}", Math.Pow(Math.Sqrt(2), (double)value / (double)32));
                        //if (ret.Substring(ret.Length - 2, 2) == ".0") { ret = ret.Split(new char[1] { '.' })[0]; }
                        break;
                    }
                case 23: //MaxFocalLength
                case 24: //MinFocalLength
                    ret = value.ToString();
                    break;
                case 25: {//FocalUnits
                        try {
                            _MakerNoteList["MaxFocalLength"] = string.Format("{0:0.0}", float.Parse(_MakerNoteList["MaxFocalLength"]) / (float)value) + " mm";
                            _MakerNoteList["MinFocalLength"] = string.Format("{0:0.0}", float.Parse(_MakerNoteList["MinFocalLength"]) / (float)value) + " mm";
                        }
                        catch { }
                        FocalUnits = value;
                        ret = value.ToString() + "/mm";
                        break;
                    }
                case 28: {//FlashActivity
                        ret = value.ToString();
                        break;
                    }
                case 29: {//FlashBits
                        ret = string.Empty;
                        string by = extensions.Functions.Func.FillChar(Convert.ToString(value, 2), 16, "0");
                        int idx = 0;
                        for (int i = by.Length - 1; i >= 0; i--) {
                            if (by.Substring(i, 1) == "1") {
                                switch (idx) {
                                    case 0: ret += "Manual,"; break;
                                    case 1: ret += "TTL,"; break;
                                    case 2: ret += "A-TTL,"; break;
                                    case 3: ret += "E-TTL,"; break;
                                    case 4: ret += "FP sync enabled,"; break;
                                    case 7: ret += "2nd-curtain sync used,"; break;
                                    case 11: ret += "FP sync used,"; break;
                                    case 13: ret += "Built-in,"; break;
                                    case 14: ret += "External,"; break;
                                }
                            }
                            idx++;
                        }
                        if (ret.Length > 2) ret = ret.Substring(0, ret.Length - 1);
                        else ret = "(none)";
                        break;
                    }
                case 32: { //FocusContinuous
                        switch (value) {
                            case 0: ret = "Single"; break;
                            case 1: ret = "Continuous"; break;
                            case 8: ret = "Manual"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 33: { //AESetting
                        switch (value) {
                            case 0: ret = "Normal AE"; break;
                            case 1: ret = "Exposure Compensation"; break;
                            case 2: ret = "AE Lock"; break;
                            case 3: ret = "AE Lock + Exposure Comp."; break;
                            case 4: ret = "No AE"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 34: { //ImageStabilization
                        switch (value) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "On"; break;
                            case 2: ret = "Shoot Only"; break;
                            case 3: ret = "Panning"; break;
                            case 4: ret = "Dynamic"; break;
                            case 256: ret = "Off (2)"; break;
                            case 257: ret = "On (2)"; break;
                            case 258: ret = "Shoot Only (2)"; break;
                            case 259: ret = "Panning (2)"; break;
                            case 260: ret = "Dynamic (2)"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 35: { //DisplayAperture
                        ushort v = IntToUShort(value);
                        switch (v) {
                            case 0: ret = string.Empty; break;
                            case 15: ret = "1.5"; break;
                            case 24: ret = "2.4"; break;
                            case 26: ret = "2.6"; break;
                            case 27: ret = "2.7"; break;
                            case 28: ret = "2.8"; break;
                            case 30: ret = "3"; break;
                            case 31: ret = "3.1"; break;
                            case 32: ret = "3.2"; break;
                            case 34: ret = "3.4"; break;
                            case 35: ret = "3.5"; break;
                            case 36: ret = "3.6"; break;
                            case 38: ret = "3.8"; break;
                            case 39: ret = "3.9"; break;
                            case 40: ret = "4"; break;
                            case 41: ret = "4.1"; break;
                            case 43: ret = "4.3"; break;
                            case 45: ret = "4.5"; break;
                            case 47: ret = "4.7"; break;
                            case 48: ret = "4.8"; break;
                            case 49: ret = "4.9"; break;
                            case 53: ret = "5.3"; break;
                            case 54: ret = "5.4"; break;
                            case 55: ret = "5.5"; break;
                            case 56: ret = "5.6"; break;
                            case 57: ret = "5.7"; break;
                            case 58: ret = "5.8"; break;
                            case 59: ret = "5.9"; break;
                            case 71: ret = "7.1"; break;
                            case 72: ret = "7.2"; break;
                            case 80: ret = "8"; break;
                            case 90: ret = "9"; break;
                            case 100: ret = "10"; break;
                            case 110: ret = "11"; break;
                            case 130: ret = "13"; break;
                            case 140: ret = "14"; break;
                            default: ret = ExifFunc.GetAPEXFNumber(v, 32); break;
                        }
                        break;
                    }
                case 36: //ZoomSourceWidth
                case 37: {//ZoomTargetWidth
                        ushort v = IntToUShort(value);
                        ret = v.ToString();
                        break;
                    }
                case 39: {//SpotMeteringMode
                        switch (value) {
                            case 0: ret = "Center"; break;
                            case 1: ret = "AF Point"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 40: { //PhotoEffect
                        switch (value) {
                            case 0: ret = "Off"; break;
                            case 1: ret = "Vivid"; break;
                            case 2: ret = "Neutral"; break;
                            case 3: ret = "Smooth"; break;
                            case 4: ret = "Sepia"; break;
                            case 5: ret = "B&W"; break;
                            case 6: ret = "Custom"; break;
                            case 100: ret = "My Color Data"; break;
                            default: ret = string.Empty; break;
                        }
                        break;
                    }
                case 41: { //ManualFlashOutput
                        ushort v = IntToUShort(value);
                        switch (v) {
                            case 0x0: ret = "n/a"; break;
                            case 0x500: ret = "Full"; break;
                            case 0x502: ret = "Medium"; break;
                            case 0x504: ret = "Low"; break;
                            case 0x7fff: ret = "n/a"; break;
                        }
                        break;
                    }
                case 42: { //ColorTone
                        short v = IntToShort(value);
                        switch (v) {
                            case 0: ret = "Normal"; break;
                            case 32767: ret = "n/a"; break;
                            default: ret = value.ToString(); break;
                        }
                        break;
                    }
                case 46: { //SRAWQuality
                        switch (value) {
                            case 0: ret = "n/a"; break;
                            case 1: ret = "sRAW1 (mRAW)"; break;
                            case 2: ret = "sRAW2 (sRAW)"; break;
                        }
                        break;
                    }
            }
            return ret;
        }
    }
}
