using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectSecondVersion
{
    /// <summary>
    /// レシピ変数クラス
    /// </summary>
    [Serializable]
    public class RecipeValue
    {
        /// <summary>
        /// レシピ変数アルファ
        /// </summary>
        double _Alpha;

        /// <summary>
        /// レシピ変数ベータ
        /// </summary>
        double _Beta;

        #region コンストラクタ
        public RecipeValue(double AlphaValue, double BetaValue)
        {
            this.Alpha = AlphaValue;
            this.Beta = BetaValue;
        }

        public RecipeValue(RecipeValue RecipeValue)
        {
            this.Alpha = RecipeValue.Alpha;
            this.Beta = RecipeValue.Beta;
        }
        #endregion

        /// <summary>
        /// Vector12方向の内分比
        /// </summary>
        public double Alpha
        {
            get { return _Alpha; }
            set { _Alpha = Math.Max(Math.Min(1d, value), 0); }
        }

        /// <summary>
        /// Vector13方向の内分比
        /// </summary>
        public double Beta
        {
            get { return _Beta; }
            set { _Beta = Math.Max(Math.Min(1d, value), 0); }
        }

        /// <summary>
        /// レシピの種類を取得します。
        /// </summary>
        public virtual KindOfRecipe Kind
        {
            get
            {
                if (this.Alpha == 0 & this.Beta == 0)
                    return KindOfRecipe.None;
                else if (this.Alpha == 0)
                    return KindOfRecipe.OnSide1_2;
                else if (this.Beta == 0)
                    return KindOfRecipe.OnSide3_1;
                else if (this.Alpha + this.Beta == 1)
                    return KindOfRecipe.OnSide2_3;
                else
                    return KindOfRecipe.Inner;
            }
        }
    }
}
