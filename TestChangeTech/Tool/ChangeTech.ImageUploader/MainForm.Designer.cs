namespace ChangeTech.ImageUploader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.categoryNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filesDataGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.clearButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.FileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.totalFileCountLabel = new System.Windows.Forms.Label();
            this.leftFileCountLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.currentFileLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryNameComboBox
            // 
            this.categoryNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryNameComboBox.FormattingEnabled = true;
            this.categoryNameComboBox.Location = new System.Drawing.Point(303, 15);
            this.categoryNameComboBox.Name = "categoryNameComboBox";
            this.categoryNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.categoryNameComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose which category you want to put the resources in:";
            // 
            // filesDataGridView
            // 
            this.filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileNameColumn,
            this.PathColumn,
            this.StatusColumn});
            this.filesDataGridView.Location = new System.Drawing.Point(28, 87);
            this.filesDataGridView.Name = "filesDataGridView";
            this.filesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.filesDataGridView.Size = new System.Drawing.Size(628, 268);
            this.filesDataGridView.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select files to upload:";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(140, 45);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(581, 49);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(263, 361);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start upload";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(337, 361);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // FileNameColumn
            // 
            this.FileNameColumn.HeaderText = "File Name";
            this.FileNameColumn.Name = "FileNameColumn";
            this.FileNameColumn.ReadOnly = true;
            this.FileNameColumn.Width = 180;
            // 
            // PathColumn
            // 
            this.PathColumn.HeaderText = "Path";
            this.PathColumn.Name = "PathColumn";
            this.PathColumn.ReadOnly = true;
            this.PathColumn.Width = 300;
            // 
            // StatusColumn
            // 
            this.StatusColumn.HeaderText = "Status";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(495, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Type:";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Image",
            "Video",
            "Audio"});
            this.typeComboBox.Location = new System.Drawing.Point(535, 15);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeComboBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(671, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Upload process:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(671, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Total:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(671, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Left:";
            // 
            // totalFileCountLabel
            // 
            this.totalFileCountLabel.AutoSize = true;
            this.totalFileCountLabel.Location = new System.Drawing.Point(671, 154);
            this.totalFileCountLabel.Name = "totalFileCountLabel";
            this.totalFileCountLabel.Size = new System.Drawing.Size(13, 13);
            this.totalFileCountLabel.TabIndex = 13;
            this.totalFileCountLabel.Text = "0";
            // 
            // leftFileCountLabel
            // 
            this.leftFileCountLabel.AutoSize = true;
            this.leftFileCountLabel.Location = new System.Drawing.Point(671, 210);
            this.leftFileCountLabel.Name = "leftFileCountLabel";
            this.leftFileCountLabel.Size = new System.Drawing.Size(13, 13);
            this.leftFileCountLabel.TabIndex = 14;
            this.leftFileCountLabel.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(674, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Current uploading file:";
            // 
            // currentFileLabel
            // 
            this.currentFileLabel.AutoSize = true;
            this.currentFileLabel.Location = new System.Drawing.Point(674, 270);
            this.currentFileLabel.Name = "currentFileLabel";
            this.currentFileLabel.Size = new System.Drawing.Size(0, 13);
            this.currentFileLabel.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 414);
            this.Controls.Add(this.currentFileLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.leftFileCountLabel);
            this.Controls.Add(this.totalFileCountLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filesDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.categoryNameComboBox);
            this.Name = "MainForm";
            this.Text = "ChangeTech Resource Uploader V 1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox categoryNameComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView filesDataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label totalFileCountLabel;
        private System.Windows.Forms.Label leftFileCountLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label currentFileLabel;
    }
}

