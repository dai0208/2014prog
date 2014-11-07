using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DeSerialize = ObjectSecondVersion.TrianglePartData;

namespace ObjectSecondVersion
{
    /// <summary>
    /// 特徴点番号の三角形データ(ndat)を保持するクラス
    /// </summary>
    [Serializable]
    public class TrianglePartData
    {
        /// <summary>
        /// 頂点番号三角形のリスト
        /// </summary>
        List<TrianglePointData> _TrianglePointDataList = new List<TrianglePointData>();

        /// <summary>
        /// レシピのリスト
        /// </summary>
        FeaturePointRecipeList _RecipeList = new FeaturePointRecipeList();

        /// <summary>
        /// 新しく追加した頂点番号三角形
        /// </summary>
        TrianglePointData[] _NewTrianglePoint;

        #region コンストラクタ
        public TrianglePartData() { }
        public TrianglePartData(TrianglePartData TrianglePartData)
        {
            foreach (TrianglePointData TrianglePointData in TrianglePartData._TrianglePointDataList)
                _TrianglePointDataList.Add(TrianglePointData);

            _RecipeList = new FeaturePointRecipeList(TrianglePartData._RecipeList);

            _NewTrianglePoint = (TrianglePointData[])TrianglePartData._NewTrianglePoint.Clone();
        }

        public TrianglePartData(string ndatFileName)
        {
            this.LoadNdatFile(ndatFileName);
        }
        #endregion

        #region ファイル入出力関係
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
                    BinaryFormatter bf = new BinaryFormatter();
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
                    return (DeSerialize)bf.Deserialize(fs);
                }
            }
            catch (Exception error)
            {
                throw new ApplicationException(error.Message);
            }
        }


        /// <summary>
        /// Ndat形式でファイルを保存します
        /// </summary>
        /// <param name="SaveFileName">保存するファイル名</param>
        public virtual void SaveNdat(string SaveFileName)
        {
            #region Ndatファイルの書き込み
            try
            {
                using (StreamWriter sw = new StreamWriter(SaveFileName))
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
        /// ファイル名を指定して三角形部分データを読み込みます。
        /// </summary>
        /// <param name="strNdatFilePath">読み込みファイル名</param>
        /// <returns>三角形部分データ</returns>
        private void LoadNdatFile(string strNdatFilePath)
        {
            #region ndatファイルの読み込み
            string LoadLines;
            int[][] NdatData;

            using (StreamReader sr = new StreamReader(strNdatFilePath))
            {
                LoadLines = sr.ReadToEnd();
            }

            //[EOF]が最後の行だったら一行削除
            LoadLines = LoadLines.TrimEnd('\n');

            //改行で分離
            string[] LoadLine = LoadLines.Split('\n');

            //基準三角形の個数行の配列を作成
            NdatData = new int[LoadLine.Length][];

            for (int i = 0; i < LoadLine.Length; i++)
            {
                //\rの削除
                LoadLine[i] = LoadLine[i].Replace("\r", "");

                //三角形なので３列+重みづけで1列、計4列作成
                NdatData[i] = new int[4];

                //タブで区切って各々をジャグ配列に格納
                NdatData[i][0] = int.Parse(LoadLine[i].Split('\t')[0]);
                NdatData[i][1] = int.Parse(LoadLine[i].Split('\t')[1]);
                NdatData[i][2] = int.Parse(LoadLine[i].Split('\t')[2]);

                //重みづけが入っていれば代入。なければ30を代入
                try { NdatData[i][3] = int.Parse(LoadLine[i].Split('\t')[3]); }
                catch { NdatData[i][3] = 30; }
            }

            int MaxPointIndex = 0;
            foreach (int[] PointArray in NdatData)
            {
                TrianglePointData PointData = new TrianglePointData(PointArray[0], PointArray[1], PointArray[2], PointArray[3]);
                //頂点番号三角形を追加
                _TrianglePointDataList.Add(PointData);
                MaxPointIndex = Math.Max(Math.Max(Math.Max(MaxPointIndex, PointArray[0]), PointArray[1]), PointArray[2]);
            }

            //空レシピを追加
            for (int i = 0; i < MaxPointIndex +1 ; i++)
                _RecipeList.AddFeaturePoint();

            //すべてが新しい頂点番号三角形です。
            _NewTrianglePoint = _TrianglePointDataList.ToArray();
            #endregion
        }

        /// <summary>
        /// rdat形式(各特長点のレシピ)でファイルを保存します。
        /// </summary>
        /// <param name="SaveFileName">保存するファイル名</param>
        public virtual void SaveRdat(string SaveFileName)
        {
            #region rdatファイルの書き込み
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < _RecipeList.Count; i++)
                {
                    TrianglePointData TrianglePointData = _RecipeList[i].TrianglePointData;
                    RecipeValue RecipeValue = _RecipeList[i].RecipeValue;
                    string Recipe = TrianglePointData.TrianglePoint1 + "\t" + TrianglePointData.TrianglePoint2 + "\t" + TrianglePointData.TrianglePoint3 + "\t" + RecipeValue.Alpha + "\t" + RecipeValue.Beta + "\n";
                    sb.Append(Recipe);
                }

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(SaveFileName))
                {
                    sw.Write(sb);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            #endregion
        }

        /// <summary>
        /// ndat互換のデータを出力します。
        /// </summary>
        /// <returns>ndat互換データ</returns>
        public virtual string StringOut()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _TrianglePointDataList.Count; i++)
                sb.Append(_TrianglePointDataList[i].TrianglePoint1 + "\t" + _TrianglePointDataList[i].TrianglePoint2 + "\t" + _TrianglePointDataList[i].TrianglePoint3 + "\t" + _TrianglePointDataList[i].Weight + "\n");

            return sb.ToString();
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// Index番目のTrianglePointDataを取得、設定します
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>Index番目のTrianglePointData</returns>
        public TrianglePointData this[int Index]
        {
            get { return _TrianglePointDataList[Index]; }
            set { _TrianglePointDataList[Index] = value; }
        }

        /// <summary>
        /// 頂点番号の三角形リストを取得、設定します。
        /// </summary>
        public List<TrianglePointData> DataList
        {
            get { return _TrianglePointDataList; }
            set { _TrianglePointDataList = value; }
        }

        /// <summary>
        /// 三角形の個数を取得します。
        /// </summary>
        public int TriangleCount
        {
            get { return _TrianglePointDataList.Count; }
        }

        /// <summary>
        /// 新しく追加した頂点番号三角形を取得します。
        /// </summary>
        public TrianglePointData[] NewTrianglePointData
        {
            get { return _NewTrianglePoint; }
            protected set { _NewTrianglePoint = value; }
        }

        /// <summary>
        /// 特徴点の個数を取得します。
        /// </summary>
        public int FeaturePointCount
        {
            get { return _RecipeList.Count; }
        }
        #endregion

        /// <summary>
        /// 指定したインデックスのレシピを取得します
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>レシピ</returns>
        public FeaturePointRecipe GetRecipe(int Index)
        {
            return new FeaturePointRecipe(_RecipeList[Index]);
        }

        /// <summary>
        /// 指定したインデックスの三角形を削除します
        /// </summary>
        /// <param name="Index">削除する三角形のインデックス</param>
        public void RemoveTriangleAt(int Index)
        {
            //頂点番号三角形を削除
            _TrianglePointDataList.RemoveAt(Index);
        }

        /// <summary>
        /// 指定したレシピで特徴点を追加します
        /// </summary>
        /// <param name="Recipe">レシピ</param>
        public void AddFeaturePoint(FeaturePointRecipe Recipe)
        {
            //新しく追加する頂点番号三角形を取得し、リストに追加
            TrianglePointData[] NewTrianglePointData = this.GetNewTrianglePointData(Recipe);
            foreach (TrianglePointData NewTrianglePoint in NewTrianglePointData)
                _TrianglePointDataList.Add(NewTrianglePoint);

            //新しく追加した頂点番号三角形を設定
            _NewTrianglePoint = NewTrianglePointData;

            //レシピをリストに追加
            _RecipeList.AddFeaturePoint(Recipe);
        }

        /// <summary>
        /// レシピと指定した三角形から新しい三角形を取得するメソッド
        /// </summary>
        /// <param name="Recipe">レシピ</param>
        /// <returns>レシピと指定した三角形から新しい三角形</returns>
        protected virtual TrianglePointData[] GetNewTrianglePointData(FeaturePointRecipe Recipe)
        {
            int Index = FeaturePointCount;
            TrianglePointData[] ReturnTriangle;
            TrianglePointData TrianglePointData = Recipe.TrianglePointData;
            int Weight = 30;
            switch (Recipe.Kind)
            {
                case KindOfRecipe.Inner:
                    ReturnTriangle = new TrianglePointData[3];
                    ReturnTriangle[0] = new TrianglePointData(TrianglePointData.TrianglePoint1, TrianglePointData.TrianglePoint2, Index,Weight);
                    ReturnTriangle[1] = new TrianglePointData(TrianglePointData.TrianglePoint2, TrianglePointData.TrianglePoint3, Index, Weight);
                    ReturnTriangle[2] = new TrianglePointData(TrianglePointData.TrianglePoint3, TrianglePointData.TrianglePoint1,Index, Weight);
                    break;
                case KindOfRecipe.OnSide1_2:
                    ReturnTriangle = new TrianglePointData[2];
                    ReturnTriangle[0] = new TrianglePointData(TrianglePointData.TrianglePoint3, TrianglePointData.TrianglePoint1, Index, Weight);
                    ReturnTriangle[1] = new TrianglePointData(TrianglePointData.TrianglePoint2, TrianglePointData.TrianglePoint3, Index, Weight);
                    break;
                case KindOfRecipe.OnSide2_3:
                    ReturnTriangle = new TrianglePointData[2];
                    ReturnTriangle[0] = new TrianglePointData(TrianglePointData.TrianglePoint1, TrianglePointData.TrianglePoint2, Index, Weight);
                    ReturnTriangle[1] = new TrianglePointData(TrianglePointData.TrianglePoint3, TrianglePointData.TrianglePoint1, Index, Weight);
                    break;
                case KindOfRecipe.OnSide3_1:
                    ReturnTriangle = new TrianglePointData[2];
                    ReturnTriangle[0] = new TrianglePointData(TrianglePointData.TrianglePoint1, TrianglePointData.TrianglePoint2, Index, Weight);
                    ReturnTriangle[1] = new TrianglePointData(TrianglePointData.TrianglePoint2, TrianglePointData.TrianglePoint3, Index, Weight);
                    break;
                default:
                    return null;
            }
            return ReturnTriangle;
        }
    }
}
