using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using DeSerialize = ObjectSecondVersion.FeaturePointRecipeList;

namespace ObjectSecondVersion
{
    /// <summary>
    /// レシピリスト(rdat)を保持するクラス
    /// </summary>
    [Serializable]
    public class FeaturePointRecipeList
    {
        /// <summary>
        /// 特徴点のレシピリスト
        /// </summary>
        List<FeaturePointRecipe> _FeaturePointRecipeList = new List<FeaturePointRecipe>();

        #region コンストラクタ
        public FeaturePointRecipeList() { }

        public FeaturePointRecipeList(FeaturePointRecipeList FeaturePointRecipeList)
        {
            foreach (FeaturePointRecipe FeaturePointRecipe in FeaturePointRecipeList._FeaturePointRecipeList)
                _FeaturePointRecipeList.Add(FeaturePointRecipe);
        }
			 
        
        #endregion

        /// <summary>
        /// レシピをリストに追加します。
        /// </summary>
        /// <param name="Recipe">追加するレシピ</param>
        public void AddFeaturePoint(FeaturePointRecipe Recipe)
        {
            _FeaturePointRecipeList.Add(Recipe);
        }

        /// <summary>
        /// 空のレシピを追加します。
        /// </summary>
        /// <param name="Recipe">追加するレシピ</param>
        public void AddFeaturePoint()
        {
            _FeaturePointRecipeList.Add(new FeaturePointRecipe());
        }

        /// <summary>
        /// 指定したインデックスの特徴点を削除します
        /// </summary>
        /// <param name="Index">インデックス</param>
        public void FeaturePointRemove(int Index)
        {
            //TODO 特徴点を削除する処理を実装する？
            throw new NotImplementedException();
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

        #region プロパティ
        /// <summary>
        /// インデックス番目のレシピを取得、設定します。
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>レシピ</returns>
        public FeaturePointRecipe this[int Index]
        {
            get { return _FeaturePointRecipeList[Index]; }
            set { _FeaturePointRecipeList[Index] = value; }
        }

        /// <summary>
        /// 特徴点リストに登録されている特徴点の個数を取得します
        /// </summary>
        public int Count
        {
            get { return _FeaturePointRecipeList.Count; }
        }
        #endregion
    }
}
