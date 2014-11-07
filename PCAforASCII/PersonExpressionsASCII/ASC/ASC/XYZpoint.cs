using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASC
{
    /// <summary>
    /// 三次元形状の点データ単体を表すクラス
    /// </summary>
    public class XYZpoint
    {
        protected double _X, _Y, _Z;

        //空コンストラクタ(x,y,z)=0
        public XYZpoint()
        {
            _X = 0; _Y = 0; _Z = 0;
        }

        //座標を入れるコンストラクタ
        public XYZpoint(double x, double y, double z)
        {
            _X = x; _Y = y; _Z = z;
        }

        /// <summary>
        /// 三次元形状の点データをascファイル用の形式にして返すメソッド
        /// </summary>
        public string XYZpointoutput()
        {
            return _X.ToString() + "\t" + _Y.ToString() + "\t" + _Z.ToString() + "\n";
        }

        /// <summary>
        /// 三次元形状点のX座標のget&set
        /// </summary>
        public double X
        {
            get { return _X; }
            set
            {
                _X = value;
            }
        }

        /// <summary>
        /// 三次元形状点のY座標のget&set
        /// </summary>
        public double Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
            }
        }

        /// <summary>
        /// 三次元形状点のZ座標のget&set
        /// </summary>
        public double Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
            }
        }
    }
}
