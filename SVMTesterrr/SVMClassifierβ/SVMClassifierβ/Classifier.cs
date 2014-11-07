using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MatrixVector;
using SVM;

namespace SVMClassifierβ
{
    public class Classifier
    {
        private int dim; //パラメータ空間の次元数
        private double InDataCls; //入力データ所属クラス
        private Node[] InDataNode; //入力データ
        private Node[] tmpDataNode;//一時保存用データ
        private List<Node[]> ParamsClassA; //クラス１のデータ群
        private List<Node[]> ParamsClassB; //クラス２のデータ群
        private Model model; //識別後モデルデータ

        protected TextBox tbxLog;
        protected ToolStripProgressBar pgbMain;

        //コンストラクタ。後から１つずつ設定することも可能です。
        public Classifier(){}

        public Classifier(ColumnVector InParamData, ColumnVector[] ListA, ColumnVector[] ListB)
        {
            SVMManager svmmanager = new SVMManager();
            this.dim = InParamData.Length;
            this.ParamsClassA = MakeNodeList(svmmanager.MatrixToArray(svmmanager.VectorToMatrix(ListA)));
            this.ParamsClassB = MakeNodeList(svmmanager.MatrixToArray(svmmanager.VectorToMatrix(ListB)));
        }

        public Classifier(ColumnVector InParamData, Matrix ListA, Matrix ListB)
        {
            SVMManager svmmanager = new SVMManager();
            this.dim = InParamData.Length;
            this.InDataNode = CreateParams(svmmanager.VectorToArray(InParamData));
            this.ParamsClassA = MakeNodeList(svmmanager.MatrixToArray(ListA));
            this.ParamsClassB = MakeNodeList(svmmanager.MatrixToArray(ListB));
        }

        public Classifier(List<double> InParamData, List<List<double>> ListA, List<List<double>> ListB)
        {
            this.dim = InParamData.Count;
            this.InDataNode = CreateParams(InParamData);
            this.ParamsClassA = MakeNodeList(ListA);
            this.ParamsClassB = MakeNodeList(ListB);
        }

        #region ゲッター&セッター
        public Node[] editInData
        {
            set { InDataNode = value; }
            get { return this.InDataNode; }
        }
        public Node[] editTmpData
        {
            set { tmpDataNode = value; }
            get { return this.tmpDataNode; }
        }
        /// <summary>
        /// 入力データのNode[]を取得しフィールドに保持します。ついでに次元数も取得します。
        /// </summary>
        /// <param name="InParamData">List[データの中身]</param>
        public void SetInDataNode(List<double> InParamData)
        {
            this.dim = InParamData.Count;
            InDataNode = CreateParams(InParamData);
        }

        /// <summary>
        /// クラスAのNode[]を取得しフィールドに保持します。
        /// </summary>
        /// <param name="ListA">List[データ][double型データの値]</param>
        public void setClassNodeA(List<List<double>> ListA)
        {
            this.ParamsClassA = MakeNodeList(ListA);
        }

        /// <summary>
        /// クラスBのNode[]を取得しフィールドに保持します。
        /// </summary>
        /// <param name="ListA">List[データ][double型データの値]</param>
        public void setClassNodeB(List<List<double>> ListB)
        {
            this.ParamsClassB = MakeNodeList(ListB);
        }

        public Model getModel
        {
            get{ return model; }
        }

        public Node[][] getSupportVector
        {
            get { return model.SupportVectors; }
        }

        #endregion

        #region 教師＋ラベル生成系
        /// <summary>
        /// リストから教師データを設定し、ラベル付けします。
        /// </summary>
        /// <param name="datalist">テキストデータから読み込んだパラメータリスト</param>
        /// <param name="Class">クラス番号</param>
        /// <returns>合成後のラベル＋教師データ</returns>
        private Problem SetTandL(List<Node[]> datalist, int Class)
        {
            //Creating teacher data
            Problem prob;
            Node[][] t = new Node[datalist.Count][];
            double[] klass;

            for (int i = 0; i < datalist.Count; i++)
            {
                t[i] = datalist[i];
            }

            klass = new double[datalist.Count];

            if(Class == 1)
            {
                for(int i=0;i<datalist.Count; i++)
                {
                    klass[i] = 1;
                }
                prob = new Problem(datalist.Count, klass, t, 7);
            }

            else
            {
                for (int i = 0; i < datalist.Count; i++)
                {
                    klass[i] = -1;
                }
                prob = new Problem(datalist.Count, klass, t, 7);
            }
            return prob;
        }
        #endregion

        #region 合成系
        /// <summary>
        /// Programクラスから作られた2つのインスタンスを合成します
        /// </summary>
        /// <param name="prob1"></param>
        /// <param name="prob2"></param>
        /// <returns></returns>
        private Problem mixProb(Problem prob1, Problem prob2)
        {
            Node[][] tmpVector = new Node[prob1.Count + prob2.Count][];
            double[] tmpKlass = new double[prob1.Count + prob2.Count];

            prob1.X.CopyTo(tmpVector, 0);
            prob2.X.CopyTo(tmpVector, prob1.Count);
            prob1.Y.CopyTo(tmpKlass, 0);
            prob2.Y.CopyTo(tmpKlass, prob1.Count);

            Problem mixedProb = new Problem(tmpVector.Length,tmpKlass,tmpVector,7);
            return mixedProb;
        }
        #endregion

        #region 識別系
        /// <summary>
        /// SVMによる識別を行います。
        /// </summary>
        /// <param name="trainingData"></param>
        public String DoSVMRecognize()
        {
            double C;
            double Gamma;
            int MaxStep = 3;

            //ストップウォッチ
            System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();
            
            //情報ウィンドウを設定
            fmLog ifmLog = new fmLog();
            tbxLog = ifmLog.tbxLog;
            pgbMain = ifmLog.pgbMain;
            ifmLog.Show();

            StopWatch.Start();
            SetProgressbarMaxValue(MaxStep);

            //Step1.教師データのラベル付け
            this.Log("ラベル付け教師データ作成中...");
            Problem trainingData = mixProb(SetTandL(ParamsClassA, 1), SetTandL(ParamsClassB, 2));
            this.ProgressbarStep();
            this.Log("...作成完了");
            
            //Step2.学習方法の設定
            this.Log("グリッドで最適なCとGammaの設定中...");
            Parameter param = new Parameter();
            param.SvmType = SvmType.C_SVC;
            param.KernelType = KernelType.RBF;
            param.Probability = true;
            ParameterSelection.Grid(trainingData, param, "GridParam.txt", out C, out Gamma);
            param.C = C;
            param.Gamma = Gamma;
            this.Log("...設定完了");

            //Step3.モデルの生成
            this.Log("モデルの作成中...");
            model = Training.Train(trainingData, param);
            this.Log("...作成完了");

            StopWatch.Stop();
            this.Log("計算に" + StopWatch.ElapsedMilliseconds + "ミリ秒かかりました。");
            ifmLog.Dispose();

            String Result = PrintResult();
            return Result;
        }
        #endregion

        #region 出力系
        /// <summary>
        /// 結果の出力です。正解の場合1、不正解の場合-1を出力します
        /// </summary>
        public string PrintResult()
        {
            double[] prob = Prediction.PredictProbability(model, InDataNode);
            InDataCls = Prediction.Predict(model, InDataNode);
            string OutPutText = "";
            
            OutPutText += String.Format( "分類クラス：{0}\n" ,(int)InDataCls );

            if (prob[0]>prob[1])
            {
                OutPutText += String.Format("クラス1との適合度{0:f}%\n", prob[0] * 100.0);
            }
            else
            {
                OutPutText += String.Format("クラス-1との適合度{0:f}%\n", prob[1] * 100.0);
            }

            return OutPutText;
        }
        #endregion

        #region 変換系
        /// <summary>
        /// 識別結果を基に印象変換を実行します。（過去の論文の手法）
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public void ConvertInputParams(double weight)
        {
            Node[] NewInDataNode = NodeInitialization(InDataNode.Length);
            Node[] ImpressionTransferVector = NodeInitialization(InDataNode.Length);
            Node[][] SupportVectors = getSupportVector;

            /*Phase1.入力データから最も近い相手クラスのサポートベクタを探索*/

            int NearestSVKey = -1; //最近傍のキーを保持。メソッドを抜けると破棄されます。
            double NearestDist = 99999; //最近傍の距離値を保持。メソッドを抜けると破棄されます。

            //クラス１に所属の場合
            if (InDataCls == 1)
            {
                /*クラス２のサポートベクトルから最近傍のものを探索*/
                for (int i = model.NumberOfSVPerClass[0]; i < (model.NumberOfSVPerClass[0] + model.NumberOfSVPerClass[1]); i++)
                {
                    if (NearestDist > EuclideanDistance_between_Nodes(InDataNode, SupportVectors[i]))
                    {
                        NearestDist = EuclideanDistance_between_Nodes(InDataNode, SupportVectors[i]);
                        NearestSVKey = i;
                    }
                }
            }

            //クラス２に所属の場合
            else
            {
                /*クラス１のサポートベクトルから最近傍のものを探索*/
                for (int i = 0; i < model.NumberOfSVPerClass[0]; i++)
                {
                    if (NearestDist > EuclideanDistance_between_Nodes(InDataNode, SupportVectors[i]))
                    {
                        NearestDist = EuclideanDistance_between_Nodes(InDataNode, SupportVectors[i]);
                        NearestSVKey = i;
                    }
                }
            }
            
            /*Phase2.印象変換ベクトルのノルム正規化*/

            double scale = 0;
            for (int j = 0; j < InDataNode.Length; j++)
            {
                ImpressionTransferVector[j].Value = SupportVectors[NearestSVKey][j].Value - InDataNode[j].Value;
                scale += Math.Pow(ImpressionTransferVector[j].Value, 2);
            }
            scale = Math.Sqrt(scale);

            for (int k = 0; k < InDataNode.Length; k++)
            {
                ImpressionTransferVector[k].Value /= scale;
            }

            /*Phase3.印象変換ベクトル法による印象変換*/

            Node[] Proto_A = Average(ParamsClassA);
            Node[] Proto_B = Average(ParamsClassB);
            double dist = EuclideanDistance_between_Nodes(Proto_A, Proto_B);
            dist = dist / 10.0;

            for (int i = 0; i < InDataNode.Length; i++)
            {
                NewInDataNode[i].Value = InDataNode[i].Value + weight * dist * ImpressionTransferVector[i].Value;
            }

            editTmpData = NewInDataNode;
        }

        /// <summary>
        /// 識別結果を基に印象変換を実行します。（新規提案手法）
        /// </summary>
        /// <param name="weight"></param>
        public void ConvertInputParams_2(double weight)
        {
            /* Phase1.モデルから重みベクトルを取得します*/

            Node[] NewInDataNode = NodeInitialization(InDataNode.Length);
            double[][] coef = model.SupportVectorCoefficients; 
            double[] b = model.Rho; //バイアス項 -b
            double[] w = new double[model.SupportVectors[0].Length];

            //パラメータ長(＝主成分空間の次元数)だけ回す。
            for (int i = 0; i < model.SupportVectors[0].Length; i++)
            {
                //サポートベクタの数だけ回す
                for (int j = 0; j < model.SupportVectorCount; j++)
                {
                    /* 重みベクトルを取得 */
                    w[i] += model.SupportVectors[j][i].Value * coef[0][j];
                }
            }

            /* Phase2.決定境界との直交方向に印象変換ベクトルを設定します*/

            double scale = 0;
            for (int j = 0; j < w.Length; j++)
            {
                scale += Math.Pow(w[j], 2);
            }
            scale = Math.Sqrt(scale);

            for (int k = 0; k < w.Length; k++)
            {
                w[k] /= scale;
            }

            /* Phase3.印象変換ベクトル法による印象変換*/

            Node[] ProtoA = Average(ParamsClassA);
            Node[] ProtoB = Average(ParamsClassB);
            double dist = EuclideanDistance_between_Nodes(ProtoA, ProtoB);
            dist = dist / 10.0; 

            for (int i = 0; i < InDataNode.Length; i++)
            {
                NewInDataNode[i].Value = InDataNode[i].Value + weight * dist * w[i];
            }

            editTmpData = NewInDataNode;
        }

        /// <summary>
        /// ２ノード間のユークリッド距離を返します。※２ノードの次元数は揃えてください。
        /// </summary>
        /// <param name="node1">ノード１</param>
        /// <param name="node2">ノード２</param>
        /// <returns>距離値</returns>
        private double EuclideanDistance_between_Nodes(Node[] node1, Node[] node2)
        {
            double dist = 0;

            for (int i = 0; i < node1.Length; i++)
            {
                dist += Math.Abs(node1[i].Value - node2[i].Value);
            }
            return dist;
        }

        private Node[] Average(List<Node[]> nodeList)
        {
            Node[] ProtoTypeNode = NodeInitialization(nodeList.Count);
            for (int i = 0; i < nodeList.Count; i++)
            {
                double tmp = 0;
                for (int j = 0; j < nodeList[i].Length; j++)
                {
                    tmp += nodeList[i][j].Value;
                }
                ProtoTypeNode[i].Value = tmp / nodeList[i].Length;
            }
            return ProtoTypeNode;
        }
        /// <summary>
        /// ノードの初期化を行います。
        /// </summary>
        /// <param name="NodeSize">初期化ノードのサイズ</param>
        /// <returns></returns>
        public Node[] NodeInitialization(int NodeSize)
        {
            Node[] tmpnode = new Node[NodeSize];
            for (int i = 0; i < NodeSize; i++)
            {
                tmpnode[i] = new Node(i,0);
            }
            return tmpnode;
        }
        #endregion

        #region 保存系
        public void SaveASC(string savepath)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = savepath;
            sfd.Filter = "ascファイル(*.asc)|*.asc|すべてのファイル(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream stream;
                stream = sfd.OpenFile();
                if (stream != null)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                    for (int i = 0; i < editTmpData.Length; i++)
                    {
                        sw.WriteLine(editTmpData[i].Value);
                    }
                    sw.Close();
                    stream.Close();
                }
            }
        }
        #endregion

        #region ノード生成用インターフェース
        /// <summary>
        /// double型の主成分パラメータを使ってSVM空間上にプロットしNodeを取得します。
        /// </summary>
        /// <param name="Params">double型パラメータ群データ</param>
        /// <returns>生成されたノード</returns>
        private Node[] CreateParams(List<double> Params)
        {
            Node[] nodes = new Node[Params.Count];
            for (int i = 0; i < Params.Count; i++)
            {
                nodes[i] = new Node(i, Params[i]);
            }
            return nodes;
        }

        /// <summary>
        /// CreateParamsの拡張メソッド。教師用の多パラメータからNode[]を取得します。
        /// </summary>
        /// <param name="ParamsList">Listに格納されたdouble型パラメータ群の集合データ</param>
        /// <returns></returns>
        private List<Node[]> MakeNodeList(List<List<double>> ParamsList)
        {
            List<Node[]> nodeList = new List<Node[]>();
            for (int i = 0; i < ParamsList.Count; i++)
            {
                nodeList.Add(CreateParams(ParamsList[i]));
            }
            return nodeList;
        }
        #endregion

        #region ログフォーム操作
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
            if (tbxLog == null)
                return;
            tbxLog.Text = tbxLog.Text + LogText + System.Environment.NewLine;
            tbxLog.Select(tbxLog.Text.Length, 0);
            tbxLog.ScrollToCaret();
            Application.DoEvents();
        }

        #endregion
    }
}
