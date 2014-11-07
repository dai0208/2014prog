using System;
using System.IO;
using System.Collections;

namespace PointFormat
{
	
	// <summary>
	/// ポイントデータを書き込むためのクラスです。
	/// </summary>
	public class cSavePoint : cSaveFile
	{
		private cPoint[] ipdPointData;

		/// <summary>
		/// ファイル名を指定してデータを書き込むためのメソッドです。
		/// </summary>
		/// <param name="strSaveFileName">書き込み先のファイル名</param>
		/// <param name="ipdData">書き込むポイントデータ配列</param>
		public cSavePoint(string strSaveFileName, cPoint[] ipdData)
		{
			base.strOpenFileName = strSaveFileName;
			this.ipdPointData = ipdData;
			base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
			base.fiOpenFile = new FileInfo(base.strOpenFileName);  
		}

        public cSavePoint()
        {
        }

        public void vSetSavePoint(string strSaveFileName, cPoint[] ipdData)
        {
            base.strOpenFileName = strSaveFileName;
            this.ipdPointData = ipdData;
            base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
            base.fiOpenFile = new FileInfo(base.strOpenFileName);
        }

        public void vSetSavePoint(string strSaveFileName, ArrayList alData)
        {
            base.strOpenFileName = strSaveFileName;
            this.ipdPointData = new cPoint[alData.Count];
            for (int i = 0; i < alData.Count; i++)
                ipdPointData[i] = new cPoint((cPoint)alData[i]);
            base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
            base.fiOpenFile = new FileInfo(base.strOpenFileName);
        }


		/// <summary>
		/// ファイルにデータを実際に書き込むためのメソッドです。
		/// </summary>
		/// <returns>trueなら書き込み成功。falseなら書き込み失敗です。</returns>
		protected override bool bWriteFile()
		{
			//保存する物体があるかどうか。
			if(this.ipdPointData == null)
				return false;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < this.ipdPointData.Length; i++)
                sb.Append(this.ipdPointData[i].strOutput());

            try
            {
                swSaveFile.Write(sb.ToString());
                /*
                for(int i = 0; i < this.ipdPointData.Length ; i++)
                {
                    swSaveFile.WriteLine(this.ipdPointData[i].strOutput());

                    //進行状況を表すプログレスバーの値を増加。ここの意味がわからなくても影響ないです。
                    if(pgbMain != null)
                        if(this.ipdPointData.Length >= 100)
                            if((i % ((int)(this.ipdPointData.Length / 100) * 5)) == 1)
                                base.pgbMain.PerformStep();
                        else
                            base.pgbMain.Value = 100;
                }
                */
            }
            catch { return false; }
			finally
			{
				if(swSaveFile != null)
					swSaveFile.Close();
			}

			return true;
		}
	}
}
