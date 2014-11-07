using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASC
{
    /// <summary>
    /// 三次元形状及び色情報の点データ単体を表すクラス
    /// </summary>
    public class XYZandCOLORpoint:XYZpoint
    {
        protected int _R, _G, _B;
        //空コンストラクタ(x,y,z,R,G)=0
        public XYZandCOLORpoint()
        {
            _X = 0; _Y = 0; _Z = 0;
            _R = 0; _G = 0; _B = 0;
        }

        //座標、色を入れるコンストラクタ
        public XYZandCOLORpoint(double x, double y, double z, int r, int g, int b)
        {
            _X = x; _Y = y; _Z = z;
            if (r > 255)    _R = 255;
            else if (r < 0) _R = 0;
            else            _R = r; 
            if (g > 255)    _G = 255;
            else if (g < 0) _G = 0;
            else            _G = g;
            if (b > 255)    _B = 255;
            else if (b < 0) _B = 0;
            else            _B = b;
        }

        public XYZandCOLORpoint(XYZandCOLORpoint indata)
        {
            this._X = indata._X;
            this._Y = indata._Y;
            this._Z = indata._Z;
            this._R = indata._R;
            this._G = indata._G;
            this._B = indata._B;
        }
        /// <summary>
        /// 三次元形状点のR値get&set
        /// </summary>
        public int R
        {
            get { return _R; }
            set
            {
                if (value > 255)
                    _R = 255;
                else if (value < 0)
                    _R = 0;
                else
                    _R = value;
            }
        }

        /// <summary>
        /// 三次元形状点のB値get&set
        /// </summary>
        public int B
        {
            get { return _B; }
            set
            {
                if (value > 255)
                    _B = 255;
                else if (value < 0)
                    _B = 0;
                else
                    _B = value;
            }
        }

        /// <summary>
        /// 三次元形状点G値get&set
        /// </summary>
        public int G
        {
            get { return _G; }
            set
            {
                if (value > 255)
                    _G = 255;
                else if (value < 0)
                    _G = 0;
                else
                    _G = value;
            }
        }

        /// <summary>
        /// 三次元形状点データをascファイル用の形式にして返すメソッド
        /// </summary>
        /// <returns></returns>
        public string XYZandCOLORoutput()
        {
            return _X.ToString() + "\t" + _Y.ToString() + "\t" + _Z.ToString() + "\t" + _R.ToString() + "\t" + _G.ToString() + "\t" + _B.ToString() + "\n";
       
        }
        /// <summary>
        /// 三次元形状点データのRGB値のみをascファイル用の形式にして返すメソッド
        /// </summary>
        /// <returns></returns>
        public string COLORoutput()
        {
            return _R.ToString() + "\t" + _G.ToString() + "\t" + _B.ToString() + "\n";
        }
    }
}
