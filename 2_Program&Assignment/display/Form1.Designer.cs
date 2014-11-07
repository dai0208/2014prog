namespace display
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
            this.btnOK = new System.Windows.Forms.Button();
            this.pbxShow = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGrayScale = new System.Windows.Forms.Button();
            this.btnNegaPosi = new System.Windows.Forms.Button();
            this.btnMosaic = new System.Windows.Forms.Button();
            this.nmcMosaWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nmcMosaHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcMosaWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcMosaHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(0, 227);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(142, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "参照";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pbxShow
            // 
            this.pbxShow.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbxShow.Location = new System.Drawing.Point(0, 0);
            this.pbxShow.Name = "pbxShow";
            this.pbxShow.Size = new System.Drawing.Size(284, 221);
            this.pbxShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxShow.TabIndex = 2;
            this.pbxShow.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(148, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGrayScale
            // 
            this.btnGrayScale.Location = new System.Drawing.Point(148, 256);
            this.btnGrayScale.Name = "btnGrayScale";
            this.btnGrayScale.Size = new System.Drawing.Size(136, 23);
            this.btnGrayScale.TabIndex = 4;
            this.btnGrayScale.Text = "ｸﾞﾚｲｽｹｰﾙ化";
            this.btnGrayScale.UseVisualStyleBackColor = true;
            this.btnGrayScale.Click += new System.EventHandler(this.btnGrayScale_Click);
            // 
            // btnNegaPosi
            // 
            this.btnNegaPosi.Location = new System.Drawing.Point(0, 256);
            this.btnNegaPosi.Name = "btnNegaPosi";
            this.btnNegaPosi.Size = new System.Drawing.Size(142, 23);
            this.btnNegaPosi.TabIndex = 5;
            this.btnNegaPosi.Text = "ﾈｶﾞﾎﾟｼﾞ";
            this.btnNegaPosi.UseVisualStyleBackColor = true;
            this.btnNegaPosi.Click += new System.EventHandler(this.btnNegaPosi_Click);
            // 
            // btnMosaic
            // 
            this.btnMosaic.Location = new System.Drawing.Point(0, 285);
            this.btnMosaic.Name = "btnMosaic";
            this.btnMosaic.Size = new System.Drawing.Size(142, 23);
            this.btnMosaic.TabIndex = 6;
            this.btnMosaic.Text = "ﾓｻﾞｲｸ";
            this.btnMosaic.UseVisualStyleBackColor = true;
            this.btnMosaic.Click += new System.EventHandler(this.btnMosaic_Click);
            // 
            // nmcMosaWidth
            // 
            this.nmcMosaWidth.Location = new System.Drawing.Point(175, 288);
            this.nmcMosaWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmcMosaWidth.Name = "nmcMosaWidth";
            this.nmcMosaWidth.Size = new System.Drawing.Size(41, 19);
            this.nmcMosaWidth.TabIndex = 7;
            this.nmcMosaWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "横";
            // 
            // nmcMosaHeight
            // 
            this.nmcMosaHeight.Location = new System.Drawing.Point(243, 289);
            this.nmcMosaHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmcMosaHeight.Name = "nmcMosaHeight";
            this.nmcMosaHeight.Size = new System.Drawing.Size(41, 19);
            this.nmcMosaHeight.TabIndex = 9;
            this.nmcMosaHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "縦";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 306);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nmcMosaHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmcMosaWidth);
            this.Controls.Add(this.btnMosaic);
            this.Controls.Add(this.btnNegaPosi);
            this.Controls.Add(this.btnGrayScale);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pbxShow);
            this.Controls.Add(this.btnOK);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcMosaWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcMosaHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbxShow;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGrayScale;
        private System.Windows.Forms.Button btnNegaPosi;
        private System.Windows.Forms.Button btnMosaic;
        private System.Windows.Forms.NumericUpDown nmcMosaWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmcMosaHeight;
        private System.Windows.Forms.Label label2;
    }
}

