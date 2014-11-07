using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using PointFormat;
using System.Windows.Forms;
using System.IO;

namespace MakeTeacherDataFromPCAData
{
    class MakeParams
    {
        private string[] filelist; //保存用のパスを保管します。
        private int EigenVectorCount; //ユーザから受け取った使用する主成分数

        public MakeParams(string[] filelist)
        {
            this.filelist = filelist;
        }

        public int setEigenVectorCount
        {
            set{ this.EigenVectorCount = value;}
        }

        protected ColumnVector getAverageVectorFromFile(string PassToAverageVector)
        {
            ColumnVector vct = new ColumnVector(ColumnVector.Load(PassToAverageVector));
            return vct;
        }

        /// <summary>
        /// listBoxにある拡張子ascデータへのパスを読み込んで各々を点群リストとして返します。
        /// </summary>
        /// <param name="PassDataList"></param>
        /// <returns></returns>
        protected List<XYZPointData> ReadAscFaceDatas(ListBox PassDataList)
        {
            List<XYZPointData> InAscFaceData = new List<XYZPointData>();
            for (int i = 0; i < PassDataList.Items.Count; i++)
            {
                InAscFaceData.Add(new XYZPointData((string)PassDataList.Items[i]));
            }
            return InAscFaceData;
        }

        /// <summary>
        /// listBoxにある固有ベクトルへのパスを読み込んでベクトル群リストとして返します。
        /// </summary>
        /// <param name="EigenVectorsList"></param>
        /// <returns></returns>
        protected List<RowVector> ReadEigenVectors(ListBox EigenVectorsList)
        {
            List<RowVector> EigenVectors = new List<RowVector>();

            //読み込む固有ベクトルの個数を手動で調整できるように929に変更しました。
            for (int i = 0; i < EigenVectorCount; i++)
            {
                EigenVectors.Add(new RowVector(RowVector.Load((string)EigenVectorsList.Items[i])));
            }
            return EigenVectors;
        }
        
        public void DoOperation(ListBox IDLs ,ListBox EVLs, TextBox AVGP, TextBox nAVGP)
        {
            List<Vector> outd = new List<Vector>(); //出力ベクトル
            List<XYZPointData> IDs = ReadAscFaceDatas(IDLs); //入力ベクトルのXYZ(RGBの場合にも流用できるかと・・・)
            List<RowVector> EVs = ReadEigenVectors(EVLs); //固有ベクトル群
            XYZPointData nAVG = new XYZPointData(nAVGP.Text); //真顔のXYZ(RGBの場合にも流用できるかと・・・)
            ColumnVector AVG = getAverageVectorFromFile(AVGP.Text);

            /* Phase1. 展開係数の算出を行います。*/

            for(int i=0;i<IDs.Count;i++)
            {
                ColumnVector SUBDatas = new ColumnVector(AVG.Length);
                for(int j=0;j<IDs[i].Length;j++)
                {
                    int count = 0;
                    SUBDatas[6 * j + count] = IDs[i][j].X - AVG[6 * j + count];
                    count++;
                    SUBDatas[6 * j + count] = IDs[i][j].Y - AVG[6 * j + count];
                    count++;
                    SUBDatas[6 * j + count] = IDs[i][j].Z - AVG[6 * j + count];
                    
                    /*RGBにはとりあえず０を入れておきます*/                 
                }
                outd.Add(new Vector(EVs.Count));
                for (int k = 0; k < EVs.Count; k++)
                {
                    outd[i][k] = EVs[k].InnerProduct(SUBDatas); //Fik = Uik * (Xik - k)
                }
            }

            /* Phase2. 真顔平均ベクトルに対しても同様の処理をします。*/

            for (int j = 0; j < nAVG.Length; j++)
            {
                int count = 0;
                AVG[6 * j + count] = nAVG[j].X - AVG[6 * j + count];
                count++;
                AVG[6 * j + count] = nAVG[j].Y - AVG[6 * j + count];
                count++;
                AVG[6 * j + count] = nAVG[j].Z - AVG[6 * j + count];

                /*RGBにはとりあえず０を入れておきます*/                 
            }

            ColumnVector nPrm = new ColumnVector(EVs.Count);

            for (int k = 0; k < EVs.Count; k++)
            {
                nPrm[k] = EVs[k].InnerProduct(AVG); //Fik = Uik * (Xik - k)
            }

            /* Phase3. 各表情毎の主成分分析後のパラメータの平均の真顔との差分(dertafc)を導出します。*/

            for (int i = 0; i < IDs.Count; i++)
            {
                for (int j = 0; j < EVs.Count; j++)
                {
                    outd[i][j] = outd[i][j] - nPrm[j];
                }
            }

            for (int i = 0; i < filelist.Length; i++)
            {
                outd[i].Save(Path.GetDirectoryName(filelist[i]) + "\\" + Path.GetFileNameWithoutExtension(filelist[i]) + "_Param "+ EigenVectorCount +".asc");
            }
        }


    }
}
