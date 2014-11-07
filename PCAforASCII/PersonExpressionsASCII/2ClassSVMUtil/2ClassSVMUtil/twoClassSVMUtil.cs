using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MatrixVector;
using SVM;

namespace _2ClassSVMUtil
{
    public class twoClassSVMUtil : IMachineLearning
    {
        protected int _dimension;
        protected Vector _UnknownData;
        protected Node[] _UnknownData_Node;
        protected Matrix _TrueTeacherDatas;
        protected Matrix _FalseTeacherDatas;

        protected Model model;
        protected double _ResultClass;
        protected double[] _Problem;
        protected ColumnVector _EditParam;

        protected TextBox tbxLog;

        //コンストラクタ。後で値をセットすることも可能です。
        public twoClassSVMUtil() { }
        public twoClassSVMUtil(double[] inData,double[][] TrueTeacherDatas, double[][] FalseTeacherDatas)
        {
            _dimension = inData.Length;
            _UnknownData = SVMManager.ArrayConverter(inData);
            _TrueTeacherDatas = SVMManager.MatrixConverter(TrueTeacherDatas);
            _FalseTeacherDatas = SVMManager.MatrixConverter(FalseTeacherDatas);
        }
        public twoClassSVMUtil(List<double> inData,List<List<double>> TrueTeacherDatas, List<List<double>> FalseTeacherDatas)
        {
            _dimension = inData.Count;
            _UnknownData = SVMManager.ArrayConverter(inData);
            _TrueTeacherDatas = SVMManager.MatrixConverter(TrueTeacherDatas);
            _FalseTeacherDatas = SVMManager.MatrixConverter(FalseTeacherDatas);
        }
        public twoClassSVMUtil(Vector inData, Vector[] TrueTeacherDatas, Vector[] FalseTeacherDatas)
        {
            _dimension = inData.Length;
            _UnknownData = inData;
            _TrueTeacherDatas = SVMManager.MatrixConverter(TrueTeacherDatas);
            _FalseTeacherDatas = SVMManager.MatrixConverter(FalseTeacherDatas);
        }
        public twoClassSVMUtil(Vector inData, Matrix TrueTeacherDatas, Matrix FalseTeacherDatas)
        {
            _dimension = inData.Length;
            _UnknownData = inData;
            _TrueTeacherDatas = TrueTeacherDatas;
            _FalseTeacherDatas = FalseTeacherDatas;
        }

        #region ゲッターセッター

        public int dimension
        {
            set { _dimension = value; }
        }

        public double[] UnknownData_a
        {
            set{
                this.dimension = value.Length;
                _UnknownData = SVMManager.ArrayConverter(value); 
            }
        }
        public List<double> UnknownData_l
        {
            set{
                this.dimension = value.Count;
                _UnknownData = SVMManager.ArrayConverter(value);
            }
        }
        public Vector UnknownData
        {
            set{
                this.dimension = value.Length;
                _UnknownData = value;
            }
        }

        public Matrix TrueTeacherDatas
        {
            set { _TrueTeacherDatas = value; }
        }
        public double[][] TrueTeacherDatas_a
        {
            set { _TrueTeacherDatas = SVMManager.MatrixConverter(value); }
        }
        public List<List<double>> TrueTeacherDatas_l
        {
            set { _TrueTeacherDatas = SVMManager.MatrixConverter(value); }
        }
        public Vector[] TrueTeacherDatas_v
        {
            set { _TrueTeacherDatas = SVMManager.MatrixConverter(value); }
        }

        public Matrix FalseTeacherDatas
        {
            set { _FalseTeacherDatas = value; }
        }
        public double[][] FalseTeacherDatas_a
        {
            set { _FalseTeacherDatas = SVMManager.MatrixConverter(value); }
        }
        public List<List<double>> FalseTeacherDatas_l
        {
            set { _FalseTeacherDatas = SVMManager.MatrixConverter(value); }
        }
        public Vector[] FalseTeacherDatas_v
        {
            set { _FalseTeacherDatas = SVMManager.MatrixConverter(value); }
        }

        /// <summary>
        /// 識別されたクラスを保持します。
        /// </summary>
        public double ResultClass
        {
            get { return _ResultClass; }
            protected set { _ResultClass = value; }
        }

        /// <summary>
        /// 識別結果に対するＳＶＭの自信(？)を表すパラメータです。
        /// </summary>
        public double[] Problem
        {
            get { return _Problem; }
            protected set { _Problem = value; }
        }

        /// <summary>
        /// 印象変換を行った後のベクトルを保持します。
        /// </summary>
        public ColumnVector EditParam
        {
            set { _EditParam = value; }
        }
        #endregion

        #region 教師＋ラベル生成系
        /// <summary>
        /// リストから教師データを設定し、ラベル付けします。
        /// </summary>
        /// <param name="datalist">パラメータリスト</param>
        /// <param name="Class">クラス番号</param>
        /// <returns>合成後のラベル＋教師データ</returns>
        private Problem SetTandL(List<Node[]> datalist, int Class)
        {
            Problem prob;
            Node[][] t = new Node[datalist.Count][];
            double[] klass;

            for (int i = 0; i < datalist.Count; i++)
            {
                t[i] = datalist[i];
            }

            klass = new double[datalist.Count];

            if (Class == 1)
            {
                for (int i = 0; i < datalist.Count; i++)
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

            Problem mixedProb = new Problem(tmpVector.Length, tmpKlass, tmpVector, 7);
            return mixedProb;
        }
        #endregion

        #region 識別前処理系

        /// <summary>
        /// 入力データをNode[]に変換して一時保管しておきます。
        /// </summary>
        public void setUnknownData_Node()
        {
            if (_UnknownData == null)
                throw new ApplicationException("入力データに値がセットされていません。");

            _UnknownData_Node = getNode(_UnknownData);
        }
            
        /// <summary>
        /// 教師データを使ってSVMに学習させます。入力データのセットはしません。
        /// </summary>
        public virtual void DoTrainingOnly()
        {
            if (_TrueTeacherDatas == null || _FalseTeacherDatas == null)
                throw new ApplicationException("教師データに値がセットされていません。");

            double C;
            double Gamma;

            //教師データのラベル付け & 合成
            Problem trainingData = mixProb(SetTandL(getNodeList(_TrueTeacherDatas), 1), SetTandL(getNodeList(_FalseTeacherDatas), 0));

            //学習方法の設定
            Parameter param = new Parameter();
            param.SvmType = SvmType.C_SVC;
            param.KernelType = KernelType.RBF;
            param.Probability = true;
            ParameterSelection.Grid(trainingData, param, "GridParam.txt", out C, out Gamma);
            param.C = C;
            param.Gamma = Gamma;

            //モデルの生成
            this.model = Training.Train(trainingData, param);
        }

        /// <summary>
        /// 教師データを使ってSVMに学習させます。同時に入力データもセットします。
        /// </summary>
        public void DoTrainingAll()
        {
            setUnknownData_Node();
            DoTrainingOnly();
        }

        #endregion

        #region 識別系
        /// <summary>
        /// 未知データをクラスタリングします。先にDoTraining()でモデルデータを作成してください。識別クラスのみセットされます。
        /// </summary>
        public void RunSVM()
        {
            ResultClass = Prediction.Predict(model, _UnknownData_Node);
        }

        /// <summary>
        /// Probability等の細かい識別結果を出力します。使用する場合は先にRunSVMしてください。
        /// </summary>
        /// <returns></returns>
        public string PrintResult()
        {
            if (ResultClass == 0.0)
                throw new ApplicationException("RunSVMされていません。");

            string OutPutText = "";
            double[] prob = Prediction.PredictProbability(model, _UnknownData_Node);

            OutPutText += String.Format("Class:{0}\n", (int)_ResultClass);

            if (prob[0] > prob[1])
            {
                OutPutText += String.Format("False's probability:{0:f}%\n", prob[0] * 100.0);
            }
            else
            {
                OutPutText += String.Format("True's probability:{0:f}%\n", prob[1] * 100.0);
            }
            return OutPutText;
        }
        #endregion

        #region 変換系
        /// <summary>
        /// 識別結果を基に印象変換を実行します。
        /// </summary>
        /// <param name="weight"></param>
        public virtual void ConvertInputParams_2(double weight)
        {
            /* Phase1.モデルから重みベクトルを取得します*/

            ColumnVector tmpEditParam = new ColumnVector(_UnknownData_Node.Length);
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

            Node[] ProtoA = Average(getNodeList(_TrueTeacherDatas));
            Node[] ProtoB = Average(getNodeList(_FalseTeacherDatas));
            double dist = EuclideanDistance_between_Nodes(ProtoA, ProtoB);
            dist = dist / 10.0;

            for (int i = 0; i < _UnknownData_Node.Length; i++)
            {
                tmpEditParam[i] = _UnknownData_Node[i].Value + weight * dist * w[i];
            }

            EditParam = tmpEditParam;
        }

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

        #endregion

        #region ノード生成用インターフェース

        /// <summary>
        /// ノードの初期化を行います。ノード番号は付与されますが値は全て０になります。
        /// </summary>
        /// <param name="NodeSize">初期化ノードのサイズ</param>
        /// <returns></returns>
        public Node[] NodeInitialization(int NodeSize)
        {
            Node[] tmpnode = new Node[NodeSize];
            for (int i = 0; i < NodeSize; i++)
            {
                tmpnode[i] = new Node(i, 0);
            }
            return tmpnode;
        }

        /// <summary>
        /// 主成分パラメータからNodeを取得します。
        /// </summary>
        /// <param name="Params">パラメータベクトル</param>
        /// <returns>変換ノード</returns>
        private Node[] getNode(Vector Params)
        {
            Node[] nodes = new Node[Params.Length];
            for (int i = 0; i < Params.Length; i++)
            {
                nodes[i] = new Node(i, Params[i]);
            }
            return nodes;
        }

        /// <summary>
        /// 多パラメータからNodeを取得します。
        /// </summary>
        /// <param name="ParamsList">パラメータマトリクス</param>
        /// <returns>変換ノード配列</returns>
        private List<Node[]> getNodeList(Matrix Params)
        {
            List<Node[]> nodeList = new List<Node[]>();
            for (int i = 0; i < Params.ColSize; i++)
            {
                nodeList.Add(getNode(Params.GetColVector(i)));
            }
            return nodeList;
        }
        #endregion

        #region 保存系
        public void SaveParameter(string FileName)
        {
            _EditParam.Save(FileName);
        }
        #endregion
    }
}
