using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace CvUtil
{
    public class CvUtility
    {
        public static MatrixType GetMatrixType(BitDepth BitDepth, int Channels)
        {

            switch (BitDepth)
            {
                case OpenCvSharp.BitDepth.U8:
                    switch (Channels)
                    {
                        case 1:
                            return MatrixType.U8C1;
                        case 3:
                            return MatrixType.U8C3;
                    }
                    break;
                case OpenCvSharp.BitDepth.F32:
                    switch (Channels)
                    {
                        case 1:
                            return MatrixType.F32C1;
                        case 3:
                            return MatrixType.F32C3;
                    }
                    break;
            }
            throw new ApplicationException();

        }

        public static MatrixType GetMatrixType(int BitDepth, int Channels)
        {
            return GetMatrixType((OpenCvSharp.BitDepth)BitDepth, Channels);
        }

        public static double GetAverageScalar(IplImage Image)
        {
            double Average;
            IplImage GrayImage;
            GetGray(Image, out GrayImage);
            using (IplImage RowAverageImage = new IplImage(Image.Width, 1, GrayImage.Depth, 1))
            using (IplImage AverageMat = new IplImage(1, 1, Image.Depth, 1))
            {
                GrayImage.Reduce(RowAverageImage, ReduceDimension.Row, ReduceOperation.Avg);
                RowAverageImage.Reduce(AverageMat, ReduceDimension.Column, ReduceOperation.Avg);
                Average = AverageMat[0, 0];
            }
            GrayImage.Dispose();
            return Average;
        }

        public static double GetAverageScalar(CvMat ImageMat)
        {
            return GetAverageScalar(Cv.GetImage(ImageMat));
        }

        public static double GetSumScalar(IplImage Image)
        {
            double Sum;
            IplImage GrayImage;
            GetGray(Image, out GrayImage);
            using (IplImage RowAverageImage = new IplImage(Image.Width, 1, GrayImage.Depth, 1))
            using (IplImage AverageMat = new IplImage(1, 1, Image.Depth, 1))
            {
                GrayImage.Reduce(RowAverageImage, ReduceDimension.Row, ReduceOperation.Sum);
                RowAverageImage.Reduce(AverageMat, ReduceDimension.Column, ReduceOperation.Sum);
                Sum = AverageMat[0, 0];
            }
            GrayImage.Dispose();
            return Sum;
        }

        public static double GetSumScalar(CvMat MatImage)
        {
            return GetSumScalar(Cv.GetImage(MatImage));
        }

        public static void GetAverage(string[] FileNames, out CvMat dstImageMat)
        {
            int Count = 0;
            if (FileNames.Length == 0)
            {
                dstImageMat = null;
                return;
            }

            //ResultMatの大きさを決める
            using (CvMat WorkMat = new CvMat(FileNames[0]))
            {
                //フォルダの最初の画像の大きさと仮定する。
                //もしも違う大きさの画像が入っていた場合、今後の処理でエラーとなる可能性がある（自己責任）
                dstImageMat = new CvMat(WorkMat.Rows, WorkMat.Cols, MatrixType.F32C3, 0);
            }

            //フォルダの中全ての画像ファイルの値を合計する
            foreach (string FileName in FileNames)
            {
                //拡張子チェック（画像ファイル以外は無視したい。サムネイルが勝手に作成されてたりするので(´；ω；`)）
                if (!CheckExt(FileName))
                    continue;

                using (CvMat WorkMat = new CvMat(FileName))
                {
                    //画像のサイズが違ったらエラーは出さずに一応続ける・・・
                    if (WorkMat.Width != dstImageMat.Width || WorkMat.Height != dstImageMat.Height)
                        continue;

                    //↓この書き方だとメモリリークが起こるみたい(´・ω・`)
                    //ResultMat = ResultMat+ WorkMat;

                    //こっちの記法で(´・ω・`)(´・ω・`)
                    dstImageMat.Add(WorkMat, dstImageMat);
                }

                //ファイルの個数チェック
                //di.GetFiles().Lengthだと読み込んでないファイルの数も含まれる(・ω・;)
                Count++;
            }

            //合計値 ÷ 個数 ＝ 平均(`・ω・')
            dstImageMat /= Count;

            return;
        }


        public static void GetHist(IplImage Image, out IplImage dstImage)
        {
            if (Image == null)
            {
                dstImage = null;
                return;
            }

            IplImage GrayImage;
            GetGray(Image, out GrayImage);

            dstImage = new IplImage(1, GrayImage.Height, GrayImage.Depth, 1);
            GrayImage.Reduce(dstImage, ReduceDimension.Row, ReduceOperation.Avg);
            GrayImage.Dispose();

            return;
        }


        public static void GetHist(CvMat Image, out CvMat dstImageMat)
        {
            if (Image == null)
            {
                dstImageMat = null;
                return;
            }

            CvMat GrayMat;
            GetGray(Image, out GrayMat);

            dstImageMat = new CvMat(GrayMat.Rows, GrayMat.Cols, GetMatrixType(Image.ElemDepth, 1));
            GrayMat.Reduce(dstImageMat, ReduceDimension.Row, ReduceOperation.Avg);
            GrayMat.Dispose();

            return;
        }


        public static void GetGray(CvMat srcImageMat, out CvMat dstImageMat)
        {
            if (srcImageMat.ElemChannels == 1)
                dstImageMat = srcImageMat.Clone();
            else
            {
                dstImageMat = new CvMat(srcImageMat.Rows, srcImageMat.Cols, GetMatrixType(srcImageMat.ElemDepth, 1));
                Cv.CvtColor(srcImageMat, dstImageMat, ColorConversion.BgrToGray);
            }

            return;
            //return Cv.GetMat(GetGray(Cv.GetImage(ImageMat)));
        }

        public static void GetGray(IplImage srcImage, out IplImage dstImage)
        {
            if (srcImage.ElemChannels == 1)
                dstImage = Cv.CloneImage(srcImage);
            else
            {
                dstImage = new IplImage(srcImage.Width, srcImage.Height, srcImage.Depth, 1);
                Cv.CvtColor(srcImage, dstImage, ColorConversion.BgrToGray);
            }
            return;
        }

        public static void GetU8GrayImage(IplImage Image, out IplImage dstImage)
        {
            if (Image.ElemDepth == (int)BitDepth.U8 && Image.ElemChannels == 1)
            {
                dstImage = Image.Clone();
                return;
            }

            {
                IplImage GrayImage;
                if (Image.ElemChannels != 1 && Image.ElemDepth == (int)BitDepth.U8)
                {
                    GetGray(Image, out dstImage);
                    return;
                }
                else
                    GrayImage = Cv.CloneImage(Image);

                dstImage = new IplImage(GrayImage.Width, GrayImage.Height, BitDepth.U8, 1);
                Cv.CvtColor(GrayImage, dstImage, ColorConversion.BgrToGray);
                GrayImage.Dispose();
            }
            return;
        }

        public static void GetU8GrayImage(CvMat ImageMat, out CvMat dstImageMat)
        {
            CvMat WorkMat = ImageMat.Clone();
            
            if (WorkMat.ElemDepth == (int)BitDepth.F32)
            {
                OpenCvSharp.CPlusPlus.Mat Mat = new OpenCvSharp.CPlusPlus.Mat(WorkMat);
                OpenCvSharp.CPlusPlus.Mat Result = new OpenCvSharp.CPlusPlus.Mat(WorkMat.Rows, WorkMat.Cols, MatrixType.U8C1);
                Mat.ConvertTo(Result, MatrixType.U8C1);
                WorkMat = Result.ToCvMat();
                Mat.Dispose();
                Result.Dispose();
            }
            
            if (ImageMat.ElemType == MatrixType.U8C1)
            {
                dstImageMat = ImageMat.Clone();
                return;
            }
                        
            dstImageMat = new CvMat(ImageMat.Rows, ImageMat.Cols, MatrixType.U8C1);

                Cv.CvtColor(WorkMat, dstImageMat, ColorConversion.BgrToGray);

            WorkMat.Dispose();

            return;
        }

        public static void GetImageBlock(IplImage Image, int XBlockSize, int YBlockSize, out IplImage dstImage)
        {
            IplImage GrayImage = Cv.CloneImage(Image);

            dstImage = new IplImage(
                (int)Math.Ceiling(GrayImage.Width / (double)XBlockSize),
                (int)Math.Ceiling(GrayImage.Height / (double)YBlockSize),
                Image.Depth,
                Image.ElemChannels);

            for (int x = 0; x < dstImage.Width; x += XBlockSize)
                for (int y = 0; y < dstImage.Height; y += YBlockSize)
                {
                    int Count = 0;
                    double Sum = 0;

                    for (int NowX = 0; NowX < XBlockSize; NowX++)
                        for (int NowY = 0; NowY < YBlockSize; NowY++)
                        {
                            if ((NowX + x) >= GrayImage.Width || (NowY + y) >= GrayImage.Height)
                                break;

                            Count++;
                            Sum += GrayImage[(y + NowY) * GrayImage.Width + x + NowX];
                        }

                    dstImage[y * dstImage.Width + x] = Sum / Count;
                }

            GrayImage.Dispose();

            return;
        }

        public static void GetImageBlock(CvMat ImageMat, int XBlockSize, int YBlockSize, out CvMat dstImageMat)
        {
            CvMat GrayMat = ImageMat.Clone();

            dstImageMat = new CvMat(
                (int)Math.Ceiling(GrayMat.Rows / (double)YBlockSize),
                (int)Math.Ceiling(GrayMat.Cols / (double)XBlockSize),
                GetMatrixType(ImageMat.ElemDepth, ImageMat.ElemChannels));

            for (int x = 0; x < dstImageMat.Cols / XBlockSize; x++)
                for (int y = 0; y < dstImageMat.Rows / YBlockSize; y ++)
                {
                    int Count = 0;
                    double Sum = 0;

                    for (int NowX = 0; NowX < XBlockSize; NowX++)
                        for (int NowY = 0; NowY < YBlockSize; NowY++)
                        {
                            if (x * XBlockSize + NowX >= GrayMat.Cols || y * YBlockSize + NowY >= GrayMat.Rows)
                                break;

                            Count++;
                            Sum += GrayMat[y * YBlockSize + NowY, x * XBlockSize + NowX];
                        }
                    dstImageMat[y , x] = Sum / Count;
                }
            GrayMat.Dispose();

            return;// Cv.GetMat(GetImageBlock(Cv.GetImage(ImageMat), XBlockSize, YBlockSize));
        }

        public static void GetCircles(CvMat MatImage, out CvSeq<CvCircleSegment> destSeq, int MinDist = 100, int Param1 = 150, int Param2 = 55, int MinRadius = 5, int MaxRadius = 0)
        {
            if (MaxRadius == 0)
                MaxRadius = Math.Min(MatImage.Width, MatImage.Height);

            CvMat GrayMat;
            if (MatImage.ElemChannels == 1 && MatImage.ElemDepth == (int)BitDepth.U8)
                GrayMat = MatImage.Clone();
            else
                GetU8GrayImage(MatImage, out GrayMat);

            using (CvMemStorage strage = new CvMemStorage())
            {
                destSeq = GrayMat.HoughCircles(strage, HoughCirclesMethod.Gradient, 1, MinDist, Param1, Param2, MinRadius, MaxRadius);
            }

            GrayMat.Dispose();

            return;
        }

        public static void GetBinaryImage(IplImage Image, double Threshold, out IplImage dstImage)
        {
            if (Image == null)
            {
                dstImage = null;
                return;
            }

            dstImage = new IplImage(Image.Width, Image.Height, BitDepth.U8, 1);

            IplImage WorkImage = Image.Clone();
            //グレイスケール処理
            double ThresholdRatio = GetAverageScalar(WorkImage) * Threshold;
            //Cv.Threshold(dstImage, dstImage, ThresholdRatio, 255, ThresholdType.Binary);

            Cv.AdaptiveThreshold(dstImage, dstImage, 255, AdaptiveThresholdType.GaussianC);
            WorkImage.Dispose();

            return;
        }

        public static void GetBinaryImage(CvMat Image, double Threshold, out CvMat dstImageMat)
        {
            if (Image == null)
            {
                dstImageMat = null;
                return;
            }

            CvMat GrayImage;
            if (Image.ElemChannels == 1)
                GrayImage = Image.Clone();
            else
                GetGray(Image, out GrayImage);


            dstImageMat = new CvMat(Image.Rows, Image.Cols, MatrixType.U8C1);

            double ThresholdRatio = GetAverageScalar(GrayImage) * Threshold;
            Cv.Threshold(GrayImage, dstImageMat, ThresholdRatio, 255, ThresholdType.Binary);
            //GetU8GrayImage(Image, out GrayImage);
            //Cv.AdaptiveThreshold(GrayImage, dstImageMat, 255, AdaptiveThresholdType.MeanC);

            GrayImage.Dispose();
            return;
        }

        /*
        public static Vector MatToVector(CvMat Mat)
        {
            Vector ResultVectore = new Vector(Mat.Rows * Mat.Cols);
            for (int x = 0; x < Mat.Cols; x++)
                for (int y = 0; y < Mat.Rows; y++)
                    ResultVectore[x + y * Mat.Cols] = Mat[y * Mat.Cols + x];

            return ResultVectore;
        }

        public static CvMat VectorToMat(Vector Vector)
        {
            CvMat ResultMat = new CvMat(1, Vector.Length, MatrixType.F32C1);
            for (int x = 0; x < Vector.Length; x++)
                ResultMat[0, x] = Vector[x];

            return ResultMat;
        }

        public static CvMat VectorToMat(Vector Vector, int ColSize)
        {
            if (Vector.Length % ColSize != 0)
                throw new ApplicationException("サイズがおかしいです");

            CvMat ResultMat = new CvMat(Vector.Length / ColSize, ColSize, MatrixType.F32C1);
            for (int y = 0; y < Vector.Length / ColSize; y++)
                for (int x = 0; x < ColSize; x++)
                    ResultMat[y, x] = Vector[y * ColSize + x];

            return ResultMat;
        }

        public static void MatrixToCvMat(Matrix Matrix, out CvMat dstMat)
        {
            if (Matrix == null)
            {
                dstMat = null;
                return;
            }

            dstMat = new CvMat(Matrix.RowSize, Matrix.ColSize, MatrixType.F32C1);
            for (int x = 0; x < Matrix.ColSize; x++)
                for (int y = 0; y < Matrix.RowSize; y++)
                    dstMat[y, x] = Matrix[y, x];

            return;
        }

        public static Matrix CvMatToMatrix(CvMat Mat)
        {
            if (Mat == null)
                return null;

            Matrix ResultMat = new Matrix(Mat.Rows, Mat.Cols);
            for (int x = 0; x < ResultMat.ColSize; x++)
                for (int y = 0; y < ResultMat.RowSize; y++)
                    ResultMat[y, x] = Mat[y * Mat.Cols + x];

            return ResultMat;
        }

        public static CvMat VectorToMat(Vector Vector, int RowSize, int ColSize)
        {
            if (Vector.Length != ColSize * RowSize)
                throw new ApplicationException("サイズが正しくありません。");

            CvMat ResultMat = new CvMat(RowSize, ColSize, MatrixType.F32C1);
            for (int x = 0; x < ResultMat.Cols; x++)
                for (int y = 0; y < ResultMat.Rows; y++)
                    ResultMat[y, x] = Vector[x + y * ColSize];

            return ResultMat;
        }


        public static void GetReduceRowData(CvMat Image, out CvMat dstImageMat)
        {
            dstImageMat = new CvMat(1, Image.Width, GetMatrixType(Image.ElemDepth, Image.ElemChannels));
            Image.Reduce(dstImageMat, ReduceDimension.Row, ReduceOperation.Avg);

            return;
        }*/

        /// <summary>
        /// 読み込める拡張子かチェック
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>読み込めるならTrue、そうでないならFalse</returns>
        public static bool CheckExt(string FileName)
        {
            string Extension = System.IO.Path.GetExtension(FileName).ToLower();
            if (Extension != ".png" &&
                Extension != ".jpg" &&
                Extension != ".jpeg" &&
                Extension != ".bmp")
                return false;
            else
                return true;
        }
    }
}
