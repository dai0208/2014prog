using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOMan
{
    /// <summary>
    /// ファイルの存在チェックを行うクラス
    /// </summary>
    public static class cFileExist
    {
        /// <summary>
        /// ファイルの存在チェックを行い、ファイルが無かった場合はfalseを返します。
        /// </summary>
        /// <param name="FileName">チェックするファイル名</param>
        /// <returns>ファイルがあればtrue、無い場合はfalse</returns>
        public static bool bCheckFileExist(string FileName)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(FileName);
            if (!fi.Exists) return false;
            return true;
        }

        /// <summary>
        /// ファイルの存在チェックを行い、ファイルが無かった場合は例外を返します。
        /// </summary>
        /// <param name="FileName">チェックするファイル名</param>
        public static void vCheckFileExist(string FileName)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(FileName);
            if (!fi.Exists)
                throw new ApplicationException("指定したファイルは存在しません");
        }
    }
}
