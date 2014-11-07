using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;
using DeSerialize = ObjectSecondVersion.ShapePartData;
using MatrixVector;

namespace ObjectSecondVersion
{
    /// <summary>
    /// 実座標の三角形データと座標データリスト（pdat)と再サンプリング前後データを保持するクラス
    /// </summary>
    [Serializable]
    public class ShapePartData
    {
        /// <summary>
        /// 参照元の点群データ
        /// </summary>
        cPointData _BeforeShapeData;

        /// <summary>
        /// 三角形の形状データ等
        /// </summary>
        List<ShapeTriangle> _ShapeTriangleDataList = new List<ShapeTriangle>();

        /// <summary>
        /// 特徴点の実座標リスト
        /// </summary>
        List<XYZPoint> _FeaturePointData = new List<XYZPoint>();

        #region コンストラクタ
        public ShapePartData()
        {
        }

        public ShapePartData(string pdatFileName, string AscFileName)
        {
            //Asciiファイルを読み込んだ後、ShapeTriangleの参照データとして登録
            _BeforeShapeData = new cPointData(AscFileName);
            ShapeTriangle.SetRefShapePointData(_BeforeShapeData);

            XYZPointData pdatData = new XYZPointData(pdatFileName);
            for (int i = 0; i < pdatData.Length; i++)
                _FeaturePointData.Add(pdatData[i]);
        }

        public ShapePartData(ShapePartData ShapePartData)
        {
            foreach (ShapeTriangle ShapeTriangle in ShapePartData._ShapeTriangleDataList)
                this._ShapeTriangleDataList.Add(ShapeTriangle);

            foreach (XYZPoint XYZPoint in ShapePartData._FeaturePointData)
                this._FeaturePointData.Add(XYZPoint);

            _BeforeShapeData = new cPointData(ShapePartData.BeforeShapeData);

            ObjectSecondVersion.ShapeTriangle.SetRefShapePointData(_BeforeShapeData);
        }
        #endregion


        #region 入出力関係
        /// <summary>
        /// データをバイナリ形式からロードするメソッド
        /// </summary>
        /// <param name="strLoadFileName">ロードファイル名</param>
        /// <returns>ロードしたデータ</returns>
        public static DeSerialize BinaryDataLoad(string strLoadFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strLoadFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                     System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                     DeSerialize DeSerializeData = (DeSerialize)bf.Deserialize(fs);
                    ShapeTriangle.SetRefShapePointData(DeSerializeData._BeforeShapeData);
                    return DeSerializeData;
                }
            }
            catch (Exception error)
            {
                throw new ApplicationException(error.Message);
            }
        }
        
        /// <summary>
        /// このクラスをバイナリ形式でセーブするメソッド
        /// </summary>
        /// <param name="strSaveFileName">セーブファイル名</param>_RecipeList
        /// <returns>成功ならTrue</returns>
        public bool BinaryDataSave(string strSaveFileName)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(strSaveFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bf.Serialize(fs, this);
                }
            }
            catch (Exception error)
            {
                throw new ApplicationException(error.Message);
            }
            return true;
        }

        /// <summary>
        /// Pdat形式でデータを保存します
        /// </summary>
        /// <param name="SaveFileName">保存ファイル名</param>
        public void SavePdat(string SaveFileName)
        {
            #region pdatファイルの書き込み
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(SaveFileName))
                {
                    sw.Write(this.StringOut());
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            #endregion
        }

        /// <summary>
        /// pdat互換のデータを出力します。
        /// </summary>
        /// <returns>pdat互換データ</returns>
        protected virtual string StringOut()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _FeaturePointData.Count; i++)
                sb.Append(_FeaturePointData[i].X + "\t" + _FeaturePointData[i].Y + "\t" + _FeaturePointData[i].Z + "\n");

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// レシピ変数を元に特徴点を追加します。
        /// </summary>
        /// <param name="Recipe"></param>
        public void AddFeaturePoint(FeaturePointRecipe Recipe)
        {
            XYZPoint RefPoint = new XYZPoint(Recipe.RecipeValue.Alpha, Recipe.RecipeValue.Beta,0);
            ShapeTriangle ThisTriangle = this.GetTrianglePointToShapeTriangle(Recipe.TrianglePointData);
            int NewPointIndex = PointFormat.SearchNearestPoint.Get2DNearestPointIndexFastOnBase(new XYZPointData(_BeforeShapeData), RefPoint, ThisTriangle, 10d);
            _FeaturePointData.Add((XYZPoint)_BeforeShapeData[NewPointIndex]);
        }

        /// <summary>
        /// 指定した頂点番号三角形から実座標三角形を取得するメソッド
        /// </summary>
        /// <param name="TrianglePointData">頂点番号三角形</param>
        /// <returns>実座標三角形</returns>
        public virtual ShapeTriangle GetTrianglePointToShapeTriangle(TrianglePointData TrianglePointData)
        {
            XYZPoint[] Point = new XYZPoint[3];
            Point[0] = _FeaturePointData[TrianglePointData.TrianglePoint1];
            Point[1] = _FeaturePointData[TrianglePointData.TrianglePoint2];
            Point[2] = _FeaturePointData[TrianglePointData.TrianglePoint3];

            ShapeTriangle ShapeTriangle = new ShapeTriangle(Point);
            return ShapeTriangle;
        }

        /// <summary>
        /// 指定した頂点番号三角形配列から実座標三角形配列を取得するメソッド
        /// </summary>
        /// <param name="TrianglePointDataArray">頂点番号三角形配列</param>
        /// <returns>実座標三角形配列</returns>
        public virtual ShapeTriangle[] GetTrianglePointToShapeTriangle(TrianglePointData[] TrianglePointDataArray)
        {
            List<ShapeTriangle> ShapeTriangleList = new List<ShapeTriangle>();
            foreach (TrianglePointData TrianglePointData in TrianglePointDataArray)
                ShapeTriangleList.Add(this.GetTrianglePointToShapeTriangle(TrianglePointData));

            return ShapeTriangleList.ToArray();
        }

        /// <summary>
        /// まだ再サンプリングが行われていない実座標三角形のインデックスを取得します
        /// </summary>
        /// <returns>再サンプリングが行われていない実座標三角形のインデックス</returns>
        public int[] GetNewTriangleIndex()
        {
            List<int> ShapeTriangleIndexList = new List<int>();
            for(int i = 0; i < _ShapeTriangleDataList.Count;i++)
                if (_ShapeTriangleDataList[i].CalclatedFlag == false)
                    ShapeTriangleIndexList.Add(i);
            return ShapeTriangleIndexList.ToArray();
        }

        /// <summary>
        /// レシピと新しく追加した頂点番号三角形から、実座標三角形リストを更新します。
        /// </summary>
        /// <param name="Recipe">レシピ</param>
        /// <param name="TrianglePointData">新しく追加された頂点番号三角形配列</param>
        public void ResetTriangleData(FeaturePointRecipe Recipe, TrianglePointData[] TrianglePointData)
        {
            //レシピから不要の実座標三角形を削除
            _ShapeTriangleDataList.Remove(this.GetTrianglePointToShapeTriangle(Recipe.TrianglePointData));

            //新しく追加された頂点番号三角形から実座標三角形リストに三角形を追加
            foreach (TrianglePointData TrianglePoint in TrianglePointData)
                _ShapeTriangleDataList.Add(this.GetTrianglePointToShapeTriangle(TrianglePoint));
        }

        /// <summary>
        /// 三角形の形状データを追加します。
        /// </summary>
        /// <param name="TrianglePointData"></param>
        public void AddShapeTriangle(TrianglePointData TrianglePointData)
        {
            _ShapeTriangleDataList.Add(this.GetTrianglePointToShapeTriangle(TrianglePointData));
        }

        /// <summary>
        /// 三角形ごとの面積を出力します
        /// </summary>
        /// <returns>三角形ごとの面積</returns>
        public virtual Vector GetArea()
        {
            Vector AreaVector = new Vector(_ShapeTriangleDataList.Count);

            for (int i = 0; i < _ShapeTriangleDataList.Count; i++)
                AreaVector[i] = _ShapeTriangleDataList[i].Area;
               
            return AreaVector;
        }

        /// <summary>
        /// 指定したインデックスの三角形を削除します
        /// </summary>
        /// <param name="RemoveIndex">削除する三角形のインデックス</param>
        public void RemoveTriangleAt(int RemoveIndex)
        {
            _ShapeTriangleDataList.RemoveAt(RemoveIndex);
        }

        #region プロパティ
        /// <summary>
        /// 再サンプリング後の形状データを取得
        /// </summary>
        /// <returns>再サンプリング後の形状データ</returns>
        public cPointData AfterShapeData
        {
            get
            {
                if (_ShapeTriangleDataList.Count == 0)
                    throw new ApplicationException("このShapeObjectDataに再サンプリング後のデータは入っていません");

                cPointData[] PointData = new cPointData[_ShapeTriangleDataList.Count];
                for (int i = 0; i < _ShapeTriangleDataList.Count; i++)
                    PointData[i] = _ShapeTriangleDataList[i].AfterShapeData;

                return new cPointData(PointData);
            }
        }

        /// <summary>
        /// 再サンプリング前の形状データを取得
        /// </summary>
        /// <returns>再サンプリング前の形状データ</returns>
        public cPointData BeforeShapeData
        {
            get { return _BeforeShapeData; }
        }

        public Vector[] RecallIndex
        {
            get
            {
                Vector[] RecallIndexVector = new Vector[this.TriangleCount];
                for (int i = 0; i < RecallIndexVector.Length; i++)
                    RecallIndexVector[i] = _ShapeTriangleDataList[i].RecallIndex;
                return RecallIndexVector;
            }
        }

        /// <summary>
        /// 三角形形状データを取得します
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>三角形形状データ</returns>
        public ShapeTriangle this[int Index]
        {
            get { return _ShapeTriangleDataList[Index]; }
            set { _ShapeTriangleDataList[Index] = value; }
        }

        /// <summary>
        /// 三角形形状データのリストを取得、設定します。
        /// </summary>
        public List<ShapeTriangle> ShapeTriangleDataList
        {
            get { return _ShapeTriangleDataList; }
            set { _ShapeTriangleDataList = value; }
        }

        /// <summary>
        /// 特徴点実座標リストを取得します。
        /// </summary>
        public List<XYZPoint> FeaturePointList { get { return _FeaturePointData; } }

        /// <summary>
        /// 特徴点の数を取得します。
        /// </summary>
        public int FeaturePointCount
        {
            get { return _FeaturePointData.Count; }
        }

        /// <summary>
        /// オブジェクトが保持している三角形点の個数を取得します
        /// </summary>
        public int TriangleCount
        {
            get { return _ShapeTriangleDataList.Count; }
        }
        #endregion

    }
}
