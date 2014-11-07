namespace SVMClient
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveASCFile = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.LearningBtn = new System.Windows.Forms.Button();
            this.TransformType2Btn = new System.Windows.Forms.Button();
            this.WeightController = new System.Windows.Forms.TrackBar();
            this.WeightValueBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DoClassifierBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxUnknownData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WeightController)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.出力ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(445, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 出力ToolStripMenuItem
            // 
            this.出力ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveASCFile});
            this.出力ToolStripMenuItem.Name = "出力ToolStripMenuItem";
            this.出力ToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.出力ToolStripMenuItem.Text = "出力";
            // 
            // SaveASCFile
            // 
            this.SaveASCFile.Name = "SaveASCFile";
            this.SaveASCFile.Size = new System.Drawing.Size(215, 22);
            this.SaveASCFile.Text = "変換後ascパラメータ出力";
            this.SaveASCFile.Click += new System.EventHandler(this.SaveASCFile_Click);
            // 
            // listBox2
            // 
            this.listBox2.AllowDrop = true;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(8, 34);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(196, 64);
            this.listBox2.Sorted = true;
            this.listBox2.TabIndex = 2;
            this.listBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
            this.listBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox2_DragEnter);
            // 
            // listBox3
            // 
            this.listBox3.AllowDrop = true;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.HorizontalScrollbar = true;
            this.listBox3.ItemHeight = 12;
            this.listBox3.Location = new System.Drawing.Point(8, 116);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(196, 64);
            this.listBox3.Sorted = true;
            this.listBox3.TabIndex = 3;
            this.listBox3.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox3_DragDrop);
            this.listBox3.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox3_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "教師データ(True)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "教師データ(False)";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(196, 175);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // LearningBtn
            // 
            this.LearningBtn.Location = new System.Drawing.Point(8, 186);
            this.LearningBtn.Name = "LearningBtn";
            this.LearningBtn.Size = new System.Drawing.Size(196, 23);
            this.LearningBtn.TabIndex = 9;
            this.LearningBtn.Text = "学習開始";
            this.LearningBtn.UseVisualStyleBackColor = true;
            this.LearningBtn.Click += new System.EventHandler(this.LearningBtn_Click);
            // 
            // TransformType2Btn
            // 
            this.TransformType2Btn.Enabled = false;
            this.TransformType2Btn.Location = new System.Drawing.Point(8, 291);
            this.TransformType2Btn.Name = "TransformType2Btn";
            this.TransformType2Btn.Size = new System.Drawing.Size(196, 23);
            this.TransformType2Btn.TabIndex = 11;
            this.TransformType2Btn.Text = "変換開始";
            this.TransformType2Btn.UseVisualStyleBackColor = true;
            this.TransformType2Btn.Click += new System.EventHandler(this.TransformType2Btn_Click);
            // 
            // WeightController
            // 
            this.WeightController.Location = new System.Drawing.Point(8, 222);
            this.WeightController.Minimum = -10;
            this.WeightController.Name = "WeightController";
            this.WeightController.Size = new System.Drawing.Size(196, 45);
            this.WeightController.TabIndex = 12;
            this.WeightController.Scroll += new System.EventHandler(this.WeightControllBar_Scroll);
            // 
            // WeightValueBox
            // 
            this.WeightValueBox.Location = new System.Drawing.Point(8, 266);
            this.WeightValueBox.Name = "WeightValueBox";
            this.WeightValueBox.ReadOnly = true;
            this.WeightValueBox.Size = new System.Drawing.Size(196, 19);
            this.WeightValueBox.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LearningBtn);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.listBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 216);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SVM学習";
            // 
            // DoClassifierBtn
            // 
            this.DoClassifierBtn.Location = new System.Drawing.Point(8, 69);
            this.DoClassifierBtn.Name = "DoClassifierBtn";
            this.DoClassifierBtn.Size = new System.Drawing.Size(196, 23);
            this.DoClassifierBtn.TabIndex = 17;
            this.DoClassifierBtn.Text = "識別開始";
            this.DoClassifierBtn.UseVisualStyleBackColor = true;
            this.DoClassifierBtn.Click += new System.EventHandler(this.DoClassifierBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxUnknownData);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DoClassifierBtn);
            this.groupBox2.Location = new System.Drawing.Point(8, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 101);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SVM識別";
            // 
            // tbxUnknownData
            // 
            this.tbxUnknownData.AllowDrop = true;
            this.tbxUnknownData.Location = new System.Drawing.Point(8, 44);
            this.tbxUnknownData.Name = "tbxUnknownData";
            this.tbxUnknownData.Size = new System.Drawing.Size(196, 19);
            this.tbxUnknownData.TabIndex = 20;
            this.tbxUnknownData.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbxUnknownData_DragDrop);
            this.tbxUnknownData.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbxUnknownData_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "未知データ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(this.WeightController);
            this.groupBox3.Controls.Add(this.WeightValueBox);
            this.groupBox3.Controls.Add(this.TransformType2Btn);
            this.groupBox3.Location = new System.Drawing.Point(224, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 323);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "印象変換";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "出力結果";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 362);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WeightController)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 出力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveASCFile;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button LearningBtn;
        private System.Windows.Forms.Button TransformType2Btn;
        private System.Windows.Forms.TrackBar WeightController;
        private System.Windows.Forms.TextBox WeightValueBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DoClassifierBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxUnknownData;
    }
}

