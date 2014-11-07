using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;

namespace PersonExpressionsASCII
{
    public class Expression
    {
        protected string _eName;
        protected cPointData _cData;

        #region ゲッターセッター
        public string eName
        {
            get{ return _eName; }
            set{ _eName = value; }
        }

        public cPointData cData
        {
            get { return _cData; }
            set { _cData = value; }
        }
        #endregion

        #region オペレータオーバーロード

        /// <summary>
        /// 足し算をします。結果はcDataの方に格納されます。
        /// </summary>
        /// <param name="xyz1">足される対象</param>
        /// <param name="xyz2">足す対象</param>
        /// <returns></returns>
        public static Expression operator +(Expression xyz1, Expression xyz2)
        {
            Expression ReturnData = new Expression(xyz1);
            for (int i = 0; i < xyz1._cData.Length; i++)
            {
                ReturnData._cData[i].X = xyz1._cData[i].X + xyz2._cData[i].X;
                ReturnData._cData[i].Y = xyz1._cData[i].Y + xyz2._cData[i].Y;
                ReturnData._cData[i].Z = xyz1._cData[i].Z + xyz2._cData[i].Z;

                ReturnData._cData[i].R = xyz1._cData[i].R + xyz2._cData[i].R;
                ReturnData._cData[i].G = xyz1._cData[i].G + xyz2._cData[i].G;
                ReturnData._cData[i].B = xyz1._cData[i].B + xyz2._cData[i].B;

            }
            return ReturnData;
        }

        /// <summary>
        /// 引き算をします。結果はcDataの方に格納されます。
        /// </summary>
        /// <param name="xyz1">引かれる対象</param>
        /// <param name="xyz2">引く対象</param>
        /// <returns></returns>
        public static Expression operator -(Expression xyz1, Expression xyz2)
        {
            Expression ReturnData = new Expression(xyz1);
            for (int i = 0; i < xyz1._cData.Length; i++)
            {
                ReturnData._cData[i].X = xyz1._cData[i].X - xyz2._cData[i].X;
                ReturnData._cData[i].Y = xyz1._cData[i].Y - xyz2._cData[i].Y;
                ReturnData._cData[i].Z = xyz1._cData[i].Z - xyz2._cData[i].Z;

                ReturnData._cData[i].R = xyz1._cData[i].R - xyz2._cData[i].R;
                ReturnData._cData[i].G = xyz1._cData[i].G - xyz2._cData[i].G;
                ReturnData._cData[i].B = xyz1._cData[i].B - xyz2._cData[i].B;
            }
            return ReturnData;
        }

        /// <summary>
        /// 1番目の引数から2番目の引数の掛け算をします。測定点の数は左辺項の発話aに合わせることとします。
        /// </summary>
        /// <param name="xyz1">対象</param>
        /// <param name="xyz2">掛ける数値</param>
        /// <returns></returns>
        public static Expression operator *(Expression xyz1, int mVal)
        {
            Expression ReturnData = new Expression(xyz1);
            for (int i = 0; i < xyz1._cData.Length; i++)
            {
                ReturnData._cData[i].X = xyz1._cData[i].X * mVal;
                ReturnData._cData[i].Y = xyz1._cData[i].Y * mVal;
                ReturnData._cData[i].Z = xyz1._cData[i].Z * mVal;

                ReturnData._cData[i].R = xyz1._cData[i].R * mVal;
                ReturnData._cData[i].G = xyz1._cData[i].G * mVal;
                ReturnData._cData[i].B = xyz1._cData[i].B * mVal;
            }
            return ReturnData;
        }

        /// <summary>
        /// 1番目の引数から2番目の引数の割り算をします。測定点の数は左辺項の発話aに合わせることとします。
        /// </summary>
        /// <param name="xyz1">対象</param>
        /// <param name="xyz2">割る数値</param>
        /// <returns></returns>
        public static Expression operator /(Expression xyz1, int dVal)
        {
            Expression ReturnData = new Expression(xyz1);
            for (int i = 0; i < xyz1._cData.Length; i++)
            {
                ReturnData._cData[i].X = xyz1._cData[i].X / dVal;
                ReturnData._cData[i].Y = xyz1._cData[i].Y / dVal;
                ReturnData._cData[i].Z = xyz1._cData[i].Z / dVal;

                ReturnData._cData[i].R = xyz1._cData[i].R / dVal;
                ReturnData._cData[i].G = xyz1._cData[i].G / dVal;
                ReturnData._cData[i].B = xyz1._cData[i].B / dVal;
            }
            return ReturnData;
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// アンダーバーで区切られた3番目を表情名として取り込みます。
        /// </summary>
        public Expression(string Path)
        {
            _eName = Path.Split('_')[3];
            _cData = new cPointData(Path);
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="PreviousPerson">コピー元インスタンス</param>
        public Expression(Expression PreviousExpression)
        {
            this._eName = PreviousExpression._eName;
            this._cData = PreviousExpression._cData;
        }
        #endregion

        #region クラスメソッド
        /// <summary>
        /// cDataからShapeデータを取得します。
        /// </summary>
        public XYZPointData MakeShape()
        {
            XYZPoint[] s_pt = new XYZPoint[_cData.Length];
            
            for (int i = 0; i < _cData.Length; i++)
            {
                s_pt[i] = new XYZPoint(_cData[i].X, _cData[i].Y, _cData[i].Z);
            }
            return new XYZPointData(s_pt);
        }

        /// <summary>
        /// cDataからTextureデータを取得します。
        /// </summary>
        public XYZPointData MakeTexture()
        {
            XYZPoint[] t_pt = new XYZPoint[_cData.Length];
            
            for (int i = 0; i < _cData.Length; i++)
            {
                t_pt[i] = new XYZPoint(_cData[i].R, _cData[i].G, _cData[i].B);
            }
            return new XYZPointData(t_pt);
        }


        /// <summary>
        /// 分離されたデータを基にcDataを更新します。
        /// </summary>
        public void Update(XYZPointData _ShapeData,XYZPointData _TextureData)
        {
            if (_ShapeData == null || _TextureData == null)
                return;

            for (int i = 0; i < _cData.Length; i++)
            {
                this._cData[i].X = _ShapeData[i].X;
                this._cData[i].Y = _ShapeData[i].Y;
                this._cData[i].Z = _ShapeData[i].Z;

                this._cData[i].R = (int)_TextureData[i].X;
                this._cData[i].G = (int)_TextureData[i].Y;
                this._cData[i].B = (int)_TextureData[i].Z;
            }
        }

        #endregion
    }
}
