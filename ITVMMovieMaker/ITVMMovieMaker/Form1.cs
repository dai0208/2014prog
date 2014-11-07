using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using cBitmap;
using MatrixVector;
using PointFormat;
using AForge.Video.VFW;

namespace ITVMMovieMaker
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 3DASCIIデータ
        /// </summary>
        cPointData[] eDatas;
        
        /// <summary>
        /// Bitmap化された3Dデータ
        /// </summary>
        Bitmap[] fBitmaps;

        public Form1()
        {
            InitializeComponent();
        }

        #region プログレスバー関連
        /// <summary>
        /// プログレスバーの値を増加させます。
        /// </summary>
        protected virtual void vProgressBarValueUp()
        {
            if (pgbMain != null)
            {
                pgbMain.PerformStep();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// プログレスバーをリセットします。
        /// </summary>
        /// <param name="iMax">最大値</param>
        protected virtual void vProgressBarReset(int iMax)
        {
            if (pgbMain != null)
            {
                pgbMain.Maximum = iMax;
                pgbMain.Minimum = 0;
                pgbMain.Value = 0;
                pgbMain.Step = 1;
            }
        }

        /// <summary>
        /// プログレスバーの設定をします。
        /// </summary>
        public virtual ToolStripProgressBar pgbProgressBar
        {
            set { pgbMain = value; }
        }
        #endregion

        #region ドラッグ＆ドロップ関連
        private void lbxMain_DragDrop(object sender, DragEventArgs e)
        {
            lbxMain.Items.Clear();

            /* ファイルまたはディレクトリ内のファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Exists(tempFilePath))
                {
                    lbxMain.Items.Add(tempFilePath);
                }
                else if (Directory.Exists(tempFilePath))
                {
                    string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                    lbxMain.Items.AddRange(files);
                }
                else { }
            }
        }

        private void lbxMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion

        #region 生成系
        /// <summary>
        /// フレームにあたるBitmapを生成します。
        /// </summary>
        /// <param name="Degree">回転角</param>
        public void CreateBitmapsForFrame(int Degree)
        {
            cCreateBitmapFrom3DPointFast[] BitmapMakerArray = new cCreateBitmapFrom3DPointFast[eDatas.Length];
            this.fBitmaps = new Bitmap[eDatas.Length];

            for (int FileIndex = 0; FileIndex < eDatas.Length; FileIndex++)
            {
                eDatas[FileIndex].RotateYDegree(Degree);
                BitmapMakerArray[FileIndex] = new cCreateBitmapFrom3DPointFast(eDatas[FileIndex], 4);
                this.fBitmaps[FileIndex] = new Bitmap(BitmapMakerArray[FileIndex].Bitmap, BitmapMakerArray[FileIndex].Bitmap.Width, BitmapMakerArray[FileIndex].Bitmap.Height);
            }
        }

        /// <summary>
        /// 保持しているfBitmapを使ってAVIファイルを生成します。
        /// </summary>
        public void AutoSave()
        {
            if (eDatas != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "avi files(*.avi)|*.avi";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    AVIWriter aviWriter = new AVIWriter("MSVC");

                    //ここの一行が問題あり、入力画像の縦横とも２の倍数？じゃないといけないらしい
                    aviWriter.Open(sfd.FileName, fBitmaps[0].Width, fBitmaps[0].Height);

                    for (int i = 0; i < fBitmaps.Length; i++)
                    {
                        aviWriter.AddFrame(fBitmaps[i]);
                    }
                    aviWriter.Close();
                }
            }
        }

        public Bitmap[] CutBitmaps(Bitmap[] srcBitmaps, int RangeW, int RangeH)
        {
            Bitmap[] dstBitmaps = new Bitmap[srcBitmaps.Length];

            for (int i = 0; i < srcBitmaps.Length; i++)
                dstBitmaps[i] = CutBitmap(srcBitmaps[i], RangeW, RangeH);

            return dstBitmaps;
        }

        public Bitmap CutBitmap(Bitmap srcBitmap, int RangeW, int RangeH)
        {
            Bitmap CutBitmap = new Bitmap(RangeW, RangeH);
            double[,] cut = new double[RangeW, RangeH];

            for (int i = 0; i < RangeH; i++)
            {
                for (int j = 0; j < RangeW; j++)
                {
                    CutBitmap.SetPixel(j, i, srcBitmap.GetPixel(j, i));
                }
            }
            return CutBitmap;
        }

        #endregion

        #region ボタン関連
        
        #endregion

        private void btnMakeMovie_Click(object sender, EventArgs e)
        {
            AutoSave();
            MessageBox.Show("保存完了!");
        }

        private void btnMakeBitmap_Click(object sender, EventArgs e)
        {
            this.eDatas = new cPointData[lbxMain.Items.Count];

            for (int i = 0; i < lbxMain.Items.Count; i++)
                this.eDatas[i] = new cPointData(lbxMain.Items[i].ToString());

            CreateBitmapsForFrame(0);

            //1番目のフレームをpictureBoxに表示
            pbxPreView.Image = fBitmaps[0];
            tbxNowFrame.Text = "0";
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            int faceNo = int.Parse(tbxNowFrame.Text);
            if (faceNo > 0)
            {
                pbxPreView.Image = fBitmaps[--faceNo];
                tbxNowFrame.Text = faceNo.ToString();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            int faceNo = int.Parse(tbxNowFrame.Text);
            if (faceNo < fBitmaps.Length-1)
            {
                pbxPreView.Image = fBitmaps[++faceNo];
                tbxNowFrame.Text = faceNo.ToString();
            }

        }
    }
}
