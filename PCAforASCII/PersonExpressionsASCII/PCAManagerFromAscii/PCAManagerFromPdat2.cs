using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoPCA;
using ObjectSecondVersion;
using MatrixVector;
using PointFormat;

namespace PCAManagerFromAsciiData
{
    public class PCAManagerFromPdat2:PCABaseManager
    {
        public PCAManagerFromPdat2() {
            OpeningMessage = "pdat2ファイルから主成分分析を行います";
            this.PCASource = PCASource.AsciiDataShapeOnly;
        }

        public PCAManagerFromPdat2(List<string> FileList)
            :base(FileList)
        {
            OpeningMessage = "pdat2ファイルから主成分分析を行います";
            this.PCASource = PCASource.AsciiDataShapeOnly; 
        }

        protected override MatrixVector.Matrix LoadFile(List<string> LoadFileList)
        {
            Vector[] ResultVector = new Vector[LoadFileList.Count];

            for (int i = 0; i < ResultVector.Length; i++)
            {
                PointFormat.XYZPointData PointData = new XYZPointData(ShapePartData.BinaryDataLoad(LoadFileList[i]).AfterShapeData);
                ResultVector[i] = CreateVectorFromPointFormat.GetVectorFromPoint(PointData);
            }

            return new Matrix(ResultVector);
        }
    }
}
