namespace PCAforASCII
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
            this.lvFilelist = new System.Windows.Forms.ListView();
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbDictionary = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.pgbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.lblMain = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFilelist
            // 
            this.lvFilelist.AllowDrop = true;
            this.lvFilelist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chFilePath});
            this.lvFilelist.Location = new System.Drawing.Point(14, 32);
            this.lvFilelist.Name = "lvFilelist";
            this.lvFilelist.Size = new System.Drawing.Size(490, 256);
            this.lvFilelist.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvFilelist.TabIndex = 2;
            this.lvFilelist.UseCompatibleStateImageBehavior = false;
            this.lvFilelist.View = System.Windows.Forms.View.Details;
            this.lvFilelist.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFilelist_DragDrop);
            this.lvFilelist.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvFilelist_DragEnter);
            // 
            // chFileName
            // 
            this.chFileName.Text = "ファイル名";
            // 
            // chFilePath
            // 
            this.chFilePath.Text = "ファイルパス";
            // 
            // gbDictionary
            // 
            this.gbDictionary.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gbDictionary.Location = new System.Drawing.Point(8, 10);
            this.gbDictionary.Name = "gbDictionary";
            this.gbDictionary.Size = new System.Drawing.Size(500, 286);
            this.gbDictionary.TabIndex = 10;
            this.gbDictionary.TabStop = false;
            this.gbDictionary.Text = "再サンプリング後の点群データ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbMain,
            this.lblMain});
            this.toolStrip1.Location = new System.Drawing.Point(0, 301);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(516, 25);
            this.toolStrip1.TabIndex = 12;
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
            this.lblMain.Size = new System.Drawing.Size(296, 18);
            this.lblMain.Text = "自動でパラメータ・固有値固有ベクトルを算出します";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 326);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lvFilelist);
            this.Controls.Add(this.gbDictionary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "PCAforASCII";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvFilelist;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chFilePath;
        private System.Windows.Forms.GroupBox gbDictionary;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripProgressBar pgbMain;
        private System.Windows.Forms.ToolStripLabel lblMain;

    }
}

