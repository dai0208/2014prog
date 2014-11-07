using System;
using System.Collections.Generic;

namespace PointFormat
{


    /// <summary>
    /// 三角形の各頂点の座標を保持する構造体
    /// </summary>
    [Serializable]
    public class TriangleData
    {
        #region プライベート変数
        /// <summary>
        /// 頂点１
        /// </summary>
        private XYZPoint _Point1;

        /// <summary>
        /// 頂点２
        /// </summary>
        private XYZPoint _Point2;

        /// <summary>
        /// 頂点３
        /// </summary>
        private XYZPoint _Point3;
        
        /// <summary>
        /// 三角形の重み
        /// </summary>
        private int _Weight;

        #endregion

        #region コンストラクタ

        public TriangleData()
        {
            this._Point1 = new XYZPoint(0, 0, 0);
            this._Point2 = new XYZPoint(0, 0, 0);
            this._Point3 = new XYZPoint(0, 0, 0);
            this.Weight = 30;
        }

        public TriangleData(TriangleData TriangleData)
        {
            this._Point1 = new XYZPoint(TriangleData.Point1);
            this._Point2 = new XYZPoint(TriangleData.Point2);
            this._Point3 = new XYZPoint(TriangleData.Point3);
            this._Weight = TriangleData.Weight;
        }
        #endregion

        /// <summary>
        /// 三角形の重心の座標を計算します
        /// </summary>
        /// <returns>重心の座標</returns>
        public XYZPoint GetCenterPoint()
        {
            XYZPoint cp = new XYZPoint();

            cp.X = (_Point1.X + _Point2.X + _Point3.X) / 3d;
            cp.Y = (_Point1.Y + _Point2.Y + _Point3.Y) / 3d;
            cp.Z = (_Point1.Z + _Point2.Z + _Point3.Z) / 3d;

            return cp;
        }

        /// <summary>
        /// 三角形の面積を取得します。
        /// </summary>
        public double Area
        {
            get
            {
                XYZPoint VectorA = new XYZPoint(_Point2.X - _Point1.X, _Point2.Y - _Point1.Y, _Point2.Z - _Point1.Z);
                XYZPoint VectorB = new XYZPoint(_Point3.X - _Point1.X, _Point3.Y - _Point1.Y, _Point3.Z - _Point1.Z);

                double NormA = Math.Sqrt(VectorA.X * VectorA.X + VectorA.Y * VectorA.Y + VectorA.Z * VectorA.Z);
                double NormB = Math.Sqrt(VectorB.X * VectorB.X + VectorB.Y * VectorB.Y + VectorB.Z * VectorB.Z);

                double InnerProduct = VectorA.X * VectorB.X + VectorA.Y * VectorB.Y + VectorA.Z * VectorB.Z;

                double Area = (1d / 2d) * Math.Sqrt(NormA * NormA * NormB * NormB - InnerProduct * InnerProduct);

                return Area;
            }
        }

        #region 三角形の内部PointData取得系
        public int[] GetIndexInTriangle(cPointData PointData,  double Threshould)
        {
            if (Threshould <= 0)
                throw new ApplicationException("閾値は正の値にして下さい。");

            MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(this);
            var MovedPointData = MoveOnBaseTriangle.GetDataInOrderOnBase(PointData);

            //インデックスリスト
            List<int> IndexList = new List<int>();

            //すべての点が三角形内かどうか判別
            for (int i = 0; i < PointData.Length; i++)
            {
                double ChangeX = MovedPointData[i].X;
                double ChangeY = MovedPointData[i].Y;
                //三角形内のときは何番目かを書き込む(三角形の周り４％も三角形内と見なす）
                if ((ChangeX >= -0.04) & (ChangeY >= -0.04) & (ChangeX + ChangeY <= 1.04) & (MovedPointData[i].Z > -Threshould))
                    IndexList.Add(i);
            }

            return IndexList.ToArray();
        }

        public int[] GetIndexInTriangle(XYZPointData PointData, double Threshould)
        {
            if (Threshould <= 0)
                throw new ApplicationException("閾値は正の値にして下さい。");

            MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(this);
            var MovedPointData = MoveOnBaseTriangle.GetDataInOrderOnBase(PointData);

            //インデックスリスト
            List<int> IndexList = new List<int>();

            //すべての点が三角形内かどうか判別
            for (int i = 0; i < PointData.Length; i++)
            {
                double ChangeX = MovedPointData[i].X;
                double ChangeY = MovedPointData[i].Y;
                //三角形内のときは何番目かを書き込む(三角形の周り４％も三角形内と見なす）
                if ((ChangeX >= -0.04) & (ChangeY >= -0.04) & (ChangeX + ChangeY <= 1.04) & (MovedPointData[i].Z > -Threshould))
                    IndexList.Add(i);
            }

            return IndexList.ToArray();
        }

        public cPointData GetPointDataInTriangle(cPointData PointData, double Threshould)
        {
            int[] Index = GetIndexInTriangle(PointData, Threshould);
            return PointData.GetIndexData(Index);
        }

        public XYZPointData GetPointDataInTriangle(XYZPointData PointData, double Threshould)
        {
            int[] Index = GetIndexInTriangle(PointData, Threshould);
            return PointData.GetIndexData(Index);
        }

        #endregion

        #region 移動・回転
        /// <summary>
        /// 三角形を移動させる
        /// </summary>
        /// <param name="dX">Xの移動量</param>
        /// <param name="dY">Yの移動量</param>
        /// <param name="dZ">Zの移動量</param>
        public void Move(double dX, double dY, double dZ)
        {
            this.Point1.Move(dX, dY, dZ);
            this.Point2.Move(dX, dY, dZ);
            this.Point3.Move(dX, dY, dZ);
        }

        /// <summary>
        /// X軸まわりに回転させる
        /// </summary>
        /// <param name="dTheta">回転角度</param>
        public void Rotate_X(double dTheta)
        {
            this.Point1.RotateR_X(dTheta);
            this.Point2.RotateR_X(dTheta);
            this.Point3.RotateR_X(dTheta);

        }

        /// <summary>
        /// Y軸まわりに回転させる
        /// </summary>
        /// <param name="dTheta">回転角度</param>
        public void Rotate_Y(double dTheta)
        {
            this.Point1.RotateR_Y(dTheta);
            this.Point2.RotateR_Y(dTheta);
            this.Point3.RotateR_Y(dTheta);
        }

        /// <summary>
        /// Z軸まわりに回転させる
        /// </summary>
        /// <param name="dTheta">回転角度</param>
        public void Rotate_Z(double dTheta)
        {
            this.Point1.RotateR_Z(dTheta);
            this.Point2.RotateR_Z(dTheta);
            this.Point3.RotateR_Z(dTheta);

        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 三角形頂点を取得、設定します。
        /// </summary>
        public virtual XYZPoint[] Triangles
        {
            get
            {
                XYZPoint[] icpWorkPoint = new XYZPoint[3];
                icpWorkPoint[0] = _Point1;
                icpWorkPoint[1] = _Point2;
                icpWorkPoint[2] = _Point3;

                return icpWorkPoint;
            }
            set
            {
                _Point1 = new XYZPoint(value[0]);
                _Point2 = new XYZPoint(value[1]);
                _Point3 = new XYZPoint(value[2]);
            }
        }

        /// <summary>
        /// 頂点１を取得、設定します。
        /// </summary>
        public virtual XYZPoint Point1
        {
            get { return _Point1; }
            set { _Point1 = value; }
        }

        /// <summary>
        /// 頂点２を取得、設定します。
        /// </summary>
        public virtual XYZPoint Point2
        {
            get { return _Point2; }
            set { _Point2 = value; }
        }

        /// <summary>
        /// 頂点３を取得、設定します。
        /// </summary>
        public virtual XYZPoint Point3
        {
            get { return _Point3; }
            set { _Point3 = value; }
        }

        public virtual XYZPoint this[int Index]
        {
            get { return this.Triangles[Index]; }
            set { this.Triangles[Index] = value; }
        }

        public virtual int Weight
        {
            get { return this._Weight; }
            set { this._Weight = value; }
        }

        #endregion

        public override string ToString()
        {
            return "Point1: " + this._Point1.ToString() + "   \tPoint2: " + this._Point2.ToString() + "   \tPoint3: " + this._Point3.ToString() + "\n";
        }

        public void SaveAscii(string SaveFileName)
        {
            XYZPointData xyzData = new XYZPointData();
            xyzData.Items = new XYZPoint[3];
            xyzData[0] = Point1;
            xyzData[1] = Point2;
            xyzData[2] = Point3;

            xyzData.Save(SaveFileName);
        }
    }
}