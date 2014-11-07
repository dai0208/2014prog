using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    /// <summary>
    /// オブジェクトをXY平面に載せるためのパラメータを保持する構造体
    /// </summary>
    public class MoveParam
    {
        protected TriangleData _BaseTriangle;
        protected TriangleData _TransformedTriangle;
        protected double _MoveX;
        protected double _MoveY;
        protected double _MoveZ;
        protected double _AngleZ;
        protected double _AngleY;
        protected double _AngleX;
        protected double _ReverseAngleX;
        protected double _ReverseAngleY;

        protected MoveParam() { }

        /// <summary>
        /// 三角形をXY平面にのせます。
        /// 第一頂点が原点、第二頂点がY軸上、第三頂点がX軸上になります。
        /// </summary>
        /// <param name="TriangleData">回転させる三角形</param>
        public MoveParam(TriangleData TriangleData)
        {
            #region XY平面に乗せる
            _BaseTriangle = new TriangleData(TriangleData);
            _TransformedTriangle = new TriangleData(TriangleData);

            //Step１：頂点１を原点に移動
            {
                //移動量
                _MoveX = -1 * _TransformedTriangle.Point1.X;
                _MoveY = -1 * _TransformedTriangle.Point1.Y;
                _MoveZ = -1 * _TransformedTriangle.Point1.Z;

                //移動                
                _TransformedTriangle.Move(_MoveX, _MoveY, _MoveZ);
            }

            //Step２：頂点３をｘ軸上に乗せる
            {
                //ｙ軸周りの回転
                _AngleY = -1 * Math.Atan2(_TransformedTriangle.Point3.Z, _TransformedTriangle.Point3.X);
                _TransformedTriangle.Rotate_Y(_AngleY);

                //ｚ軸周りの回転
                _AngleZ = -1 * Math.Atan2(_TransformedTriangle.Point3.Y, _TransformedTriangle.Point3.X);
                _TransformedTriangle.Rotate_Z(_AngleZ);
            }

            //Step３：頂点２をｘｙ平面に乗せる
            {
                //x軸周りの回転
                _AngleX = -1 * Math.Atan2(_TransformedTriangle.Point2.Z, _TransformedTriangle.Point2.Y);
                _TransformedTriangle.Rotate_X(_AngleX);
            }

            //Step４：頂点３のｘ座標がマイナスの場合（三角形をｘｙ平面の第一象限に持っていく）
            {
                if (_TransformedTriangle.Point3.X < 0)
                    _ReverseAngleX = Math.PI;
                else
                    _ReverseAngleX = 0;

                _TransformedTriangle.Rotate_Y(_ReverseAngleX);

            }

            //Step５：頂点２のｙ座標がマイナスの場合（三角形をｘｙ平面の第一象限に持っていく）
            {
                if (_TransformedTriangle.Point2.Y < 0)
                    _ReverseAngleY = Math.PI;
                else
                    _ReverseAngleY = 0;

                _TransformedTriangle.Rotate_X(_ReverseAngleY);
            }
            #endregion
        }

        #region 順処理
        public TriangleData GetDataInOrder(TriangleData TriangleData)
        {
            TriangleData ResultTriangle = new TriangleData(TriangleData);
            ResultTriangle.Move(_MoveX, _MoveY, _MoveZ);
            ResultTriangle.Rotate_Y(_AngleY);
            ResultTriangle.Rotate_Z(_AngleZ);
            ResultTriangle.Rotate_X(_AngleX);
            ResultTriangle.Rotate_Y(_ReverseAngleX);
            ResultTriangle.Rotate_X(_ReverseAngleY);
            return ResultTriangle;
        }

        public cPoint GetDataInOrder(cPoint Point)
        {
            cPoint ResultPoint = new cPoint(Point);
            ResultPoint.Move(_MoveX, _MoveY, _MoveZ);
            ResultPoint.RotateR_Y(_AngleY);
            ResultPoint.RotateR_Z(_AngleZ);
            ResultPoint.RotateR_X(_AngleX);
            ResultPoint.RotateR_Y(_ReverseAngleX);
            ResultPoint.RotateR_X(_ReverseAngleY);
            return ResultPoint;
        }

        public XYZPoint GetDataInOrder(XYZPoint Point)
        {
            XYZPoint ResultPoint = new XYZPoint(Point);
            ResultPoint.Move(_MoveX, _MoveY, _MoveZ);
            ResultPoint.RotateR_Y(_AngleY);
            ResultPoint.RotateR_Z(_AngleZ);
            ResultPoint.RotateR_X(_AngleX);
            ResultPoint.RotateR_Y(_ReverseAngleX);
            ResultPoint.RotateR_X(_ReverseAngleY);

            return new XYZPoint(ResultPoint.X, ResultPoint.Y, ResultPoint.Z);
        }

        public cPointData GetDataInOrder(cPointData PointData)
        {
            cPointData ResultData = new cPointData(PointData);
            ResultData.Move(_MoveX, _MoveY, _MoveZ);
            ResultData.RotateYRadian(_AngleY);
            ResultData.RotateZRadian(_AngleZ);
            ResultData.RotateXRadian(_AngleX);
            ResultData.RotateYRadian(_ReverseAngleX);
            ResultData.RotateXRadian(_ReverseAngleY);
            return ResultData;
        }

        public XYZPointData GetDataInOrder(XYZPointData PointData)
        {
            XYZPointData ResultData = new XYZPointData(PointData);
            ResultData.Move(_MoveX, _MoveY, _MoveZ);
            ResultData.RotateYRadian(_AngleY);
            ResultData.RotateZRadian(_AngleZ);
            ResultData.RotateXRadian(_AngleX);
            ResultData.RotateYRadian(_ReverseAngleX);
            ResultData.RotateXRadian(_ReverseAngleY);
            return ResultData;
        }
        #endregion
        #region 逆順処理
        public TriangleData GetDataInverseOrder(TriangleData TriangleData)
        {
            TriangleData ResultTriangle = new TriangleData(TriangleData);
            ResultTriangle.Rotate_X(-_ReverseAngleY);
            ResultTriangle.Rotate_Y(-_ReverseAngleX);
            ResultTriangle.Rotate_X(-_AngleX);
            ResultTriangle.Rotate_Z(-_AngleZ);
            ResultTriangle.Rotate_Y(-_AngleY);
            ResultTriangle.Move(-_MoveX, -_MoveY, -_MoveZ);
            return ResultTriangle;
        }
        public cPoint GetDataInverseOrder(cPoint Point)
        {
            cPoint ResultPoint = new cPoint(Point);
            ResultPoint.RotateR_X(-_ReverseAngleY);
            ResultPoint.RotateR_Y(-_ReverseAngleX);
            ResultPoint.RotateR_X(-_AngleX);
            ResultPoint.RotateR_Z(-_AngleZ);
            ResultPoint.RotateR_Y(-_AngleY);
            ResultPoint.Move(-_MoveX, -_MoveY, -_MoveZ);
            return ResultPoint;
        }

        public XYZPoint GetDataInverseOrder(XYZPoint Point)
        {
            XYZPoint ResultPoint = new XYZPoint(Point);
            ResultPoint.RotateR_X(-_ReverseAngleY);
            ResultPoint.RotateR_Y(-_ReverseAngleX);
            ResultPoint.RotateR_X(-_AngleX);
            ResultPoint.RotateR_Z(-_AngleZ);
            ResultPoint.RotateR_Y(-_AngleY);
            ResultPoint.Move(-_MoveX, -_MoveY, -_MoveZ);
            return ResultPoint;
        }

        public cPointData GetDataInverseOrder(cPointData PointData)
        {
            cPointData ResultData = new cPointData(PointData);
            ResultData.RotateXRadian(-_ReverseAngleY);
            ResultData.RotateYRadian(-_ReverseAngleX);
            ResultData.RotateXRadian(-_AngleX);
            ResultData.RotateZRadian(-_AngleZ);
            ResultData.RotateYRadian(-_AngleY);
            ResultData.Move(-_MoveX, -_MoveY, -_MoveZ);
            return ResultData;
        }

        public XYZPointData GetDataInverseOrder(XYZPointData PointData)
        {
            XYZPointData ResultData = new XYZPointData(PointData);
            ResultData.RotateXRadian(-_ReverseAngleY);
            ResultData.RotateYRadian(-_ReverseAngleX);
            ResultData.RotateXRadian(-_AngleX);
            ResultData.RotateZRadian(-_AngleZ);
            ResultData.RotateYRadian(-_AngleY);
            ResultData.Move(-_MoveX, -_MoveY, -_MoveZ);
            return ResultData;
        }


        #endregion
        #region プロパティ
        /// <summary>
        /// X方向の移動量を取得、設定します。
        /// </summary>
        public double MoveX { get { return _MoveX; } set { _MoveX = value; } }
        /// <summary>
        /// Y方向の移動量を取得、設定します。
        /// </summary>
        public double MoveY { get { return _MoveY; } set { _MoveY = value; } }
        /// <summary>
        /// Z方向の移動量を取得、設定します。
        /// </summary>
        public double MoveZ { get { return _MoveZ; } set { _MoveZ = value; } }
        /// <summary>
        /// Z軸回転角を取得、設定します。
        /// </summary>
        public double AngleZ { get { return _AngleZ; } set { _AngleZ = value; } }
        /// <summary>
        /// Y軸回転角を取得、設定します。
        /// </summary>
        public double AngleY { get { return _AngleY; } set { _AngleY = value; } }
        /// <summary>
        /// X軸回転角を取得、設定します。
        /// </summary>
        public double AngleX { get { return _AngleX; } set { _AngleX = value; } }
        /// <summary>
        /// X軸反転回転角度を取得、設定します。
        /// </summary>
        public double ReverseAngleX { get { return _ReverseAngleX; } set { _ReverseAngleX = value; } }
        /// <summary>
        /// Y軸反転回転角度を取得、設定します。
        /// </summary>
        public double ReverseAngleY { get { return _ReverseAngleY; } set { _ReverseAngleY = value; } }
        /// <summary>
        /// 基準となる三角形を取得します
        /// </summary>
        public TriangleData BaseTriangleData { get { return new TriangleData(_BaseTriangle); } }
        /// <summary>
        /// 基準となる三角形を回転させ、XY平面に載るようにしたものを取得します。
        /// </summary>
        public TriangleData TransformedTriangle { get { return new TriangleData(_TransformedTriangle); } }
        /// <summary>
        /// Triangle2のX座標を取得します。
        /// </summary>
        public double X2 { get { return _TransformedTriangle.Point2.X; } }
        /// <summary>
        /// Triangle3のX座標を取得します。
        /// </summary>
        public double X3 { get { return _TransformedTriangle.Point3.X; } }
        /// <summary>
        /// Triangle2のY座標を取得します。
        /// </summary>
        public double Y2 { get { return _TransformedTriangle.Point2.Y; } }
        /// <summary>
        /// Triangle3のY座標を取得します。
        /// </summary>
        public double Y3 { get { return _TransformedTriangle.Point3.Y; } }
        /// <summary>
        /// 行列式の逆数を取得します。
        /// </summary>
        public double DetInverse { get { return 1d / (X2 * Y3 - Y2 * X3); } }
        #endregion
    }
}
