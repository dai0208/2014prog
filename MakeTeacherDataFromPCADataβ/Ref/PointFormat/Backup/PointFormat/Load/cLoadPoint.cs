using System;
using System.IO;

namespace PointFormat
{
	/// <summary>
	///  ファイルからポイントデータを読み出すためのクラスです。
	///  基底クラスはcLoadFileです。
	/// </summary>
	public class cLoadPoint : cLoadFile
	{
		private cPoint[] ipdPointData;
		private string strLoadLines = "";
		private string[] strLoadLine;

		/// <summary>
		///読み出したポイントデータを取得するためのプロパティです。
		/// </summary>
        public cPoint[] ipPoint
        {
            get { return this.ipdPointData; }
        }

		/// <summary>
		/// 読み出したポイントデータ配列の要素数を取得するためのプロパティです。
		/// </summary>
        public int iPointNo
        {
            get { return this.ipdPointData.Length; }
        }

		/// <summary>
		/// コンストラクタです。読み出すファイル名を指定します。読み出す時はbReadFileメソッドを実行してください。
		/// </summary>
		/// <param name="strLoadFileName">読み出すファイル名</param>
		public cLoadPoint(string strLoadFileName)
		{
            try
            {
                base.strOpenFileName = strLoadFileName;
                base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
                base.fiOpenFile = new FileInfo(base.strOpenFileName);
            }
            catch
            {
                base.strFileName = "";
                base.diOpenFile = null;
                base.fiOpenFile = null;
            }
		}

		/// <summary>
		///このメソッドを実行するとファイルからデータを読み出します。
		/// </summary>
		/// <returns>trueなら読み出し成功。falseなら読み出し失敗です。</returns>
        protected override bool bReadFile()
        {
            try { strLoadLines = srLoadFile.ReadToEnd(); }
            catch { return false; }
            finally { srLoadFile.Close(); }

            //最後の不要な改行を削除
            strLoadLines = strLoadLines.TrimEnd('\n');
            strLoadLines = strLoadLines.Replace(" ", ",");
            strLoadLines = strLoadLines.Replace("\r", "");

            //改行ごとにデータをわける
            strLoadLine = strLoadLines.Split('\n');
            this.ipdPointData = new cPoint[strLoadLine.Length];

            if (base.pgbMain != null)
                base.pgbMain.Value = 0;

            for (int i = 0; i < strLoadLine.Length; i++)
            {
                double dX, dY, dZ;
                int iR, iG, iB;

                strLoadLine[i] = strLoadLine[i].Replace(",", "\t");
                strLoadLine[i] = strLoadLine[i].Replace(" ", "\t");

                //X,Y,Zデータを分離。
                //データはタブ区切りになっているのでタブで分ける。
                dX = Double.Parse(strLoadLine[i].Split('\t')[0]);
                dY = Double.Parse(strLoadLine[i].Split('\t')[1]);
                dZ = Double.Parse(strLoadLine[i].Split('\t')[2]);

                //同様にR,G,Bのデータを分離。
                if (strLoadLine[i].Split('\t').Length > 3)
                {
                    iR = Int32.Parse(strLoadLine[i].Split('\t')[3]);
                    iG = Int32.Parse(strLoadLine[i].Split('\t')[4]);
                    iB = Int32.Parse(strLoadLine[i].Split('\t')[5]);
                }
                else
                {
                    iR = 0;
                    iG = 0;
                    iB = 0;
                }
                //データを保持するクラスの配列に追加。
                this.ipdPointData[i] = new cPoint(dX, dY, dZ, iR, iG, iB);

                //進行状況を表すプログレスバーの値を増加。ここの意味がわからなくても影響ないです。
                if (pgbMain != null)
                {
                    if (strLoadLine.Length >= 100)
                        if ((i % ((int)(strLoadLine.Length / 100) * 5)) == 1)
                            base.pgbMain.PerformStep();
                        else
                            base.pgbMain.Value = base.pgbMain.Maximum;
                }
            }
            if (base.pgbMain != null)
                base.pgbMain.Value = 0;
            return true;
        }
    }
}
