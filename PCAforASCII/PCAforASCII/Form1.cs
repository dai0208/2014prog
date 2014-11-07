using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PersonExpressionsASCII;
using PCAforASCII;
using MatrixVector;

namespace PCAforASCII
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lvFilelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void lvFilelist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lvFilelist_DragDrop(object sender, DragEventArgs e)
        {
            lvFilelist.Items.Clear();

            /* ファイルまたはディレクトリ内のファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Exists(tempFilePath))
                {
                    lvFilelist.Items.Add(new ListViewItem(new String[]{Path.GetFileNameWithoutExtension(tempFilePath),tempFilePath}));
                }
                else if (Directory.Exists(tempFilePath))
                {
                    string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                    for(int i=0;i<files.Length;i++)
                        lvFilelist.Items.AddRange(new ListViewItem[]{new ListViewItem(new String[]{Path.GetFileNameWithoutExtension(files[i]), files[i]})});
                }
                else { }
            }
            List<string> Expressions = new List<string>();

            for (int i = 0; i < lvFilelist.Items.Count; i++)
                Expressions.Add(lvFilelist.Items[i].SubItems[1].Text);
            
            lvFilelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


            Matrix XYZMatrix, RGBMatrix;
            using(cPersonManager manager = new cPersonManager(Clusterling(Expressions)))
            {
                Expressions.Clear();
                vProgressBarReset(5);

                lblMain.Text = "差分表情取得中...";
                manager.Substruct();
                if (pgbMain != null)
                    this.vProgressBarValueUp();

                lblMain.Text = "分離マトリクス生成中...";
                XYZMatrix = manager.GetMatrixFromShape();
                if (pgbMain != null)
                    this.vProgressBarValueUp();

                RGBMatrix = manager.GetMatrixFromTexture();
                if (pgbMain != null)
                    this.vProgressBarValueUp();
            }
            lblMain.Text = "主成分分析中...";

            using(cPersonPCAManager pcaXYZ = new cPersonPCAManager(XYZMatrix))
            {
                pcaXYZ.PCA();
                //pcaXYZ.PCADataSave();
                if (pgbMain != null)
                    this.vProgressBarValueUp();
            }

            using(cPersonPCAManager pcaRGB = new cPersonPCAManager(RGBMatrix))
            {
                pcaRGB.PCA();
                //pcaRGB.PCADataSave();
                if (pgbMain != null)
                    this.vProgressBarValueUp();
            }
            lblMain.Text = "...完了!";

            
        }

        /// <summary>
        /// ファイルネーム情報を基に各人物のグルーピングを行います。
        /// </summary>
        /// <param name="ExpressionPath"></param>
        /// <returns></returns>
        private List<cPerson> Clusterling(List<string> ExpressionPath)
        {
            vProgressBarReset(ExpressionPath.Count);
            lblMain.Text = "表情自動分類中...";

            List<cPerson> Expressions = new List<cPerson>();

            IEnumerable<IGrouping<string, string>> query = from Persons in ExpressionPath
                                                           group Persons by Path.GetFileNameWithoutExtension(Persons).Split('_')[2];

            foreach (var PersonGroup in query)
            {
                List<string> myGroup = new List<string>();
                foreach (var Persons in PersonGroup)
                {
                    myGroup.Add(Persons);

                    if (pgbMain != null)
                        this.vProgressBarValueUp();
                }
                Expressions.Add(new cPerson(PersonGroup.Key, myGroup));
            }
            lblMain.Text = "...分類完了!";

            return Expressions;
        }

        #region ProgressBar関連
        /// <summary>
        /// プログレスバーの値を増加させます。
        /// </summary>
        protected virtual void vProgressBarValueUp()
        {
            if (pgbMain != null)
            {
                pgbMain.PerformStep();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// プログレスバーをリセットします。
        /// </summary>
        /// <param name="iMax">最大値</param>
        protected virtual void vProgressBarReset(int iMax)
        {
            if (pgbMain != null)
            {
                pgbMain.Maximum = iMax;
                pgbMain.Minimum = 0;
                pgbMain.Value = 0;
                pgbMain.Step = 1;
            }
        }

        /// <summary>
        /// プログレスバーの設定をします。
        /// </summary>
        public virtual ToolStripProgressBar pgbProgressBar
        {
            set { pgbMain = value; }
        }
        #endregion
    }
}
