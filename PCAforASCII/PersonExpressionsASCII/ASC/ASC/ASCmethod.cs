using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ASC
{
    /// <summary>
    /// .ascファイルの読み書き用のメソッドが入ったクラス
    /// </summary>
    public class ASCmethods
    {
        //ReadXYZfile
        public XYZpoint[] LoadXYZDATAFILE(string filename)
        {
            XYZpoint[] XYZDATA;
            string sLine = "";
            string[] sLines;
            int line_count;

            StreamReader sr = new StreamReader(filename);
            sLine = sr.ReadToEnd();
            sr.Close();
            sLine = sLine.TrimEnd('\n');
            sLine = sLine.Replace(" ", ",");
            sLine = sLine.Replace("\r", "");
            sLines = sLine.Split('\n');
            line_count = sLines.Length;
            XYZDATA = new XYZpoint[line_count];
            for (int i = 0; i < XYZDATA.Length; i++)
            {
                XYZDATA[i] = new XYZpoint();
                sLines[i] = sLines[i].Replace(",", "\t");
                sLines[i] = sLines[i].Replace(" ", "\t");

                XYZDATA[i].Xgos = Double.Parse(sLines[i].Split('\t')[0]);
                XYZDATA[i].Ygos = Double.Parse(sLines[i].Split('\t')[1]);
                XYZDATA[i].Zgos = Double.Parse(sLines[i].Split('\t')[2]);
            }
            return XYZDATA;
        }
        //ReadXYZRGBfile
        public XYZandCOLORpoint[] LoadXYZandCOLORDATAFILE(string filename)
        {
            XYZandCOLORpoint[] DATA;
            string sLine = "";
            string[] sLines;
            int line_count;

            StreamReader sr = new StreamReader(filename);
            sLine = sr.ReadToEnd();
            sr.Close();
            sLine = sLine.TrimEnd('\n');
            sLine = sLine.Replace(" ", ",");
            sLine = sLine.Replace("\r", "");
            sLines = sLine.Split('\n');
            line_count = sLines.Length;
            DATA = new XYZandCOLORpoint[line_count];
            for (int i = 0; i < DATA.Length; i++)
            {
                DATA[i] = new XYZandCOLORpoint();
                sLines[i] = sLines[i].Replace(",", "\t");
                sLines[i] = sLines[i].Replace(" ", "\t");

                DATA[i].Xgos = Double.Parse(sLines[i].Split('\t')[0]);
                DATA[i].Ygos = Double.Parse(sLines[i].Split('\t')[1]);
                DATA[i].Zgos = Double.Parse(sLines[i].Split('\t')[2]);
                DATA[i].Red = int.Parse(sLines[i].Split('\t')[3]);
                DATA[i].Green = int.Parse(sLines[i].Split('\t')[4]);
                DATA[i].Brue = int.Parse(sLines[i].Split('\t')[5]);
            }
            return DATA;
        }

        //SaveXYZfile
        public bool SaveXYZDATAFILE(XYZDATAFILE savedata)
        {
            if(savedata.filenamegos==null||savedata.XYZDATAgos==null)
                return false;

            System.Text.StringBuilder savestring = new StringBuilder();
            for (int i = 0; i < savedata.XYZDATAgos.Length; i++)
                savestring.Append(savedata.XYZDATAgos[i].XYZpointoutput());
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("SHIFT_JIS");
            StreamWriter savefile = new StreamWriter(savedata.filenamegos,false,enc);
            savefile.Write(savestring.ToString());
            savefile.Close();
            return true;
        }
        //SaveXYZRGBfile
        public bool SaveXYZRGBDATAFILE(XYZandCOLORDATAFILE savedata)
        {
            if (savedata.filenamegos == null || savedata.DATAgos== null)
                return false;

            System.Text.StringBuilder savestring = new StringBuilder();
            for (int i = 0; i < savedata.DATAgos.Length; i++)
                savestring.Append(savedata.DATAgos[i].XYZandCOLORoutput());
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("SHIFT_JIS");
            StreamWriter savefile = new StreamWriter(savedata.filenamegos, false, enc);
            savefile.Write(savestring.ToString());
            savefile.Close();
            return true;
        }
    }
}
