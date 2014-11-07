using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;
using MatrixVector;

namespace ErrorIndex
{
    /// <summary>
    /// 再現指数(RecallIndex)・復元指数(RestorationIndex)を計算します。
    /// </summary>
    public static class cCalcIndex
    {
        /// <summary>
        /// 再サンプリング前後のデータから再現指数を計算します。
        /// このメソッドを実行すると再サンプリング前のデータのtagに最近傍点までの距離が入ります。
        /// </summary>
        /// <param name="BeforeAreaPoint">再サンプリング前のデータ</param>
        /// <param name="AfterAreaPoint">再サンプリング後のデータ</param>
        /// <returns>再現指数</returns>
        public static Vector GetRecallIndex(cPointData BeforeAreaPoint, cPointData AfterAreaPoint)
        {
            Vector RecallIndexVector = new Vector(BeforeAreaPoint.Length);

            for (int i = 0; i < BeforeAreaPoint.Length; i++)
            {
                double MinimumDistance = SearchNearestPoint.Get3DNearestPointDistanceFast(new XYZPointData(AfterAreaPoint), BeforeAreaPoint[i], 3d);
                BeforeAreaPoint[i].Tag = MinimumDistance;
                RecallIndexVector[i] = MinimumDistance;
            }
            
            return RecallIndexVector;
        }

        /// <summary>
        ///再構築前後のデータから復元指数を計算します。
        /// </summary>
        /// <param name="Original">再構築前のデータ</param>
        /// <param name="Object">再構築後のデータ</param>
        /// <returns>復元指数</returns>
        public static double GetRestorationIndex(cPointData Original, cPointData Object)
        {
            double dRestorationIndex = 0;

            //距離の和を計算
            for (int i = 0; i < Original.Length; i++)
            {
                double dDistance = Original[i].Distance(Object[i].X, Object[i].Y, Object[i].Z);
                dRestorationIndex += dDistance;
                Original[i].Tag = dDistance;
                Object[i].Tag = dDistance;
            }

            //距離の平均値を復元指数とする
            dRestorationIndex /= Original.Length;

            return dRestorationIndex;            
        }
    }
}
