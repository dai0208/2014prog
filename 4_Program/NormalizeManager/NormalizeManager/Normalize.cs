using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NormalizeManager
{
    public class Normalize
    {
        private Bitmap srcImage;
        private Bitmap dstImage;

        #region コンストラクタ
        public Normalize(Bitmap srcImage)
        {
            this.srcImage = srcImage;
        }

        #endregion

        #region ゲッターセッター
        private Bitmap setSrcImage
        {
            set{ srcImage = value; }
        }

        public Bitmap getSrcImage
        {
            get{ return srcImage; }
        }

        private Bitmap setDstImage
        {
            set{ dstImage = value; }
        }

        public Bitmap getDstImage
        {
            get{ return dstImage; }
        }
        #endregion

        #region クラスメソッド
        /// <summary>
        /// Bitmap全画素の平均値を求めます。
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        static public double calcMean(Bitmap bmp)
        {
            double R = 0.0;
            double G = 0.0;
            double B = 0.0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    R += color.R;
                    G += color.G;
                    B += color.B;
                }
            }
            return (double)((R + G + B)/ (3 * bmp.Height * bmp.Width));
        }

        /// <summary>
        /// Bitmap全画素の標準偏差を求めます。
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        static public double calcDiv(Bitmap bmp)
        {
            double R = 0.0;
            double G = 0.0;
            double B = 0.0;
            double mean = calcMean(bmp);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    R += Math.Pow((color.R - (double)mean), 2.0);
                    G += Math.Pow((color.G - (double)mean), 2.0);
                    B += Math.Pow((color.B - (double)mean), 2.0);
                }
            }
            R /= (bmp.Height * bmp.Width);
            G /= (bmp.Height * bmp.Width);
            B /= (bmp.Height * bmp.Width);
            return (Math.Sqrt(R) + Math.Sqrt(G) + Math.Sqrt(B) )/3.0;
        }

        /// <summary>
        /// 濃淡値の正規化を行ってdstImageにセットします。
        /// </summary>
        /// <param name="aftmean">正規化後の平均値</param>
        /// <param name="aftdiv">正規化後の標準偏差</param>
        public void normalizing(double aftmean, double aftdiv)
        {
            double bfmean = calcMean(srcImage);
            double bfdiv = calcDiv(srcImage);

            dstImage = new Bitmap(srcImage.Width, srcImage.Height);

            for (int y = 0; y < srcImage.Height; y++)
            {
                for (int x = 0; x < srcImage.Width; x++)
                {
                    Color color = srcImage.GetPixel(x, y);
                    double R = aftdiv * ((color.R - bfmean) / bfdiv) + aftmean;

                    //最大・最小化処理
                    if (R > 255) R = 255;
                    if (R < 0) R = 0;

                    double G = aftdiv * ((color.G - bfmean) / bfdiv) + aftmean;

                    //最大・最小化処理
                    if (G > 255) G = 255;
                    if (G < 0) G = 0;

                    double B = aftdiv * ((color.B - bfmean) / bfdiv) + aftmean;

                    //最大・最小化処理
                    if (B > 255) B = 255;
                    if (B < 0) B = 0;

                    dstImage.SetPixel(x, y, Color.FromArgb((int)R, (int)G, (int)B));
                }
            }
        }
        #endregion
    }
}
