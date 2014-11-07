using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace MatrixVector
{
    /// <summary>
    /// 縦（列方向）ベクトルのクラス
    /// </summary>
    [Serializable]
    public class ColumnVector:Vector
    {
        /// <summary>
        /// 与えられたベクトルと同一の横ベクトルを作成します。
        /// </summary>
        /// <param name="Vector">コピー元のベクトル</param>
        public ColumnVector(Vector Vector) : base(Vector) { }

        /// <summary>
        /// 与えられたベクトルと同一の横ベクトルを作成します。
        /// </summary>
        /// <param name="Vector">コピー元のベクトル</param>
        public ColumnVector(RowVector Vector) : base((Vector)Vector) { }

        /// <summary>
        /// 与えられた要素を持つ横ベクトルを作成します。
        /// </summary>
        /// <param name="Element">要素の配列</param>
        public ColumnVector(double[] Element) : base(Element) { }

        /// <summary>
        /// 指定された次元数の横ベクトルを作成します。値は全て0になります。
        /// </summary>
        /// <param name="ElementSize">ベクトルの次元数</param>
        public ColumnVector(int ElementSize) : base(ElementSize) { }

        /// <summary>
        /// 与えられた次元数の横ベクトルを作成します。値は全てValueになります。
        /// </summary>
        /// <param name="Value">値</param>
        /// <param name="ElementSize">次元数</param>
        public ColumnVector(double Value, int ElementSize) : base(Value, ElementSize) { }

        /// <summary>
        /// 空の横ベクトルのインスタンスを作成します。
        /// </summary>
        protected ColumnVector() { }

        /// <summary>
        /// 正規化したベクトルを取得するメソッドです。
        /// </summary>
        /// <returns>正規化されたベクトル</returns>
        public new ColumnVector GetNormlizeVector()
        {
            return new ColumnVector(base.GetNormlizeVector());
        }

        /// <summary>
        /// 指定したベクトルとの内積を求めます。ベクトルの次元数は等しくないとエラーが発生します。
        /// </summary>
        /// <param name="Vector">内積を求めるための対象となるベクトル</param>
        /// <returns>内積</returns>
        public double InnerProduct(ColumnVector Vector)
        {
            if (this.Length != Vector.Length)
                throw new ApplicationException("要素の数が一致しません。");

            double Sum = 0;
            for (int i = 0; i < this.Length; i++)
                Sum += this[i] * Vector[i];
            return Sum;
        }

        #region オペレータオーバーロード
        #region かけ算の定義
        public static ColumnVector operator *(ColumnVector vector, double scalar)
        {
            return new ColumnVector((Vector)vector * scalar);
        }

        public static ColumnVector operator *(double scalar, ColumnVector vector)
        {
            return new ColumnVector( (Vector)vector * scalar);
        }

        public static Matrix operator *(ColumnVector ColumnVector, RowVector RowVector)
        {
            if (ColumnVector.Length != RowVector.Length)
                throw new ApplicationException("要素の数が正しくありません");

            SquareMatrix ReturnMatrix = new SquareMatrix(ColumnVector.Length);

            for (int i = 0; i < ColumnVector.Length; i++)
                for (int j = 0; j < RowVector.Length; j++)
                {
                    ReturnMatrix[j, i] = ColumnVector[i] * RowVector[j];
                }

            return ReturnMatrix;
        }

        #endregion

        #region 足し算の定義
        public static ColumnVector operator +(ColumnVector LeftVecotr, ColumnVector RightVector)
        {
            if (LeftVecotr.Length != RightVector.Length)
                throw new ApplicationException("要素の数が一致しません。");

            ColumnVector ReturnVector = new ColumnVector(LeftVecotr);

            for (int i = 0; i < ReturnVector.Length; i++)
                ReturnVector[i] += RightVector[i];

            return ReturnVector;
        }
        #endregion

        #region 引き算の定義
        public static ColumnVector operator -(ColumnVector LeftVector, ColumnVector RightVector)
        {
            if (LeftVector.Length != RightVector.Length)
                throw new ApplicationException("要素の数が一致しません。");

            ColumnVector ReturnVector = new ColumnVector(LeftVector);

            for (int i = 0; i < ReturnVector.Length; i++)
                ReturnVector[i] -= RightVector[i];

            return ReturnVector;
        }
        #endregion

        #region 割り算の定義
        public static ColumnVector operator /(ColumnVector vector, double scalar)
        {
            return new ColumnVector((Vector)vector / scalar);
        }
        #endregion
        #endregion

        /// <summary>
        /// バイナリデータをロードするメソッド
        /// </summary>
        /// <param name="strLoadFileName">ロードファイル名</param>
        /// <returns>ロードしたデータ</returns>
        public new static ColumnVector LoadBinary(string strLoadFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strLoadFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (ColumnVector)bf.Deserialize(fs);
                }
            }
            catch (Exception error) { return null; }
        }
    }
}
