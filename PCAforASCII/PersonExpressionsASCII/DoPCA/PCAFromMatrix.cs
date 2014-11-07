using System;
using System.Collections.Generic;
using System.Linq;
using MatrixVector;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace DoPCA
{
    public class PCAFromMatrix:PCABaseManager
    {
        private Matrix LoadMatrix; //マトリクスを直接指定

        public Matrix _LoadMatrix
        {
            get { return LoadMatrix; }
            set { LoadMatrix = value; }
        }

        public override PCAData GetPCAData()
        {
            ///ストップウォッチ
            System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();

            ///情報ウィンドウを設定
            fmLog ifmLog = new fmLog();
            tbxLog = base.tbxLog;
            pgbMain = base.pgbMain;
            ifmLog.Show();

            #region 主成分分析処理
            this.Log("分離マトリクスの主成分分析をします。");
            this.Log(" ");

            Stopwatch.Start();

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
            SymmetricMatrix LMatrix = new SymmetricMatrix(DiffMatrix.GetTranspose() * DiffMatrix);
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
                if (EigenSystemData[i].EigenValue > 0.0001)
                    FinalEigenSystem.Add(new EigenVectorAndValue(FinalEigenVector.GetColVector(i), EigenSystemData[i].EigenValue));
            }
            this.ProgressbarStep();
            this.Log("...登録完了");

            ///Step9
            ///差分行列と固有ベクトルの転置をかけると係数が取得できます。
            this.Log("展開係数を計算中...");
            Matrix CoefficientMatrix = FinalEigenSystem.GetEigenVectors().GetTranspose() * DiffMatrix;
            this.ProgressbarStep();
            this.Log("...計算完了");

            Stopwatch.Stop();
            this.Log("計算に" + Stopwatch.ElapsedMilliseconds + "ミリ秒かかりました");

            #endregion
            ifmLog.Dispose();
            return new PCAData(PCASource, FinalEigenSystem, CoefficientMatrix, AverageVector, Tag);
        }
    }
}
