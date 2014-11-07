using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace MatrixVector
{
    /// <summary>
    /// 横（行方向）ベクトルのクラス
    /// </summary>
    [Serializable]
    public class RowVector:Vector
    {
        /// <summary>
        /// 与えられたベクトルと同一の横ベクトルを作成します。
        /// </summary>
        /// <param name="Vector">コピー元のベクトル</param>
        public RowVector(Vector Vector) : base(Vector) { }

        /// <summary>
        /// 与えられたベクトルと同一の横ベクトルを作成します。
        /// </summary>
        /// <param name="Vector">コピー元のベクトル</param>
        public RowVector(RowVector Vector) : base((Vector)Vector) { }

        /// <summary>
        /// 与えられた要素を持つ横ベクトルを作成します。
        /// </summary>
        /// <param name="Element">要素の配列</param>
        public RowVector(double[] Element) : base(Element) { }

        /// <summary>
        /// 指定された次元数の横ベクトルを作成します。値は全て0になります。
        /// </summary>
        /// <param name="ElementSize">ベクトルの次元数</param>
        public RowVector(int ElementSize) : base(ElementSize) { }

        /// <summary>
        /// 与えられた次元数の横ベクトルを作成します。値は全てValueになります。
        /// </summary>
        /// <param name="Value">値</param>
        /// <param name="ElementSize">次元数</param>
        public RowVector(double Value, int ElementSize) : base(Value, ElementSize) { }

        /// <summary>
        /// 空の横ベクトルのインスタンスを作成します。
        /// </summary>
        protected RowVector() { }


        /// <summary>
        /// 正規化したベクトルを取得するメソッドです。
        /// </summary>
        /// <returns>正規化されたベクトル</returns>
        public new RowVector GetNormlizeVector()
        {
            return new RowVector(base.GetNormlizeVector());
        }

        /// <summary>
        /// 指定したベクトルとの内積を求めます。ベクトルの次元数は等しくないとエラーが発生します。
        /// </summary>
        /// <param name="Vector">内積を求めるための対象となるベクトル</param>
        /// <returns>内積</returns>
        public double InnerProduct(RowVector Vector)
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
        public static RowVector operator *(RowVector vector, double scalar)
        {
            return new RowVector((Vector)vector * scalar);
        }

        public static RowVector operator *(double scalar, RowVector vector)
        {
            return new RowVector( (Vector)vector * scalar);
        }

        public static double operator *(RowVector RowVector, ColumnVector ColumnVector)
        {
            if (RowVector.Length != ColumnVector.Length)
                throw new ApplicationException("要素の数が一致しません。");

            double Sum = 0;
            for (int i = 0; i < RowVector.Length; i++)
                Sum += RowVector[i] * ColumnVector[i];
            return Sum;
        }
        #endregion

        #region 足し算の定義
        public static RowVector operator +(RowVector LeftVecotr, RowVector RightVector)
        {
            if (LeftVecotr.Length != RightVector.Length)
                throw new ApplicationException("要素の数が一致しません。");

            RowVector ReturnVector = new RowVector(LeftVecotr);

            for (int i = 0; i < ReturnVector.Length; i++)
                ReturnVector[i] += RightVector[i];

            return ReturnVector;
        }
        #endregion

        #region 引き算の定義
        public static RowVector operator -(RowVector LeftVector, RowVector RightVector)
        {
            if (LeftVector.Length != RightVector.Length)
                throw new ApplicationException("要素の数が一致しません。");

            RowVector ReturnVector = new RowVector(LeftVector);

            for (int i = 0; i < ReturnVector.Length; i++)
                ReturnVector[i] -= RightVector[i];

            return ReturnVector;
        }
        #endregion

        #region 割り算の定義
        public static RowVector operator /(RowVector vector, double scalar)
        {
            return new RowVector((Vector)vector / scalar);
        }
        #endregion
        #endregion

        /// <summary>
        /// バイナリデータをロードするメソッド
        /// </summary>
        /// <param name="strLoadFileName">ロードファイル名</param>
        /// <returns>ロードしたデータ</returns>
        public new static RowVector LoadBinary(string strLoadFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strLoadFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (RowVector)bf.Deserialize(fs);
                }
            }
            catch (Exception error) { return null; }
        }
    }
}
