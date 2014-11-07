using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    public class MoveOnBaseTriangle:MoveParam
    {
        protected MoveOnBaseTriangle() { }

        public MoveOnBaseTriangle(TriangleData TriangleData)
            : base(TriangleData)
        {        }

        #region 順処理 
        public XYZPoint GetDataInOrderOnBase(XYZPoint XYZPoint)
        {
            var ResultPoint = base.GetDataInOrder(XYZPoint);
            double ChangeX = DetInverse * (Y3 * ResultPoint.X - X3 * ResultPoint.Y);
            double ChangeY = DetInverse * (X2 * ResultPoint.Y - Y2 * ResultPoint.X);
            return new XYZPoint(ChangeX, ChangeY, ResultPoint.Z);
        }

        public cPoint GetDataInOrderOnBase(cPoint cPoint)
        {
            var ResultPoint = base.GetDataInOrder(cPoint);
            double ChangeX = DetInverse * (Y3 * ResultPoint.X - X3 * ResultPoint.Y);
            double ChangeY = DetInverse * (X2 * ResultPoint.Y - Y2 * ResultPoint.X);
            return new cPoint(ChangeX, ChangeY, ResultPoint.Z, ResultPoint.R, ResultPoint.G, ResultPoint.B);
        }
        
        public XYZPointData GetDataInOrderOnBase(XYZPointData XYZPointData)
        {
            var MovedPointData = new XYZPointData(XYZPointData);
            for (int i = 0; i < XYZPointData.Length; i++)
                MovedPointData[i] = this.GetDataInOrderOnBase(MovedPointData[i]);

            return MovedPointData;
        }

        public cPointData GetDataInOrderOnBase(cPointData PointData)
        {
            cPointData MovedPointData = new cPointData(PointData);
            for (int i = 0; i < MovedPointData.Length; i++)
                MovedPointData[i] = this.GetDataInOrderOnBase(MovedPointData[i]);

            return MovedPointData;
        }

        
        #endregion

        #region 逆順処理
        public XYZPoint GetDataInverseOrderOnBase(DoublePoint DoublePoint)
        {
            double ChangeX = DoublePoint.X * _TransformedTriangle.Point2.X + DoublePoint.Y * _TransformedTriangle.Point3.X;
            double ChangeY = DoublePoint.X * _TransformedTriangle.Point2.Y + DoublePoint.Y * _TransformedTriangle.Point3.Y;
            return base.GetDataInverseOrder(new XYZPoint(ChangeX, ChangeY, 0));
        }

        public XYZPointData GetDataInverseOrderOnBase(XYZPointData XYZPointData)
        {
            var ResultPointData = new XYZPointData(XYZPointData);
            for (int i = 0; i < ResultPointData.Length; i++)
                ResultPointData[i] = this.GetDataInverseOrderOnBase(new DoublePoint(ResultPointData[i].X, ResultPointData[i].Y));

            return ResultPointData;
        }

        public cPoint GetDataInverseOrderOnBase(cPoint Point)
        {
            double ChangeX = Point.X * _TransformedTriangle.Point2.X + Point.Y * _TransformedTriangle.Point3.X;
            double ChangeY = Point.X * _TransformedTriangle.Point2.Y + Point.Y * _TransformedTriangle.Point3.Y;
            return base.GetDataInverseOrder(new cPoint(ChangeX, ChangeY, 0, Point.R, Point.G, Point.B));
        }

        public cPointData GetDataInverseOrderOnBase(cPointData PointData)
        {
            var ResultPointData = new cPointData(PointData);
            for (int i = 0; i < ResultPointData.Length; i++)
                ResultPointData[i] = this.GetDataInverseOrderOnBase(PointData[i]);

            return ResultPointData;
        }
        #endregion

    }
}
