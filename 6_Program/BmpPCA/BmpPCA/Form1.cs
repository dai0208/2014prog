using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MatrixVector;
using MatrixVectorForBitmap;

namespace BmpPCA
{
    public partial class Form1 : Form
    {
        myPCAData ePCAItems;
        Bitmap[] EigenFaces;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Bitmap[] bitmaps = new Bitmap[lbxBitmaps.Items.Count];

            for (int i = 0; i < lbxBitmaps.Items.Count; i++)
                bitmaps[i] = new Bitmap(lbxBitmaps.Items[i].ToString());

            Matrix xVectors = new Matrix(gcBitmapConverter.gBitmapsToVectors(bitmaps));

            ColumnVector AverageVector = xVectors.GetAverageRow();

            Matrix AverageMatrix = Matrix.GetSameElementMatrix(AverageVector, xVectors.ColSize);

            Matrix aMatrix = xVectors - AverageMatrix;

            SymmetricMatrix LMatrix = new SymmetricMatrix(aMatrix.GetTranspose() * aMatrix);

            ShowMatrix(LMatrix);

            EigenSystem EigenSystemData = LMatrix.GetEigenVectorAndValue();
            Matrix LEigenVector = EigenSystemData.GetEigenVectors();

            Matrix FinalEigenVector = (xVectors * LEigenVector).GetNormalizedMatrixCol();

            EigenSystem FinalEigenSystem = new EigenSystem();
            for (int i = 0; i < EigenSystemData.Count; i++)
            {
                if (EigenSystemData[i].EigenValue > 0.0001)
                    FinalEigenSystem.Add(new EigenVectorAndValue(FinalEigenVector.GetColVector(i), EigenSystemData[i].EigenValue));
            }

            Matrix CoefficientMatrix = FinalEigenSystem.GetEigenVectors().GetTranspose() * aMatrix;
            ePCAItems = new myPCAData(FinalEigenSystem, CoefficientMatrix, AverageVector);
            AfterProcessing();
        }

        #region コントロール関係
        private void lbxBitmap_DragDrop(object sender, DragEventArgs e)
        {
            lbxBitmaps.Items.Clear();

            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Exists(tempFilePath))
                {
                    lbxBitmaps.Items.Add(tempFilePath);
                }
                else if (Directory.Exists(tempFilePath))
                {
                    string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                    lbxBitmaps.Items.AddRange(files);
                }
                else { }
            }
        }

        private void lbxBitmap_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void cbEigenFaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEigenFaces.SelectedIndex != -1)
            {
                pbxEigenFace.Image = EigenFaces[cbEigenFaces.SelectedIndex];
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// lvMatrixにマトリックスの内容を出力します。
        /// </summary>
        /// <param name="mtx">出力用マトリクス</param>
        private void ShowMatrix(Matrix mtx)
        {
            for (int i = 0; i < mtx.ColSize; i++)
            {
                lvMatrix.Columns.Add("第" + (i + 1) + "列");
                string[] pRow = new string[mtx.RowSize];

                for (int j = 0; j < mtx.RowSize; j++)
                {
                    pRow[j] = mtx[j, i].ToString("f");
                }
                lvMatrix.Items.Add(new ListViewItem(pRow));
            }

            foreach (ColumnHeader ch in lvMatrix.Columns)
            {
                ch.Width = -1;
            }
        }

        /// <summary>
        /// PCA結果データを表示します。
        /// </summary>
        private void AfterProcessing()
        {
            for (int i = 0; i < ePCAItems.DataCount; i++)
            {
                lvPCAParam.Columns.Add("第" + (i + 1) + "ﾊﾟﾗﾒｰﾀ");
                string[] Coef = new string[ePCAItems.ParamCount];

                for (int j = 0; j < ePCAItems.ParamCount; j++)
                {
                    Coef[j] = ePCAItems.Coefficient[j, i].ToString("f");
                }
                lvPCAParam.Items.Add(new ListViewItem(Coef));
            }

            foreach (ColumnHeader ch in lvPCAParam.Columns)
            {
                ch.Width = -1;
            }

            for (int i = 0; i < ePCAItems.ParamCount; i++)
                cbEigenFaces.Items.Add("第" + (i + 1) + "固有顔");

            Matrix NormMatrix = NormalizeMatrix(ePCAItems.EigenSystem.GetEigenVectors());

            EigenFaces = new Bitmap[NormMatrix.ColSize];
            for (int i = 0; i < NormMatrix.ColSize; i++)
            {
                EigenFaces[i] = gcBitmapConverter.gBitmapFromVector(NormMatrix.GetColVector(i), 128, 128);
            }
        }

        /// <summary>
        /// 縦の固有ﾍﾞｸﾄﾙ行列を正規化して0~255の範囲を持つ値にします。
        /// </summary>
        /// <param name="beforeMatrix"></param>
        /// <returns></returns>
        Matrix NormalizeMatrix(Matrix beforeMatrix)
        {
            Vector bAve = new Vector(beforeMatrix.ColSize);
            Vector bMin = new Vector(beforeMatrix.ColSize);
            Vector bMax = new Vector(beforeMatrix.ColSize);

            for (int col = 0; col < beforeMatrix.ColSize; col++)
                for (int row = 0; row < beforeMatrix.RowSize; row++)
                {
                    if (bMin[col] > beforeMatrix[row, col])
                    {
                        bMin[col] = beforeMatrix[row, col];
                    }
                    if (bMax[col] < beforeMatrix[row, col])
                    {
                        bMax[col] = beforeMatrix[row, col];
                    }
                    bAve[col] += beforeMatrix[row, col];
                    bAve[col] /= beforeMatrix.RowSize;
                }

            for (int col = 0; col < beforeMatrix.ColSize; col++)
                for (int row = 0; row < beforeMatrix.RowSize; row++)
                {
                    beforeMatrix[row, col] -= bMin[col];
                    beforeMatrix[row, col] /= bMax[col] - bMin[col];
                    beforeMatrix[row, col] *= 255;
                }

            return beforeMatrix;
        }
    }
    #endregion
}
