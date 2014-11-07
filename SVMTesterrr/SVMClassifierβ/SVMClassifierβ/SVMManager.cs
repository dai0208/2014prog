using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MatrixVector;

namespace SVMClassifierβ
{
    class SVMManager
    {
        //コンストラクタ
        public SVMManager(){}

        #region Vector → Matrix 変換系
        public Matrix VectorToMatrix(ColumnVector[] vcts)
        {
            Matrix mtx = new Matrix(vcts);
            return mtx;
        }
        #endregion

        #region MatrixVector → Array 変換系
        /// <summary>
        /// マトリクスデータをdouble型List配列に変換します。
        /// </summary>
        /// <param name="mtx"></param>
        public List<List<double>> MatrixToArray(Matrix mtx)
        {
            List<List<double>> MatrixArray = new List<List<double>>();
            for (int col = 0; col < mtx.ColSize; col++)
            {
                MatrixArray.Add(VectorToArray(mtx.GetColVector(col)));
            }
            return MatrixArray;
        }

        /// <summary>
        /// ベクトルデータをdouble型List配列に変換します。
        /// </summary>
        /// <param name="vct"></param>
        public List<double> VectorToArray(ColumnVector vct)
        {
            List<double> VectorArray = new List<double>();
            for (int row = 0; row < vct.Length; row++)
            {
                VectorArray.Add(vct.VectorElement[row]);
            }
            return VectorArray;
        }

        /// <summary>
        /// ベクトル配列をdouble型List配列に変換します。
        /// </summary>
        /// <param name="vcts"></param>
        /// <returns></returns>
        public List<List<double>> VectArrayToArray(ColumnVector[] vcts)
        {
            List<List<double>> VectorArray = new List<List<double>>();
            for (int col = 0; col < vcts.Length; col++)
            {
                VectorArray.Add(VectorToArray(vcts[col]));
            }
            return VectorArray;
        }
        #endregion

        #region Array → MatrixVector 変換系
        public ColumnVector ArrayToVector(List<double> data)
        {
            ColumnVector myVector = new ColumnVector(data.ToArray());
            return myVector;
        }

        public Matrix ArrayToMatrix(List<List<double>> datas)
        {
            ColumnVector[] dataMatrix = new ColumnVector[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                dataMatrix[i] = ArrayToVector(datas[i]);
            }
            Matrix myMatrix = new Matrix(dataMatrix);
            return myMatrix;
        }

        #endregion

        #region txt → LIST 変換系
        /// <summary>
        /// .txtを読んでLISTへ格納するメソッドです
        /// </summary>
        /// <param name="pass">テキストファイルへのフルパス</param>
        /// <returns>格納後のLIST</returns>
        public List<double> ReadTextFile(string pass)
        {
            string line = "";
            List<double> Contents = new List<double>();
            using (StreamReader sr = new StreamReader(pass, Encoding.GetEncoding("Shift_JIS")))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Contents.Add(double.Parse(line));
                }
            }
            return Contents;
        }

        /// <summary>
        /// 複数ファイル読み込み用
        /// </summary>
        /// <param name="passes"></param>
        /// <returns></returns>
        public List<List<double>> ReadTextFileList(string[] passes)
        {
            List<List<double>> Contents = new List<List<double>>();
            for (int i = 0; i < passes.Length; i++)
            {
                Contents.Add(ReadTextFile(passes[i]));
            }
            return Contents;
        }

        #endregion
    }
}
