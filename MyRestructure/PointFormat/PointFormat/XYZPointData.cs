using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ReturnPointDataType = PointFormat.XYZPointData;
using ReturnPointType = PointFormat.XYZPoint;

namespace PointFormat
{
    [Serializable]
    public class XYZPointData
    {
                /// <summary>
        /// 保持しているXYZPointの配列
        /// </summary>
        protected ReturnPointType[] PointData;

        #region コンストラクタ
        public XYZPointData()
        {
        }

        public XYZPointData(string strLoadFileName)
        {
            cLoadPoint iclpLoadPoint = new cLoadPoint(strLoadFileName);
            iclpLoadPoint.bReadData();
            PointData = iclpLoadPoint.ipPoint;
        }

        public XYZPointData(ReturnPointDataType icpdSentPointData)
            :this(icpdSentPointData.Items)
        {
        }

        public XYZPointData(ReturnPointType[] icaSentPoint)
        {
            PointData = new ReturnPointType[icaSentPoint.Length];
            for (int i = 0; i < Length; i++)
                PointData[i] = new ReturnPointType(icaSentPoint[i]);
        }

        public XYZPointData(ReturnPointDataType[] XYZPointData)
        {
            List<ReturnPointType> DataList = new List<ReturnPointType>();
            foreach (ReturnPointDataType Data in XYZPointData)
                for (int i = 0; i < Data.Length; i++)
                    DataList.Add(Data[i]);
            this.Items = DataList.ToArray();
        }

        public XYZPointData(ReturnPointDataType XYZPointData, int[] IndexArray)
        {
            this.Items = new ReturnPointType[IndexArray.Length];
            for (int i = 0; i < IndexArray.Length; i++)
                this[i] = new ReturnPointType(XYZPointData[IndexArray[i]]);
        }

        public XYZPointData(cPointData PointData, int[] IndexArray)
        {
            this.Items = new ReturnPointType[IndexArray.Length];
            for (int i = 0; i < IndexArray.Length; i++)
                this[i] = new ReturnPointType(PointData[IndexArray[i]]);
        }

        public XYZPointData(cPointData PointData)
        {
            this.Items = new ReturnPointType[PointData.Length];
            for (int i = 0; i < PointData.Length; i++)
                this[i] = new ReturnPointType(PointData[i].X, PointData[i].Y, PointData[i].Z);
        }
        #endregion

        public ReturnPointDataType GetIndexData(int[] IndexArray)
        {
            ReturnPointDataType RePointData = new ReturnPointDataType();
            ReturnPointType[] RePoint = new ReturnPointType[IndexArray.Length];

            for (int i = 0; i < IndexArray.Length; i++)
                RePoint[i] = this[IndexArray[i]];

            RePointData.Items = RePoint;

            return RePointData;
        }

        public void Move(double dX, double dY, double dZ)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].Move(dX, dY, dZ);
        }

        public ReturnPointDataType GetMovePointData(double dX, double dY, double dZ)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].Move(dX, dY, dZ);
            return ResultData;
        }

        public void RatioChange(double dRatio)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].Ratio(dRatio);
        }

        public ReturnPointDataType GetRatioChangeData(double dRatio)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].Ratio(dRatio);
            return ResultData;
        }

        public void RatioChange(double dX_Ratio, double dY_Ratio, double dZ_Ratio)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].Ratio(dX_Ratio, dY_Ratio, dZ_Ratio);
        }

        public ReturnPointDataType GetRatioChangeData(double dX_Ratio, double dY_Ratio, double dZ_Ratio)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].Ratio(dX_Ratio, dY_Ratio, dZ_Ratio);
            return ResultData;
        }

        public void RotateXDegree(double dDegreeAngle)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].RotateD_X(dDegreeAngle);
        }

        public ReturnPointDataType GetRotateXDataDegree(double dDegreeAngle)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateD_X(dDegreeAngle);
            return ResultData;
        }

        public void RotateYDegree(double dDegreeAngle)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].RotateD_Y(dDegreeAngle);
        }

        public ReturnPointDataType GetRotateYDataDegree(double dDegreeAngle)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateD_Y(dDegreeAngle);
            return ResultData;
        }

        public void RotateZDegree(double dDegreeAngle)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].RotateD_Z(dDegreeAngle);
        }

        public ReturnPointDataType GetRotateZDataDegree(double dDegreeAngle)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateD_Z(dDegreeAngle);
            return ResultData;
        }

        public void RotateXRadian(double dRadianAngle)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].RotateR_X(dRadianAngle);
        }

        public ReturnPointDataType GetRotateXDataRadian(double dRadianAngle)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateR_X(dRadianAngle);
            return ResultData;
        }

        public void RotateYRadian(double dRadianAngle)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].RotateR_Y(dRadianAngle);
        }

        public ReturnPointDataType GetRotateYDataRadian(double dRadianAngle)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateR_Y(dRadianAngle);
            return ResultData;
        }

        public void RotateZRadian(double dRadianAngle)
        {
            for (int i = 0; i < Length; i++)
                PointData[i].RotateR_Z(dRadianAngle);
        }

        public ReturnPointDataType GetRotateZDataRadian(double dRadianAngle)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateR_Z(dRadianAngle);
            return ResultData;
        }

        public ReturnPointDataType GetRotateXYZDataRadian(double dRadianAngle_X, double dRadianAngle_Y, double dRadianAngle_Z)
        {
            ReturnPointDataType ResultData = new ReturnPointDataType(this);
            for (int i = 0; i < Length; i++)
                ResultData[i].RotateR_XYZ(dRadianAngle_X, dRadianAngle_Y, dRadianAngle_Z);
            return ResultData;
        }

        public double[] GetAll_X()
        {
            double[] Result = new double[Length];
            for (int i = 0; i < Length; i++)
                Result[i] = PointData[i].X;

            return Result;
        }

        public double[] GetAll_Y()
        {
            double[] Result = new double[Length];
            for (int i = 0; i < Length; i++)
                Result[i] = PointData[i].Y;

            return Result;
        }

        public double[] GetAll_Z()
        {
            double[] Result = new double[Length];
            for (int i = 0; i < Length; i++)
                Result[i] = PointData[i].Z;

            return Result;
        }

        public int Length
        {
            get { return PointData.Length; }
        }

        public ReturnPointType[] Items
        {
            get { return PointData; }
            set { PointData = value; }
        }

        public void Save(string SaveFileName)
        {
            cPoint[] PointData = new cPoint[Length];
            for (int i = 0; i < Length; i++)
                PointData[i] = new cPoint(this.PointData[i]);
            cSavePoint icSavePoint = new cSavePoint(SaveFileName, PointData);
            icSavePoint.bForceSave_value = true;
            icSavePoint.bSaveFile();
        }

        public ReturnPointType this[int Index]
        {
            get { return PointData[Index]; }
            set { PointData[Index] = value; }
        }

        /// <summary>
        /// このクラスをバイナリ形式でセーブするメソッド
        /// </summary>
        /// <param name="strSaveFileName">セーブファイル名</param>
        /// <returns>成功ならTrue</returns>
        public bool BinaryDataSave(string strSaveFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strSaveFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, this);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ファイル書き込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// データをバイナリ形式からロードするメソッド
        /// </summary>
        /// <param name="strLoadFileName">ロードファイル名</param>
        /// <returns>ロードしたデータ</returns>
        public static ReturnPointDataType BinaryDataLoad(string strLoadFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strLoadFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (ReturnPointDataType)bf.Deserialize(fs);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ファイル読み込みエラー", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
