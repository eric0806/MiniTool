using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiniTool
{
    static class DrawBG
    {
        public static Bitmap GetBG(int Width, int Height, bool IsMax) {
            Bitmap bg = new Bitmap(Width, Height); //產生畫布
            Graphics g = Graphics.FromImage(bg);
            
            SolidBrush dark = new SolidBrush(Color.FromArgb(49, 49, 49));
            if (IsMax) {
                g.FillRectangle(dark, 0, 0, Width, Height);
            }
            else {
                //Pen line = new Pen(Color.Red, 2);
                //Pen line = new Pen(Color.FromArgb(174,255,0), 2);
                Pen line = new Pen(Color.WhiteSmoke, 2);
                g.DrawLine(line, new Point(0, 1), new Point(Width, 1)); //上橫線
                g.DrawLine(line, new Point(Width - 1, 0), new Point(Width - 1, Height)); //右直線
                g.DrawLine(line, new Point(1, 0), new Point(1, Height)); //左直線
                g.DrawLine(line, new Point(0, Height - 1), new Point(Width, Height - 1)); //下橫線
                g.FillRectangle(dark, 2, 2, Width - 4, Height - 4);
            }
            return bg;
        }

    }
}
