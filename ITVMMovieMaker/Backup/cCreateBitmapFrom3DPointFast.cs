using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using PointFormat;

namespace cBitmap
{
    public class cCreateBitmapFrom3DPointFast:cCreateBitmapFrom3DPoint
    {
        /// <summary>
        /// 色を保持する配列
        /// </summary>
                protected byte[,][] ColorByteArray;

        protected int PixelSize = 4;

        public cCreateBitmapFrom3DPointFast(cPointData PointData, int PixelSize)
            :base(PointData)
        {
            base.DrawMethod = this.DrawMethodFast;
            this.PixelSize = PixelSize;
        }

        public cCreateBitmapFrom3DPointFast(cPoint[] PointData, int PixelSize)
            :base(PointData)
        {
            base.DrawMethod = this.DrawMethodFast;
            this.PixelSize = PixelSize;
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
                int x = (int)((icPoint[i].X - dXMin) * dRatio);
                int y = (int)((dYMax - icPoint[i].Y) * dRatio);

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

        protected virtual Bitmap DrawMethodFast(Color[,] ColorArray)
        {
            #region ドローメソッド
#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif
            Bitmap ReturnBitmap = new Bitmap(iXMax, iYMax);
            unsafe
            {
                Rectangle Rect = new Rectangle(0, 0, iXMax, iYMax);
                BitmapData BitmapData = ReturnBitmap.LockBits(Rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int stride = BitmapData.Stride;
                
                byte* Point = (byte*)(void*)BitmapData.Scan0;
                int Residual = stride - iXMax * 3;
                int Offset = (this.PixelSize + 1 )/2;
                for(int i = Offset; i < iYMax -Offset ;i++)
                    for (int j = Offset; j < iXMax - Offset; j++)
                    {
                        if (ColorByteArray[j, i][0] == 0 & ColorByteArray[j, i][1] == 0 & ColorByteArray[j, i][2] == 0)
                            continue;

                        for (int SmallY = -(Offset); SmallY < (Offset); SmallY++)
                            for (int SmallX = -(Offset); SmallX < (Offset); SmallX++)
                            {
                                int Adress = (i + SmallY) * stride + (j + SmallX) * 3;
                                Point[Adress + 0] = ColorByteArray[j, i][0];
                                Point[Adress + 1] = ColorByteArray[j, i][1];
                                Point[Adress + 2] = ColorByteArray[j, i][2];
                            }

                    }
                ReturnBitmap.UnlockBits(BitmapData);
            }
#if DEBUG
            StopWatch.Stop();
            Console.WriteLine("画像の実際の描画に" + StopWatch.ElapsedMilliseconds + "ミリ秒かかりました");
#endif
            return ReturnBitmap;
            #endregion
        }
    }
}
