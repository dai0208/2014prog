using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using _3DPoint;

namespace PCA
{
    public class RecreateMatrixFromMtxData
    {
        PCAData PCAData;

        protected RecreateMatrixFromMtxData()
        {
        }

        public RecreateMatrixFromMtxData(PCAData PCAData)
        {
            this.PCAData = PCAData;
        }

        public virtual Bitmap GetBitmap(int DataIndex,int UseParamAmount)
        {
            if (UseParamAmount > PCAData.ParamCount)
                throw new ApplicationException("パラメータの数が多すぎます");

            throw new NotImplementedException();
        }

        public virtual Bitmap GetBitmap(int DataIndex)
        {
            this.GetBitmap(DataIndex, PCAData.ParamCount);
        }

        public virtual cPoint GetPointData(int DataIndex, int UseParamAmount)
        {
            if (UseParamAmount > PCAData.ParamCount)
                throw new ApplicationException("パラメータの数が多すぎます");

            throw new NotImplementedException();
        }

        public virtual cPoint GetPointData(int DataIndex)
        {
            this.GetPointData(DataIndex, PCAData.ParamCount);
        }

    }
}
