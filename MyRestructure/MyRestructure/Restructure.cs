using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using PointFormat;
using System.IO;

namespace MyRestructure
{
    public class Restructure
    {
        private Vector average;
        private Vector Eigenvalue;
        private Vector[] eParams;
        private Vector[] EigenVector;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="average">主成分空間の原点座標(重心)</param>
        /// <param name="Eigenvalue">固有値。使う主成分の数だけ記述されたデータを用意してください。</param>
        /// <param name="EigenVector">固有ベクトル群。使う主成分の数だけ用意してください。</param>
        /// <param name="eParams">パラメータ群。フレーム数だけ用意してください。</param>
        public Restructure(Vector average, Vector Eigenvalue, Vector[] EigenVector, Vector[] eParams)
        {
            this.average = average;
            this.Eigenvalue = Eigenvalue;
            this.EigenVector = EigenVector;
            this.eParams = eParams;
        }

        public XYZPointData[] DoRestructure(XYZPointData magao)
        {
            //パラメータの数だけ用意
            Vector[] temp = new Vector[eParams.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = new Vector(average);
            }
            //一種の主成分のみで作成
            for (int i = 0; i < eParams.Length; i++)
            {
                for (int ii = 0; ii < Eigenvalue.Length; ii++)
                {
                    temp[ii] = temp[ii] + (eParams[i][ii] * EigenVector[ii]);
                }
            }

            XYZPoint[][] outd = new XYZPoint[temp.Length][];
            for (int i = 0; i < outd.Length; i++)
            {
                outd[i] = new XYZPoint[magao.Items.Length];
            }

            int count = 0;
            XYZPointData[] outdata = new XYZPointData[outd.Length];

            for (int k = 0; k < outd.Length; k++)
            {
                for (int i = 0; i < outd[k].Length; i++)
                {
                    outd[k][i] = new XYZPoint(magao.Items[i].X, magao.Items[i].Y, magao.Items[i].Z);
                    outd[k][i].X += temp[k][count];
                    count++;
                    outd[k][i].Y += temp[k][count];
                    count++;
                    outd[k][i].Z += temp[k][count];
                    count++;
                    //outd[k][i].R += (int)temp[k][count];
                    count++;
                    //outd[k][i].G += (int)temp[k][count];
                    count++;
                    //outd[k][i].B += (int)temp[k][count];
                    count++;
                }
                count = 0;
                outdata[k] = new XYZPointData(outd[k]);
            }
            return outdata;
            
        }
    }
}
