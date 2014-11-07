using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PointFormat;

namespace cBitmap {
    /// <summary>
    /// Bitmapを描画するメソッドのデリゲート
    /// </summary>
    /// <param name="ColorArray">描画する色の配列</param>
    /// <returns>作成されたBitmap</returns>
    public delegate Bitmap DrawMethod(Color[,] ColorArray);

    /// <summary>
    /// 描画をする方法の列挙型
    /// </summary>
    public enum DrawType
    {
        /// <summary>
        /// BitmapクラスのSetPixelを使用します。
        /// </summary>
        SetPixel,

        /// <summary>
        /// GraphicのEllipsを使用します。
        /// </summary>
        GraphicEllipse,

        /// <summary>
        /// その他のメソッドを利用します。必ずDrawMethodを実装して下さい。
        /// </summary>
        other = 99,
    }

    /// <summary>
    /// 3DPointDataからBitmap画像を作成するクラスです。
    /// </summary>
    public class cCreateBitmapFrom3DPoint {
        protected cPointData icpdPoint;
        protected double dXMin = double.MaxValue;
        protected double dXMax = double.MinValue;
        protected double dYMin = double.MaxValue;
        protected double dYMax = double.MinValue;
        protected double dMeanX, dMeanY;
        protected int iXMax, iYMax;
        protected const double dRatio = 2.5;
        protected int[,] iaWhereComeFrom;
        protected Color[,] clraImage;
        protected double[,] daZPoint;
        protected DrawMethod DrawMethod;
        protected DrawType DrawType;

        #if DEBUG
                protected System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();
        #endif

        #region コンストラクタ
        public cCreateBitmapFrom3DPoint(cPointData PointData)
            : this(PointData, DrawType.SetPixel)
        {
            
        }

        public cCreateBitmapFrom3DPoint(cPointData PointData, DrawType DrawType) {
            icpdPoint = new cPointData(PointData);
            switch (DrawType)
            {
                case DrawType.SetPixel:
                    DrawMethod = this.DrawWithSetPixel;
                    break;
                case DrawType.GraphicEllipse:
                    DrawMethod = this.DrawWithEllipse;
                    break;
            }
            this.vInitialize(PointData.Items);
        }

        public cCreateBitmapFrom3DPoint(cPoint[] PointData)
            :this(PointData, DrawType.SetPixel)
        { 
        }

        public cCreateBitmapFrom3DPoint(cPoint[] PointData, DrawType DrawType)  
        {
            switch (DrawType)
            {
                case DrawType.SetPixel:
                    DrawMethod = this.DrawWithSetPixel;
                    break;
                case DrawType.GraphicEllipse:
                    DrawMethod = this.DrawWithEllipse;
                    break;
            }
            icpdPoint = new cPointData(PointData);
            this.vInitialize(PointData);
        }
    
        #endregion

        /// <summary>
        /// 与えられたポイントデータから二次元Bitmap画像を作成します。
        /// </summary>
        /// <param name="icaPoint">Bitmap画像を作成したい顔画像ポイントです。</param>
        protected virtual void vInitialize(cPoint[] icaPoint)
        {

            #region 与えられたポイントデータからBitmapを作成します。
#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif
            for (int i = 0; i < icaPoint.Length; i++)
            {
                //画像の最大X座標と最小X座標、最大Y座標と最小Y座標を求める
                dXMin = Math.Min(icaPoint[i].X, dXMin);
                dXMax = Math.Max(icaPoint[i].X, dXMax);
                dYMin = Math.Min(icaPoint[i].Y, dYMin);
                dYMax = Math.Max(icaPoint[i].Y, dYMax);
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

        /// <summary>
        /// ポイントデータから実際にビットマップ画像を作成するメソッドです。
        /// </summary>
        protected virtual Bitmap bmpCreate()
        {
            #region Bipmapを作成する。
#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif

            //色を保持する配列
            clraImage = new Color[iXMax, iYMax];


            //描画用ビットマップ作成
            for (int i = 0; i < iYMax; i++)
                for (int j = 0; j < iXMax; j++)
                    iaWhereComeFrom[j, i] = -1;

            for (int x = 0; x < iXMax; x++)
                for (int y = 0; y < iYMax; y++)
                    clraImage[x, y] = Color.FromArgb(0, 0, 0);
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
                    clraImage[x, y] = Color.FromArgb(icPoint[i].R, icPoint[i].G, icPoint[i].B);
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

        #region デリゲート用メソッド
        protected virtual Bitmap DrawWithSetPixel(Color[,] ColorArray)
        {
#if DEBUG
            StopWatch.Reset();
            StopWatch.Start();
#endif
            Bitmap ReturnBitmap = new Bitmap(iXMax, iYMax);
            Graphics.FromImage(ReturnBitmap).FillRectangle(Brushes.Black, 0, 0, ReturnBitmap.Width, ReturnBitmap.Height);

            for (int x = 0; x < iXMax; x++)
                for (int y = 0; y < iYMax; y++)
                {
                    if (ColorArray[x, y] == Color.FromArgb(0, 0, 0))
                        continue;
                    ReturnBitmap.SetPixel(x, y, ColorArray[x, y]);
                }
#if DEBUG
            StopWatch.Stop();
            Console.WriteLine("画像の実際の描画に" + StopWatch.ElapsedMilliseconds + "ミリ秒かかりました");
#endif
            return ReturnBitmap;
        }

        protected virtual Bitmap DrawWithEllipse(Color[,] ColorArray)
        {
            Bitmap ReturnBitmap = new Bitmap(iXMax, iYMax);
            Graphics g = Graphics.FromImage(ReturnBitmap);
            g.FillRectangle(Brushes.Black, 0, 0, ReturnBitmap.Width, ReturnBitmap.Height);
            float fPenWidth = 3;

            for (int x = 0; x < iXMax; x++)
                for (int y = 0; y < iYMax; y++)
                {
                    if (ColorArray[x, y] == Color.FromArgb(0, 0, 0))
                        continue;
                    Pen Pen = new Pen(ColorArray[x, y], fPenWidth);
                    g.DrawEllipse(Pen, x - fPenWidth / 2, y - fPenWidth, fPenWidth, fPenWidth);
                }
            return ReturnBitmap;
        }
        #endregion

        /// <summary>
        /// 指定された2次元画像位置の点データを返します。
        /// </summary>
        /// <param name="iX">指定したX座標</param>
        /// <param name="iY">指定したY座標</param>
        public cPoint icpGetPointFrom(int iX, int iY) {
            return icpGetPointFrom(new Point(iX, iY));
        }

        /// <summary>
        /// 指定された2次元画像位置の点データを返します。
        /// </summary>
        /// <param name="Point">指定点</param>
        public cPoint icpGetPointFrom(Point Point) {
            #region 指定された2次元画像位置の点データを返します。
            int iYDataOffset, iXDataOffset;
            int iSetX = Point.X;
            int iSetY = Point.Y;

            //指定された場所にデータが無い場合は上下左右10Pixel以内の点を探す
            for (int iPoint = 0; iPoint < 10; iPoint++)
                for (iYDataOffset = -iPoint; iYDataOffset <= iPoint; iYDataOffset++)
                    for (iXDataOffset = -iPoint; iXDataOffset <= iPoint; iXDataOffset++) {
                        //範囲外ならやり直し
                        if ((iSetX + iXDataOffset < 0 || iSetY + iYDataOffset < 0) || (iSetX + iXDataOffset >= iXMax || iSetY + iYDataOffset >= iYMax))
                            continue;

                        //データがあったらその場所を戻す
                        if (!(iaWhereComeFrom[iSetX + iXDataOffset, iSetY + iYDataOffset] == -1))
                            return icpdPoint.Items[iaWhereComeFrom[iSetX + iXDataOffset, iSetY + iYDataOffset]];
                    }
            return null;
            #endregion
        }

        /// <summary>
        /// 指定した3次元PointはBitmapのどこの点かを返します。
        /// </summary>
        /// <param name="icpSentPoint">指定する3次元Point</param>
        /// <returns>対応する2次元Point</returns>
        public Point pntWhereIsThisPoint(cPoint icpSentPoint) {
            #region 指定した3次元PointはBitmapのどこの点かを返します。
            if (icpSentPoint.X == 0 & icpSentPoint.Y == 0 & icpSentPoint.Z == 0)
                return new Point(0, 0);

            int x = (int)((icpSentPoint.X - dXMin) * dRatio);
            int y = (int)((dYMax - icpSentPoint.Y) * dRatio);

            return new Point(x, y);
            #endregion
        }

        #region プロパティ
        /// <summary>
        /// 与えられた点群から作成したビットマップ画像を取得します
        /// </summary>
        public Bitmap Bitmap {
            get {
#if DEBUG
                System.Diagnostics.Stopwatch TotalStopWatch = new System.Diagnostics.Stopwatch();
                TotalStopWatch.Start();
#endif
                Bitmap ReturnBitmap = bmpCreate();
#if DEBUG
                TotalStopWatch.Stop();
                Console.WriteLine("画像の作成全てにに" + TotalStopWatch.ElapsedMilliseconds + "ミリ秒かかりました");
#endif
                return ReturnBitmap;

            }
        }

        /// <summary>
        /// 指定した座標の点は何番目の点かを取得するための配列を取得します。
        /// </summary>
        public int[,] WhereComeFrom {
            get { return iaWhereComeFrom; }
        }
        
        /// <summary>
        /// 指定した座標の点は3次元点群データの何番目の点かを返す
        /// </summary>
        /// <param name="X">X座標</param>
        /// <param name="Y">Y座標</param>
        /// <returns>3次元点群データの何番目の点か</returns>
        public int GetWhereComeFrom(int X, int Y)
        {
            if (X < 0 || X > this.iXMax || Y < 0 || Y > this.iYMax)
                return -1;
            else
            {
                return iaWhereComeFrom[X, Y];
            }
        }

        #endregion
    }
}