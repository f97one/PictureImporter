namespace PictureImporter
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxImportDir = new System.Windows.Forms.TextBox();
            this.buttonImportBrowse = new System.Windows.Forms.Button();
            this.buttonExportBrowse = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonExec = new System.Windows.Forms.Button();
            this.groupBoxTreatSameFile = new System.Windows.Forms.GroupBox();
            this.radioButtonOverwrite = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialogImport = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogExport = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxTreatSameFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "入力フォルダ：";
            // 
            // textBoxImportDir
            // 
            this.textBoxImportDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImportDir.Location = new System.Drawing.Point(89, 10);
            this.textBoxImportDir.Name = "textBoxImportDir";
            this.textBoxImportDir.Size = new System.Drawing.Size(329, 19);
            this.textBoxImportDir.TabIndex = 1;
            // 
            // buttonImportBrowse
            // 
            this.buttonImportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImportBrowse.Location = new System.Drawing.Point(425, 8);
            this.buttonImportBrowse.Name = "buttonImportBrowse";
            this.buttonImportBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonImportBrowse.TabIndex = 2;
            this.buttonImportBrowse.Text = "参照...(&I)";
            this.buttonImportBrowse.UseVisualStyleBackColor = true;
            // 
            // buttonExportBrowse
            // 
            this.buttonExportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportBrowse.Location = new System.Drawing.Point(425, 38);
            this.buttonExportBrowse.Name = "buttonExportBrowse";
            this.buttonExportBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonExportBrowse.TabIndex = 3;
            this.buttonExportBrowse.Text = "参照...(&O)";
            this.buttonExportBrowse.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(89, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(329, 19);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "出力フォルダ：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(89, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "[マイ ピクチャ](&P)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonExec
            // 
            this.buttonExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExec.Location = new System.Drawing.Point(417, 157);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(83, 23);
            this.buttonExec.TabIndex = 7;
            this.buttonExec.Text = "コピー開始(&E)";
            this.buttonExec.UseVisualStyleBackColor = true;
            // 
            // groupBoxTreatSameFile
            // 
            this.groupBoxTreatSameFile.Controls.Add(this.radioButton2);
            this.groupBoxTreatSameFile.Controls.Add(this.radioButton1);
            this.groupBoxTreatSameFile.Controls.Add(this.radioButtonOverwrite);
            this.groupBoxTreatSameFile.Location = new System.Drawing.Point(15, 87);
            this.groupBoxTreatSameFile.Name = "groupBoxTreatSameFile";
            this.groupBoxTreatSameFile.Size = new System.Drawing.Size(137, 92);
            this.groupBoxTreatSameFile.TabIndex = 8;
            this.groupBoxTreatSameFile.TabStop = false;
            this.groupBoxTreatSameFile.Text = "同名ファイルの処理";
            // 
            // radioButtonOverwrite
            // 
            this.radioButtonOverwrite.AutoSize = true;
            this.radioButtonOverwrite.Location = new System.Drawing.Point(7, 19);
            this.radioButtonOverwrite.Name = "radioButtonOverwrite";
            this.radioButtonOverwrite.Size = new System.Drawing.Size(92, 16);
            this.radioButtonOverwrite.TabIndex = 0;
            this.radioButtonOverwrite.Text = "上書きする(&W)";
            this.radioButtonOverwrite.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "コピーしない(&G)";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 65);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(102, 16);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "問い合わせる(&A)";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 192);
            this.Controls.Add(this.groupBoxTreatSameFile);
            this.Controls.Add(this.buttonExec);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonExportBrowse);
            this.Controls.Add(this.buttonImportBrowse);
            this.Controls.Add(this.textBoxImportDir);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Picture Importer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxTreatSameFile.ResumeLayout(false);
            this.groupBoxTreatSameFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxImportDir;
        private System.Windows.Forms.Button buttonImportBrowse;
        private System.Windows.Forms.Button buttonExportBrowse;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.GroupBox groupBoxTreatSameFile;
        private System.Windows.Forms.RadioButton radioButtonOverwrite;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogImport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogExport;
    }
}

