namespace ChangeTech.UpdateBigImage
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
            this.resourceLstBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.totalCountLbl = new System.Windows.Forms.Label();
            this.finishCountLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.resourceCategoryLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resourceLstBox
            // 
            this.resourceLstBox.FormattingEnabled = true;
            this.resourceLstBox.Location = new System.Drawing.Point(26, 12);
            this.resourceLstBox.Name = "resourceLstBox";
            this.resourceLstBox.Size = new System.Drawing.Size(467, 303);
            this.resourceLstBox.TabIndex = 2;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(220, 404);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Start";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total:";
            // 
            // totalCountLbl
            // 
            this.totalCountLbl.AutoSize = true;
            this.totalCountLbl.Location = new System.Drawing.Point(66, 354);
            this.totalCountLbl.Name = "totalCountLbl";
            this.totalCountLbl.Size = new System.Drawing.Size(67, 13);
            this.totalCountLbl.TabIndex = 5;
            this.totalCountLbl.Text = "<totalCount>";
            // 
            // finishCountLbl
            // 
            this.finishCountLbl.AutoSize = true;
            this.finishCountLbl.Location = new System.Drawing.Point(422, 354);
            this.finishCountLbl.Name = "finishCountLbl";
            this.finishCountLbl.Size = new System.Drawing.Size(71, 13);
            this.finishCountLbl.TabIndex = 6;
            this.finishCountLbl.Text = "<finishCount>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Finished:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Resource Category:";
            // 
            // resourceCategoryLbl
            // 
            this.resourceCategoryLbl.AutoSize = true;
            this.resourceCategoryLbl.Location = new System.Drawing.Point(137, 322);
            this.resourceCategoryLbl.Name = "resourceCategoryLbl";
            this.resourceCategoryLbl.Size = new System.Drawing.Size(110, 13);
            this.resourceCategoryLbl.TabIndex = 9;
            this.resourceCategoryLbl.Text = "<Resource Category>";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 439);
            this.Controls.Add(this.resourceCategoryLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.finishCountLbl);
            this.Controls.Add(this.totalCountLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.resourceLstBox);
            this.Name = "MainForm";
            this.Text = "Resource Update";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox resourceLstBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label totalCountLbl;
        private System.Windows.Forms.Label finishCountLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label resourceCategoryLbl;
    }
}

