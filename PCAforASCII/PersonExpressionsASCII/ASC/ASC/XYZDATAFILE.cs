using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ASC
{
    /// <summary>
    /// 三次元形状点を持つデータファイルを表すクラス
    /// </summary>
    public class XYZDATAFILE
    {
        protected string _filename;
        protected XYZpoint[] _XYZDATA;

        //空コンストラクタ
        public XYZDATAFILE()
        {
        }

        //ファイル名を受け取り三次元形状データを持つコンストラクタ
        public XYZDATAFILE(string file)
        {
            _filename = file;
            ASCmethods asc = new ASCmethods();
            _XYZDATA=asc.LoadXYZDATAFILE(_filename);
        }
        //三次元形状データを持つコンストラクタ
        public XYZDATAFILE(XYZpoint[] inDATA)
        {
            this._XYZDATA = inDATA;
        }
        //コピー用コンストラクタ
        public XYZDATAFILE(XYZDATAFILE inDATA)
        {
            this._filename = inDATA._filename;
            this._XYZDATA = inDATA._XYZDATA;
        }

        //filenameのget&set
        public string filename
        {
            get{return _filename;}
            set
            {
                _filename = value;
            }
        }
        //XYZDATAのget&set
        public XYZpoint[] XYZDATA
        {
            get { return _XYZDATA; }
            set
            {
                _XYZDATA = value;
            }
        }
 
    }
}
