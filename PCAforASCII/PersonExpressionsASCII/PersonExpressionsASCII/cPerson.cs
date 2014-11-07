using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;

namespace PersonExpressionsASCII
{
    public class cPerson
    {
        /// <summary>
        /// 表情「あ」
        /// </summary>
        private Expression _a;

        /// <summary>
        /// 表情「い」
        /// </summary>
        private Expression _i;

        /// <summary>
        /// 表情「う」
        /// </summary>
        private Expression _u;

        /// <summary>
        /// 表情「え」
        /// </summary>
        private Expression _e;

        /// <summary>
        /// 表情「お」
        /// </summary>
        private Expression _o;

        /// <summary>
        /// 閉口笑顔
        /// </summary>
        private Expression _cl;

        /// <summary>
        /// 開口笑顔
        /// </summary>
        private Expression _ol;

        /// <summary>
        /// 真顔
        /// </summary>
        private Expression _n;

        /// <summary>
        /// 人物名
        /// </summary>
        private string _pKey;

        /// <summary>
        /// 表情数
        /// </summary>
        private int _ExpressionCount = 8;

        #region ゲッターセッター
        public string pKey
        {
            get { return _pKey; }
        }

        public int Count
        {
            get{ return _ExpressionCount; }
        }

        public Expression a
        {
            get { return _a; }
            set { this._a = value; }
        }

        public Expression i
        {
            get { return _i; }
            set { this._i = value; }
        }

        public Expression u
        {
            get { return _u; }
            set { this._u = value; }
        }

        public Expression e
        {
            get { return _e; }
            set { this._e = value; }
        }

        public Expression o
        {
            get { return _o; }
            set { this._o = value; }
        }

        public Expression cl
        {
            get { return _cl; }
            set { this._cl = value; }
        }

        public Expression ol
        {
            get { return _ol; }
            set { this._ol = value; }
        }

        public Expression n
        {
            get { return _n; }
            set { this._n = value; }
        }

        #endregion

        #region オペレータオーバーロード
        /// <summary>
        /// 足し算をします。
        /// </summary>
        /// <param name="xyz1"></param>
        /// <param name="xyz2"></param>
        /// <returns></returns>
        public static cPerson operator +(cPerson xyz1, cPerson xyz2)
        {
            cPerson tPerson = new cPerson(xyz1);
            tPerson._a = xyz1._a + xyz2._a;
            tPerson._i = xyz1._i + xyz2._i;
            tPerson._u = xyz1._u + xyz2._u;
            tPerson._e = xyz1._e + xyz2._e;
            tPerson._o = xyz1._o + xyz2._o;
            tPerson._cl = xyz1._cl + xyz2._cl;
            tPerson._ol = xyz1._ol + xyz2._ol;
            tPerson._n = xyz1._n + xyz2._n;
            return tPerson;
        }

        /// <summary>
        /// 1番目の引数から2番目の引数の引き算をします。
        /// </summary>
        /// <param name="xyz1">引かれる対象</param>
        /// <param name="xyz2">引く対象</param>
        /// <returns></returns>
        public static cPerson operator -(cPerson xyz1, cPerson xyz2)
        {
            cPerson tPerson = new cPerson(xyz1);
            tPerson._a = xyz1._a - xyz2._a;
            tPerson._i = xyz1._i - xyz2._i;
            tPerson._u = xyz1._u - xyz2._u;
            tPerson._e = xyz1._e - xyz2._e;
            tPerson._o = xyz1._o - xyz2._o;
            tPerson._cl = xyz1._cl - xyz2._cl;
            tPerson._ol = xyz1._ol - xyz2._ol;
            tPerson._n = xyz1._n - xyz2._n;
            return tPerson;
        }

        /// <summary>
        /// 1番目の引数から2番目の引数の掛け算をします。
        /// </summary>
        /// <param name="xyz1">対象</param>
        /// <param name="xyz2">掛ける数値</param>
        /// <returns></returns>
        public static cPerson operator *(cPerson xyz1, int mVal)
        {
            cPerson tPerson = new cPerson(xyz1);
            tPerson._a = xyz1._a * mVal;
            tPerson._i = xyz1._i * mVal;
            tPerson._u = xyz1._u * mVal;
            tPerson._e = xyz1._e * mVal;
            tPerson._o = xyz1._o * mVal;
            tPerson._cl = xyz1._cl * mVal;
            tPerson._ol = xyz1._ol * mVal;
            tPerson._n = xyz1._n * mVal;
            return tPerson;
        }

        /// <summary>
        /// 1番目の引数から2番目の引数の割り算をします。
        /// </summary>
        /// <param name="xyz1">対象</param>
        /// <param name="xyz2">割る数値</param>
        /// <returns></returns>
        public static cPerson operator /(cPerson xyz1, int dVal)
        {
            cPerson tPerson = new cPerson(xyz1);
            tPerson._a = xyz1._a / dVal;
            tPerson._i = xyz1._i / dVal;
            tPerson._u = xyz1._u / dVal;
            tPerson._e = xyz1._e / dVal;
            tPerson._o = xyz1._o / dVal;
            tPerson._cl = xyz1._cl / dVal;
            tPerson._ol = xyz1._ol / dVal;
            tPerson._n = xyz1._n / dVal;
            return tPerson;
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="PreviousPerson">コピー元インスタンス</param>
        public cPerson(cPerson PreviousPerson)
        {
            this._ExpressionCount = PreviousPerson.Count; 
            this._pKey = PreviousPerson._pKey;
            this._a = PreviousPerson._a;
            this._i = PreviousPerson._i;
            this._u = PreviousPerson._u;
            this._e = PreviousPerson._e;
            this._o = PreviousPerson._o;
            this._cl = PreviousPerson._cl;
            this._ol = PreviousPerson._ol;
            this._n = PreviousPerson._n;
        }

        /// <summary>
        /// アンダーバーで区切られた3番目を表情名として、表情毎に自動振り分けします。
        /// </summary>
        /// <param name="Key">識別キー</param>
        public cPerson(string pKey, List<string> Paths)
        {
            this._pKey = pKey;
            Expression[] tExpression = new Expression[Paths.Count];

            for (int i = 0; i < Paths.Count; i++)
            {
                tExpression[i] = new Expression(Paths[i]);
            }

            //以下紐つけ作業です。非効率なので，要手直しです。
            int index;
            
            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("a"));
            this._a = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("i"));
            this._i = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("u"));
            this._u = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("e"));
            this._e = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("n"));
            this._n = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("cl"));
            this._cl = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("ol"));
            this._ol = tExpression[index];

            index = Array.FindIndex(tExpression, s => s.eName.StartsWith("o"));
            this._o = tExpression[index];
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 形状テクスチャの分離をします。各表情の次元数は統一してください。
        /// </summary>
        /// <returns></returns>
        public XYZPointData[] cDataToShape()
        {
            XYZPointData[] ReturnPoint = 
            {
                _a.MakeShape(),
                _i.MakeShape(),
                _u.MakeShape(),
                _e.MakeShape(),
                _o.MakeShape(),
                _cl.MakeShape(),
                _ol.MakeShape(),
                _n.MakeShape()
            };
            return ReturnPoint;
        }

        public XYZPointData[] cDataToTexture()
        {
            XYZPointData[] ReturnPoint =
            {
                _a.MakeTexture(),
                _i.MakeTexture(),
                _u.MakeTexture(),
                _e.MakeTexture(),
                _o.MakeTexture(),
                _cl.MakeTexture(),
                _ol.MakeTexture(),
                _n.MakeTexture()
            };
            return ReturnPoint;
        }

        /// <summary>
        /// 自身の真顔からの差分顔を求めます。保持していたXYZRGB情報は破棄されます。
        /// </summary>
        public void SubstructFromExpressionless()
        {
            this._a = _a - _n;
            this._i = _i - _n;
            this._u = _u - n;
            this._e = _e - _n;
            this._o = _o -_n;
            this._cl =_cl - _n;
            this._ol =_ol - _n;
            this._n = _n - _n;
        }

        #endregion
    }
}
