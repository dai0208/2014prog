using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoPCA
{
    public partial class fmMain : Form
    {
        DoPCA.PCABaseManager PCAManager;

        /// <summary>
        /// PCAManagerから派生させたPCAManagerを指定してインスタンスを作成してください。
        /// </summary>
        /// <param name="PCAManager">PCAManagerを継承したPCAマネージャインスタンス</param>
        public fmMain(DoPCA.PCABaseManager PCAManager)
        {
            InitializeComponent();
            this.PCAManager = PCAManager;
        }

        #region D&Dの設定
        private void clbFileName_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void clbFileName_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string strFileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                    lbFileName.Items.Add(strFileName);
                stbLabel.Text = lbFileName.Items.Count.ToString() + "個のファイルがあります。";
            }
        }
        #endregion

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbFileName.Items.Count; i++)
                lbFileName.SetSelected(i, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            while (lbFileName.SelectedItems.Count > 0)
                lbFileName.Items.RemoveAt(lbFileName.SelectedIndex);

            stbLabel.Text = lbFileName.Items.Count.ToString() + "個のファイルがあります。";
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            List<string> FileList = new List<string>();
            foreach (string FileName in lbFileName.Items)
                FileList.Add(FileName);

            this.PCAManager.FileList = FileList;
            pnlButton.Enabled = false;

            DoPCA.PCAData PCAData = PCAManager.GetPCAData();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Mtxファイル(*.mtx)|*.mtx";
            sfd.Title = "主成分分析データを保存するファイルを指定してください。";
            if (sfd.ShowDialog() == DialogResult.OK)
                if(PCAData.DataSave(sfd.FileName) == true)
                    MessageBox.Show(sfd.FileName+"\nに主成分分析データを保存しました。","保存成功",MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("保存に失敗しました。","保存失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);

            pnlButton.Enabled = true;
        }
    }
}
