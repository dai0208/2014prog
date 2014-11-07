using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using PointFormat;

namespace cBitmap
{
    public class cCreateBitmapFrom3DPointParspective:cCreateBitmapFrom3DPointFast
    {
        protected double FocalLength = 250;
        protected double Distance =250;

        public cCreateBitmapFrom3DPointParspective(cPointData PointData, int PixelSize)
            :this(PointData.Items,PixelSize)
        {
        }

        public cCreateBitmapFrom3DPointParspective(cPoint[] PointData, int PixelSize)
            :base(PointData, PixelSize)
        {
        }

        public cCreateBitmapFrom3DPointParspective(cPointData PointData, int PixelSize, double FocalLength, double Distance)
            : this(PointData.Items,PixelSize, FocalLength, Distance)
        {
        }

        public cCreateBitmapFrom3DPointParspective(cPoint[] PointData, int PixelSize, double FocalLength, double Distance)
            : base(PointData,PixelSize)
        {
            this.FocalLength = FocalLength;
            this.Distance = Distance;
        }

        /// <summary>
        /// 与えられたポイントデータから二次元Bitmap画像を作成します。
        /// </summary>
        /// <param name="icaPoint">Bitmap画像を作成したい顔画像ポイントです。</param>
        protected override void vInitialize(cPoint[] icaPoint)
        {

            #region 与えられたポイントデータからBitmapを作成します。
#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif
            for (int i = 0; i < icaPoint.Length; i++)
            {
                //レンズのパラメータを使用する
                double LensParam = (FocalLength / (Distance - icaPoint[i].Z));

                //画像の最大X座標と最小X座標、最大Y座標と最小Y座標を求める
                dXMin = Math.Min(icaPoint[i].X * LensParam, dXMin);
                dXMax = Math.Max(icaPoint[i].X * LensParam, dXMax);
                dYMin = Math.Min(icaPoint[i].Y * LensParam, dYMin);
                dYMax = Math.Max(icaPoint[i].Y * LensParam, dYMax);
            }

            //求めた大きさからビットマップの大きさを決める。
            iXMax = (int)((dXMax - dXMin + 1) * dRatio);
            iYMax = (int)((dYMax - dYMin + 1) * dRatio);

            //奥行き情報やらその他を初期化
            daZPoint = new double[iXMax, iYMax];
            iaWhereComeFrom = new int[iXMax, iYMax];

            //画像の中央
            dMeanX = (dXMax + dXMin) / 2;
            dMeanY = (dYMax + dYMin) / 2;

#if DEBUG
            StopWatch.Stop();
            Console.WriteLine("画像の初期化に" + StopWatch.ElapsedMilliseconds + "ミリ秒かかりました");
#endif
            #endregion
        }

        protected override Bitmap bmpCreate()
        {
            #region Bipmapを作成する。
#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif
            //clraImage = new Color[iXMax, iYMax];
            ColorByteArray = new byte[iXMax, iYMax][];


            //描画用ビットマップ作成
            for (int i = 0; i < iYMax; i++)
                for (int j = 0; j < iXMax; j++)
                    iaWhereComeFrom[j, i] = -1;


            for (int x = 0; x < iXMax; x++)
                for (int y = 0; y < iYMax; y++)
                    ColorByteArray[x, y] = new byte[3];

#if DEBUG
            StopWatch.Stop();
            Console.WriteLine("色作成に" + StopWatch.ElapsedMilliseconds + "ミリ秒かかりました");
#endif

#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif
            #region BMP画像にデータを打っていく。
            cPoint[] icPoint = icpdPoint.Items;
            for (int i = 0; i < icPoint.Length; i++)
            {
                double LensParam = (FocalLength / ( Distance-icPoint[i].Z));

                int x = (int)((icPoint[i].X - dXMin) * dRatio);
                int y = (int)((dYMax - icPoint[i].Y) * dRatio);

                //ここでパースの調整
                x = (int)(LensParam * (x - iXMax / 2)) + iXMax / 2;
                y = (int)(LensParam * (y - iYMax / 2)) + iYMax / 2;

                if ((x >= 0 & x < iXMax) & (y >= 0 & y < iYMax))
                    if (iaWhereComeFrom[x, y] == -1 || daZPoint[x, y] < icPoint[i].Z)
                    {
                        //その点がどこにを記録しておく
                        icPoint[i].Tag = new Point(x, y);
                        ColorByteArray[x, y][0] = (byte)icPoint[i].B;
                        ColorByteArray[x, y][1] = (byte)icPoint[i].G;
                        ColorByteArray[x, y][2] = (byte)icPoint[i].R;
                        daZPoint[x, y] = icPoint[i].Z;
                        iaWhereComeFrom[x, y] = i;
                    }
            }
            #endregion
#if DEBUG
            StopWatch.Stop();
            Console.WriteLine("画像のデータ取得に" + StopWatch.ElapsedMilliseconds + "ミリ秒かかりました");
#endif

            #endregion
            return DrawMethod(clraImage);
        }

    }
}
