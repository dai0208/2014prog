using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectSecondVersion
{
    [Serializable]
    public class TrianglePointData
    {
        /// <summary>
        /// 三角形の1番目の頂点番号
        /// </summary>
        private int _TrianglePoint1;

        /// <summary>
        /// 三角形の2番目の頂点番号
        /// </summary>
        private int _TrianglePoint2;

        /// <summary>
        /// 三角形の3番目の頂点番号
        /// </summary>
        private int _TrianglePoint3;

        /// <summary>
        /// 三角形の重みづけ
        /// </summary>
        private int _Weight;

        #region コンストラクタ
        public TrianglePointData(int Point1, int Point2, int Point3, int Weight)
        {
            _TrianglePoint1 = Point1;
            _TrianglePoint2 = Point2;
            _TrianglePoint3 = Point3;
            _Weight = Weight;
        }

        public TrianglePointData(TrianglePointData TrianglePointData)
        {
            _TrianglePoint1 = TrianglePointData._TrianglePoint1;
            _TrianglePoint2 = TrianglePointData._TrianglePoint2;
            _TrianglePoint3 = TrianglePointData._TrianglePoint3;
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 三角形の1番目の頂点番号を取得、設定します
        /// </summary>
        public int TrianglePoint1
        {
            get { return _TrianglePoint1; }
            set { _TrianglePoint1 = value; }
        }

        /// <summary>
        /// 三角形の2番目の頂点番号を取得、設定します
        /// </summary>
        public int TrianglePoint2
        {
            get { return _TrianglePoint2; }
            set { _TrianglePoint2 = value; }
        }

        /// <summary>
        /// 三角形の3番目の頂点番号を取得、設定します
        /// </summary>
        public int TrianglePoint3
        {
            get { return _TrianglePoint3; }
            set { _TrianglePoint3 = value; }
        }

        /// <summary>
        /// 重みを設定、取得します。
        /// </summary>
        public int Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }

        /// <summary>
        /// インデックス番目の三角形特徴点番号を取得、設定します。
        /// </summary>
        /// <param name="Index"><インデックス/param>
        /// <returns>三角形特徴点番号</returns>
        public int this[int Index]
        {
            get {
                switch (Index)
                {
                    case 0:
                        return _TrianglePoint1;
                    case 1:
                        return _TrianglePoint2;
                    case 2:
                        return _TrianglePoint3;
                    default:
                        throw new ApplicationException();
                }
            }
            
            set {
                switch (Index)
                {
                    case 0:
                        _TrianglePoint1 = value;
                        break;
                    case 1:
                        _TrianglePoint2 = value;
                        break;
                    case 2:
                        _TrianglePoint3 = value;
                        break;
                    default:
                        throw new ApplicationException();
                }
            }
        }
        #endregion

        /// <summary>
        /// 三角形の頂点インデックスを取得します。
        /// </summary>
        /// <returns>頂点インデックス</returns>
        public int[] GetPointIndex()
        {
            int[] PointIndex = new int[3];
            PointIndex[0] = _TrianglePoint1;
            PointIndex[1] = _TrianglePoint2;
            PointIndex[2] = _TrianglePoint3;

            return PointIndex;
        }

        /// <summary>
        /// 現在の三角形情報を出力
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _TrianglePoint1 + "," + _TrianglePoint2 + "," + _TrianglePoint3 + "," + _Weight + "\n";
        }

        public override bool Equals(object obj)
        {
            TrianglePointData TPD = (TrianglePointData)obj;
            return TPD._TrianglePoint1 == this.TrianglePoint1 & TPD.TrianglePoint2 == this.TrianglePoint2 & TPD.TrianglePoint3 == this.TrianglePoint3;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
