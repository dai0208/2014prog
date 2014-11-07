using System;

namespace PointFormat
{
    /// <summary>
    /// 三次元の点データの座標や色を保持し、簡単なベクトル操作を扱う事のできるクラスです。
    /// </summary>
    [Serializable]
    public class cPoint : XYZPoint, IComparable
    {
        protected int _R, _G, _B;


        #region プロパティ
        /// <summary>
        /// Rの値を取得、設定します。値は0～255の範囲になります。
        /// </summary>
        public int R
        {
            get { return _R; }
            set
            {
                /*
                if (value > 255)
                    _R = 255;
                else if (value < 0)
                    _R = 0;
                else
                 */
                _R = value;
                 
            }
        }

        /// <summary>
        /// Gの値を取得、設定します。値は0～255の範囲になります。
        /// </summary>
        public int G
        {
            get { return _G; }
            set
            {
                /*
                if (value > 255)
                    _G = 255;
                else if (value < 0)
                    _G = 0;
                else
                 */ 
                    _G = value;
            }
        }

        /// <summary>
        /// Bの値を取得、設定します。値は0～255の範囲になります。
        /// </summary>
        public int B
        {
            get { return _B; }
            set
            {
                /*
                if (value > 255)
                    _B = 255;
                else if (value < 0)
                    _B = 0;
                else
                 */ 
                    _B = value;
            }
        }

        /// <summary>
        /// 点の色を取得、設定します。
        /// </summary>
        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(_R, _G, _B); }
            set
            {
                _R = value.R;
                _G = value.G;
                _B = value.B;
            }
        }

        #endregion

        #region コンストラクタ

        public cPoint()
            : base()
        {
            _R = 0;
            _G = 0;
            _B = 0;
        }

        public cPoint(double X, double Y, double Z, int R, int G, int B)
        {
            //変数に座標をセット
            _X = X;
            _Y = Y;
            _Z = Z;

            //色も同時にセット。
            _R = R;
            _G = G;
            _B = B;
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="icPoint">コピーされたインスタンス</param>
        public cPoint(cPoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;
            this._Z = icPoint._Z;

            this._R = icPoint._R;
            this._G = icPoint._G;
            this._B = icPoint._B;

            this._Tag = icPoint._Tag;
        }

        public cPoint(XYZPoint XYZPoint)
        {
            this._X = XYZPoint.X;
            this._Y = XYZPoint.Y;
            this._Z = XYZPoint.Z;

            this._R = 0;
            this._G = 0;
            this._B = 0;

            this._Tag = XYZPoint.Tag;
        }

        #endregion

        //現在の保持している情報をタブ区切りのデータとしてstring型で返します。
        //返すデータのフォーマットは dX dY dZ iR iG iBの順番でそのままRapidFormで読める形です。
        public override string strOutput()
        {
            return _X.ToString() + "\t" + _Y.ToString() + "\t" + _Z.ToString() + "\t" + _R.ToString() + "\t" + _G.ToString() + "\t" + _B.ToString() + "\n";
        }

        public override string ToString()
        {
            return string.Format("{0,3:F3}, {1,3:F3}, {2,3:F3}, {3,3:D}, {4,3:D}, {5,3:D}", _X, _Y, _Z, _R, _B, _G);
        }

    }

}
