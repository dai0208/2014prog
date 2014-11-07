namespace Normalizer
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
            this.pbxBefore = new System.Windows.Forms.PictureBox();
            this.pbxAfter = new System.Windows.Forms.PictureBox();
            this.nmrMean = new System.Windows.Forms.NumericUpDown();
            this.nmrDiv = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxMean = new System.Windows.Forms.TextBox();
            this.tbxDiv = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCalcDivMean = new System.Windows.Forms.Button();
            this.btnNormalize = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxBfDiv = new System.Windows.Forms.TextBox();
            this.tbxBfMean = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDiv)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxBefore
            // 
            this.pbxBefore.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxBefore.Location = new System.Drawing.Point(15, 19);
            this.pbxBefore.Name = "pbxBefore";
            this.pbxBefore.Size = new System.Drawing.Size(200, 200);
            this.pbxBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxBefore.TabIndex = 0;
            this.pbxBefore.TabStop = false;
            // 
            // pbxAfter
            // 
            this.pbxAfter.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxAfter.Location = new System.Drawing.Point(261, 19);
            this.pbxAfter.Name = "pbxAfter";
            this.pbxAfter.Size = new System.Drawing.Size(200, 200);
            this.pbxAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAfter.TabIndex = 1;
            this.pbxAfter.TabStop = false;
            // 
            // nmrMean
            // 
            this.nmrMean.DecimalPlaces = 2;
            this.nmrMean.Location = new System.Drawing.Point(259, 253);
            this.nmrMean.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmrMean.Name = "nmrMean";
            this.nmrMean.Size = new System.Drawing.Size(101, 19);
            this.nmrMean.TabIndex = 2;
            // 
            // nmrDiv
            // 
            this.nmrDiv.DecimalPlaces = 2;
            this.nmrDiv.Location = new System.Drawing.Point(365, 253);
            this.nmrDiv.Name = "nmrDiv";
            this.nmrDiv.Size = new System.Drawing.Size(101, 19);
            this.nmrDiv.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "正規化後平均値";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "正規化後標準偏差";
            // 
            // tbxMean
            // 
            this.tbxMean.Location = new System.Drawing.Point(261, 296);
            this.tbxMean.Name = "tbxMean";
            this.tbxMean.ReadOnly = true;
            this.tbxMean.Size = new System.Drawing.Size(95, 19);
            this.tbxMean.TabIndex = 6;
            // 
            // tbxDiv
            // 
            this.tbxDiv.Location = new System.Drawing.Point(366, 296);
            this.tbxDiv.Name = "tbxDiv";
            this.tbxDiv.ReadOnly = true;
            this.tbxDiv.Size = new System.Drawing.Size(95, 19);
            this.tbxDiv.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "平均値";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(365, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "標準偏差";
            // 
            // btnCalcDivMean
            // 
            this.btnCalcDivMean.Location = new System.Drawing.Point(15, 327);
            this.btnCalcDivMean.Name = "btnCalcDivMean";
            this.btnCalcDivMean.Size = new System.Drawing.Size(200, 23);
            this.btnCalcDivMean.TabIndex = 10;
            this.btnCalcDivMean.Text = "画像読み込み";
            this.btnCalcDivMean.UseVisualStyleBackColor = true;
            this.btnCalcDivMean.Click += new System.EventHandler(this.btnCalcDivMean_Click);
            // 
            // btnNormalize
            // 
            this.btnNormalize.Location = new System.Drawing.Point(261, 327);
            this.btnNormalize.Name = "btnNormalize";
            this.btnNormalize.Size = new System.Drawing.Size(200, 23);
            this.btnNormalize.TabIndex = 11;
            this.btnNormalize.Text = "濃淡の正規化";
            this.btnNormalize.UseVisualStyleBackColor = true;
            this.btnNormalize.Click += new System.EventHandler(this.btnNormalize_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "標準偏差";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "平均値";
            // 
            // tbxBfDiv
            // 
            this.tbxBfDiv.Location = new System.Drawing.Point(120, 296);
            this.tbxBfDiv.Name = "tbxBfDiv";
            this.tbxBfDiv.ReadOnly = true;
            this.tbxBfDiv.Size = new System.Drawing.Size(95, 19);
            this.tbxBfDiv.TabIndex = 13;
            // 
            // tbxBfMean
            // 
            this.tbxBfMean.Location = new System.Drawing.Point(15, 296);
            this.tbxBfMean.Name = "tbxBfMean";
            this.tbxBfMean.ReadOnly = true;
            this.tbxBfMean.Size = new System.Drawing.Size(95, 19);
            this.tbxBfMean.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxBfDiv);
            this.Controls.Add(this.tbxBfMean);
            this.Controls.Add(this.btnNormalize);
            this.Controls.Add(this.btnCalcDivMean);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxDiv);
            this.Controls.Add(this.tbxMean);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmrDiv);
            this.Controls.Add(this.nmrMean);
            this.Controls.Add(this.pbxAfter);
            this.Controls.Add(this.pbxBefore);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDiv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxBefore;
        private System.Windows.Forms.PictureBox pbxAfter;
        private System.Windows.Forms.NumericUpDown nmrMean;
        private System.Windows.Forms.NumericUpDown nmrDiv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxMean;
        private System.Windows.Forms.TextBox tbxDiv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCalcDivMean;
        private System.Windows.Forms.Button btnNormalize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxBfDiv;
        private System.Windows.Forms.TextBox tbxBfMean;
    }
}

