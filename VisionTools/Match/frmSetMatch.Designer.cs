namespace VisionTools.Match
{
    partial class frmSetMatch
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
            this.grBImageList_Check = new System.Windows.Forms.GroupBox();
            this.ListBoxTrainImage = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gb_ImageOperation = new System.Windows.Forms.GroupBox();
            this.btnCheckImage = new System.Windows.Forms.Button();
            this.btnDelectCheckImage = new System.Windows.Forms.Button();
            this.cBcontinual = new System.Windows.Forms.CheckBox();
            this.btnDelectAllCheckImage = new System.Windows.Forms.Button();
            this.btnLoadCheckImage = new System.Windows.Forms.Button();
            this.tab_Message = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCreateModel = new System.Windows.Forms.Button();
            this.btn_CancleConfig = new System.Windows.Forms.Button();
            this.gb_Setting = new System.Windows.Forms.GroupBox();
            this.tab_Parameter = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv_CreatModelParameter = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgv_FindModelParameter = new System.Windows.Forms.DataGridView();
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.btnLoadModelImage = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gb_Resual = new System.Windows.Forms.GroupBox();
            this.dgv_Resual = new System.Windows.Forms.DataGridView();
            this.gbImageWindow = new System.Windows.Forms.GroupBox();
            this.viewPort = new HalconDotNet.HWindowControl();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlWindow = new System.Windows.Forms.Panel();
            this.grBImageList_Check.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gb_ImageOperation.SuspendLayout();
            this.tab_Message.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gb_Setting.SuspendLayout();
            this.tab_Parameter.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CreatModelParameter)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FindModelParameter)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.gb_Resual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resual)).BeginInit();
            this.gbImageWindow.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // grBImageList_Check
            // 
            this.grBImageList_Check.Controls.Add(this.ListBoxTrainImage);
            this.grBImageList_Check.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBImageList_Check.Location = new System.Drawing.Point(3, 3);
            this.grBImageList_Check.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grBImageList_Check.Name = "grBImageList_Check";
            this.grBImageList_Check.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grBImageList_Check.Size = new System.Drawing.Size(438, 334);
            this.grBImageList_Check.TabIndex = 87;
            this.grBImageList_Check.TabStop = false;
            this.grBImageList_Check.Text = "Image File List";
            // 
            // ListBoxTrainImage
            // 
            this.ListBoxTrainImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxTrainImage.HorizontalScrollbar = true;
            this.ListBoxTrainImage.ItemHeight = 12;
            this.ListBoxTrainImage.Location = new System.Drawing.Point(4, 17);
            this.ListBoxTrainImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ListBoxTrainImage.Name = "ListBoxTrainImage";
            this.ListBoxTrainImage.Size = new System.Drawing.Size(430, 314);
            this.ListBoxTrainImage.TabIndex = 76;
            this.ListBoxTrainImage.SelectedIndexChanged += new System.EventHandler(this.ListBoxTrainImage_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grBImageList_Check);
            this.tabPage2.Controls.Add(this.gb_ImageOperation);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(444, 449);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "模板检验";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gb_ImageOperation
            // 
            this.gb_ImageOperation.Controls.Add(this.btnCheckImage);
            this.gb_ImageOperation.Controls.Add(this.btnDelectCheckImage);
            this.gb_ImageOperation.Controls.Add(this.cBcontinual);
            this.gb_ImageOperation.Controls.Add(this.btnDelectAllCheckImage);
            this.gb_ImageOperation.Controls.Add(this.btnLoadCheckImage);
            this.gb_ImageOperation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_ImageOperation.Location = new System.Drawing.Point(3, 337);
            this.gb_ImageOperation.Name = "gb_ImageOperation";
            this.gb_ImageOperation.Size = new System.Drawing.Size(438, 109);
            this.gb_ImageOperation.TabIndex = 86;
            this.gb_ImageOperation.TabStop = false;
            this.gb_ImageOperation.Text = "Image File";
            // 
            // btnCheckImage
            // 
            this.btnCheckImage.Location = new System.Drawing.Point(325, 52);
            this.btnCheckImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCheckImage.Name = "btnCheckImage";
            this.btnCheckImage.Size = new System.Drawing.Size(80, 39);
            this.btnCheckImage.TabIndex = 86;
            this.btnCheckImage.Text = "检测图像";
            this.btnCheckImage.UseVisualStyleBackColor = true;
            this.btnCheckImage.Click += new System.EventHandler(this.btnCheckImage_Click);
            // 
            // btnDelectCheckImage
            // 
            this.btnDelectCheckImage.Location = new System.Drawing.Point(119, 52);
            this.btnDelectCheckImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelectCheckImage.Name = "btnDelectCheckImage";
            this.btnDelectCheckImage.Size = new System.Drawing.Size(80, 39);
            this.btnDelectCheckImage.TabIndex = 83;
            this.btnDelectCheckImage.Text = "Delete Image";
            this.btnDelectCheckImage.Click += new System.EventHandler(this.btnDelectCheckImage_Click);
            // 
            // cBcontinual
            // 
            this.cBcontinual.AutoSize = true;
            this.cBcontinual.Location = new System.Drawing.Point(7, 20);
            this.cBcontinual.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cBcontinual.Name = "cBcontinual";
            this.cBcontinual.Size = new System.Drawing.Size(72, 16);
            this.cBcontinual.TabIndex = 85;
            this.cBcontinual.Text = "连续检测";
            this.cBcontinual.UseVisualStyleBackColor = true;
            // 
            // btnDelectAllCheckImage
            // 
            this.btnDelectAllCheckImage.Location = new System.Drawing.Point(216, 52);
            this.btnDelectAllCheckImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelectAllCheckImage.Name = "btnDelectAllCheckImage";
            this.btnDelectAllCheckImage.Size = new System.Drawing.Size(80, 39);
            this.btnDelectAllCheckImage.TabIndex = 84;
            this.btnDelectAllCheckImage.Text = "Delete All";
            this.btnDelectAllCheckImage.Click += new System.EventHandler(this.btnDelectAllCheckImage_Click);
            // 
            // btnLoadCheckImage
            // 
            this.btnLoadCheckImage.Location = new System.Drawing.Point(7, 52);
            this.btnLoadCheckImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLoadCheckImage.Name = "btnLoadCheckImage";
            this.btnLoadCheckImage.Size = new System.Drawing.Size(80, 39);
            this.btnLoadCheckImage.TabIndex = 81;
            this.btnLoadCheckImage.Text = "Load Images";
            this.btnLoadCheckImage.Click += new System.EventHandler(this.btnLoadCheckImage_Click);
            // 
            // tab_Message
            // 
            this.tab_Message.Controls.Add(this.tabPage1);
            this.tab_Message.Controls.Add(this.tabPage2);
            this.tab_Message.Controls.Add(this.tabPage3);
            this.tab_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Message.Location = new System.Drawing.Point(0, 0);
            this.tab_Message.Name = "tab_Message";
            this.tab_Message.SelectedIndex = 0;
            this.tab_Message.Size = new System.Drawing.Size(452, 475);
            this.tab_Message.TabIndex = 17;
            this.tab_Message.SelectedIndexChanged += new System.EventHandler(this.tab_Message_SelectedIndexChanged);
            this.tab_Message.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tab_Message_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCreateModel);
            this.tabPage1.Controls.Add(this.btn_CancleConfig);
            this.tabPage1.Controls.Add(this.gb_Setting);
            this.tabPage1.Controls.Add(this.btn_SaveConfig);
            this.tabPage1.Controls.Add(this.btnLoadModelImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 449);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "创建模板";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCreateModel
            // 
            this.btnCreateModel.Location = new System.Drawing.Point(148, 6);
            this.btnCreateModel.Name = "btnCreateModel";
            this.btnCreateModel.Size = new System.Drawing.Size(96, 35);
            this.btnCreateModel.TabIndex = 19;
            this.btnCreateModel.Text = "创建模板";
            this.btnCreateModel.UseVisualStyleBackColor = true;
            this.btnCreateModel.Click += new System.EventHandler(this.btnCreateModel_Click);
            // 
            // btn_CancleConfig
            // 
            this.btn_CancleConfig.Location = new System.Drawing.Point(148, 74);
            this.btn_CancleConfig.Name = "btn_CancleConfig";
            this.btn_CancleConfig.Size = new System.Drawing.Size(96, 35);
            this.btn_CancleConfig.TabIndex = 2;
            this.btn_CancleConfig.Text = "取消";
            this.btn_CancleConfig.UseVisualStyleBackColor = true;
            this.btn_CancleConfig.Click += new System.EventHandler(this.btn_CancleConfig_Click);
            // 
            // gb_Setting
            // 
            this.gb_Setting.Controls.Add(this.tab_Parameter);
            this.gb_Setting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_Setting.Location = new System.Drawing.Point(3, 235);
            this.gb_Setting.Name = "gb_Setting";
            this.gb_Setting.Size = new System.Drawing.Size(438, 211);
            this.gb_Setting.TabIndex = 18;
            this.gb_Setting.TabStop = false;
            this.gb_Setting.Text = "参数设定";
            // 
            // tab_Parameter
            // 
            this.tab_Parameter.Controls.Add(this.tabPage4);
            this.tab_Parameter.Controls.Add(this.tabPage5);
            this.tab_Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Parameter.Location = new System.Drawing.Point(3, 17);
            this.tab_Parameter.Name = "tab_Parameter";
            this.tab_Parameter.SelectedIndex = 0;
            this.tab_Parameter.Size = new System.Drawing.Size(432, 191);
            this.tab_Parameter.TabIndex = 8;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv_CreatModelParameter);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(424, 165);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "创建模板";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv_CreatModelParameter
            // 
            this.dgv_CreatModelParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CreatModelParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CreatModelParameter.Location = new System.Drawing.Point(3, 3);
            this.dgv_CreatModelParameter.Name = "dgv_CreatModelParameter";
            this.dgv_CreatModelParameter.RowTemplate.Height = 23;
            this.dgv_CreatModelParameter.Size = new System.Drawing.Size(418, 159);
            this.dgv_CreatModelParameter.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgv_FindModelParameter);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(424, 165);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "匹配";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgv_FindModelParameter
            // 
            this.dgv_FindModelParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FindModelParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_FindModelParameter.Location = new System.Drawing.Point(3, 3);
            this.dgv_FindModelParameter.Name = "dgv_FindModelParameter";
            this.dgv_FindModelParameter.RowTemplate.Height = 23;
            this.dgv_FindModelParameter.Size = new System.Drawing.Size(418, 159);
            this.dgv_FindModelParameter.TabIndex = 2;
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Location = new System.Drawing.Point(27, 74);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(96, 35);
            this.btn_SaveConfig.TabIndex = 1;
            this.btn_SaveConfig.Text = "保存";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // btnLoadModelImage
            // 
            this.btnLoadModelImage.Location = new System.Drawing.Point(27, 6);
            this.btnLoadModelImage.Name = "btnLoadModelImage";
            this.btnLoadModelImage.Size = new System.Drawing.Size(96, 35);
            this.btnLoadModelImage.TabIndex = 5;
            this.btnLoadModelImage.Text = "打开图片";
            this.btnLoadModelImage.UseVisualStyleBackColor = true;
            this.btnLoadModelImage.Click += new System.EventHandler(this.btnLoadModelImage_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gb_Resual);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(444, 449);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "结果";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gb_Resual
            // 
            this.gb_Resual.Controls.Add(this.dgv_Resual);
            this.gb_Resual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Resual.Location = new System.Drawing.Point(0, 0);
            this.gb_Resual.Name = "gb_Resual";
            this.gb_Resual.Size = new System.Drawing.Size(444, 449);
            this.gb_Resual.TabIndex = 0;
            this.gb_Resual.TabStop = false;
            this.gb_Resual.Text = "检测结果";
            // 
            // dgv_Resual
            // 
            this.dgv_Resual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Resual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Resual.Location = new System.Drawing.Point(3, 17);
            this.dgv_Resual.Name = "dgv_Resual";
            this.dgv_Resual.RowTemplate.Height = 23;
            this.dgv_Resual.Size = new System.Drawing.Size(438, 429);
            this.dgv_Resual.TabIndex = 0;
            // 
            // gbImageWindow
            // 
            this.gbImageWindow.Controls.Add(this.viewPort);
            this.gbImageWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImageWindow.Location = new System.Drawing.Point(0, 0);
            this.gbImageWindow.Name = "gbImageWindow";
            this.gbImageWindow.Size = new System.Drawing.Size(575, 475);
            this.gbImageWindow.TabIndex = 19;
            this.gbImageWindow.TabStop = false;
            this.gbImageWindow.Text = "图像窗口";
            // 
            // viewPort
            // 
            this.viewPort.BackColor = System.Drawing.Color.Black;
            this.viewPort.BorderColor = System.Drawing.Color.Black;
            this.viewPort.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.viewPort.Location = new System.Drawing.Point(22, 20);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(401, 355);
            this.viewPort.TabIndex = 2;
            this.viewPort.WindowSize = new System.Drawing.Size(401, 355);
            this.viewPort.HMouseUp += new HalconDotNet.HMouseEventHandler(this.viewPort_HMouseUp);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.tab_Message);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(575, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(452, 475);
            this.pnlRight.TabIndex = 20;
            // 
            // pnlWindow
            // 
            this.pnlWindow.Controls.Add(this.gbImageWindow);
            this.pnlWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWindow.Location = new System.Drawing.Point(0, 0);
            this.pnlWindow.Name = "pnlWindow";
            this.pnlWindow.Size = new System.Drawing.Size(575, 475);
            this.pnlWindow.TabIndex = 21;
            // 
            // frmSetMatch
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 475);
            this.Controls.Add(this.pnlWindow);
            this.Controls.Add(this.pnlRight);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetMatch";
            this.Text = "frmSetMatch";
            this.Activated += new System.EventHandler(this.frmSetMatch_Activated);
            this.Load += new System.EventHandler(this.frmSetMatch_Load);
            this.Shown += new System.EventHandler(this.frmSetMatch_Shown);
            this.grBImageList_Check.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.gb_ImageOperation.ResumeLayout(false);
            this.gb_ImageOperation.PerformLayout();
            this.tab_Message.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gb_Setting.ResumeLayout(false);
            this.tab_Parameter.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CreatModelParameter)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FindModelParameter)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.gb_Resual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resual)).EndInit();
            this.gbImageWindow.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlWindow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grBImageList_Check;
        private System.Windows.Forms.ListBox ListBoxTrainImage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gb_ImageOperation;
        private System.Windows.Forms.Button btnCheckImage;
        private System.Windows.Forms.Button btnDelectCheckImage;
        private System.Windows.Forms.CheckBox cBcontinual;
        private System.Windows.Forms.Button btnDelectAllCheckImage;
        private System.Windows.Forms.Button btnLoadCheckImage;
        private System.Windows.Forms.TabControl tab_Message;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnLoadModelImage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox gb_Resual;
        private System.Windows.Forms.DataGridView dgv_Resual;
        private System.Windows.Forms.GroupBox gbImageWindow;
        private HalconDotNet.HWindowControl viewPort;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox gb_Setting;
        private System.Windows.Forms.TabControl tab_Parameter;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgv_CreatModelParameter;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgv_FindModelParameter;
        private System.Windows.Forms.Button btn_SaveConfig;
        private System.Windows.Forms.Button btn_CancleConfig;
        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.Button btnCreateModel;
    }
}