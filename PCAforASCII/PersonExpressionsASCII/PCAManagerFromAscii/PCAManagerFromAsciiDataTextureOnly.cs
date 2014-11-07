using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using System.IO;
using IOMan;
using DoPCA;

namespace PCAManagerFromAsciiData
{
    /// <summary>
    /// Asciiファイルを読み込んで主成分分析を行うマネージャ
    /// </summary>
    public class PCAManagerFromAsciiDataTextureOnly : PCABaseManager
    {
        public PCAManagerFromAsciiDataTextureOnly() { }

        /// <summary>
        /// 読み込むファイルリストを指定してインスタンスを作成します。
        /// </summary>
        /// <param name="FileList">読み込むファイルリスト</param>
        public PCAManagerFromAsciiDataTextureOnly(List<string> FileList)
            : base(FileList)
        {
            OpeningMessage = "テクスチャ情報のみで主成分分析を行います";
        }

        /// <summary>
        /// Asciiファイルを読み込むメソッドです。
        /// </summary>
        /// <param name="LoadFileList">読み込むファイル名</param>
        /// <returns>ファイルから作成された行列</returns>
        protected override Matrix LoadFile(List<string> LoadFileList)
        {
            if (LoadFileList == null)
                throw new ApplicationException("読み込むファイルリストが設定されていません");
            if (LoadFileList.Count == 0)
                throw new ApplicationException("読み込むファイルがありません");

            ///データソースの種類をAsciiにセット
            PCASource = PCASource.AsciiDataShapeOnly;
            Tag = null;

            ///プログレスバーに最大値をセット
            SetProgressbarMaxValue(LoadFileList.Count);

            ///Ascファイルからの読み込み
            Vector[] Vector = new Vector[LoadFileList.Count];
            for (int i = 0; i < Vector.Length; i++)
                //ファイル存在チェック
                if (cFileExist.bCheckFileExist(LoadFileList[i]) && Path.GetExtension(LoadFileList[i]) == ".asc")
                {
                    ///読み込み処理
                    this.Log(LoadFileList[i] + "を読み込み中...");
                    Vector[i] = CreateVectorFromAsciiData.GetVectorFromAscRGB(LoadFileList[i]);
                    this.Log("...読み込み完了");
                    ProgressbarStep();
                }

            return new Matrix(Vector);
        }
    }
}
