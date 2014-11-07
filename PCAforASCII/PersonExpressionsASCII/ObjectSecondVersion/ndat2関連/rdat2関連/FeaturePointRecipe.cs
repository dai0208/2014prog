using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;
using MatrixVector;

namespace ObjectSecondVersion
{
    /// <summary>
    /// レシピの種類の列挙型
    /// </summary>
    [Serializable]
    public enum KindOfRecipe
    {
        #region 種類
        /// <summary>
        /// 内部
        /// </summary>
        Inner,

        /// <summary>
        ///辺１２上 
        /// </summary>
        OnSide1_2,

        /// <summary>
        /// 辺２３上
        /// </summary>
        OnSide2_3,

        /// <summary>
        /// 辺３１上
        /// </summary>
        OnSide3_1,

        /// <summary>
        /// その他
        /// </summary>
        None = 99,
        #endregion
    }

    /// <summary>
    /// 特徴点のレシピを保持、作成するクラス
    /// </summary>
    [Serializable]
    public class FeaturePointRecipe
    {
        /// <summary>
        /// レシピ変数
        /// </summary>
        protected RecipeValue _RecipeValue;

        /// <summary>
        /// 三角形の頂点番号
        /// </summary>
        protected TrianglePointData _TrianglePointData;

        #region コンストラクタ
        public FeaturePointRecipe()
        {
            _TrianglePointData = new TrianglePointData(-1, -1, -1, 30);
            _RecipeValue = new RecipeValue(0, 0);
        }

        public FeaturePointRecipe(RecipeValue RecipeValue, TrianglePointData TrianglePointData)
        {
            _TrianglePointData = TrianglePointData;
            _RecipeValue = new RecipeValue(RecipeValue);
        }

        public FeaturePointRecipe(FeaturePointRecipe FeaturePointRecipe)
        {
            this._TrianglePointData = FeaturePointRecipe._TrianglePointData;
            _RecipeValue = FeaturePointRecipe.RecipeValue;
        }


        #endregion

        /// <summary>
        /// 三角形を指定してレシピから特徴点の座標を取得します。
        /// </summary>
        /// <param name="TriangleData">実三角形座標</param>
        /// <returns>特徴点</returns>
        public XYZPoint GetPoint(ShapeTriangle ShapeTriangleData)
        {
            Vector Vector;
            Vector = ShapeTriangleData.Vector12 * _RecipeValue.Alpha;
            Vector += ShapeTriangleData.Vector13 * _RecipeValue.Beta;

            Vector[0] += ShapeTriangleData.Point1.X;
            Vector[1] += ShapeTriangleData.Point1.Y;
            Vector[2] += ShapeTriangleData.Point1.Z;

            return new XYZPoint(Vector[0], Vector[1], Vector[2]);
        }

        /// <summary>
        /// 現在のレシピを出力
        /// </summary>
        /// <returns>現在のレシピ</returns>
        public override string ToString()
        {
            return _TrianglePointData.TrianglePoint1 + "," + _TrianglePointData.TrianglePoint2 + "," + _TrianglePointData.TrianglePoint3 + "," + _RecipeValue.Alpha + "," + _RecipeValue.Beta + "\n";
        }

        #region プロパティ
        /// <summary>
        /// 三角形の頂点番号を取得、設定します。
        /// </summary>
        public virtual TrianglePointData TrianglePointData
        {
            get { return _TrianglePointData; }
            set { _TrianglePointData = value; }
        }

        /// <summary>
        /// レシピ変数を取得、設定します。
        /// </summary>
        public RecipeValue RecipeValue
        {
            get { return _RecipeValue; }
            set { _RecipeValue = value; }
        }

        /// <summary>
        /// レシピの種類を取得します。
        /// </summary>
        public virtual KindOfRecipe Kind { get { return _RecipeValue.Kind; } }
        #endregion
    }
}
