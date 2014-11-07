using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using System.Drawing;

namespace MatrixVectorForBitmap
{
    public static class gcBitmapConverter
    {
        #region Vector取得系
        /// <summary>
        /// 横スキャンで画素を取得してベクトル化します。(グレー画像用)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Vector gBitmapToVector(Bitmap inImage)
        {
            Vector ReturnVector = new Vector(inImage.Width * inImage.Height);
            for (int h = 0; h < inImage.Height; h++)
            {
                for (int w = 0; w < inImage.Width; w++)
                {
                    ReturnVector[h * inImage.Width + w] = inImage.GetPixel(w, h).R;
                }
            }
            return ReturnVector;
        }

        /// <summary>
        /// 横スキャンで画素を取得してベクトル化します。(カラー画像用。長さはグレーの3倍になります。)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Vector cBitmapToVector(Bitmap inImage)
        {
            Vector ReturnVector = new Vector(inImage.Width * inImage.Height * 3);
            for (int h = 0; h < inImage.Height; h++)
            {
                for (int w = 0; w < inImage.Width * 3; w+=3)
                {
                    ReturnVector[h * inImage.Width * 3 + w + 0] = inImage.GetPixel(w/3, h).R;
                    ReturnVector[h * inImage.Width * 3 + w + 1] = inImage.GetPixel(w/3, h).G;
                    ReturnVector[h * inImage.Width * 3 + w + 2] = inImage.GetPixel(w/3, h).B;
                }
            }
            return ReturnVector;
        }

        /// <summary>
        /// 横スキャンで画素を取得してベクトル化します。(グレー画像用)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Vector[] gBitmapsToVectors(Bitmap[] inImage)
        {
            Vector[] ReturnVector = new Vector[inImage.Length];
            for(int i = 0 ; i < inImage.Length ;i++)
            {
                ReturnVector[i] = gBitmapToVector(inImage[i]);
            }
            return ReturnVector;
        }

        /// <summary>
        /// 横スキャンで画素を取得してベクトル化します。(カラー画像用。長さはグレーの3倍になります。)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Vector[] cBitmapsToVectors(Bitmap[] inImage)
        {
            Vector[] ReturnVector = new Vector[inImage.Length];
            for (int i = 0; i < inImage.Length; i++)
            {
                ReturnVector[i] = cBitmapToVector(inImage[i]);
            }
            return ReturnVector;
        }
        #endregion

        #region Bitmap生成系
        /// <summary>
        /// ベクトルからBitmapを生成します。(グレー画像用)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Bitmap gBitmapFromVector(Vector inVector,int Width,int Height)
        {
            Bitmap ReturnBitmap = new Bitmap(Width, Height);
            
            for (int h = 0; h < ReturnBitmap.Height; h++)
            {
                for (int w = 0; w < ReturnBitmap.Width; w++)
                {
                    ReturnBitmap.SetPixel(w, h, Color.FromArgb((int)inVector[h * ReturnBitmap.Width + w], (int)inVector[h * ReturnBitmap.Width + w], (int)inVector[h * ReturnBitmap.Width + w]));
                }
            }
            return ReturnBitmap;
        }

        
        /// <summary>
        /// ベクトルからBitmapを生成します。(カラー画像用)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Bitmap cBitmapFromVector(Vector inVector,int Width, int Height)
        {
            
            Bitmap ReturnBitmap = new Bitmap(Width, Height);

            for (int h = 0; h < ReturnBitmap.Height; h++)
            {
                for (int w = 0; w < ReturnBitmap.Width * 3; w+=3)
                {
                    ReturnBitmap.SetPixel(w/3, h, Color.FromArgb((int)inVector[h * ReturnBitmap.Width * 3 + w + 0], (int)inVector[h * ReturnBitmap.Width * 3 + w + 1], (int)inVector[h * ReturnBitmap.Width * 3 + w + 2]));
                }
            }
            return ReturnBitmap;
             
        }

        /// <summary>
        /// ベクトルからBitmapを生成します。(グレー画像用)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Bitmap[] gBitmapsFromVectors(Vector[] inVector, int Width, int Height)
        {
            Bitmap[] ReturnBitmap = new Bitmap[inVector.Length];

            for (int i = 0; i < inVector.Length; i++)
            {
                ReturnBitmap[i] = gBitmapFromVector(inVector[i],Width,Height);
            }
            return ReturnBitmap;
        }

        /// <summary>
        /// ベクトルからBitmapを生成します。(カラー画像用)
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Bitmap[] cBitmapsFromVectors(Vector[] inVector,int Width, int Height)
        {
            Bitmap[] ReturnBitmap = new Bitmap[inVector.Length];

            for (int i = 0; i < inVector.Length; i++)
            {
                ReturnBitmap[i] = cBitmapFromVector(inVector[i], Width, Height);
            }
            return ReturnBitmap;
        }
        #endregion


        #region OpenCV用
        /// <summary>
        /// OpenCvSharpのラッパーCvUtilityの型に沿ったCvMatrixを生成します。他の生成系メソッドとはフォーマットが違うので注意してください。
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Matrix cBitmapToCVMatrix(Bitmap inImage)
        {
            Matrix ReturnMatrix = new Matrix(inImage.Width * inImage.Height, 3);
            for (int h = 0; h < inImage.Height; h++)
            {
                for (int w = 0; w < inImage.Width * 3; w += 3)
                {
                    Color color = inImage.GetPixel(w, h);
                    ReturnMatrix[h * inImage.Width + w, 0] = color.R;
                    ReturnMatrix[h * inImage.Width + w, 1] = color.G;
                    ReturnMatrix[h * inImage.Width + w, 2] = color.B;
                }
            }
            return ReturnMatrix;
        }

        /// <summary>
        /// OpenCvSharpのラッパーCvUtilityの型に沿ったマトリクスからカラー画像を生成します。他の生成系メソッドとはフォーマットが違うので注意してください。
        /// </summary>
        /// <param name="inImage"></param>
        /// <returns></returns>
        public static Bitmap cBitmapFromCVMatrix(Matrix cvMatrix, int Width, int Height)
        {
            Bitmap ReturnImage = new Bitmap(Width, Height);
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width * 3; w += 3)
                {
                    Color color = Color.FromArgb((int)cvMatrix[h * Width + w,0],(int)cvMatrix[h * Width + w,1],(int)cvMatrix[h * Width + w,2]);
                    ReturnImage.SetPixel(w, h, color);
                }
            }
            return ReturnImage;
        }
        #endregion
    }
}
