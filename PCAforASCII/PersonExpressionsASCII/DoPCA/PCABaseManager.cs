using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using System.Windows.Forms;
using System.IO;
using IOMan;

namespace DoPCA
{
    public class PCABaseManager
    {
        /// <summary>
        /// 最初に表示するメッセージ
        /// </summary>
        protected string OpeningMessage;

        /// <summary>
        /// 読み込むファイルリスト
        /// </summary>
        protected List<string> LoadFileList = new List<string>();

        /// <summary>
        /// 処理状態を表示するプログレスバー
        /// </summary>
        protected ToolStripProgressBar pgbMain;

        /// <summary>
        /// ログ出力用のリストボックス
        /// </summary>
        protected TextBox tbxLog;

        /// <summary>
        /// 処理内容
        /// </summary>
        protected PCASource PCASource;

        /// <summary>
        /// 汎用的なデータ保持用
        /// </summary>
        protected object Tag;

        /// <summary>
        /// 何もしないコンストラクタ（継承用）
        /// </summary>
        protected PCABaseManager()
        {
        }

        /// <summary>
        /// 読み込むファイルリストを指定してインスタンスを作成します。
        /// </summary>
        /// <param name="FileList">読み込むファイルリスト</param>
        protected PCABaseManager(List<string> FileList)
        {
            LoadFileList = FileList;
        }

        /// <summary>
        /// 主成分分析を行い、固有ベクトル、固有値、展開係数を取得します。
        /// </summary>
        /// <returns>成功ならtrue、失敗ならfalseが返ります</returns>
        public virtual PCAData GetPCAData()
        {
            ///ストップウォッチ
            System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();

            ///情報ウィンドウを設定
            fmLog ifmLog = new fmLog();
            tbxLog = ifmLog.tbxLog;
            pgbMain = ifmLog.pgbMain;
            ifmLog.Show();

            #region 主成分分析処理
            this.Log(OpeningMessage);
            this.Log(" ");

            Stopwatch.Start();
            ///Step1
            ///指定したファイルからベクトルを取得し、行列にする
            Matrix LoadMatrix;
                LoadMatrix = this.LoadFile(this.LoadFileList);
                this.ProgressbarStep();

            ///処理のステップ数(平均ベクトル取得から)
            int MaxStep = 9 - 1;
            //プログレスバー初期化
            SetProgressbarMaxValue(MaxStep);

            ///Step2
            ///平均ベクトルを取得
            this.Log("平均ベクトルを作成中...");
            ColumnVector AverageVector = LoadMatrix.GetAverageRow();
            this.ProgressbarStep();
            this.Log("...作成完了");

            ///Step3
            ///平均ベクトルがColSize個並んだ行列を作成
            this.Log("平均行列を作成中...");
            Matrix AverageMatrix = Matrix.GetSameElementMatrix(AverageVector, LoadMatrix.ColSize);
            this.ProgressbarStep();
            this.Log("...作成完了");

            ///Step4
            ///各要素ベクトルから平均ベクトルを引いて、差分行列を作成
            this.Log("差分行列を作成中...");
            Matrix DiffMatrix = LoadMatrix - AverageMatrix;
            this.ProgressbarStep();
            this.Log("...作成完了");

            ///Step5
            ///差分行列とその転置行列をかけてL行列を作成（必ず対称行列になります）
            this.Log("L行列を作成中...");
            SymmetricMatrix LMatrix = new SymmetricMatrix(DiffMatrix.GetTranspose()*DiffMatrix );
            this.ProgressbarStep();
            this.Log("...作成完了");

            ///Step6
            ///L行列は対称行列なので、さくっと固有ベクトル、固有値を取得します。
            this.Log("L行列の固有ベクトル・固有値を計算中...");
            EigenSystem EigenSystemData = LMatrix.GetEigenVectorAndValue();
            Matrix LEigenVector = EigenSystemData.GetEigenVectors();
            this.ProgressbarStep();
            this.Log("...計算完了");

            ///Step7
            ///L行列の固有ベクトルとファイルから読み込んだ行列をかけて、本物（？）の固有ベクトルにする。
            ///あと、ついでに得られたベクトルを列方向に正規化。
            this.Log("最終的な固有ベクトルを計算中...");
            Matrix FinalEigenVector = (LoadMatrix * LEigenVector).GetNormalizedMatrixCol();
            this.ProgressbarStep();
            this.Log("...計算完了");

            ///Step8
            ///固有値・固有ベクトルのリストの固有ベクトルを、計算した本物（？）の固有ベクトルに置き換える
            this.Log("固有ベクトルをリストに登録中...");
            EigenSystem FinalEigenSystem = new EigenSystem();
            for (int i = 0; i < EigenSystemData.Count; i++)
            {
                if(EigenSystemData[i].EigenValue > 0.0001)
                    FinalEigenSystem.Add(new EigenVectorAndValue(FinalEigenVector.GetColVector(i), EigenSystemData[i].EigenValue));
            }
            this.ProgressbarStep();
            this.Log("...登録完了");

            ///Step9
            ///差分行列と固有ベクトルの転置をかけると係数が取得できます。
            this.Log("展開係数を計算中...");
            Matrix CoefficientMatrix = FinalEigenSystem.GetEigenVectors().GetTranspose()*DiffMatrix ;
            this.ProgressbarStep();
            this.Log("...計算完了");

            Stopwatch.Stop();
            this.Log("計算に" + Stopwatch.ElapsedMilliseconds + "ミリ秒かかりました");

            #endregion
            ifmLog.Dispose();
            return new PCAData(PCASource, FinalEigenSystem, CoefficientMatrix, AverageVector, Tag);
        }

        /// <summary>
        /// ファイルを読み込むメソッドです。
        /// 必ずオーバーライドして読み込むファイル形式の読み込み処理を実装して下さい。
        /// </summary>
        /// <param name="LoadFileList">読み込むファイル名</param>
        /// <returns>ファイルから作成された行列</returns>
        protected virtual Matrix LoadFile(List<string> LoadFileList)
        {
            ///必ず中身をオーバーライドして下さい
            throw new NotImplementedException();
        }

        /// <summary>
        /// プログレスバーの最大値を設定し、現在の値を0にします。
        /// </summary>
        /// <param name="MaxValue">最大値</param>
        protected void SetProgressbarMaxValue(int MaxValue)
        {
            if (pgbMain == null)
                return;
            pgbMain.Value = 0;
            pgbMain.Maximum = MaxValue;
        }

        /// <summary>
        /// プログレスバーの値を1すすめます。
        /// </summary>
        protected void ProgressbarStep()
        {
            if (pgbMain == null)
                return;

            if (pgbMain.Value < pgbMain.Maximum)
                pgbMain.Value++;
            Application.DoEvents();
        }

        /// <summary>
        /// ログを出力します
        /// </summary>
        /// <param name="LogText">ログテキスト</param>
        protected void Log(string LogText)
        {
            if ( tbxLog== null)
                return;
            tbxLog.Text = tbxLog.Text + LogText + System.Environment.NewLine;
            tbxLog.Select(tbxLog.Text.Length, 0);
            tbxLog.ScrollToCaret();
            Application.DoEvents();
        }

        #region プロパティ
        /// <summary>
        /// 読み込むファイルのリストを取得、設定します。
        /// </summary>
        public List<string> FileList
        {
            set
            {
                LoadFileList.Clear();
                foreach (string FileName in value)
                    LoadFileList.Add(FileName);
            }
            get { return LoadFileList; }
        }

        #endregion
    }
}
