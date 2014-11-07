using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace aveImageMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region ボタン操作
        
        private void btnCalcAverage_Click(object sender, EventArgs e)
        {
            
            List<Bitmap> bmp = new List<Bitmap>();
                        
            for(int i=0;i<lbxItems.Items.Count;i++)
            {
                //1番目の画像とサイズが異なるものは読み飛ばし
                if (SizeCheck(bmp[i], bmp[0].Width, bmp[0].Height))
                {
                    bmp.Add(new Bitmap(lbxItems.Items[i].ToString()));
                }
            }
            pbxDstImage.Image = calcAveImage(bmp);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string dir = System.IO.Path.GetFullPath(fbd.SelectedPath);

                //サブディレクトリの中まで検索
                foreach (string exp in new string[] { "*.jpg", "*.bmp" })
                {
                    lbxItems.Items.AddRange(System.IO.Directory.GetFiles(dir, exp, SearchOption.AllDirectories));
                }
            }
        }
        #endregion

        #region メソッド

        /// <summary>
        /// ２枚の画像の平均を求めます。
        /// </summary>
        /// <param name="bmp1">１枚目の画像</param>
        /// <param name="bmp2">２枚目の画像</param>
        /// <returns></returns>
        private Bitmap AverageBetweenTwoImage(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap aveImage = new Bitmap(bmp1.Width, bmp1.Height);
            for (int row = 0; row < bmp1.Height; row++)
            {
                for (int col = 0; col < bmp1.Width; col++)
                {
                    int R = bmp1.GetPixel(col, row).R;
                    int G = bmp1.GetPixel(col, row).G;
                    int B = bmp1.GetPixel(col, row).B;

                    R += bmp2.GetPixel(col, row).R;
                    G += bmp2.GetPixel(col, row).G;
                    B += bmp2.GetPixel(col, row).B;
                    Color cAveImage = Color.FromArgb(R / 2, G / 2, B / 2);
                    aveImage.SetPixel(col, row, cAveImage);
                }
            }
            return aveImage;
        }

        /// <summary>
        /// 複数のBitmap配列の平均画像を出力します。出力画像サイズは一枚目の画像になります。
        /// </summary>
        /// <param name="bmpImage"></param>
        /// <returns></returns>
        private Bitmap calcAveImage(List<Bitmap> bmpImage)
        {
            Bitmap aveImage = new Bitmap(bmpImage[0].Width, bmpImage[0].Height);

            for (int row = 0; row < bmpImage[0].Height; row++)
            {
                for (int col = 0; col < bmpImage[0].Width; col++)
                {
                    int R = 0; int G = 0; int B = 0;

                    for (int imgCnt = 0; imgCnt < bmpImage.Count; imgCnt++)
                    {
                        R += bmpImage[imgCnt].GetPixel(col, row).R;
                        G += bmpImage[imgCnt].GetPixel(col, row).G;
                        B += bmpImage[imgCnt].GetPixel(col, row).B;
                    }

                    aveImage.SetPixel(col, row, Color.FromArgb(R / bmpImage.Count, G / bmpImage.Count, B / bmpImage.Count));
                }
            }
            return aveImage;
        }

        /// <summary>
        /// Bitmapサイズのチェックをします。一致していればTrueを返します。
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="WidthValue"></param>
        /// <param name="HeightValue"></param>
        /// <returns></returns>
        private Boolean SizeCheck(Bitmap Target, int WidthValue, int HeightValue)
        {
            if ((Target.Width == WidthValue) && (Target.Height == HeightValue))
                return true;
            else return false;
        }
        #endregion
    }
}
