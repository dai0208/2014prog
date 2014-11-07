using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixVector
{

    /// <summary>
    /// 正方行列のクラス
    /// </summary>
    [Serializable]
    public class SquareMatrix : Matrix
    {
        #region コンストラクタ
        /// <summary>
        /// 空の正方行列を作成します。
        /// </summary>
        protected SquareMatrix() { }

        /// <summary>
        /// 与えられたベクトル配列から正方行列を作成します。
        /// </summary>
        /// <param name="Vectors">ベクトル配列</param>
        public SquareMatrix(Vector[] Vectors)
            :base(Vectors)
        {
            if (this.ColSize != this.RowSize)
                throw new ApplicationException("行と列の大きさが違います");
        }

        /// <summary>
        /// 与えられた配列から正方行列を作成します。
        /// </summary>
        /// <param name="DoubleArray">配列</param>
        public SquareMatrix(double[][] DoubleArray)
            :base(DoubleArray)
        {
            if (this.ColSize != this.RowSize)
                throw new ApplicationException("行と列の大きさが違います");
        }

        /// <summary>
        /// 与えられた行列から正方行列を作成します。
        /// </summary>
        /// <param name="matrix">行列</param>
        public SquareMatrix(Matrix matrix)
            :base(matrix)
        {
            if (this.ColSize != this.RowSize)
                throw new ApplicationException("行と列の大きさが違います");
        }

        /// <summary>
        /// 与えられた次元数からなる正方行列を作成します。
        /// </summary>
        /// <param name="Dimension">次元数</param>
        public SquareMatrix(int Dimension)
            :base(Dimension,Dimension)
        {
        }
        #endregion

        /// <summary>
        /// 許容相対誤差を指定して固有ベクトルと固有値を取得します。
        /// このメソッドはオーバーライド、もしくは個別に実装して下さい。
        /// </summary>
        /// <param name="EPS">許容相対誤差</param>
        /// <returns>固有ベクトル</returns>
        public virtual EigenSystem GetEigenVectorAndValue(double EPS)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// デフォルトの許容相対誤差で固有ベクトルと固有値を取得します。
        /// </summary>
        /// <returns>固有ベクトル</returns>
        public virtual EigenSystem GetEigenVectorAndValue()
        {
            double EPS = 0.00001;
            return GetEigenVectorAndValue(EPS);
        }

        /// <summary>
        /// 指定した階数の単位行列を取得します。
        /// </summary>
        /// <param name="Rank">階数</param>
        /// <returns>単位行列</returns>
        public static SquareMatrix IdentityMatrix(int Rank)
        {
            SquareMatrix ReturnMatrix = new SquareMatrix(Rank);
            for (int i = 0; i < Rank; i++)
                ReturnMatrix[i, i] = 1d;
            return ReturnMatrix;
        }

        /// <summary>
        /// 行列の次元数を取得します。
        /// </summary>
        public int Dimension
        {
            get { return this.RowSize; }
        }
    }
}
