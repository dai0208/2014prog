using System;
using System.IO;

namespace PointFormat
{
	/// <summary>
	/// ファイル入出力を管理する一番基本となるクラスです。このクラスは必ず派生させて使わなければなりません。
	/// </summary>
	abstract public class cOpenFile
	{
		protected string strOpenFileName;
		protected DirectoryInfo diOpenFile;
		protected FileInfo fiOpenFile;
		protected System.Windows.Forms.ToolStripProgressBar pgbMain;

		/// <summary>
		/// 特に何もしないコンストラクタです。
		/// </summary>
		public cOpenFile()
		{
		}

		/// <summary>
		/// ファイルの入出力状況を示すためのプログレスバーの値を設定、取得するためのプロパティです。できる限り指定してください。
		/// </summary>
        public System.Windows.Forms.ToolStripProgressBar pgbProgressBar
		{
			set
			{
				pgbMain = value;	
			}
			get
			{
				return pgbMain;
			}
		}
	}
}
