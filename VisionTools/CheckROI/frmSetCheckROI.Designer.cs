namespace VisionTools.CheckROI
{
    partial class frmSetCheckROI
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
            this.pnlWindow = new System.Windows.Forms.Panel();
            this.gbImageWindow = new System.Windows.Forms.GroupBox();
            this.viewPort = new HalconDotNet.HWindowControl();
            this.dgv_Resual = new System.Windows.Forms.DataGridView();
            this.gb_Resual = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.tab_Message = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_CancleConfig = new System.Windows.Forms.Button();
            this.gb_ROI = new System.Windows.Forms.GroupBox();
            this.dgv_ROI = new System.Windows.Forms.DataGridView();
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.btnLoadModelImage = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grBImageList_Check = new System.Windows.Forms.GroupBox();
            this.ListBoxTrainImage = new System.Windows.Forms.ListBox();
            this.gb_ImageOperation = new System.Windows.Forms.GroupBox();
            this.btnCheckImage = new System.Windows.Forms.Button();
            this.btnDelectCheckImage = new System.Windows.Forms.Button();
            this.cBcontinual = new System.Windows.Forms.CheckBox();
            this.btnDelectAllCheckImage = new System.Windows.Forms.Button();
            this.btnLoadCheckImage = new System.Windows.Forms.Button();
            this.pnlWindow.SuspendLayout();
            this.gbImageWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resual)).BeginInit();
            this.gb_Resual.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.tab_Message.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gb_ROI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ROI)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.grBImageList_Check.SuspendLayout();
            this.gb_ImageOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWindow
            // 
            this.pnlWindow.Controls.Add(this.gbImageWindow);
            this.pnlWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWindow.Location = new System.Drawing.Point(0, 0);
            this.pnlWindow.Name = "pnlWindow";
            this.pnlWindow.Size = new System.Drawing.Size(490, 494);
            this.pnlWindow.TabIndex = 23;
            // 
            // gbImageWindow
            // 
            this.gbImageWindow.Controls.Add(this.viewPort);
            this.gbImageWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImageWindow.Location = new System.Drawing.Point(0, 0);
            this.gbImageWindow.Name = "gbImageWindow";
            this.gbImageWindow.Size = new System.Drawing.Size(490, 494);
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
            // dgv_Resual
            // 
            this.dgv_Resual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Resual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Resual.Location = new System.Drawing.Point(3, 17);
            this.dgv_Resual.Name = "dgv_Resual";
            this.dgv_Resual.RowTemplate.Height = 23;
            this.dgv_Resual.Size = new System.Drawing.Size(438, 448);
            this.dgv_Resual.TabIndex = 0;
            // 
            // gb_Resual
            // 
            this.gb_Resual.Controls.Add(this.dgv_Resual);
            this.gb_Resual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Resual.Location = new System.Drawing.Point(0, 0);
            this.gb_Resual.Name = "gb_Resual";
            this.gb_Resual.Size = new System.Drawing.Size(444, 468);
            this.gb_Resual.TabIndex = 0;
            this.gb_Resual.TabStop = false;
            this.gb_Resual.Text = "检测结果";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gb_Resual);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(444, 468);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "结果";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.tab_Message);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(490, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(452, 494);
            this.pnlRight.TabIndex = 22;
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
            this.tab_Message.Size = new System.Drawing.Size(452, 494);
            this.tab_Message.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_CancleConfig);
            this.tabPage1.Controls.Add(this.gb_ROI);
            this.tabPage1.Controls.Add(this.btn_SaveConfig);
            this.tabPage1.Controls.Add(this.btnLoadModelImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 468);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "创建模板";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_CancleConfig
            // 
            this.btn_CancleConfig.Location = new System.Drawing.Point(280, 16);
            this.btn_CancleConfig.Name = "btn_CancleConfig";
            this.btn_CancleConfig.Size = new System.Drawing.Size(96, 35);
            this.btn_CancleConfig.TabIndex = 2;
            this.btn_CancleConfig.Text = "取消";
            this.btn_CancleConfig.UseVisualStyleBackColor = true;
            this.btn_CancleConfig.Click += new System.EventHandler(this.btn_CancleConfig_Click);
            // 
            // gb_ROI
            // 
            this.gb_ROI.Controls.Add(this.dgv_ROI);
            this.gb_ROI.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_ROI.Location = new System.Drawing.Point(3, 200);
            this.gb_ROI.Name = "gb_ROI";
            this.gb_ROI.Size = new System.Drawing.Size(438, 265);
            this.gb_ROI.TabIndex = 18;
            this.gb_ROI.TabStop = false;
            this.gb_ROI.Text = "参数设定";
            // 
            // dgv_ROI
            // 
            this.dgv_ROI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ROI.Location = new System.Drawing.Point(3, 17);
            this.dgv_ROI.Name = "dgv_ROI";
            this.dgv_ROI.RowTemplate.Height = 23;
            this.dgv_ROI.Size = new System.Drawing.Size(432, 245);
            this.dgv_ROI.TabIndex = 2;
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Location = new System.Drawing.Point(151, 16);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(96, 35);
            this.btn_SaveConfig.TabIndex = 1;
            this.btn_SaveConfig.Text = "保存";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // btnLoadModelImage
            // 
            this.btnLoadModelImage.Location = new System.Drawing.Point(25, 16);
            this.btnLoadModelImage.Name = "btnLoadModelImage";
            this.btnLoadModelImage.Size = new System.Drawing.Size(96, 35);
            this.btnLoadModelImage.TabIndex = 5;
            this.btnLoadModelImage.Text = "打开图片";
            this.btnLoadModelImage.UseVisualStyleBackColor = true;
            this.btnLoadModelImage.Click += new System.EventHandler(this.btnLoadModelImage_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grBImageList_Check);
            this.tabPage2.Controls.Add(this.gb_ImageOperation);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(444, 468);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "模板检验";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grBImageList_Check
            // 
            this.grBImageList_Check.Controls.Add(this.ListBoxTrainImage);
            this.grBImageList_Check.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBImageList_Check.Location = new System.Drawing.Point(3, 3);
            this.grBImageList_Check.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grBImageList_Check.Name = "grBImageList_Check";
            this.grBImageList_Check.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grBImageList_Check.Size = new System.Drawing.Size(438, 353);
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
            this.ListBoxTrainImage.Size = new System.Drawing.Size(430, 333);
            this.ListBoxTrainImage.TabIndex = 76;
            // 
            // gb_ImageOperation
            // 
            this.gb_ImageOperation.Controls.Add(this.btnCheckImage);
            this.gb_ImageOperation.Controls.Add(this.btnDelectCheckImage);
            this.gb_ImageOperation.Controls.Add(this.cBcontinual);
            this.gb_ImageOperation.Controls.Add(this.btnDelectAllCheckImage);
            this.gb_ImageOperation.Controls.Add(this.btnLoadCheckImage);
            this.gb_ImageOperation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_ImageOperation.Location = new System.Drawing.Point(3, 356);
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
            // 
            // btnDelectCheckImage
            // 
            this.btnDelectCheckImage.Location = new System.Drawing.Point(119, 52);
            this.btnDelectCheckImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelectCheckImage.Name = "btnDelectCheckImage";
            this.btnDelectCheckImage.Size = new System.Drawing.Size(80, 39);
            this.btnDelectCheckImage.TabIndex = 83;
            this.btnDelectCheckImage.Text = "Delete Image";
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
            // 
            // btnLoadCheckImage
            // 
            this.btnLoadCheckImage.Location = new System.Drawing.Point(7, 52);
            this.btnLoadCheckImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLoadCheckImage.Name = "btnLoadCheckImage";
            this.btnLoadCheckImage.Size = new System.Drawing.Size(80, 39);
            this.btnLoadCheckImage.TabIndex = 81;
            this.btnLoadCheckImage.Text = "Load Images";
            // 
            // frmSetCheckROI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 494);
            this.Controls.Add(this.pnlWindow);
            this.Controls.Add(this.pnlRight);
            this.Name = "frmSetCheckROI";
            this.Text = "frmSetCheckROI";
            this.Load += new System.EventHandler(this.frmSetCheckROI_Load);
            this.Shown += new System.EventHandler(this.frmSetCheckROI_Shown);
            this.pnlWindow.ResumeLayout(false);
            this.gbImageWindow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resual)).EndInit();
            this.gb_Resual.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.tab_Message.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gb_ROI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ROI)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.grBImageList_Check.ResumeLayout(false);
            this.gb_ImageOperation.ResumeLayout(false);
            this.gb_ImageOperation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.GroupBox gbImageWindow;
        private HalconDotNet.HWindowControl viewPort;
        private System.Windows.Forms.DataGridView dgv_Resual;
        private System.Windows.Forms.GroupBox gb_Resual;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.TabControl tab_Message;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_CancleConfig;
        private System.Windows.Forms.GroupBox gb_ROI;
        private System.Windows.Forms.Button btn_SaveConfig;
        private System.Windows.Forms.Button btnLoadModelImage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grBImageList_Check;
        private System.Windows.Forms.ListBox ListBoxTrainImage;
        private System.Windows.Forms.GroupBox gb_ImageOperation;
        private System.Windows.Forms.Button btnCheckImage;
        private System.Windows.Forms.Button btnDelectCheckImage;
        private System.Windows.Forms.CheckBox cBcontinual;
        private System.Windows.Forms.Button btnDelectAllCheckImage;
        private System.Windows.Forms.Button btnLoadCheckImage;
        private System.Windows.Forms.DataGridView dgv_ROI;
    }
}