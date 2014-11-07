using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace cBitmap
{
    /// <summary>
    /// Bitmap処理関係をまとめたクラスです。
    /// </summary>
    public class cBitmap
    {
        /// <summary>
        /// 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
        /// </summary>
        /// <param name="bmpSent">指定したBitmap画像</param>
        /// <param name="iX">横幅</param>
        /// <param name="iY">高さ</param>
        /// <returns>変形されたBitmap画像</returns>
        static public Bitmap bmpStretchImage(Bitmap bmpSent, int iWidth, int iHeight)
        {
            #region 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
            if (bmpSent.Width == iWidth && bmpSent.Height == iHeight)
                return bmpSent;

            Bitmap bmpWork = new Bitmap(iWidth, iHeight);
            float fVRatio, fHRatio;
            int iX, iY;
            if (bmpSent.Height > bmpSent.Width)
            {
                fVRatio = (float)bmpSent.Width / (float)bmpSent.Height;
                fHRatio = 1.0f;
                iX = (int)((iWidth - iWidth * fVRatio) / 2);
                iY = 0;
            }
            else
            {
                fVRatio = 1.0f;
                fHRatio = (float)bmpSent.Height / (float)bmpSent.Width;
                iX = 0;
                iY = (int)((iHeight - iHeight * fHRatio) / 2);
            }

            Graphics gDraw = Graphics.FromImage(bmpWork);
            gDraw.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gDraw.DrawImage(bmpSent, iX, iY, iWidth * fVRatio, iHeight * fHRatio);
            gDraw.Flush();
            gDraw.Dispose();

            return bmpWork;
            #endregion
        }

        /// <summary>
        /// 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
        /// </summary>
        /// <param name="bmpSent">指定したBitmap画像</param>
        /// <param name="Size">サイズ</param>
        /// <returns>変形されたBitmap画像</returns>
        static public Bitmap bmpStretchImage(Bitmap bmpSent, Size Size)
        {
            return bmpStretchImage(bmpSent, Size.Width, Size.Height);
        }
    }
}
