namespace MakeTeacherDataFromPCAData
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.AverageDataBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InVectDataList = new System.Windows.Forms.ListBox();
            this.DirectionVectDataList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.runBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nAverageDataBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EigenCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.EigenCount)).BeginInit();
            this.SuspendLayout();
            // 
            // AverageDataBox
            // 
            this.AverageDataBox.AllowDrop = true;
            this.AverageDataBox.Location = new System.Drawing.Point(16, 30);
            this.AverageDataBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AverageDataBox.Name = "AverageDataBox";
            this.AverageDataBox.Size = new System.Drawing.Size(420, 22);
            this.AverageDataBox.TabIndex = 0;
            this.AverageDataBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.AverageDataBox_DragDrop);
            this.AverageDataBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.AverageDataBox_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "主成分軸の原点データ(Average)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "射影対象のデータ";
            // 
            // InVectDataList
            // 
            this.InVectDataList.AllowDrop = true;
            this.InVectDataList.FormattingEnabled = true;
            this.InVectDataList.HorizontalScrollbar = true;
            this.InVectDataList.ItemHeight = 15;
            this.InVectDataList.Location = new System.Drawing.Point(231, 130);
            this.InVectDataList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.InVectDataList.Name = "InVectDataList";
            this.InVectDataList.Size = new System.Drawing.Size(205, 214);
            this.InVectDataList.Sorted = true;
            this.InVectDataList.TabIndex = 4;
            this.InVectDataList.DragDrop += new System.Windows.Forms.DragEventHandler(this.InVectDataList_DragDrop);
            this.InVectDataList.DragEnter += new System.Windows.Forms.DragEventHandler(this.InVectDataList_DragEnter);
            // 
            // DirectionVectDataList
            // 
            this.DirectionVectDataList.AllowDrop = true;
            this.DirectionVectDataList.FormattingEnabled = true;
            this.DirectionVectDataList.HorizontalScrollbar = true;
            this.DirectionVectDataList.ItemHeight = 15;
            this.DirectionVectDataList.Location = new System.Drawing.Point(19, 130);
            this.DirectionVectDataList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DirectionVectDataList.Name = "DirectionVectDataList";
            this.DirectionVectDataList.Size = new System.Drawing.Size(205, 214);
            this.DirectionVectDataList.Sorted = true;
            this.DirectionVectDataList.TabIndex = 5;
            this.DirectionVectDataList.DragDrop += new System.Windows.Forms.DragEventHandler(this.DirectionVectDataList_DragDrop);
            this.DirectionVectDataList.DragEnter += new System.Windows.Forms.DragEventHandler(this.DirectionVectDataList_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "方向ベクトル(EigenVectors)";
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(19, 380);
            this.runBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(417, 29);
            this.runBtn.TabIndex = 7;
            this.runBtn.Text = "驚異の全自動生成(-.-;)";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "真顔の平均データ(nAverage)";
            // 
            // nAverageDataBox
            // 
            this.nAverageDataBox.AllowDrop = true;
            this.nAverageDataBox.Location = new System.Drawing.Point(16, 76);
            this.nAverageDataBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nAverageDataBox.Name = "nAverageDataBox";
            this.nAverageDataBox.Size = new System.Drawing.Size(420, 22);
            this.nAverageDataBox.TabIndex = 9;
            this.nAverageDataBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.nAverageDataBox_DragDrop);
            this.nAverageDataBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.nAverageDataBox_DragEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(346, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "使用する主成分の数(上限は方向ベクトルの数になります)";
            // 
            // EigenCount
            // 
            this.EigenCount.Location = new System.Drawing.Point(368, 354);
            this.EigenCount.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EigenCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EigenCount.Name = "EigenCount";
            this.EigenCount.Size = new System.Drawing.Size(68, 22);
            this.EigenCount.TabIndex = 11;
            this.EigenCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 422);
            this.Controls.Add(this.EigenCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nAverageDataBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DirectionVectDataList);
            this.Controls.Add(this.InVectDataList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AverageDataBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MakeTeacherData";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EigenCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AverageDataBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox InVectDataList;
        private System.Windows.Forms.ListBox DirectionVectDataList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nAverageDataBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown EigenCount;
    }
}

