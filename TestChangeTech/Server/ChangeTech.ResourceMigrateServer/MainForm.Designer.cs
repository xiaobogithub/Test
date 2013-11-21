namespace ChangeTech.ResourceMigrateServer
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
            this.folderPathLbl = new System.Windows.Forms.Label();
            this.folderPathTxtBox = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.uploadedFilesLstBox = new System.Windows.Forms.ListBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.resourceTypeLbl = new System.Windows.Forms.Label();
            this.resourceTypeComboBox = new System.Windows.Forms.ComboBox();
            this.totalCountTextLbl = new System.Windows.Forms.Label();
            this.totalCountNOLbl = new System.Windows.Forms.Label();
            this.uploadedFilesTextLbl = new System.Windows.Forms.Label();
            this.uploadedFilesNOLbl = new System.Windows.Forms.Label();
            this.progressTextLbl = new System.Windows.Forms.Label();
            this.progressNOLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // folderPathLbl
            // 
            this.folderPathLbl.Location = new System.Drawing.Point(12, 68);
            this.folderPathLbl.Name = "folderPathLbl";
            this.folderPathLbl.Size = new System.Drawing.Size(65, 23);
            this.folderPathLbl.TabIndex = 2;
            this.folderPathLbl.Text = "Folder:";
            this.folderPathLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // folderPathTxtBox
            // 
            this.folderPathTxtBox.Location = new System.Drawing.Point(98, 68);
            this.folderPathTxtBox.Name = "folderPathTxtBox";
            this.folderPathTxtBox.Size = new System.Drawing.Size(338, 20);
            this.folderPathTxtBox.TabIndex = 3;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(456, 65);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 4;
            this.browseBtn.Text = "Browse...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // uploadedFilesLstBox
            // 
            this.uploadedFilesLstBox.FormattingEnabled = true;
            this.uploadedFilesLstBox.HorizontalScrollbar = true;
            this.uploadedFilesLstBox.Location = new System.Drawing.Point(12, 106);
            this.uploadedFilesLstBox.Name = "uploadedFilesLstBox";
            this.uploadedFilesLstBox.ScrollAlwaysVisible = true;
            this.uploadedFilesLstBox.Size = new System.Drawing.Size(519, 251);
            this.uploadedFilesLstBox.TabIndex = 5;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(456, 25);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 6;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // resourceTypeLbl
            // 
            this.resourceTypeLbl.Location = new System.Drawing.Point(12, 25);
            this.resourceTypeLbl.Name = "resourceTypeLbl";
            this.resourceTypeLbl.Size = new System.Drawing.Size(118, 23);
            this.resourceTypeLbl.TabIndex = 7;
            this.resourceTypeLbl.Text = "Resource Type:";
            this.resourceTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resourceTypeComboBox
            // 
            this.resourceTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resourceTypeComboBox.FormattingEnabled = true;
            this.resourceTypeComboBox.Items.AddRange(new object[] {
            "Image",
            "Video",
            "Audio",
            "Document",
            "Logo"});
            this.resourceTypeComboBox.Location = new System.Drawing.Point(98, 27);
            this.resourceTypeComboBox.Name = "resourceTypeComboBox";
            this.resourceTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.resourceTypeComboBox.TabIndex = 8;
            // 
            // totalCountTextLbl
            // 
            this.totalCountTextLbl.Location = new System.Drawing.Point(9, 373);
            this.totalCountTextLbl.Name = "totalCountTextLbl";
            this.totalCountTextLbl.Size = new System.Drawing.Size(65, 23);
            this.totalCountTextLbl.TabIndex = 9;
            this.totalCountTextLbl.Text = "Total files:";
            this.totalCountTextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalCountNOLbl
            // 
            this.totalCountNOLbl.Location = new System.Drawing.Point(80, 373);
            this.totalCountNOLbl.Name = "totalCountNOLbl";
            this.totalCountNOLbl.Size = new System.Drawing.Size(65, 23);
            this.totalCountNOLbl.TabIndex = 10;
            this.totalCountNOLbl.Text = "0";
            this.totalCountNOLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uploadedFilesTextLbl
            // 
            this.uploadedFilesTextLbl.Location = new System.Drawing.Point(127, 373);
            this.uploadedFilesTextLbl.Name = "uploadedFilesTextLbl";
            this.uploadedFilesTextLbl.Size = new System.Drawing.Size(88, 23);
            this.uploadedFilesTextLbl.TabIndex = 11;
            this.uploadedFilesTextLbl.Text = "Uploaded files:";
            this.uploadedFilesTextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uploadedFilesNOLbl
            // 
            this.uploadedFilesNOLbl.Location = new System.Drawing.Point(202, 373);
            this.uploadedFilesNOLbl.Name = "uploadedFilesNOLbl";
            this.uploadedFilesNOLbl.Size = new System.Drawing.Size(88, 23);
            this.uploadedFilesNOLbl.TabIndex = 12;
            this.uploadedFilesNOLbl.Text = "0";
            this.uploadedFilesNOLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressTextLbl
            // 
            this.progressTextLbl.Location = new System.Drawing.Point(253, 373);
            this.progressTextLbl.Name = "progressTextLbl";
            this.progressTextLbl.Size = new System.Drawing.Size(122, 23);
            this.progressTextLbl.TabIndex = 13;
            this.progressTextLbl.Text = "Progress of current file:";
            this.progressTextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressNOLbl
            // 
            this.progressNOLbl.Location = new System.Drawing.Point(368, 373);
            this.progressNOLbl.Name = "progressNOLbl";
            this.progressNOLbl.Size = new System.Drawing.Size(163, 23);
            this.progressNOLbl.TabIndex = 14;
            this.progressNOLbl.Text = "0%";
            this.progressNOLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 421);
            this.Controls.Add(this.progressNOLbl);
            this.Controls.Add(this.progressTextLbl);
            this.Controls.Add(this.uploadedFilesNOLbl);
            this.Controls.Add(this.uploadedFilesTextLbl);
            this.Controls.Add(this.totalCountNOLbl);
            this.Controls.Add(this.totalCountTextLbl);
            this.Controls.Add(this.resourceTypeComboBox);
            this.Controls.Add(this.resourceTypeLbl);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.uploadedFilesLstBox);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.folderPathTxtBox);
            this.Controls.Add(this.folderPathLbl);
            this.Name = "MainForm";
            this.Text = "Upload resource to Azure blob";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label folderPathLbl;
        private System.Windows.Forms.TextBox folderPathTxtBox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.ListBox uploadedFilesLstBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label resourceTypeLbl;
        private System.Windows.Forms.ComboBox resourceTypeComboBox;
        private System.Windows.Forms.Label totalCountTextLbl;
        private System.Windows.Forms.Label totalCountNOLbl;
        private System.Windows.Forms.Label uploadedFilesTextLbl;
        private System.Windows.Forms.Label uploadedFilesNOLbl;
        private System.Windows.Forms.Label progressTextLbl;
        private System.Windows.Forms.Label progressNOLbl;
    }
}

