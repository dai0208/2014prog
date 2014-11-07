namespace CSVMaker
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
            this.lbxItems = new System.Windows.Forms.ListBox();
            this.button_Run = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxItems
            // 
            this.lbxItems.AllowDrop = true;
            this.lbxItems.FormattingEnabled = true;
            this.lbxItems.ItemHeight = 12;
            this.lbxItems.Location = new System.Drawing.Point(12, 12);
            this.lbxItems.Name = "lbxItems";
            this.lbxItems.Size = new System.Drawing.Size(259, 208);
            this.lbxItems.TabIndex = 0;
            this.lbxItems.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxItems_DragDrop);
            this.lbxItems.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxItems_DragEnter);
            // 
            // button_Run
            // 
            this.button_Run.Location = new System.Drawing.Point(12, 226);
            this.button_Run.Name = "button_Run";
            this.button_Run.Size = new System.Drawing.Size(259, 32);
            this.button_Run.TabIndex = 1;
            this.button_Run.Text = "CSV変換";
            this.button_Run.UseVisualStyleBackColor = true;
            this.button_Run.Click += new System.EventHandler(this.button_Run_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button_Run);
            this.Controls.Add(this.lbxItems);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxItems;
        private System.Windows.Forms.Button button_Run;
    }
}

