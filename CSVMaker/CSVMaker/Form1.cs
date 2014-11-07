using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CSVMaker
{
    /// <summary>
    /// Rに適合したフォーマットでCSVを生成します。
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region ドラッグ＆ドロップ
        private void lbxItems_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lbxItems_DragDrop(object sender, DragEventArgs e)
        {
            lbxItems.Items.Clear();

            /* ファイルまたはディレクトリ内のファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Exists(tempFilePath))
                {
                    lbxItems.Items.Add(tempFilePath);
                }
                else if (Directory.Exists(tempFilePath))
                {
                    string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                    lbxItems.Items.AddRange(files);
                }
                else { }

            }
        }
        #endregion

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
            for (int i = 0; i < pass.Length; i++)
            {
                tmpList.Add(ReadTextFile(pass[i]));
            }
            return tmpList;
        }



        #endregion

        private void button_Run_Click(object sender, EventArgs e)
        {
            string path = @"C:/Users/yvgqi/out.csv";
            string[] Paths = lbxItems.Items.Cast<string>().ToArray();
            List<List<double>> Datas = ReadTextFiles(Paths);

            
                // appendをtrueにすると，既存のファイルに追記
                // falseにすると，ファイルを新規作成する
                var append = false;
                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(path, append))
                {
                    sw.WriteLine("dName,Param1,Param2,Param3,Param4,Param5,Param6,Param7");
                    for (int i = 0; i < Datas.Count; ++i)
                    {
                        sw.Write(Path.GetFileNameWithoutExtension(Paths[i]));
                        sw.Write(",");
                        for (int j = 0; j < 7; j++)
                        {
                            sw.Write(Datas[i][j]);
                            sw.Write(",");
                        }
                        sw.WriteLine("");
                    }
                }
            
            MessageBox.Show("保存しました。");
        }
    }
}
