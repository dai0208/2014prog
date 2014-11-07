using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOMan
{
    static public class cDirectoryCheck
    {
        /// <summary>
        /// 指定したディレクトリが存在しなかった場合は作成するメソッドです。
        /// </summary>
        /// <param name="CheckDirectoryName">存在をチェックしたいディレクトリ名</param>
        public static void CheckDirectory(string CheckDirectoryName)
        {
            string DirectoryName = System.IO.Path.GetDirectoryName(CheckDirectoryName);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(DirectoryName);

            if (!di.Exists) di.Create();
        }

        public static bool CheckDirectoryWithYN(string CheckDirectoryName)
        {
            string DirectoryName = System.IO.Path.GetDirectoryName(CheckDirectoryName);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(DirectoryName);

            if (!di.Exists)
                if (System.Windows.Forms.MessageBox.Show("指定したフォルダは存在しません。作成しますか？", "フォルダがありません", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    di.Create();
                else
                    return false;
            return true;
        }
    }
}
