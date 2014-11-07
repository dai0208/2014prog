using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASC
{
    /// <summary>
    /// 三次元形状及び色情報を表すクラス
    /// </summary>
    public class XYZandCOLORDATAFILE
    {
        protected string _filename;
        protected XYZandCOLORpoint[] _XYZandCOLORDATA;

        //コンストラクタ
        public XYZandCOLORDATAFILE()
        {
        }
        public XYZandCOLORDATAFILE(string file)
        {
            _filename = file;
            ASCmethods asc = new ASCmethods();
            _XYZandCOLORDATA=asc.LoadXYZandCOLORDATAFILE(file);
        }
        public XYZandCOLORDATAFILE(XYZandCOLORDATAFILE indata)
        {
            this._filename = indata._filename;
            this._XYZandCOLORDATA = indata._XYZandCOLORDATA;
        }
        public XYZandCOLORDATAFILE(XYZandCOLORpoint[] indata)
        {
            this._XYZandCOLORDATA = indata;
        }


        //filenameのget&set
        public string filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
            }
        }
        //XYZandCOLORDATAのget&set
        public XYZandCOLORpoint[] XYZandCOLORDATA
        {
            get { return _XYZandCOLORDATA; }
            set
            {
                _XYZandCOLORDATA = value;
            }
        }
    }
}
