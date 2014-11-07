using System;
using MatrixVector;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DoPCA
{
    /// <summary>
    /// 主成分分析のデータを保持・保存・読み込みをするクラス
    /// </summary>
    [Serializable]
    public class PCAData
    {
        #region プライベート変数
        /// <summary>
        /// データソースの種類
        /// </summary>
        PCASource PCASource;

        /// <summary>
        /// 固有値・固有ベクトルを保持
        /// </summary>
        EigenSystem EigenSystemData;

        /// <summary>
        /// 展開係数を保持
        /// </summary>
        Matrix CoefficientMatrix;

        /// <summary>
        /// 平均ベクトルを保持
        /// </summary>
        Vector AverageVector;

        /// <summary>
        /// その他のデータを保持
        /// </summary>
        object Tag;
        #endregion

        /// <summary>
        /// 与えられたデータからインスタンスを作成します
        /// </summary>
        /// <param name="PCASource">データソースの種類</param>
        /// <param name="EigenSystem">固有値・固有ベクトル</param>
        /// <param name="CoefficientMatrix">展開係数</param>
        /// <param name="AverageVector">平均ベクトル</param>
        /// <param name="Tag">その他データ</param>
        public PCAData(PCASource PCASource, EigenSystem EigenSystem, Matrix CoefficientMatrix, Vector AverageVector, object Tag)
        {
            this.PCASource = PCASource;
            this.EigenSystemData = new EigenSystem(EigenSystem);
            this.CoefficientMatrix = new Matrix(CoefficientMatrix);
            this.AverageVector = new Vector(AverageVector);
            this.Tag = (object)Tag;
        }


        /// <summary>
        /// このクラスをセーブするメソッド
        /// </summary>
        /// <param name="strSaveFileName">セーブファイル名</param>
        /// <returns>成功ならTrue</returns>
        public bool DataSave(string strSaveFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strSaveFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, this);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ファイル書き込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// データをロードするメソッド
        /// </summary>
        /// <param name="strLoadFileName">ロードファイル名</param>
        /// <returns>ロードしたデータ</returns>
        public static PCAData DataLoad(string strLoadFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strLoadFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (PCAData)bf.Deserialize(fs);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ファイル読み込みエラー", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        #region プロパティ
        /// <summary>
        /// 主成分分析元のデータが何だったかを取得します
        /// </summary>
        public PCASource DataType
        {
            get { return this.PCASource; }
        }

        /// <summary>
        /// 固有値・固有ベクトルを取得します
        /// </summary>
        public EigenSystem EigenSystem
        {
            get { return new EigenSystem(this.EigenSystemData); }
        }

        /// <summary>
        /// 平均ベクトルを取得します
        /// </summary>
        public Vector Average
        {
            get { return new Vector(this.AverageVector); }
        }

        /// <summary>
        /// 展開係数を取得します
        /// </summary>
        public Matrix Coefficient
        {
            get { return new Matrix(this.CoefficientMatrix); }
        }

        /// <summary>
        /// データの個数を取得します
        /// </summary>
        public int DataCount
        {
            get { return this.CoefficientMatrix.ColSize; }
        }

        /// <summary>
        /// パラメータの個数を取得します
        /// </summary>
        public int ParamCount
        {
            get { return this.CoefficientMatrix.RowSize; }
        }

        /// <summary>
        /// データ固有の値を取得します
        /// </summary>
        public object DataTag
        {
            get { return this.Tag; }
        }
        #endregion
    }
}
