using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using PointFormat;

namespace PCAManagerFromAsciiData
{
    public static class CreateVectorFromPointFormat
    {
        public static Vector GetVectorFromPoint(cPoint Point)
        {
            double[] data = new double[6];
            data[0] = Point.X;
            data[1] = Point.Y;
            data[2] = Point.Z;
            data[3] = Point.R;
            data[4] = Point.G;
            data[5] = Point.B;

            return new Vector(data);
        }

        public static Vector GetVectorFromPoint(XYZPoint Point)
        {
            double[] data = new double[3];
            data[0] = Point.X;
            data[1] = Point.Y;
            data[2] = Point.Z;
            return new Vector(data);
        }

        public static Vector GetVectorFromPoint(cPointData PointData)
        {
            //なるべく高速化するためにちょっとエレガントさに欠ける実装になりました。
            Vector ResultData = new Vector();
            double[] data = new double[6 * PointData.Length];
            for (int i = 0; i < PointData.Length; i++)
            {
                data[i * 6 + 0] = PointData[i].X;
                data[i * 6 + 1] = PointData[i].Y;
                data[i * 6 + 2] = PointData[i].Z;
                data[i * 6 + 3] = PointData[i].R;
                data[i * 6 + 4] = PointData[i].G;
                data[i * 6 + 5] = PointData[i].B;
            }
            ResultData.VectorElement = data;
            return ResultData;
        }

        public static Vector GetVectorFromPoint(XYZPointData PointData)
        {
            //なるべく高速化するためにちょっとエレガントさに欠ける実装になりました。
            Vector ResultData = new Vector();
            double[] data = new double[3 * PointData.Length];
            for (int i = 0; i < PointData.Length; i++)
            {
                data[i * 3 + 0] = PointData[i].X;
                data[i * 3 + 1] = PointData[i].Y;
                data[i * 3 + 2] = PointData[i].Z;
            }
            ResultData.VectorElement = data;
            return ResultData;
        }
    }
}
