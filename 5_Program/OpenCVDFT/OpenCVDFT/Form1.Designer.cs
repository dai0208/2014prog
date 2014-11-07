namespace OpenCVDFT
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.pbxBefore = new System.Windows.Forms.PictureBox();
            this.pbxAfter = new System.Windows.Forms.PictureBox();
            this.btnFFT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAfter)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 218);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 34);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "画像読み込み";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pbxBefore
            // 
            this.pbxBefore.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxBefore.Location = new System.Drawing.Point(12, 12);
            this.pbxBefore.Name = "pbxBefore";
            this.pbxBefore.Size = new System.Drawing.Size(200, 200);
            this.pbxBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxBefore.TabIndex = 2;
            this.pbxBefore.TabStop = false;
            // 
            // pbxAfter
            // 
            this.pbxAfter.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxAfter.Location = new System.Drawing.Point(218, 12);
            this.pbxAfter.Name = "pbxAfter";
            this.pbxAfter.Size = new System.Drawing.Size(200, 200);
            this.pbxAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAfter.TabIndex = 3;
            this.pbxAfter.TabStop = false;
            // 
            // btnFFT
            // 
            this.btnFFT.Location = new System.Drawing.Point(218, 218);
            this.btnFFT.Name = "btnFFT";
            this.btnFFT.Size = new System.Drawing.Size(200, 34);
            this.btnFFT.TabIndex = 4;
            this.btnFFT.Text = "スペクトル計算";
            this.btnFFT.UseVisualStyleBackColor = true;
            this.btnFFT.Click += new System.EventHandler(this.btnFFT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 266);
            this.Controls.Add(this.btnFFT);
            this.Controls.Add(this.pbxAfter);
            this.Controls.Add(this.pbxBefore);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAfter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.PictureBox pbxBefore;
        private System.Windows.Forms.PictureBox pbxAfter;
        private System.Windows.Forms.Button btnFFT;
    }
}

