namespace BmpPCA
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
            this.lbxBitmaps = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lvPCAParam = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.pbxEigenFace = new System.Windows.Forms.PictureBox();
            this.cbEigenFaces = new System.Windows.Forms.ComboBox();
            this.lvMatrix = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEigenFace)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxBitmaps
            // 
            this.lbxBitmaps.AllowDrop = true;
            this.lbxBitmaps.FormattingEnabled = true;
            this.lbxBitmaps.ItemHeight = 12;
            this.lbxBitmaps.Location = new System.Drawing.Point(12, 31);
            this.lbxBitmaps.Name = "lbxBitmaps";
            this.lbxBitmaps.Size = new System.Drawing.Size(311, 100);
            this.lbxBitmaps.Sorted = true;
            this.lbxBitmaps.TabIndex = 0;
            this.lbxBitmaps.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxBitmap_DragDrop);
            this.lbxBitmaps.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxBitmap_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "辞書用顔画像";
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRun.Location = new System.Drawing.Point(12, 137);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(311, 30);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "主成分分析";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(326, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "パラメータ";
            // 
            // lvPCAParam
            // 
            this.lvPCAParam.Location = new System.Drawing.Point(329, 257);
            this.lvPCAParam.Name = "lvPCAParam";
            this.lvPCAParam.Size = new System.Drawing.Size(211, 199);
            this.lvPCAParam.TabIndex = 5;
            this.lvPCAParam.UseCompatibleStateImageBehavior = false;
            this.lvPCAParam.View = System.Windows.Forms.View.Details;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(13, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "基底(固有顔)";
            // 
            // pbxEigenFace
            // 
            this.pbxEigenFace.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxEigenFace.Location = new System.Drawing.Point(329, 31);
            this.pbxEigenFace.Name = "pbxEigenFace";
            this.pbxEigenFace.Size = new System.Drawing.Size(200, 200);
            this.pbxEigenFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxEigenFace.TabIndex = 8;
            this.pbxEigenFace.TabStop = false;
            // 
            // cbEigenFaces
            // 
            this.cbEigenFaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEigenFaces.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cbEigenFaces.FormattingEnabled = true;
            this.cbEigenFaces.Location = new System.Drawing.Point(12, 205);
            this.cbEigenFaces.Name = "cbEigenFaces";
            this.cbEigenFaces.Size = new System.Drawing.Size(311, 26);
            this.cbEigenFaces.TabIndex = 9;
            this.cbEigenFaces.SelectedIndexChanged += new System.EventHandler(this.cbEigenFaces_SelectedIndexChanged);
            // 
            // lvMatrix
            // 
            this.lvMatrix.Location = new System.Drawing.Point(12, 257);
            this.lvMatrix.Name = "lvMatrix";
            this.lvMatrix.Size = new System.Drawing.Size(311, 199);
            this.lvMatrix.TabIndex = 10;
            this.lvMatrix.UseCompatibleStateImageBehavior = false;
            this.lvMatrix.View = System.Windows.Forms.View.Details;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(406, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "固有顔";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(13, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(281, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "L行列　※平均ベクトルはdouble型としています。";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 468);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lvMatrix);
            this.Controls.Add(this.cbEigenFaces);
            this.Controls.Add(this.pbxEigenFace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvPCAParam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxBitmaps);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxEigenFace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxBitmaps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvPCAParam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbxEigenFace;
        private System.Windows.Forms.ComboBox cbEigenFaces;
        private System.Windows.Forms.ListView lvMatrix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

