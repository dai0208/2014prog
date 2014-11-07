namespace ITVMMovieMaker
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.pgbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.lblMain = new System.Windows.Forms.ToolStripLabel();
            this.lbxMain = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMakeBitmap = new System.Windows.Forms.Button();
            this.pbxPreView = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnMakeMovie = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.tbxNowFrame = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbMain,
            this.lblMain});
            this.toolStrip1.Location = new System.Drawing.Point(0, 318);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(547, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // pgbMain
            // 
            this.pgbMain.Name = "pgbMain";
            this.pgbMain.Size = new System.Drawing.Size(200, 22);
            // 
            // lblMain
            // 
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(306, 22);
            this.lblMain.Text = "テクスチャ付きASCIIファイルをドロップしてください";
            // 
            // lbxMain
            // 
            this.lbxMain.AllowDrop = true;
            this.lbxMain.FormattingEnabled = true;
            this.lbxMain.ItemHeight = 18;
            this.lbxMain.Location = new System.Drawing.Point(7, 47);
            this.lbxMain.Name = "lbxMain";
            this.lbxMain.Size = new System.Drawing.Size(218, 220);
            this.lbxMain.Sorted = true;
            this.lbxMain.TabIndex = 1;
            this.lbxMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxMain_DragDrop);
            this.lbxMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxMain_DragEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnMakeBitmap);
            this.groupBox1.Controls.Add(this.lbxMain);
            this.groupBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(5, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 314);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "フレーム用データ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "3D ASCIIデータ";
            // 
            // btnMakeBitmap
            // 
            this.btnMakeBitmap.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMakeBitmap.Location = new System.Drawing.Point(7, 276);
            this.btnMakeBitmap.Name = "btnMakeBitmap";
            this.btnMakeBitmap.Size = new System.Drawing.Size(219, 28);
            this.btnMakeBitmap.TabIndex = 3;
            this.btnMakeBitmap.Text = "動画用ファイル生成";
            this.btnMakeBitmap.UseVisualStyleBackColor = true;
            this.btnMakeBitmap.Click += new System.EventHandler(this.btnMakeBitmap_Click);
            // 
            // pbxPreView
            // 
            this.pbxPreView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxPreView.Location = new System.Drawing.Point(280, 22);
            this.pbxPreView.Name = "pbxPreView";
            this.pbxPreView.Size = new System.Drawing.Size(226, 226);
            this.pbxPreView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxPreView.TabIndex = 4;
            this.pbxPreView.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(244, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 18);
            this.label11.TabIndex = 23;
            this.label11.Text = "プレビュー";
            // 
            // btnMakeMovie
            // 
            this.btnMakeMovie.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMakeMovie.Location = new System.Drawing.Point(477, 277);
            this.btnMakeMovie.Name = "btnMakeMovie";
            this.btnMakeMovie.Size = new System.Drawing.Size(63, 28);
            this.btnMakeMovie.TabIndex = 23;
            this.btnMakeMovie.Text = "出力";
            this.btnMakeMovie.UseVisualStyleBackColor = true;
            this.btnMakeMovie.Click += new System.EventHandler(this.btnMakeMovie_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(244, 113);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(30, 48);
            this.btnLeft.TabIndex = 24;
            this.btnLeft.Text = "<<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(512, 113);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(30, 48);
            this.btnRight.TabIndex = 25;
            this.btnRight.Text = ">>";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // tbxNowFrame
            // 
            this.tbxNowFrame.Location = new System.Drawing.Point(384, 256);
            this.tbxNowFrame.Name = "tbxNowFrame";
            this.tbxNowFrame.ReadOnly = true;
            this.tbxNowFrame.Size = new System.Drawing.Size(122, 19);
            this.tbxNowFrame.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(286, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 18);
            this.label2.TabIndex = 27;
            this.label2.Text = "フレーム番号：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 343);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxNowFrame);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnMakeMovie);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pbxPreView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "ITVMMovieMaker";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripProgressBar pgbMain;
        private System.Windows.Forms.ToolStripLabel lblMain;
        private System.Windows.Forms.ListBox lbxMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMakeBitmap;
        private System.Windows.Forms.PictureBox pbxPreView;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnMakeMovie;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.TextBox tbxNowFrame;
        private System.Windows.Forms.Label label2;
    }
}

