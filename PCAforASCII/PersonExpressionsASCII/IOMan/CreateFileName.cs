using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IOMan
{
    public static class CreateFileName
    {
        /// <summary>
        /// ファイルパスの最後に指定された文字列・拡張子を追加します。
        /// </summary>
        /// <param name="FullPath">ファイルパス</param>
        /// <param name="addString">追加する文字列</param>
        /// <param name="Extension">拡張子(拡張子前のドットも必要)</param>
        /// <returns>追加したファイルパス</returns>
        public static string AddStringAtEnd(string FullPath, string addString, string Extension)
        {
            return Path.GetDirectoryName(FullPath) + "\\" + Path.GetFileNameWithoutExtension(FullPath) + addString + Extension;
        }

        /// <summary>
        /// ファイルパスの最後に指定された文字列を追加します。
        /// </summary>
        /// <param name="FullPath">ファイルパス</param>
        /// <param name="addString">追加する文字列</param>
        /// <returns>追加したファイルパス</returns>
        public static string AddStringAtEndWithoutExtension(string FullPath, string addString)
        {
            return Path.GetDirectoryName(FullPath) + "\\" + Path.GetFileNameWithoutExtension(FullPath) + addString + Path.GetExtension(FullPath);
        }

        public static string AddFolderName(string FullPath, string addFolderName)
        {
            return Path.GetDirectoryName(FullPath) + "\\" + addFolderName + "\\" + Path.GetFileName(FullPath);
        }
    }
}
