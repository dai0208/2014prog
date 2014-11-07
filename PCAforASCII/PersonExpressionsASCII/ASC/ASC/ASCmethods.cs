using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ASC
{
    /// <summary>
    /// ascデータの読み書き、加減算とかとにかくメソッド入れ
    /// </summary>
    public class ASCmethods
    {
        /// <summary>
        /// ReadXYZfile 読むデータがRGBを持っていてもXYZのみを読む
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
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

                XYZDATA[i].X = Double.Parse(sLines[i].Split('\t')[0]);
                XYZDATA[i].Y = Double.Parse(sLines[i].Split('\t')[1]);
                XYZDATA[i].Z = Double.Parse(sLines[i].Split('\t')[2]);
            }
            return XYZDATA;
        }
        /// <summary>
        /// ReadXYZRGBfile
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
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

                DATA[i].X = Double.Parse(sLines[i].Split('\t')[0]);
                DATA[i].Y = Double.Parse(sLines[i].Split('\t')[1]);
                DATA[i].Z = Double.Parse(sLines[i].Split('\t')[2]);
                DATA[i].R = int.Parse(sLines[i].Split('\t')[3]);
                DATA[i].G = int.Parse(sLines[i].Split('\t')[4]);
                DATA[i].B = int.Parse(sLines[i].Split('\t')[5]);
            }
            return DATA;
        }

        /// <summary>
        /// SaveXYZfile
        /// </summary>
        /// <param name="savedata">セーブしたいもの</param>
        /// <returns>セーブの成否</returns>
        public bool SaveXYZDATAFILE(XYZDATAFILE savedata)
        {
            if (savedata.filename == null || savedata.XYZDATA == null)
                return false;

            System.Text.StringBuilder savestring = new StringBuilder();
            for (int i = 0; i < savedata.XYZDATA.Length; i++)
                savestring.Append(savedata.XYZDATA[i].XYZpointoutput());
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("SHIFT_JIS");
            StreamWriter savefile = new StreamWriter(savedata.filename, false, enc);
            savefile.Write(savestring.ToString());
            savefile.Close();
            return true;
        }
        /// <summary>
        /// SaveXYZRGBfile
        /// </summary>
        /// <param name="savedata">セーブしたいもの</param>
        /// <returns>セーブの成否</returns>
        public bool SaveXYZRGBDATAFILE(XYZandCOLORDATAFILE savedata)
        {
            if (savedata.filename == null || savedata.XYZandCOLORDATA == null)
                return false;

            System.Text.StringBuilder savestring = new StringBuilder();
            for (int i = 0; i < savedata.XYZandCOLORDATA.Length; i++)
                savestring.Append(savedata.XYZandCOLORDATA[i].XYZandCOLORoutput());
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("SHIFT_JIS");
            StreamWriter savefile = new StreamWriter(savedata.filename, false, enc);
            savefile.Write(savestring.ToString());
            savefile.Close();
            return true;
        }

        /// <summary>
        /// XYZデータの引き算メソッド。データの大きさが等しくなくても無理やり計算します。
        /// </summary>
        /// <param name="data1">引かれるもの</param>
        /// <param name="data2">引くもの</param>
        /// <returns>引き算結果</returns>
        public XYZpoint[] XYZSUB(XYZpoint[] data1, XYZpoint[] data2)
        {
            XYZpoint[] outdata = new XYZpoint[data1.Length];
            for (int i = 0; i < data1.Length; i++)
            {
                outdata[i] = new XYZpoint();
                outdata[i].X = data1[i].X - data2[i].X;
                outdata[i].Y = data1[i].Y - data2[i].Y;
                outdata[i].Z = data1[i].Z - data2[i].Z;
            }
            return outdata;
        }

        /// <summary>
        /// XYZandCOLORデータの引き算メソッド。データの大きさが等しくなくても無理やり計算します。
        /// </summary>
        /// <param name="data1">引かれるもの</param>
        /// <param name="data2">引くもの</param>
        /// <returns>引き算結果</returns>
        public XYZandCOLORpoint[] XYZandCOLORSUB(XYZandCOLORpoint[] data1, XYZandCOLORpoint[] data2)
        {
            XYZandCOLORpoint[] outdata = new XYZandCOLORpoint[data1.Length];
            for (int i = 0; i < data1.Length; i++)
            {
                outdata[i] = new XYZandCOLORpoint();
                outdata[i].X = data1[i].X - data2[i].X;
                outdata[i].Y = data1[i].Y - data2[i].Y;
                outdata[i].Z = data1[i].Z - data2[i].Z;
                outdata[i].R = data1[i].R - data2[i].R;
                outdata[i].G = data1[i].G - data2[i].G;
                outdata[i].B = data1[i].B - data2[i].B;
            }
            return outdata;
        }

        /// <summary>
        /// XYZデータの足し算メソッド。データの大きさが等しくなくても無理やり計算します。
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        public XYZpoint[] XYZADD(XYZpoint[] data1, XYZpoint[] data2)
        {
            XYZpoint[] outdata = new XYZpoint[data1.Length];
            for (int i = 0; i < data1.Length; i++)
            {
                outdata[i] = new XYZpoint();
                outdata[i].X = data1[i].X + data2[i].X;
                outdata[i].Y = data1[i].Y + data2[i].Y;
                outdata[i].Z = data1[i].Z + data2[i].Z;
            }
            return outdata;
        }

        /// <summary>
        /// XYZandCOLORデータの足し算メソッド。データの大きさが等しくなくても無理やり計算します。
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        public XYZandCOLORpoint[] XYZandCOLORADD(XYZandCOLORpoint[] data1, XYZandCOLORpoint[] data2)
        {
            XYZandCOLORpoint[] outdata = new XYZandCOLORpoint[data1.Length];
            for (int i = 0; i < data1.Length; i++)
            {
                outdata[i] = new XYZandCOLORpoint();
                outdata[i].X = data1[i].X + data2[i].X;
                outdata[i].Y = data1[i].Y + data2[i].Y;
                outdata[i].Z = data1[i].Z + data2[i].Z;
                outdata[i].R = data1[i].R + data2[i].R;
                outdata[i].G = data1[i].G + data2[i].G;
                outdata[i].B = data1[i].B + data2[i].B;
            }
            return outdata;
        }
        /// <summary>
        /// XYZデータの平均を求めるメソッド。XYZDATAFILE型の配列を入れるとXYZpoint型の配列で平均を返してくれる
        /// </summary>
        /// <param name="data">XYZDATAFILE型配列</param>
        /// <returns>平均をXYZpoint型配列で</returns>
        public XYZpoint[] XYZAVE(XYZDATAFILE[] data)
        {
            XYZpoint[] outdata = new XYZpoint[data[0].XYZDATA.Length];
            double[] tempX=new double[data[0].XYZDATA.Length], tempY=new double[data[0].XYZDATA.Length], tempZ=new double[data[0].XYZDATA.Length];
            int datacount=0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].XYZDATA.Length; j++)
                {
                    tempX[j] += data[i].XYZDATA[j].X;
                    tempY[j] += data[i].XYZDATA[j].Y;
                    tempZ[j] += data[i].XYZDATA[j].Z;
                }
                datacount++;
            }
            for (int i = 0; i < outdata.Length; i++)
            {
                tempX[i] /= datacount;
                tempY[i] /= datacount;
                tempZ[i] /= datacount;
                outdata[i] = new XYZpoint(tempX[i], tempY[i], tempZ[i]);
            }
            return outdata;
        }

        /// <summary>
        /// XYZandCOLORデータの平均を求めるメソッド。XYZandCOLORDATAFILE型の配列を入れるとXYZandCOLORpoint型の配列で平均を返してくれる
        /// </summary>
        /// <param name="data">XYZandCOLORDATAFILE型配列</param>
        /// <returns>平均をXYZandCOLOR型配列で</returns>
        public XYZandCOLORpoint[] XYZandCOLORAVE(XYZandCOLORDATAFILE[] data)
        {
            XYZandCOLORpoint[] outdata = new XYZandCOLORpoint[data[0].XYZandCOLORDATA.Length];
            double[] tempX = new double[data[0].XYZandCOLORDATA.Length], tempY = new double[data[0].XYZandCOLORDATA.Length], tempZ = new double[data[0].XYZandCOLORDATA.Length];
            int datacount = 0;
            double[] tempR = new double[data[0].XYZandCOLORDATA.Length], tempG = new double[data[0].XYZandCOLORDATA.Length], tempB = new double[data[0].XYZandCOLORDATA.Length];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].XYZandCOLORDATA.Length; j++)
                {
                    tempX[j] += data[i].XYZandCOLORDATA[j].X;
                    tempY[j] += data[i].XYZandCOLORDATA[j].Y;
                    tempZ[j] += data[i].XYZandCOLORDATA[j].Z;
                    tempR[j] += data[i].XYZandCOLORDATA[j].R;
                    tempG[j] += data[i].XYZandCOLORDATA[j].G;
                    tempB[j] += data[i].XYZandCOLORDATA[j].B;
                }
                datacount++;
            }
            for (int i = 0; i < outdata.Length; i++)
            {
                tempX[i] /= datacount;
                tempY[i] /= datacount;
                tempZ[i] /= datacount;
                tempR[i] /= datacount;
                tempG[i] /= datacount;
                tempB[i] /= datacount;
                outdata[i] = new XYZandCOLORpoint(tempX[i], tempY[i], tempZ[i], (int)tempR[i], (int)tempG[i], (int)tempB[i]);
            }
            return outdata;
        }
    }
}
