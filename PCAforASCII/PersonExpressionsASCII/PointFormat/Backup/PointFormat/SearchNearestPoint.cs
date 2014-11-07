using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    /// <summary>
    /// 最近傍点を探すクラスです。
    /// </summary>
    static public class SearchNearestPoint
    {
        #region 総当最近傍点探索
        /// <summary>
        /// 参照点と指定した点群中の最近傍点のインデックスを取得します。
        /// </summary>
        /// <param name="BaseDataPoint">指定した点群</param>
        /// <param name="RefPoint">参照点</param>
        /// <returns>最近傍点のインデックス</returns>
        static public int Get3DNearestPointIndex(XYZPointData BaseDataPoint, XYZPoint RefPoint)
        {
            double MinDistance = double.MaxValue;
            int Index = 0;

            for (int i = 0; i < BaseDataPoint.Length; i++)
            {
                double Distance = BaseDataPoint[i].Distance(RefPoint);
                if (MinDistance > Distance)
                {
                    MinDistance = Distance;
                    Index = i;
                }
            }

            return Index;
        }

        /// <summary>
        /// 参照点と指定した点群中の最近傍点の距離を取得します。
        /// </summary>
        /// <param name="BaseDataPoint">指定した点群</param>
        /// <param name="RefPoint">参照点</param>
        /// <returns>最近傍点の距離</returns>
        public static double Get3DNearestPointDistance(XYZPointData BaseDataPoint, XYZPoint RefPoint)
        {
            int Index = Get3DNearestPointIndex(BaseDataPoint, RefPoint);

            return RefPoint.Distance(BaseDataPoint[Index]);
        }
        #endregion

        #region 基底三角形利用最近傍点探索
        /// <summary>
        /// 基底三角形を用いて(逆行列を用いて)指定した点郡の三角形領域内の最近傍点のインデックスを取得します。
        /// </summary>
        /// <param name="BaseDataPoint">指定した点郡</param>
        /// <param name="RefPoint">参照点</param>
        /// <param name="RefTriangleData">三角形</param>
        /// <returns>最近傍点のインデックス</returns>
        static public int Get2DNearestPointIndexFast(XYZPointData BaseDataPoint, XYZPoint RefPoint, TriangleData RefTriangleData,double Threshold)
        {
            if (Threshold < 0)
                throw new ApplicationException("閾値は正の値にしてください");

            MoveParam MoveParam = new MoveParam(RefTriangleData);
            DoublePoint MovedPoint = MoveParam.GetDataInOrder(RefPoint);
            XYZPointData MovedPointData = MoveParam.GetDataInOrder(BaseDataPoint);

            double MinDistance = double.MaxValue;
            int Index = 0;
            for (int i = 0; i < BaseDataPoint.Length; i++)
            {
                if ((MovedPointData[i].X < 0 )|( MovedPointData[i].Y < 0 )|( MovedPointData[i].X + MovedPointData[i].Y > 1)| (MovedPointData[i].Z < - Threshold))
                    continue;

                double Distance = MovedPointData[i].Distance(MovedPoint.X, MovedPoint.Y);
                if (MinDistance > Distance)
                {
                    MinDistance = Distance;
                    Index = i;
                }
            }
            //throw new NotImplementedException("まだ未実装です。");

            return Index;
        }

        /// <summary>
        /// 基底三角形を用いて(逆行列を用いて)指定した点郡の三角形領域内の最近傍点の距離を取得します。
        /// </summary>
        /// <param name="BaseDataPoint">指定した点郡</param>
        /// <param name="RefPoint">参照点</param>
        /// <param name="RefTriangleData">三角形</param>
        /// <param name="Threshold">近傍とみなす閾値</param>
        /// <returns>最近傍点の距離</returns>
        public static double Get2DNearestPointDistanceFast(XYZPointData BaseDataPoint, XYZPoint RefPoint, TriangleData RefTriangleData, double Threshold)
        {
            int Index = Get2DNearestPointIndexFast(BaseDataPoint, RefPoint, RefTriangleData, Threshold);

            return RefPoint.Distance(BaseDataPoint[Index].X, BaseDataPoint[Index].Y);
        }

        public static int Get2DNearestPointIndexFastOnBase(XYZPointData BaseDataPoint, XYZPoint RefPoint, TriangleData RefTriangleData, double Threshold)
        {
            if (Threshold < 0)
                throw new ApplicationException("閾値は正の値にしてください");

            MoveOnBaseTriangle MoveOnBaseParam = new MoveOnBaseTriangle(RefTriangleData);
            XYZPointData MovedPointData = MoveOnBaseParam.GetDataInOrderOnBase(BaseDataPoint);

            double MinDistance = double.MaxValue;
            int Index = 0;
            for (int i = 0; i < BaseDataPoint.Length; i++)
            {
                if ((MovedPointData[i].X < 0) | (MovedPointData[i].Y < 0) | (MovedPointData[i].X + MovedPointData[i].Y > 1) | (MovedPointData[i].Z < -Threshold))
                    continue;

                double Distance = MovedPointData[i].Distance(RefPoint.X, RefPoint.Y);
                if (MinDistance > Distance)
                {
                    MinDistance = Distance;
                    Index = i;
                }
            }
            return Index;

        }

        public static double Get2DNearestPointDistanceFastOnBase(XYZPointData BaseDataPoint, XYZPoint RefPoint, TriangleData RefTriangleData, double Threshold)
        {
            int Index = Get2DNearestPointIndexFastOnBase(BaseDataPoint, RefPoint, RefTriangleData, Threshold);
            return RefPoint.Distance(BaseDataPoint[Index].X, BaseDataPoint[Index].Y);
        }
        #endregion

        #region 高速化最近傍点探索
        /// <summary>
        /// 参照点と指定した点群中の最近傍点のインデックスを取得します。
        /// </summary>
        /// <param name="BaseDataPoint">指定した点群</param>
        /// <param name="RefPoint">参照点</param>
        /// <param name="Threshould">近傍とみなす閾値</param>
        /// <returns>最近傍点のインデックス</returns>
        static public int Get3DNearestPointIndexFast(XYZPointData BaseDataPoint, XYZPoint RefPoint, double Threshold)
        {
            double MinDistance = double.MaxValue;
            int Index = 0;

            for (int i = 0; i < BaseDataPoint.Length; i++)
            {
                if ((BaseDataPoint[i].X > RefPoint.X + Threshold) | (BaseDataPoint[i].X < RefPoint.X - Threshold) |
                    (BaseDataPoint[i].Y > RefPoint.Y + Threshold) | (BaseDataPoint[i].Y < RefPoint.Y - Threshold) |
                    (BaseDataPoint[i].Z > RefPoint.Z + Threshold) | (BaseDataPoint[i].Z < RefPoint.Z - Threshold))
                    continue;

                double Distance = BaseDataPoint[i].Distance(RefPoint);

                if (MinDistance > Distance)
                {
                    MinDistance = Distance;
                    Index = i;
                }
            }

            if (MinDistance == double.MaxValue)
                return Get3DNearestPointIndex(BaseDataPoint, RefPoint);

            return Index;
        }

        /// <summary>
        /// 参照点と指定した点群中の最近傍点の距離を取得します。
        /// </summary>
        /// <param name="BaseDataPoint">指定した点群</param>
        /// <param name="RefPoint">参照点</param>
        /// <param name="Threshould">近傍とみなす閾値</param>
        /// <returns>最近傍点の距離</returns>
        public static double Get3DNearestPointDistanceFast(XYZPointData BaseDataPoint, XYZPoint RefPoint, double Threshould)
        {
            int Index = Get3DNearestPointIndexFast(BaseDataPoint, RefPoint, Threshould);

            return RefPoint.Distance(BaseDataPoint[Index]);
        }
        #endregion
    }
}
