using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;
using MatrixVector;

namespace PCAManagerFromAsciiData
{
    /// <summary>
    /// 3次元点データから1次元ベクトルデータにするクラスです
    /// </summary>
    public class CreateVectorFromAsciiData
    {
        private CreateVectorFromAsciiData()
        {
        }

        /// <summary>
        /// 指定したファイルのXYZデータからベクトルを作成します
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>XYZベクトルデータ</returns>
        public static Vector  GetVectorFromAscXYZ(string FileName)
        {
            XYZPointData PointData;
            try
            {
                PointData = new XYZPointData(FileName);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return CreateVectorFromPointFormat.GetVectorFromPoint(PointData);
        }
        
        /// <summary>
        /// 指定したファイルのRGBデータからベクトルを作成します
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>RGBベクトルデータ</returns>
        public static Vector GetVectorFromAscRGB(string FileName)
        {
            double[] Data;
            try
            {
                cPointData PointData = new cPointData(FileName);
                Data = new double[PointData.Length * 3];
                for (int i = 0; i < PointData.Length; i++)
                {
                    Data[i * 3 + 0] = PointData.Items[i].R;
                    Data[i * 3 + 1] = PointData.Items[i].G;
                    Data[i * 3 + 2] = PointData.Items[i].B;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return new Vector(Data);
        }

        /// <summary>
        /// 指定したファイルのXYZRGBデータからベクトルを作成します
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>XYZRGBベクトルデータ</returns>
        public static Vector GetVectorFromAscXYZRGB(string FileName)
        {
            cPointData PointData;
            try
            {
                PointData = new cPointData(FileName);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return CreateVectorFromPointFormat.GetVectorFromPoint(PointData);
        }
    }
}
