using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CvUtil;
using OpenCvSharp;

namespace OpenCVDFT
{
    public partial class Form1 : Form
    {
        private string fName;

        public Form1()
        {
            InitializeComponent();
        }

        #region メソッド
        public static Bitmap DFT(string fName)
        {
            // 離散フーリエ変換を用いて，振幅画像を生成する．

            using (IplImage src_img = new IplImage(fName, LoadMode.GrayScale))
            using (IplImage realInput = Cv.CreateImage(src_img.Size, BitDepth.F64, 1))
            using (IplImage imaginaryInput = Cv.CreateImage(src_img.Size, BitDepth.F64, 1))
            using (IplImage complexInput = Cv.CreateImage(src_img.Size, BitDepth.F64, 2))
            {
                // (1)入力画像を実数配列にコピーし，虚数配列とマージして複素数平面を構成
                Cv.Scale(src_img, realInput, 1.0, 0.0);
                Cv.Zero(imaginaryInput);
                Cv.Merge(realInput, imaginaryInput, null, null, complexInput);

                // (2)DFT用の最適サイズを計算し，そのサイズで行列を確保する
                int dft_M = Cv.GetOptimalDFTSize(src_img.Height - 1);
                int dft_N = Cv.GetOptimalDFTSize(src_img.Width - 1);
                using (CvMat dft_A = Cv.CreateMat(dft_M, dft_N, MatrixType.F64C2))
                using (IplImage image_Re = new IplImage(new CvSize(dft_N, dft_M), BitDepth.F64, 1))
                using (IplImage image_Im = new IplImage(new CvSize(dft_N, dft_M), BitDepth.F64, 1))
                {
                    // (3)複素数平面をdft_Aにコピーし，残りの行列右側部分を0で埋める
                    CvMat tmp;
                    Cv.GetSubRect(dft_A, out tmp, new CvRect(0, 0, src_img.Width, src_img.Height));
                    Cv.Copy(complexInput, tmp, null);
                    if (dft_A.Cols > src_img.Width)
                    {
                        Cv.GetSubRect(dft_A, out tmp, new CvRect(src_img.Width, 0, dft_A.Cols - src_img.Width, src_img.Height));
                        Cv.Zero(tmp);
                    }

                    // (4)離散フーリエ変換を行い，その結果を実数部分と虚数部分に分解
                    Cv.DFT(dft_A, dft_A, DFTFlag.Forward, complexInput.Height);
                    Cv.Split(dft_A, image_Re, image_Im, null, null);

                    // (5)スペクトルの振幅を計算 Mag = sqrt(Re^2 + Im^2)
                    Cv.Pow(image_Re, image_Re, 2.0);
                    Cv.Pow(image_Im, image_Im, 2.0);
                    Cv.Add(image_Re, image_Im, image_Re, null);
                    Cv.Pow(image_Re, image_Re, 0.5);

                    // (6)振幅の対数をとる log(1 + Mag) 
                    Cv.AddS(image_Re, CvScalar.ScalarAll(1.0), image_Re, null);
                    Cv.Log(image_Re, image_Re);

                    // (7)原点（直流成分）が画像の中心にくるように，画像の象限を入れ替える
                    cvShiftDFT(image_Re, image_Re);

                    // (8)振幅画像のピクセル値が0.0-1.0に分布するようにスケーリング
                    double m, M;
                    Cv.MinMaxLoc(image_Re, out m, out M);
                    Cv.Scale(image_Re, image_Re, 1.0 / (M - m), 1.0 * (-m) / (M - m));
                    return image_Re.ToBitmap();
                }
            }
        }


        /// <summary>
        /// 原点（直流成分）が画像の中心にくるように，画像の象限を入れ替える関数.
        /// src_arr, dst_arr は同じサイズ，タイプの配列.
        /// </summary>
        /// <param name="src_arr"></param>
        /// <param name="dst_arr"></param>
        private static void cvShiftDFT(CvArr src_arr, CvArr dst_arr)
        {
            CvSize size = Cv.GetSize(src_arr);
            CvSize dst_size = Cv.GetSize(dst_arr);
            if (dst_size.Width != size.Width || dst_size.Height != size.Height)
            {
                Cv.Error(CvStatus.StsUnmatchedSizes, "cvShiftDFT", "Source and Destination arrays must have equal sizes", "SampleDFT.cs", 74);
            }

            // (9)インプレースモード用のテンポラリバッファ
            CvMat tmp = null;
            if (src_arr == dst_arr)
            {
                tmp = Cv.CreateMat(size.Height / 2, size.Width / 2, Cv.GetElemType(src_arr));
            }
            int cx = size.Width / 2;   /* 画像中心 */
            int cy = size.Height / 2;

            // (10)1〜4象限を表す配列と，そのコピー先
            CvMat q1stub, q2stub;
            CvMat q3stub, q4stub;
            CvMat d1stub, d2stub;
            CvMat d3stub, d4stub;
            CvMat q1 = Cv.GetSubRect(src_arr, out q1stub, new CvRect(0, 0, cx, cy));
            CvMat q2 = Cv.GetSubRect(src_arr, out q2stub, new CvRect(cx, 0, cx, cy));
            CvMat q3 = Cv.GetSubRect(src_arr, out q3stub, new CvRect(cx, cy, cx, cy));
            CvMat q4 = Cv.GetSubRect(src_arr, out q4stub, new CvRect(0, cy, cx, cy));
            CvMat d1 = Cv.GetSubRect(src_arr, out d1stub, new CvRect(0, 0, cx, cy));
            CvMat d2 = Cv.GetSubRect(src_arr, out d2stub, new CvRect(cx, 0, cx, cy));
            CvMat d3 = Cv.GetSubRect(src_arr, out d3stub, new CvRect(cx, cy, cx, cy));
            CvMat d4 = Cv.GetSubRect(src_arr, out d4stub, new CvRect(0, cy, cx, cy));

            // (11)実際の象限の入れ替え
            if (src_arr != dst_arr)
            {
                if (!Cv.ARE_TYPES_EQ(q1, d1))
                {
                    Cv.Error(CvStatus.StsUnmatchedFormats, "cvShiftDFT", "Source and Destination arrays must have the same format", "SampleDFT", 98);
                }
                Cv.Copy(q3, d1, null);
                Cv.Copy(q4, d2, null);
                Cv.Copy(q1, d3, null);
                Cv.Copy(q2, d4, null);
            }
            else
            {      /* インプレースモード */
                Cv.Copy(q3, tmp, null);
                Cv.Copy(q1, q3, null);
                Cv.Copy(tmp, q1, null);
                Cv.Copy(q4, tmp, null);
                Cv.Copy(q2, q4, null);
                Cv.Copy(tmp, q2, null);
            }
            if (tmp != null)
            {
                tmp.Dispose();
            }
        }
        #endregion

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fName = ofd.FileName;
                pbxBefore.Image = new Bitmap(fName);

            }
        }

        private void btnFFT_Click(object sender, EventArgs e)
        {
            pbxAfter.Image = DFT(fName);
        }
    }
}
