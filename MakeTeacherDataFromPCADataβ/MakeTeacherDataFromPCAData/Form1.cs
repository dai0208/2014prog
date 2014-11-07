using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MakeTeacherDataFromPCAData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InVectDataList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void AverageDataBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void DirectionVectDataList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void nAverageDataBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void nAverageDataBox_DragDrop(object sender, DragEventArgs e)
        {
            nAverageDataBox.Text = "";
            string[] folder = (string[])e.Data.GetData(DataFormats.FileDrop);
            nAverageDataBox.Text = folder[0];
        }

        private void InVectDataList_DragDrop(object sender, DragEventArgs e)
        {
            InVectDataList.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            InVectDataList.Items.AddRange(files);
        }

        private void AverageDataBox_DragDrop(object sender, DragEventArgs e)
        {
            AverageDataBox.Text = "";
            string[] folder = (string[])e.Data.GetData(DataFormats.FileDrop);
            AverageDataBox.Text = folder[0];
        }

        private void DirectionVectDataList_DragDrop(object sender, DragEventArgs e)
        {
            DirectionVectDataList.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            DirectionVectDataList.Items.AddRange(files);
            EigenCount.Maximum = files.Length;
            EigenCount.Value = files.Length;
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            MakeParams MP = new MakeParams(getItem(InVectDataList));
            MP.setEigenVectorCount = (int)EigenCount.Value;
            MP.DoOperation(InVectDataList, DirectionVectDataList, AverageDataBox, nAverageDataBox);
            MessageBox.Show("完了！");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("平均ベクトルと入力データの長さは統一して使用してください。","使用前の注意",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

       
        /// <summary>
        /// リストボックスのアイテム名をstring[]変換して出力します。
        /// </summary>
        /// <param name="lst">リストボックスのコントロール</param>
        /// <returns></returns>
        private string[] getItem(ListBox lst)
        {
            string[] items = new string[lst.Items.Count];
            for (int i = 0; i < lst.Items.Count; i++)
            {
                items[i] = (string)lst.Items[i];
            }
            return items;
        }
    }
}
