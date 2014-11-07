using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MatrixVector;
using DoPCA;

namespace PersonExpressionsASCII
{
    public class cPersonPCAManager:IDisposable
    {
        Matrix mtxData;
        PCAData PCAResult;

        #region コンストラクタ
        public cPersonPCAManager(Matrix mtxData)
        {
            this.mtxData = mtxData;
        }
        #endregion
        void IDisposable.Dispose()
        {
        }

        /// <summary>
        /// 主成分分析をして結果を保持します。
        /// </summary>
        public void PCA()
        {
            PCAFromMatrix manager = new PCAFromMatrix();

            manager._LoadMatrix = mtxData;

            PCAResult = manager.GetPCAData();

            //表情毎にパラメータの重心を求めておきます。
 
            //ついでに結果も保存してしまいます。
            /*
            PCADataSave(pcadataXYZ,PersonExpressionsXYZ);
            PCADataSave(pcadataRGB,PersonExpressionsRGB);*/
        }


        /// <summary>
        /// PCA結果データの保存
        /// </summary>
        /// <param name="pcadata">PCA結果そのもの</param>
        /// <param name="PersonalExpressions">対応するパーソナルリスト</param>
        public void PCADataSave()
        {
            int ExpressionCount = 8;

            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }

            //平均ベクトルの保存
            PCAResult.Average.Save(path + "\\AVERAGE.asc");

            //展開係数の保存
            /*
            for (int i = 0; i < PersonalExpressions.Count; i++)
            {
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 0).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_a.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 1).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_i.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 2).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_u.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 3).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_e.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 4).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_o.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 5).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_cl.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 6).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_ol.asc");
                PCAResult.Coefficient.GetColVector(i * ExpressionCount + 7).Save(path + "\\" + PersonalExpressions[i].pKey + "_Param_n.asc");
            }
            */

            //PCADataの保存
            PCAResult.DataSave(path + "\\PCADATA.mtx");

            //固有値・固有ベクトルの保存
            EigenSystem Eigen = new EigenSystem(PCAResult.EigenSystem);
            for (int i = 0; i < Eigen.GetEigenVectors().ColSize; i++)
            {
                Eigen.GetEigenVectors().GetColVector(i).Save(path + "\\EigenVector_" + (i + 1) + ".asc");
            }
            double[] eigenvalue = new double[Eigen.Count];
            for (int i = 0; i < eigenvalue.Length; i++)
                eigenvalue[i] = Eigen[i].EigenValue;
            Vector eigenvalueV = new Vector(eigenvalue);
            eigenvalueV.Save(path + "\\Eigenvalue.asc");
        }
    }
}
