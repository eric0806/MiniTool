using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace MiniTool
{
    public static class ImageFunc
    {
        //public static Dictionary<string, Dictionary<string, string>> ImageList = new Dictionary<string, Dictionary<string, string>>();
        public static List<List<string>> ImageList = new List<List<string>>();

        /// <summary>
        /// [0]=FileName [1]=拍照日期 [2]=額外資料夾名稱
        /// </summary>
        public static List<List<string>> SelectedImageList = new List<List<string>>();



        /// <summary>
        /// 取得縮圖區正確的顯示尺寸
        /// </summary>
        /// <param name="SourceImg">原始圖</param>
        /// <param name="ThumbWidth">要縮成的寬度</param>
        /// <param name="ThumbHeight">要縮成的高度</param>
        /// <returns></returns>
        public static Image GetThumb(Bitmap SourceImg, int ThumbWidth, int ThumbHeight) {
            Image TargetImg = new Bitmap(ThumbWidth, ThumbHeight);
            Image tmp = null;
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            int NewWidth = 0;
            int NewHeight = 0;
            int XOffset = 0;
            int YOffset = 0;

            //比例會有小數，如果用int會無法真正判斷是否相等，造成縮圖錯亂
            float ThumbScale = (float)ThumbWidth / (float)ThumbHeight;
            float SourceScale = (float)SourceImg.Width / (float)SourceImg.Height;

            if (SourceScale == ThumbScale) {
                //圖片的長寬比例與Box相同，w h直接給縮圖的尺寸即可
                NewWidth = ThumbWidth;
                NewHeight = ThumbHeight;
            }
            if (SourceScale > ThumbScale) { //圖片寬高比大於縮圖寬高比，將w指定為ThumbWidth
                NewWidth = ThumbWidth;
                //a:b=c:x  x = b*c/a，國小數學
                NewHeight = (int)(SourceImg.Height * NewWidth / SourceImg.Width);
            }
            if (SourceScale < ThumbScale) { //圖片寬高比小於縮圖寬高比，將h指定為ThumbHeight
                NewHeight = ThumbHeight;
                NewWidth = (int)(SourceImg.Width * NewHeight / SourceImg.Height);
            }

            tmp = SourceImg.GetThumbnailImage(NewWidth, NewHeight, myCallback, IntPtr.Zero);
            XOffset = (int)((ThumbWidth - NewWidth) / 2);
            YOffset = (int)((ThumbHeight - NewHeight) / 2);

            using (Graphics g = Graphics.FromImage(TargetImg)) {
                g.DrawImage(tmp, XOffset, YOffset, tmp.Width, tmp.Height);
                g.DrawRectangle(Pens.Transparent, 0, 0, TargetImg.Width - 1, TargetImg.Height - 1);
            }

            return TargetImg;
        }
        private static bool ThumbnailCallback() {
            return true;
        }

    }
}
