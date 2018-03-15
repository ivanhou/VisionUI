namespace VisionUI.Modle
{
    partial class frmDetails
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
            this.groupBoxComments = new System.Windows.Forms.GroupBox();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.textBoxBossID = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelBossID = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.groupBoxComments.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxComments
            // 
            this.groupBoxComments.Controls.Add(this.textBoxComments);
            this.groupBoxComments.Location = new System.Drawing.Point(21, 104);
            this.groupBoxComments.Name = "groupBoxComments";
            this.groupBoxComments.Size = new System.Drawing.Size(317, 172);
            this.groupBoxComments.TabIndex = 24;
            this.groupBoxComments.TabStop = false;
            this.groupBoxComments.Text = "Comments:";
            // 
            // textBoxComments
            // 
            this.textBoxComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxComments.Location = new System.Drawing.Point(3, 17);
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComments.Size = new System.Drawing.Size(311, 152);
            this.textBoxComments.TabIndex = 0;
            // 
            // textBoxBossID
            // 
            this.textBoxBossID.Location = new System.Drawing.Point(232, 9);
            this.textBoxBossID.Name = "textBoxBossID";
            this.textBoxBossID.ReadOnly = true;
            this.textBoxBossID.Size = new System.Drawing.Size(106, 21);
            this.textBoxBossID.TabIndex = 23;
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(69, 9);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(106, 21);
            this.textBoxID.TabIndex = 22;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(69, 70);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(269, 21);
            this.textBoxTitle.TabIndex = 17;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(69, 35);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(269, 21);
            this.textBoxName.TabIndex = 15;
            // 
            // labelBossID
            // 
            this.labelBossID.Location = new System.Drawing.Point(184, 9);
            this.labelBossID.Name = "labelBossID";
            this.labelBossID.Size = new System.Drawing.Size(58, 17);
            this.labelBossID.TabIndex = 21;
            this.labelBossID.Text = "BossID:";
            // 
            // labelID
            // 
            this.labelID.Location = new System.Drawing.Point(12, 9);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(19, 17);
            this.labelID.TabIndex = 20;
            this.labelID.Text = "ID:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(204, 285);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 25);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(60, 285);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(90, 25);
            this.buttonOK.TabIndex = 18;
            this.buttonOK.Text = "OK";
            // 
            // labelTitle
            // 
            this.labelTitle.Location = new System.Drawing.Point(12, 70);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(38, 17);
            this.labelTitle.TabIndex = 16;
            this.labelTitle.Text = "Title:";
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(12, 35);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(48, 17);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "Name:";
            // 
            // frmDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 333);
            this.Controls.Add(this.groupBoxComments);
            this.Controls.Add(this.textBoxBossID);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelBossID);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelName);
            this.Name = "frmDetails";
            this.Text = "frmDetails";
            this.groupBoxComments.ResumeLayout(false);
            this.groupBoxComments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxComments;
        public System.Windows.Forms.TextBox textBoxComments;
        public System.Windows.Forms.TextBox textBoxBossID;
        public System.Windows.Forms.TextBox textBoxID;
        public System.Windows.Forms.TextBox textBoxTitle;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelBossID;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelName;
    }
}