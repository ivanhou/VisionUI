namespace VisionUI.Modle.Cam
{
    partial class frmCam
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
            this.components = new System.ComponentModel.Container();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.gb_CamNamList = new System.Windows.Forms.GroupBox();
            this.tv_CamList = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddCam = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelectCam = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.gbSetCam = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labCamType = new System.Windows.Forms.Label();
            this.cmbCamTypeList = new System.Windows.Forms.ComboBox();
            this.gbCamControlParam = new System.Windows.Forms.GroupBox();
            this.pnlSerialNum = new System.Windows.Forms.Panel();
            this.labSerialNumber = new System.Windows.Forms.Label();
            this.txtCamSerialNumber = new System.Windows.Forms.TextBox();
            this.cbCamUse = new System.Windows.Forms.CheckBox();
            this.pnlFilePath = new System.Windows.Forms.Panel();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.txtCamName = new System.Windows.Forms.TextBox();
            this.pnlExposure = new System.Windows.Forms.Panel();
            this.labExposure = new System.Windows.Forms.Label();
            this.pnlGain = new System.Windows.Forms.Panel();
            this.labGain = new System.Windows.Forms.Label();
            this.numUDExposure = new System.Windows.Forms.NumericUpDown();
            this.numUDGain = new System.Windows.Forms.NumericUpDown();
            this.btnTestGrab = new System.Windows.Forms.Button();
            this.pnlLeft.SuspendLayout();
            this.gb_CamNamList.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbSetCam.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbCamControlParam.SuspendLayout();
            this.pnlSerialNum.SuspendLayout();
            this.pnlFilePath.SuspendLayout();
            this.pnlExposure.SuspendLayout();
            this.pnlGain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDExposure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDGain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gb_CamNamList);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(205, 575);
            this.pnlLeft.TabIndex = 0;
            // 
            // gb_CamNamList
            // 
            this.gb_CamNamList.Controls.Add(this.tv_CamList);
            this.gb_CamNamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_CamNamList.Location = new System.Drawing.Point(0, 0);
            this.gb_CamNamList.Name = "gb_CamNamList";
            this.gb_CamNamList.Size = new System.Drawing.Size(205, 575);
            this.gb_CamNamList.TabIndex = 0;
            this.gb_CamNamList.TabStop = false;
            this.gb_CamNamList.Text = "相机列表";
            // 
            // tv_CamList
            // 
            this.tv_CamList.ContextMenuStrip = this.contextMenuStrip1;
            this.tv_CamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_CamList.Location = new System.Drawing.Point(3, 17);
            this.tv_CamList.Name = "tv_CamList";
            this.tv_CamList.Size = new System.Drawing.Size(199, 555);
            this.tv_CamList.TabIndex = 0;
            this.tv_CamList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tv_CamList_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddCam,
            this.tsmDelectCam});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmAddCam
            // 
            this.tsmAddCam.Name = "tsmAddCam";
            this.tsmAddCam.Size = new System.Drawing.Size(100, 22);
            this.tsmAddCam.Text = "添加";
            this.tsmAddCam.Click += new System.EventHandler(this.tsmAddCam_Click);
            // 
            // tsmDelectCam
            // 
            this.tsmDelectCam.Name = "tsmDelectCam";
            this.tsmDelectCam.Size = new System.Drawing.Size(100, 22);
            this.tsmDelectCam.Text = "删除";
            this.tsmDelectCam.Click += new System.EventHandler(this.tsmDelectCam_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbSetCam);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(205, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(756, 575);
            this.pnlRight.TabIndex = 1;
            // 
            // gbSetCam
            // 
            this.gbSetCam.Controls.Add(this.btnTestGrab);
            this.gbSetCam.Controls.Add(this.panel2);
            this.gbSetCam.Controls.Add(this.gbCamControlParam);
            this.gbSetCam.Controls.Add(this.pnlFilePath);
            this.gbSetCam.Controls.Add(this.btnSave);
            this.gbSetCam.Controls.Add(this.btnCancle);
            this.gbSetCam.Controls.Add(this.txtCamName);
            this.gbSetCam.Location = new System.Drawing.Point(6, 3);
            this.gbSetCam.Name = "gbSetCam";
            this.gbSetCam.Size = new System.Drawing.Size(371, 528);
            this.gbSetCam.TabIndex = 0;
            this.gbSetCam.TabStop = false;
            this.gbSetCam.Text = "相机设置";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labCamType);
            this.panel2.Controls.Add(this.cmbCamTypeList);
            this.panel2.Location = new System.Drawing.Point(143, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 45);
            this.panel2.TabIndex = 9;
            // 
            // labCamType
            // 
            this.labCamType.AutoSize = true;
            this.labCamType.Location = new System.Drawing.Point(10, 15);
            this.labCamType.Name = "labCamType";
            this.labCamType.Size = new System.Drawing.Size(59, 12);
            this.labCamType.TabIndex = 3;
            this.labCamType.Text = "相机类型:";
            // 
            // cmbCamTypeList
            // 
            this.cmbCamTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCamTypeList.FormattingEnabled = true;
            this.cmbCamTypeList.Location = new System.Drawing.Point(75, 12);
            this.cmbCamTypeList.Name = "cmbCamTypeList";
            this.cmbCamTypeList.Size = new System.Drawing.Size(121, 20);
            this.cmbCamTypeList.TabIndex = 2;
            this.cmbCamTypeList.SelectedIndexChanged += new System.EventHandler(this.cmbCamTypeList_SelectedIndexChanged);
            // 
            // gbCamControlParam
            // 
            this.gbCamControlParam.Controls.Add(this.pnlGain);
            this.gbCamControlParam.Controls.Add(this.pnlExposure);
            this.gbCamControlParam.Controls.Add(this.pnlSerialNum);
            this.gbCamControlParam.Controls.Add(this.cbCamUse);
            this.gbCamControlParam.Location = new System.Drawing.Point(15, 156);
            this.gbCamControlParam.Name = "gbCamControlParam";
            this.gbCamControlParam.Size = new System.Drawing.Size(333, 185);
            this.gbCamControlParam.TabIndex = 8;
            this.gbCamControlParam.TabStop = false;
            this.gbCamControlParam.Text = "相机控制参数";
            // 
            // pnlSerialNum
            // 
            this.pnlSerialNum.Controls.Add(this.labSerialNumber);
            this.pnlSerialNum.Controls.Add(this.txtCamSerialNumber);
            this.pnlSerialNum.Location = new System.Drawing.Point(17, 24);
            this.pnlSerialNum.Name = "pnlSerialNum";
            this.pnlSerialNum.Size = new System.Drawing.Size(199, 45);
            this.pnlSerialNum.TabIndex = 5;
            // 
            // labSerialNumber
            // 
            this.labSerialNumber.AutoSize = true;
            this.labSerialNumber.Location = new System.Drawing.Point(10, 16);
            this.labSerialNumber.Name = "labSerialNumber";
            this.labSerialNumber.Size = new System.Drawing.Size(47, 12);
            this.labSerialNumber.TabIndex = 4;
            this.labSerialNumber.Text = "序列号:";
            // 
            // txtCamSerialNumber
            // 
            this.txtCamSerialNumber.Location = new System.Drawing.Point(63, 13);
            this.txtCamSerialNumber.Name = "txtCamSerialNumber";
            this.txtCamSerialNumber.Size = new System.Drawing.Size(114, 21);
            this.txtCamSerialNumber.TabIndex = 1;
            this.txtCamSerialNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbCamUse
            // 
            this.cbCamUse.AutoSize = true;
            this.cbCamUse.Location = new System.Drawing.Point(238, 39);
            this.cbCamUse.Name = "cbCamUse";
            this.cbCamUse.Size = new System.Drawing.Size(72, 16);
            this.cbCamUse.TabIndex = 0;
            this.cbCamUse.Text = "启用相机";
            this.cbCamUse.UseVisualStyleBackColor = true;
            // 
            // pnlFilePath
            // 
            this.pnlFilePath.Controls.Add(this.btnFilePath);
            this.pnlFilePath.Controls.Add(this.txtFilePath);
            this.pnlFilePath.Location = new System.Drawing.Point(15, 93);
            this.pnlFilePath.Name = "pnlFilePath";
            this.pnlFilePath.Size = new System.Drawing.Size(333, 46);
            this.pnlFilePath.TabIndex = 7;
            // 
            // btnFilePath
            // 
            this.btnFilePath.Location = new System.Drawing.Point(251, 4);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(75, 36);
            this.btnFilePath.TabIndex = 6;
            this.btnFilePath.Text = "图片路径";
            this.btnFilePath.UseVisualStyleBackColor = true;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(10, 13);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(235, 21);
            this.txtFilePath.TabIndex = 5;
            this.txtFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(151, 464);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 36);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(264, 464);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 36);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // txtCamName
            // 
            this.txtCamName.Location = new System.Drawing.Point(15, 29);
            this.txtCamName.Name = "txtCamName";
            this.txtCamName.ReadOnly = true;
            this.txtCamName.Size = new System.Drawing.Size(100, 21);
            this.txtCamName.TabIndex = 4;
            this.txtCamName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlExposure
            // 
            this.pnlExposure.Controls.Add(this.numUDExposure);
            this.pnlExposure.Controls.Add(this.labExposure);
            this.pnlExposure.Location = new System.Drawing.Point(17, 75);
            this.pnlExposure.Name = "pnlExposure";
            this.pnlExposure.Size = new System.Drawing.Size(199, 45);
            this.pnlExposure.TabIndex = 6;
            // 
            // labExposure
            // 
            this.labExposure.AutoSize = true;
            this.labExposure.Location = new System.Drawing.Point(22, 16);
            this.labExposure.Name = "labExposure";
            this.labExposure.Size = new System.Drawing.Size(35, 12);
            this.labExposure.TabIndex = 4;
            this.labExposure.Text = "曝光:";
            // 
            // pnlGain
            // 
            this.pnlGain.Controls.Add(this.numUDGain);
            this.pnlGain.Controls.Add(this.labGain);
            this.pnlGain.Location = new System.Drawing.Point(17, 126);
            this.pnlGain.Name = "pnlGain";
            this.pnlGain.Size = new System.Drawing.Size(199, 45);
            this.pnlGain.TabIndex = 7;
            // 
            // labGain
            // 
            this.labGain.AutoSize = true;
            this.labGain.Location = new System.Drawing.Point(22, 16);
            this.labGain.Name = "labGain";
            this.labGain.Size = new System.Drawing.Size(35, 12);
            this.labGain.TabIndex = 4;
            this.labGain.Text = "增益:";
            // 
            // numUDExposure
            // 
            this.numUDExposure.Location = new System.Drawing.Point(63, 14);
            this.numUDExposure.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUDExposure.Name = "numUDExposure";
            this.numUDExposure.Size = new System.Drawing.Size(114, 21);
            this.numUDExposure.TabIndex = 10;
            this.numUDExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numUDGain
            // 
            this.numUDGain.DecimalPlaces = 2;
            this.numUDGain.Location = new System.Drawing.Point(63, 14);
            this.numUDGain.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            131072});
            this.numUDGain.Name = "numUDGain";
            this.numUDGain.Size = new System.Drawing.Size(114, 21);
            this.numUDGain.TabIndex = 11;
            this.numUDGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnTestGrab
            // 
            this.btnTestGrab.Location = new System.Drawing.Point(266, 375);
            this.btnTestGrab.Name = "btnTestGrab";
            this.btnTestGrab.Size = new System.Drawing.Size(75, 36);
            this.btnTestGrab.TabIndex = 10;
            this.btnTestGrab.Text = "采集图像";
            this.btnTestGrab.UseVisualStyleBackColor = true;
            // 
            // frmCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 575);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCam";
            this.Text = "frmCam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCam_FormClosing);
            this.Load += new System.EventHandler(this.frmCam_Load);
            this.pnlLeft.ResumeLayout(false);
            this.gb_CamNamList.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.gbSetCam.ResumeLayout(false);
            this.gbSetCam.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbCamControlParam.ResumeLayout(false);
            this.gbCamControlParam.PerformLayout();
            this.pnlSerialNum.ResumeLayout(false);
            this.pnlSerialNum.PerformLayout();
            this.pnlFilePath.ResumeLayout(false);
            this.pnlFilePath.PerformLayout();
            this.pnlExposure.ResumeLayout(false);
            this.pnlExposure.PerformLayout();
            this.pnlGain.ResumeLayout(false);
            this.pnlGain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDExposure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDGain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox gb_CamNamList;
        private System.Windows.Forms.TreeView tv_CamList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmAddCam;
        private System.Windows.Forms.ToolStripMenuItem tsmDelectCam;
        private System.Windows.Forms.GroupBox gbSetCam;
        private System.Windows.Forms.TextBox txtCamSerialNumber;
        private System.Windows.Forms.CheckBox cbCamUse;
        private System.Windows.Forms.Label labCamType;
        private System.Windows.Forms.ComboBox cmbCamTypeList;
        private System.Windows.Forms.TextBox txtCamName;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlFilePath;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbCamControlParam;
        private System.Windows.Forms.Label labSerialNumber;
        private System.Windows.Forms.Panel pnlSerialNum;
        private System.Windows.Forms.Panel pnlGain;
        private System.Windows.Forms.Label labGain;
        private System.Windows.Forms.Panel pnlExposure;
        private System.Windows.Forms.Label labExposure;
        private System.Windows.Forms.NumericUpDown numUDGain;
        private System.Windows.Forms.NumericUpDown numUDExposure;
        private System.Windows.Forms.Button btnTestGrab;
    }
}