namespace DoPCA
{
    partial class fmMain
    {
        /// <summary>
        /// 必要なデザイナ変数です。
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFileName = new System.Windows.Forms.ListBox();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.pgbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.stbLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.stbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFileName
            // 
            this.lbFileName.AllowDrop = true;
            this.lbFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFileName.FormattingEnabled = true;
            this.lbFileName.ItemHeight = 12;
            this.lbFileName.Location = new System.Drawing.Point(0, 0);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFileName.Size = new System.Drawing.Size(586, 261);
            this.lbFileName.TabIndex = 10;
            this.lbFileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.clbFileName_DragDrop);
            this.lbFileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.clbFileName_DragEnter);
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbMain,
            this.stbLabel});
            this.stbMain.Location = new System.Drawing.Point(0, 293);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(586, 23);
            this.stbMain.TabIndex = 9;
            // 
            // pgbMain
            // 
            this.pgbMain.Name = "pgbMain";
            this.pgbMain.Size = new System.Drawing.Size(240, 17);
            // 
            // stbLabel
            // 
            this.stbLabel.Name = "stbLabel";
            this.stbLabel.Size = new System.Drawing.Size(208, 18);
            this.stbLabel.Text = "リストボックスにファイルをドロップしてください。";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(84, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(312, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "分析実行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(3, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "すべて選択";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbFileName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 261);
            this.panel1.TabIndex = 11;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnDelete);
            this.pnlButton.Controls.Add(this.btnSelectAll);
            this.pnlButton.Controls.Add(this.btnRun);
            this.pnlButton.Location = new System.Drawing.Point(93, 262);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(391, 30);
            this.pnlButton.TabIndex = 11;
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 316);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbMain);
            this.Name = "fmMain";
            this.Text = "Form1";
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFileName;
        private System.Windows.Forms.StatusStrip stbMain;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripProgressBar pgbMain;
        private System.Windows.Forms.ToolStripStatusLabel stbLabel;
        private System.Windows.Forms.Panel pnlButton;

    }
}

