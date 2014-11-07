using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointFormat
{
    class cQuaternion
    {
        protected double dMatrix11, dMatrix12, dMatrix13, dMatrix14;
        protected double dMatrix21, dMatrix22, dMatrix23, dMatrix24;
        protected double dMatrix31, dMatrix32, dMatrix33, dMatrix34;
        protected double dMatrix41, dMatrix42, dMatrix43, dMatrix44;

        protected double dX, dY, dZ, dW;

        public cQuaternion()
        {
        }

        public cQuaternion(double dSent_X, double dSent_Y, double dSent_Z, double dSent_W)
        {
            dX = dSent_X;
            dY = dSent_Y;
            dZ = dSent_Z;
            dW = dSent_W;
        }

        public cQuaternion(double dPitch, double dYow, double dRoll)
        {
            double dCos_Phi = Math.Cos(dRoll);
            double dSin_Phi = Math.Sin(dRoll);

            double dCos_Th = Math.Cos(dPitch);
            double dSin_Th = Math.Sin(dPitch);

            double dCos_Psi = Math.Cos(dYow);
            double dSin_Psi = Math.Sin(dYow);

            dMatrix11 = dCos_Phi * dCos_Th;
            dMatrix12 = dCos_Phi * dSin_Th * dSin_Psi - dSin_Phi * dCos_Psi;
            dMatrix13 = dCos_Phi * dSin_Th * dCos_Psi + dSin_Phi * dSin_Psi;
            dMatrix14 = 0;

            dMatrix21 = dSin_Phi * dCos_Th;
            dMatrix22 = dSin_Phi * dSin_Th * dSin_Psi + dCos_Phi * dCos_Psi;
            dMatrix23 = dSin_Phi * dSin_Th * dCos_Psi - dCos_Phi * dSin_Psi;
            dMatrix24 = 0;

            dMatrix31 = -dSin_Th;
            dMatrix32 = dCos_Th * dSin_Psi;
            dMatrix33 = dCos_Th * dCos_Psi;
            dMatrix34 = 0;

            dMatrix41 = 0;
            dMatrix42 = 0;
            dMatrix43 = 0;
            dMatrix44 = 1;
        }

        public XYZPoint cpRotate(XYZPoint icSentPoint)
        {
            double dTemp_X = dMatrix14 + dMatrix11 * icSentPoint.X + dMatrix12 * icSentPoint.Y + dMatrix13 * icSentPoint.Z;
            double dTemp_Y = dMatrix24 + dMatrix21 * icSentPoint.X + dMatrix22 * icSentPoint.Y + dMatrix23 * icSentPoint.Z;
            double dTemp_Z = dMatrix34 + dMatrix31 * icSentPoint.X + dMatrix32 * icSentPoint.Y + dMatrix33 * icSentPoint.Z;

            return new XYZPoint(dTemp_X, dTemp_Y, dTemp_Z);
        }
    }
}
