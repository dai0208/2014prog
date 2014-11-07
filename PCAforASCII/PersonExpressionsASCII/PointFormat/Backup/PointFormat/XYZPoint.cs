using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    [Serializable]
    public class XYZPoint:DoublePoint
    {
        protected double  _Z;

        public XYZPoint()
        {
            //値が渡されなかったら全部の座標を0でセット
            _Z = 0;
        }

        public XYZPoint(double X, double Y, double Z)
            :base(X,Y)
        {
            //変数に座標をセット
            _Z = Z;
        }

                /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="icPoint">コピーされたインスタンス</param>
        public XYZPoint(XYZPoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;
            this._Z = icPoint._Z;

            this._Tag = icPoint._Tag;
        }

        public XYZPoint(cPoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;
            this._Z = icPoint._Z;

            this._Tag = icPoint._Tag;
        }

        public XYZPoint(DoublePoint Point)
        {
            this._X = Point.X;
            this._Y = Point.Y;
        }

        #region プロパティ
        /// <summary>
        /// Zの値を取得、設定します。
        /// </summary>
        public double Z
        {
            set { _Z = value; }
            get { return _Z; }
        }
        #endregion

        #region ベクトル操作
        #region 回転処理いっぱい

        //回転変換です。最後のアルファベットはその軸周りに回転するということで、RとDはradianとDegreeの違いです。
        //Radianの方はvRotateに送り、Degreeの方は単位をRadianに変換してvRotateに送ってるだけです。
        public virtual void RotateD_X(double dDegreeAngle)
        {
            //角度をラジアンに変換して回転処理へ渡す
            //変換処理後座標の値が変わっていないといけないので参照引渡しにする。
            Rotate(ref _Y, ref _Z, (dDegreeAngle / 180) * System.Math.PI);
        }

        public virtual void RotateR_X(double dRadianAngle)
        {
            //最初から角度がラジアンなので回転処理へ渡す
            Rotate(ref _Y, ref _Z, dRadianAngle);
        }

        public virtual void RotateD_Y(double dDegreeAngle)
        {
            //角度をラジアンに変換して回転処理へ渡す
            //変換処理後座標の値が変わっていないといけないので参照引渡しにする。
            Rotate(ref _X, ref _Z, (dDegreeAngle / 180) * System.Math.PI);
        }

        public virtual void RotateR_Y(double dRadianAngle)
        {
            //最初から角度がラジアンなので回転処理へ渡す
            Rotate(ref _X, ref _Z, dRadianAngle);
        }

        public virtual void RotateD_Z(double dDegreeAngle)
        {
            //角度をラジアンに変換して回転処理へ渡す
            //変換処理後座標の値が変わっていないといけないので参照引渡しにする。
            Rotate(ref _X, ref _Y, (dDegreeAngle / 180) * System.Math.PI);
        }

        public virtual void RotateR_Z(double dRadianAngle)
        {
            //最初から角度がラジアンなので回転処理へ渡す
            Rotate(ref _X, ref _Y, dRadianAngle);
        }

        public virtual void RotateR_XYZ(double dSentAngle_X, double dSentAngle_Y, double dSentAngle_Z)
        {
            cQuaternion icqRot = new cQuaternion(dSentAngle_X, dSentAngle_Y, dSentAngle_Z);

            XYZPoint icWorkPoint = icqRot.cpRotate(this);

            this._X = icWorkPoint.X;
            this._Y = icWorkPoint.Y;
            this._Z = icWorkPoint.Z;
        }


        //回転処理の実体部分です。
        //もしもこの関数をPublicにして使う時は引数の最初の2個は参照呼出しを指定するrefを忘れないように注意してください。
        //参照呼出しにすることによって軸ごとの回転関数を用意する必要がなくなります。
        //X軸周りの回転ならY,Zの値を、Y軸周りならX,Zの値を、Z軸周りならX,Yの値を渡してください。
        private void Rotate(ref double d_X, ref double d_Y, double dAngle)
        {
            double dTempX = d_X, dTempY = d_Y;
            //ポイントとして、ここでとられている引数dAngleの単位は必ずラジアンになっています。
            //また便宜上引数の名前をXとかYとかにしているけど、呼び出し元の値が必ずしもX座標の値やY座標の値とは限りません。
            //Y軸を中心に回転ならばX座標の値ととZ座標の値となります。

            //回転したい角度のCosとSinを計算
            double dCos = System.Math.Cos(dAngle);
            double dSin = System.Math.Sin(dAngle);

            //回転処理
            d_X = dTempX * dCos - dTempY * dSin;
            d_Y = dTempY * dCos + dTempX * dSin;
        }
        #endregion




        //二点間の距離を返す関数です。用途によって二種類のオーバーロードがあります。
        //X,Y,Zの3次元の距離かX,Yの2次元の距離のどっちかです。Y,Zとかの2次元は用意していませんが
        //dDistance(cPoint.dX_Value, Y, Z)の様に呼び出してやると(Y, Z)と(dY, dZ)の距離を返すことができます。
        //        ※cPointはこの呼び出し元のインスタンスの意味です。cPoint.dX_ValueとdXは同じ値なので実質2次元の距離になりますね。
        public virtual double Distance(double d_X, double d_Y, double d_Z)
        {
            //2点間の三次元ユークリッド距離を返します。
            double dDx = d_X - _X, dDy = d_Y - _Y, dDz = d_Z - _Z;
            return Math.Sqrt((Math.Pow(dDx, 2) + Math.Pow(dDy, 2) + Math.Pow(dDz, 2)));
        }


        public virtual double Distance(XYZPoint Point)
        {
            return this.Distance(Point._X, Point._Y, Point._Z);
        }

        //移動です。引数の移動量はRapidForm上での値です。
        public virtual void Move(double d_X, double d_Y, double d_Z)
        {
            _X += d_X;
            _Y += d_Y;
            _Z += d_Z;
        }


        //拡大縮小です。すべての軸に対して同じ倍率の変換をかけます。
        public virtual void Ratio(double dRatio)
        {
            _X *= dRatio;
            _Y *= dRatio;
            _Z *= dRatio;
        }


        //拡大縮小です。軸ごとの倍率を変えることができます。
        public virtual void Ratio(double dXRatio, double dYRatio, double dZRatio)
        {
            _X *= dXRatio;
            _Y *= dYRatio;
            _Z *= dZRatio;
        }
        #endregion


        #region IComparable メンバ

        public int CompareTo(object obj)
        {
            //Sort関数が呼ばれたときにこのメソッドが呼ばれます。
            //今回はY座標を比較して値を返してますが、別にX座標を返すことにしてもかまいません。
            //どの値でソートするかを保持する変数を持たせて可変にしてもいいかもしれません。
            //ま、今回は使わないと思うのでとりあえずY座標で比較ということで。
            cPoint icPointCompare = (cPoint)obj;
            return (int)((this._Y - icPointCompare._Y) * 1000);
        }

        #endregion

        #region 明示的な型変換
        /// <summary>
        /// cPoint型からPoint型への変換
        /// </summary>
        /// <param name="icpPoint">cPoint型のインスタンス</param>
        /// <returns>Point型のインスタンス</returns>
        public static explicit operator System.Drawing.Point(XYZPoint icpPoint)
        {
            return new System.Drawing.Point((int)icpPoint.X, (int)icpPoint.Y);
        }

        /// <summary>
        /// Point型からcPoint型への変換
        /// </summary>
        /// <param name="pPoint">Point型のインスタンス</param>
        /// <returns>cPoint型のインスタンス</returns>
        public static explicit operator XYZPoint(System.Drawing.Point pPoint)
        {
            XYZPoint XYZPoint = new XYZPoint( pPoint.X, pPoint.Y,0);  
            return XYZPoint;
        }
        #endregion

        //現在の保持している情報をタブ区切りのデータとしてstring型で返します。
        //返すデータのフォーマットは dX dY dZ iR iG iBの順番でそのままRapidFormで読める形です。
        public override string strOutput()
        {
            return _X.ToString() + "\t" + _Y.ToString() + "\t" + _Z.ToString() + "\n";
        }

        public override string ToString()
        {
            return string.Format("{0,3:F3}, {1,3:F3}, {2,3:F3}", _X, _Y, _Z);
        }

        public override bool Equals(object obj)
        {
            XYZPoint RefXYZPoint = (XYZPoint)obj;
            if (RefXYZPoint.X == this.X & RefXYZPoint.Y == this.Y & RefXYZPoint.Z == this.Z)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
