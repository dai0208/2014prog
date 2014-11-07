using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonExpressionsASCII;
using MatrixVector;
using PCAManagerFromAsciiData;
using PointFormat;

namespace PCAforASCII
{
    //マトリクスを生成したら本クラスのインスタンスは破棄してください。

    public class cPersonManager:IDisposable
    {
        private List<cPerson> _cPersonList { get; set; }

        public cPersonManager(List<cPerson> cPersonList)
        {
            _cPersonList = cPersonList;
        }

        void IDisposable.Dispose()
        {
        }

        /// <summary>
        /// 各表情の平均顔を求めて返します。
        /// </summary>
        public cPerson Average()
        {
            cPerson average = _cPersonList[0];
            for (int i = 1; i < _cPersonList.Count; i++)
            {
                average = average + _cPersonList[i];
            }
            average = average / _cPersonList.Count;

            return average;
        }

        /// <summary>
        /// 自身の真顔からの差分顔を求めて上書きします。
        /// </summary>
        public List<cPerson> Substruct()
        {
            for (int i = 0; i < _cPersonList.Count; i++)
                _cPersonList[i].SubstructFromExpressionless();

            return _cPersonList; 
        }

        /// <summary>
        /// 形状とテクスチャの分離をします。並びはa,i,u,e,o,cl,ol,nの順で全員分の分離データを返します。
        /// </summary>
        public XYZPointData[][] ReturnXYZ()
        {
            XYZPointData[][] ReturnData = new XYZPointData[_cPersonList.Count][];

            for (int i = 0; i < _cPersonList.Count; i++)
            {
                ReturnData[i] = _cPersonList[i].cDataToShape();
            }
            return ReturnData;
        }

        /// <summary>
        /// テクスチャの分離をします。並びはa,i,u,e,o,cl,ol,nの順で全員分の分離データを返します。
        /// </summary>
        public XYZPointData[][] ReturnRGB()
        {
            XYZPointData[][] ReturnData = new XYZPointData[_cPersonList.Count][];

            for (int i = 0; i < _cPersonList.Count; i++)
            {
                ReturnData[i] = _cPersonList[i].cDataToShape();
            }
            return ReturnData;
        }

        /// <summary>
        /// cPersonを基に形状マトリクスを生成します。マトリクスは横にa,i,u,e,o,cl,ol,nの並びになっています。
        /// </summary>
        /// <returns>マトリクスデータ</returns>
        public Matrix GetMatrixFromShape()
        {
            int ExpressionCount = 8; //使う表情数に応じて適宜変更してください。

            Vector[] vector = new Vector[_cPersonList.Count * ExpressionCount];
            for (int i = 0; i < _cPersonList.Count; i++)
            {
                vector[0 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].a.MakeShape());
                vector[1 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].i.MakeShape());
                vector[2 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].u.MakeShape());
                vector[3 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].e.MakeShape());
                vector[4 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].o.MakeShape());
                vector[5 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].cl.MakeShape());
                vector[6 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].ol.MakeShape());
                vector[7 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].n.MakeShape());
            }
            return new Matrix(vector);
        }

        /// <summary>
        /// cPersonを基にテクスチャマトリクスを生成します。マトリクスは横にa,i,u,e,o,cl,ol,nの並びになっています。
        /// </summary>
        /// <returns>再構築マトリクスデータ</returns>
        public Matrix GetMatrixFromTexture()
        {
            int ExpressionCount = 8; //使う表情数に応じて適宜変更してください。

            Vector[] vector = new Vector[_cPersonList.Count * ExpressionCount];
            for (int i = 0; i < _cPersonList.Count; i++)
            {
                vector[0 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].a.MakeTexture());
                vector[1 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].i.MakeTexture());
                vector[2 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].u.MakeTexture());
                vector[3 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].e.MakeTexture());
                vector[4 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].o.MakeTexture());
                vector[5 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].cl.MakeTexture());
                vector[6 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].ol.MakeTexture());
                vector[7 * _cPersonList.Count + i] = CreateVectorFromPointFormat.GetVectorFromPoint(_cPersonList[i].n.MakeTexture());

                //リソース確保のため、変換後オブジェクトは破棄します。
                _cPersonList[i] = null;
            }
            return new Matrix(vector);
        }
    }
}
