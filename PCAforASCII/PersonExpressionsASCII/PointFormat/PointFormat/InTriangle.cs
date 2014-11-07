using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    /// <summary>
    /// cPointDataと三角形からその三角形領域内の点を抽出します。
    /// </summary>
    public class InTriangle
    {
        public static int[] GetIndexInTriangle(cPointData PointData, TriangleData TriangleData, double Threshould)
        {
            if (Threshould <= 0)
                throw new ApplicationException("閾値は正の値にして下さい。");

            MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(TriangleData);
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

        public static int[] GetIndexInTriangle(XYZPointData PointData, TriangleData TriangleData, double Threshould)
        {
            if (Threshould <= 0)
                throw new ApplicationException("閾値は正の値にして下さい。");

            MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(TriangleData);
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

        public static cPointData GetPointDataInTriangle(cPointData PointData, TriangleData TriangleData, double Threshould)
        {
            int[] Index = GetIndexInTriangle(PointData, TriangleData, Threshould);
            return PointData.GetIndexData(Index);
        }

        public static XYZPointData GetPointDataInTriangle(XYZPointData PointData, TriangleData TriangleData, double Threshould)
        {
            int[] Index = GetIndexInTriangle(PointData, TriangleData, Threshould);
            return PointData.GetIndexData(Index);
        }
    }
}
