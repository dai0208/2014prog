using System;
using System.IO;
using System.Windows.Forms;

namespace PointFormat
{
	/// <summary>
	/// rapidFormのデータを読み込むためのクラスです。cOpenFileが基底クラスです。
	/// </summary>
	abstract public class cLoadFile : cOpenFile
	{
		protected StreamReader srLoadFile;

		public string strFileName
		{
			get
			{
				return base.strOpenFileName;
			}
			set
			{
				base.strOpenFileName = value;
			}
		}
	

		/// <summary>
		/// コンストラクタですが、特に何もしません。
		/// </summary>
		protected cLoadFile()
		{
		}


		/// <summary>
		/// ファイルの内容を読み込むためのメソッドです。外部からはこのメソッドを使ってファイルを読み込みます。
		/// ファイルの存在チェックなども行っているので、存在しないファイル名を指定されていても大丈夫です。
		/// </summary>
		/// <returns>trueなら読み込み成功。falseなら読み込み失敗です。</returns>
		public bool bReadData()
		{
            if (base.fiOpenFile == null)
                return false;

			//ファイルが存在するかどうかのチェック。
            if (base.fiOpenFile.Exists != true)
            {
                MessageBox.Show("読み込むファイルが存在しません。\nファイル名をもう一度指定してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

			//読み込みファイルのストリーム設定。
			srLoadFile = new StreamReader(base.strOpenFileName);

			return bReadFile();
		}

		/// <summary>
		/// 派生したクラスが実際にファイルの中身を読み込むためのメソッドです。
		/// 派生クラスは必ずこのメソッドを実装しなければなりません。
		/// </summary>
		/// <returns>trueなら読み込み成功。falseなら読み込み失敗として作ってください。</returns>
		//抽象メソッドなので継承してメソッドを必ず実装しなければいけないのです。
		//上のreturnで呼ばれているのは継承先のbReadFile()です。
		protected abstract bool bReadFile();
	}
}
