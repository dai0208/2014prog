using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace display
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region ボタン操作
        private void btnOK_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pbxShow.Image = new Bitmap(ofd.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pbxShow.Image == null)
            {
                MessageBox.Show("画像を読み込んでいません。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "bmpファイル(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pbxShow.Image.Save(sfd.FileName);
            }
            MessageBox.Show("保存が終わりました。");
        }

        private void btnGrayScale_Click(object sender, EventArgs e)
        {
            if (pbxShow.Image == null)
            {
                MessageBox.Show("画像を読み込んでいません。");
                return;
            }

            pbxShow.Image = GrayScale(new Bitmap(pbxShow.Image));
        }

        private void btnNegaPosi_Click(object sender, EventArgs e)
        {
            if (pbxShow.Image == null)
            {
                MessageBox.Show("画像を読み込んでいません。");
                return;
            }

            pbxShow.Image = ReverseColor(new Bitmap(pbxShow.Image));

        }

        private void btnMosaic_Click(object sender, EventArgs e)
        {
            if (pbxShow.Image == null)
            {
                MessageBox.Show("画像を読み込んでいません。");
                return;
            }

            pbxShow.Image = Mosaic(new Bitmap(pbxShow.Image),(int)nmcMosaHeight.Value,(int)nmcMosaWidth.Value);
        }
        #endregion

        #region メソッド
        /// <summary>
        /// ｸﾞﾚｲｽｹｰﾙ化を行います。
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        private Bitmap GrayScale(Bitmap GrayImage)
        {
            for (int row = 0; row < GrayImage.Height; row++)
                for (int col = 0; col < GrayImage.Width; col++)
                {
                    int Red = GrayImage.GetPixel(col, row).R;
                    int Green = GrayImage.GetPixel(col, row).G;
                    int Blue = GrayImage.GetPixel(col, row).B;

                    int Gray = (Red + Green + Blue) / 3;

                    //指定画素に色を設定
                    GrayImage.SetPixel(col, row, Color.FromArgb(Gray, Gray, Gray));

                }
            return GrayImage;
        }

        /// <summary>
        /// 反転(ﾈｶﾞﾎﾟｼﾞ)画像の生成を行います。
        /// </summary>
        /// <param name="revImage"></param>
        /// <returns></returns>
        private Bitmap ReverseColor(Bitmap revImage)
        {
            for (int row = 0; row < revImage.Height; row++)
                for (int col = 0; col < revImage.Width; col++)
                {
                    revImage.SetPixel(col, row, Color.FromArgb(255 - revImage.GetPixel(col, row).R, 255 - revImage.GetPixel(col, row).G,255 - revImage.GetPixel(col, row).B));
                }
            return revImage;
        }

        /// <summary>
        /// モザイク画像の生成を行います。
        /// </summary>
        /// <param name="mosaImage">モザイク対象画像</param>
        /// <param name="mosaSize">モザイク後画像の分割サイズ。画像サイズより大きくならないようにしてください。</param>
        /// <returns></returns>
        private Bitmap Mosaic(Bitmap mosaImage, int mosaHeight, int mosaWidth)
        {
            Bitmap dstMosaImage = new Bitmap(mosaImage.Width, mosaImage.Height);
            for (int dstRow = 0; dstRow < mosaImage.Height; dstRow += mosaHeight)
            {
                for (int dstCol = 0; dstCol < mosaImage.Width; dstCol += mosaWidth)
                {
                    int R = 0; int G = 0; int B = 0;
                    int BlockWidth = mosaWidth; int BlockHeight = mosaHeight;

                    //ブロック内代表画素の算出
                    for (int blockRow = 0; blockRow < mosaHeight; blockRow++)
                    {
                        if ((blockRow + dstRow) >= mosaImage.Height-1)
                        {
                            BlockHeight = blockRow+1;
                            break;
                        }
                        for (int blockCol = 0; blockCol < mosaWidth; blockCol++)
                        {
                            if ((blockCol + dstCol) >= mosaImage.Width-1)
                            {
                                BlockWidth = blockCol+1;
                                break;
                            }
                            Color color = mosaImage.GetPixel(blockCol + dstCol, blockRow + dstRow);
                            R += color.R;
                            G += color.G;
                            B += color.B;
                        }
                    }
                    R /= BlockWidth * BlockHeight;
                    G /= BlockWidth * BlockHeight;
                    B /= BlockWidth * BlockHeight;

                    //画素値のセット
                    for (int blockRow = 0; blockRow < BlockHeight; blockRow++)
                    {
                        for (int blockCol = 0; blockCol < BlockWidth; blockCol++)
                        {
                            mosaImage.SetPixel(blockCol+dstCol,blockRow+dstRow,Color.FromArgb(R,G,B));
                        }
                    }
                }
            }
            return mosaImage;
        }
        #endregion
    }
}
