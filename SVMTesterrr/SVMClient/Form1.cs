using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using _2ClassSVMUtil;

namespace SVMClient
{
    public partial class Form1 : Form
    {
        twoClassSVMUtil svmtester;
        string PathToUnknownData;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            svmtester = new twoClassSVMUtil();
        }

        private void WeightControllBar_Scroll(object sender, EventArgs e)
        {
            WeightValueBox.Text = WeightController.Value.ToString();
        }

        #region txt → LIST 変換系
        /// <summary>
        /// .txtを読んでLISTへ格納するメソッドです
        /// </summary>
        /// <param name="pass">テキストファイルへのフルパス</param>
        /// <returns>格納後のLIST</returns>
        private List<double> ReadTextFile(string pass)
        {
            string line = "";
            List<double> Contents = new List<double>();
            using (StreamReader sr = new StreamReader(pass, Encoding.GetEncoding("Shift_JIS")))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Contents.Add(double.Parse(line));
                }
            }
            return Contents;
        }

        /// <summary>
        /// ReadTextFileの拡張メソッドです。
        /// </summary>
        /// <param name="pass">テキストファイル群</param>
        /// <returns></returns>
        private List<List<double>> ReadTextFiles(string[] pass)
        {
            List<List<double>> tmpList = new List<List<double>>();
            for(int i=0;i<pass.Length;i++)
            {
                tmpList.Add(ReadTextFile(pass[i]));
            }
            return tmpList;
        }

        #endregion

        #region ドラッグ＆ドロップ動作
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void tbxUnknownData_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void tbxUnknownData_DragDrop(object sender, DragEventArgs e)
        {
            tbxUnknownData.Text="";
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            PathToUnknownData = files[0];

            tbxUnknownData.Text = files[0];
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            listBox2.Items.Clear();

            /* ファイルまたはディレクトリ内のファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Exists(tempFilePath))
                {
                    listBox2.Items.Add(tempFilePath);
                }
                else if (Directory.Exists(tempFilePath))
                {
                    string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                    listBox2.Items.AddRange(files);
                }
                else { }

            }
            //string[] files = listBox2.Items.Cast<string>().ToArray();
        }

        private void listBox3_DragDrop(object sender, DragEventArgs e)
        {
            listBox3.Items.Clear();
            List<string> AddPath = new List<string>();

            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                listBox3.Items.AddRange(files);
            } 
            
            //string[] files = listBox3.Items.Cast<string>().ToArray();
        }
        #endregion

        #region ボタン動作

        private void LearningBtn_Click(object sender, EventArgs e)
        {
            string[] Truefiles = listBox2.Items.Cast<string>().ToArray();
            string[] Falsefiles = listBox3.Items.Cast<string>().ToArray();

            List<List<double>> TrueDatas = new List<List<double>>();
            TrueDatas.AddRange(ReadTextFiles(Truefiles));

            List<List<double>> FalseDatas = new List<List<double>>();
            FalseDatas.AddRange(ReadTextFiles(Falsefiles));

            svmtester.TrueTeacherDatas_l = TrueDatas;
            svmtester.FalseTeacherDatas_l = FalseDatas;
            svmtester.DoTrainingOnly();
        }

        private void DoClassifierBtn_Click(object sender, EventArgs e)
        {
            List<double> UnKnownData = new List<double>();
            UnKnownData = ReadTextFile(PathToUnknownData);
            svmtester.UnknownData_l = UnKnownData;
            svmtester.setUnknownData_Node();
            svmtester.RunSVM();

            richTextBox1.Text += svmtester.PrintResult();
            TransformType2Btn.Enabled = true;
        }

        private void SaveASCFile_Click(object sender, EventArgs e)
        {
            string DirName = Path.GetDirectoryName(PathToUnknownData)+"\\CONVERTED_BY_SVM\\";
            if (System.IO.Directory.Exists(DirName)){　}
            else
            {
                MessageBox.Show("フォルダが存在しないので作成します。");
                System.IO.Directory.CreateDirectory(DirName);
            }

            string FilePath = DirName + Path.GetFileName(PathToUnknownData);
            svmtester.SaveParameter(FilePath);
        }

        private void TransformType2Btn_Click(object sender, EventArgs e)
        {
            svmtester.ConvertInputParams(3,WeightController.Value);
            MessageBox.Show("新手法で印象変換完了しました");
            richTextBox1.Text += "印象変換実行完了！\n";
        }
        #endregion



        
    }
}
