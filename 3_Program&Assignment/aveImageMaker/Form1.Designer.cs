namespace aveImageMaker
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
            this.pbxDstImage = new System.Windows.Forms.PictureBox();
            this.btnCalcAverage = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lbxItems = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDstImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxDstImage
            // 
            this.pbxDstImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxDstImage.Location = new System.Drawing.Point(12, 12);
            this.pbxDstImage.Name = "pbxDstImage";
            this.pbxDstImage.Size = new System.Drawing.Size(100, 100);
            this.pbxDstImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxDstImage.TabIndex = 2;
            this.pbxDstImage.TabStop = false;
            // 
            // btnCalcAverage
            // 
            this.btnCalcAverage.Location = new System.Drawing.Point(172, 89);
            this.btnCalcAverage.Name = "btnCalcAverage";
            this.btnCalcAverage.Size = new System.Drawing.Size(100, 23);
            this.btnCalcAverage.TabIndex = 3;
            this.btnCalcAverage.Text = "平均化";
            this.btnCalcAverage.UseVisualStyleBackColor = true;
            this.btnCalcAverage.Click += new System.EventHandler(this.btnCalcAverage_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(172, 60);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "読み込み";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lbxItems
            // 
            this.lbxItems.FormattingEnabled = true;
            this.lbxItems.HorizontalScrollbar = true;
            this.lbxItems.ItemHeight = 12;
            this.lbxItems.Location = new System.Drawing.Point(12, 120);
            this.lbxItems.Name = "lbxItems";
            this.lbxItems.Size = new System.Drawing.Size(260, 124);
            this.lbxItems.Sorted = true;
            this.lbxItems.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbxItems);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnCalcAverage);
            this.Controls.Add(this.pbxDstImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxDstImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxDstImage;
        private System.Windows.Forms.Button btnCalcAverage;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListBox lbxItems;
    }
}

