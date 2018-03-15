namespace VisionUI
{
    partial class frmMDI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCam = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSetImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMatch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectionLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelpDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.tsmCheckRoi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 382);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(585, 22);
            this.statusBar.TabIndex = 9;
            this.statusBar.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUser,
            this.tsmFile,
            this.tsmSystem,
            this.tsmSetImage,
            this.tsmSelectionLanguage,
            this.tsmHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(585, 25);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmUser
            // 
            this.tsmUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLogin,
            this.tsmLogout,
            this.tsmExit});
            this.tsmUser.Name = "tsmUser";
            this.tsmUser.Size = new System.Drawing.Size(47, 21);
            this.tsmUser.Text = "User";
            // 
            // tsmLogin
            // 
            this.tsmLogin.Name = "tsmLogin";
            this.tsmLogin.Size = new System.Drawing.Size(117, 22);
            this.tsmLogin.Text = "Login";
            this.tsmLogin.Click += new System.EventHandler(this.tsbLogin_out_Click);
            // 
            // tsmLogout
            // 
            this.tsmLogout.Name = "tsmLogout";
            this.tsmLogout.Size = new System.Drawing.Size(117, 22);
            this.tsmLogout.Text = "Logout";
            this.tsmLogout.Click += new System.EventHandler(this.tsbLogin_out_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(117, 22);
            this.tsmExit.Text = "Exit";
            this.tsmExit.Click += new System.EventHandler(this.tsbEXIT_Click);
            // 
            // tsmFile
            // 
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(39, 21);
            this.tsmFile.Text = "File";
            // 
            // tsmSystem
            // 
            this.tsmSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCam});
            this.tsmSystem.Name = "tsmSystem";
            this.tsmSystem.Size = new System.Drawing.Size(61, 21);
            this.tsmSystem.Text = "System";
            // 
            // tsmCam
            // 
            this.tsmCam.Name = "tsmCam";
            this.tsmCam.Size = new System.Drawing.Size(102, 22);
            this.tsmCam.Text = "Cam";
            this.tsmCam.Click += new System.EventHandler(this.tsmCam_Click);
            // 
            // tsmSetImage
            // 
            this.tsmSetImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmMatch,
            this.tsmCheckRoi});
            this.tsmSetImage.Name = "tsmSetImage";
            this.tsmSetImage.Size = new System.Drawing.Size(75, 21);
            this.tsmSetImage.Text = "SetImage";
            // 
            // tsmMatch
            // 
            this.tsmMatch.Name = "tsmMatch";
            this.tsmMatch.Size = new System.Drawing.Size(152, 22);
            this.tsmMatch.Text = "Match";
            this.tsmMatch.Click += new System.EventHandler(this.tsmMatch_Click);
            // 
            // tsmSelectionLanguage
            // 
            this.tsmSelectionLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCN,
            this.tsmUS});
            this.tsmSelectionLanguage.Name = "tsmSelectionLanguage";
            this.tsmSelectionLanguage.Size = new System.Drawing.Size(77, 21);
            this.tsmSelectionLanguage.Text = "Language";
            // 
            // tsmCN
            // 
            this.tsmCN.Name = "tsmCN";
            this.tsmCN.Size = new System.Drawing.Size(121, 22);
            this.tsmCN.Text = "Chinese";
            this.tsmCN.Click += new System.EventHandler(this.tsmChangeLanguage_Click);
            // 
            // tsmUS
            // 
            this.tsmUS.Name = "tsmUS";
            this.tsmUS.Size = new System.Drawing.Size(121, 22);
            this.tsmUS.Text = "English";
            this.tsmUS.Click += new System.EventHandler(this.tsmChangeLanguage_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHelpDoc,
            this.tsmAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(47, 21);
            this.tsmHelp.Text = "Help";
            // 
            // tsmHelpDoc
            // 
            this.tsmHelpDoc.Name = "tsmHelpDoc";
            this.tsmHelpDoc.Size = new System.Drawing.Size(130, 22);
            this.tsmHelpDoc.Text = "Help Doc";
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(130, 22);
            this.tsmAbout.Text = "About";
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 25);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(585, 357);
            this.dockPanel.TabIndex = 12;
            // 
            // tsmCheckRoi
            // 
            this.tsmCheckRoi.Name = "tsmCheckRoi";
            this.tsmCheckRoi.Size = new System.Drawing.Size(152, 22);
            this.tsmCheckRoi.Text = "CheckRoi";
            this.tsmCheckRoi.Click += new System.EventHandler(this.tsmCheckRoi_Click);
            // 
            // frmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 404);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMDI";
            this.Text = "VisionUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMDI_FormClosing);
            this.Load += new System.EventHandler(this.frmMDI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmUser;
        private System.Windows.Forms.ToolStripMenuItem tsmLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmLogout;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmHelpDoc;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectionLanguage;
        private System.Windows.Forms.ToolStripMenuItem tsmCN;
        private System.Windows.Forms.ToolStripMenuItem tsmUS;
        private System.Windows.Forms.ToolStripMenuItem tsmSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmCam;
        private System.Windows.Forms.ToolStripMenuItem tsmSetImage;
        private System.Windows.Forms.ToolStripMenuItem tsmMatch;
        private System.Windows.Forms.ToolStripMenuItem tsmCheckRoi;
    }
}

