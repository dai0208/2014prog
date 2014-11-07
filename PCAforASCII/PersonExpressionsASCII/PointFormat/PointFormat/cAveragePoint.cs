using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    public static class cAveragePoint
    {
        public static cPoint GetAveragePoint(cPoint FirstPoint, cPoint SecondPoint)
        {
            cPoint ReturnPoint = new cPoint();
            ReturnPoint.X = (FirstPoint.X + SecondPoint.X) / 2;
            ReturnPoint.Y = (FirstPoint.Y + SecondPoint.Y) / 2;
            ReturnPoint.Z = (FirstPoint.Z + SecondPoint.Z) / 2;

            ReturnPoint.R = (FirstPoint.R + SecondPoint.R) / 2;
            ReturnPoint.G = (FirstPoint.G + SecondPoint.G) / 2;
            ReturnPoint.B = (FirstPoint.B + SecondPoint.B) / 2;

            return ReturnPoint;
        }
        public static XYZPoint GetAveragePoint(XYZPoint FirstPoint, XYZPoint SecondPoint)
        {
            XYZPoint ReturnPoint = new XYZPoint();
            ReturnPoint.X = (FirstPoint.X + SecondPoint.X) / 2;
            ReturnPoint.Y = (FirstPoint.Y + SecondPoint.Y) / 2;
            ReturnPoint.Z = (FirstPoint.Z + SecondPoint.Z) / 2;

            return ReturnPoint;
        }
        public static System.Drawing.Point GetAveragePoint(System.Drawing.Point FirstPoint, System.Drawing.Point SecondPoint)
        {
            System.Drawing.Point ReturnPoint = new System.Drawing.Point();
            ReturnPoint.X = (FirstPoint.X + SecondPoint.X) / 2;
            ReturnPoint.Y = (FirstPoint.Y + SecondPoint.Y) / 2;
            return ReturnPoint;
        }
    }
}
