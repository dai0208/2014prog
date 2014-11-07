using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    [Serializable]
    public class DoublePoint
    {
        protected double _X, _Y;
        protected object _Tag;

        public DoublePoint()
        {
            //値が渡されなかったら全部の座標を0でセット
            _X = 0;
            _Y = 0;
            _Tag = null;
        }

        public DoublePoint(double X, double Y)
        {
            //変数に座標をセット
            _X = X;
            _Y = Y;
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="icPoint">コピーされたインスタンス</param>
        public DoublePoint(DoublePoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;

            this._Tag = icPoint._Tag;
        }

        public DoublePoint(cPoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;

            this._Tag = icPoint._Tag;
        }
        public DoublePoint(XYZPoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;

            this._Tag = icPoint._Tag;
        }
        public virtual double Distance(double d_X, double d_Y)
        {
            //2点間の二次元（XY平面限定）ユークリッド距離を返します。
            double dDx = d_X - _X, dDy = d_Y - _Y;
            return Math.Sqrt(Math.Pow(dDx, 2) + Math.Pow(dDy, 2));
        }

        public virtual DoublePoint GetRotatePointR(double Radian)
        {
            DoublePoint ResultPoint = new DoublePoint();
            ResultPoint.X = this.X * Math.Cos(Radian) + this.Y * Math.Sin(Radian);
            ResultPoint.Y = -this.X * Math.Sin(Radian) + this.Y * Math.Cos(Radian);
            return ResultPoint;
        }

        public virtual DoublePoint GetRotatePointD(double Degree)
        {
            return this.GetRotatePointR(Degree * Math.PI / 180);
        }

        public virtual void RotatePointR(double Radian)
        {
            DoublePoint ResultPoint = this.GetRotatePointR(Radian);
            this.X = ResultPoint.X;
            this.Y = ResultPoint.Y;
        }

        public virtual void RotatePointD(double Degree)
        {
            this.RotatePointR(Degree * Math.PI / 180);
        }


        /// <summary>
        /// Xの値を取得、設定します。
        /// </summary>
        public double X
        {
            set { _X = value; }
            get { return _X; }
        }

        /// <summary>
        /// Yの値を取得、設定します。
        /// </summary>
        public double Y
        {
            set { _Y = value; }
            get { return _Y; }
        }

        /// <summary>
        /// 汎用的なオブジェクトを保持します。object型にキャストして使ってください。
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        public virtual string strOutput()
        {
            return _X.ToString() + "\t" + _Y.ToString() + "\n";
        }

        public override string ToString()
        {
            return string.Format("{0,3:F3}, {1,3:F3}", _X, _Y);
        }
    }
}
