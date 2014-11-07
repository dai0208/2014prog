using System;
using System.IO;
using System.Windows.Forms;

namespace PointFormat
{
	/// <summary>
	/// cSaveFile の概要の説明です。
	/// </summary>
	abstract public class cSaveFile : cOpenFile
	{
		protected StreamWriter swSaveFile;
        private bool bForceSave = false;

		/// <summary>
		/// 特に何もしないコンストラクタです。
		/// </summary>
		protected cSaveFile()
		{
		}

		//データをセーブするためのメソッド。
		/// <summary>
		/// ファイルの内容を書き込むためのメソッドです。外部からはこのメソッドを使ってファイルを書き込みます。
		/// ファイルの存在チェックなども行っているので、存在しないファイル名を指定されていても自動的に作成し書き込みを行います。
		/// また、フォルダが存在しない場合、すでにファイルが存在している場合も同様にメッセージを出し、自動的に処理を行います。
		/// </summary>
		/// <returns>trueなら書き込み成功。falseなら書き込み失敗。</returns>
		public bool bSaveFile()
		{
			//保存するフォルダが存在するかどうか。
			if(base.diOpenFile.Exists != true)
			{
				if(MessageBox.Show("指定されたフォルダは存在しません。\n作成しますか？", "フォルダがありません", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					//フォルダを作成しないらしいので保存失敗。
					return false;
				}

				//保存ディレクトリを作成
				base.diOpenFile.Create();
			}

			//ここから先は必ず保存するフォルダがある状態

            if (base.fiOpenFile.Exists == true && bForceSave == false)
            {
                if (MessageBox.Show("指定されたファイルはすでに存在します。\n上書き保存しますか？", "上書き確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    //上書き保存はいやらしいので保存失敗。
                    return false;
                }
                else
                {
                    //上書きOKらしいのでファイルを削除
                    base.fiOpenFile.Delete();
                }
            }
            else
            {
                base.fiOpenFile.Delete();
            }

			//ここから先は基本的に保存できる環境が存在する状態。

			//データをセーブするためのストリームを作成。
			swSaveFile = new StreamWriter(base.strOpenFileName);

			return bWriteFile();
		}

        public bool bForceSave_value
        {
            get
            {
                return bForceSave;
            }
            set
            {
                bForceSave = value;
            }
        }

		/// <summary>
		/// 派生したクラスが実際にファイルの中身を書き込むためのメソッドです。
		/// 派生クラスは必ずこのメソッドを実装しなければなりません。
		/// </summary>
		/// <returns></returns>
		//抽象メソッドなので継承してメソッドを必ず実装しなければいけないのです。
		//上のreturnで呼ばれているのは継承先のbWriteFile()です。
		protected abstract bool bWriteFile();

	}
}
