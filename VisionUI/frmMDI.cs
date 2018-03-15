using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using HalconDotNet;

namespace VisionUI
{
    public partial class frmMDI : Form
    {
        public frmMDI()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            this._deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            this.dockPanel.Theme = this.vS2015DarkTheme1;
            if (this.dockPanel.Theme.ColorPalette != null)
            {
                this.statusBar.BackColor = this.dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
            }
            
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (File.Exists(configFile))
            {
                this.dockPanel.LoadFromXml(configFile, this._deserializeDockContent);
            }

            this.WindowState = FormWindowState.Maximized;
            this._Appvalue = AppValue.Instance();
            this._Appvalue.setMDIForm(this);
            this._Appvalue.getSystemConfig();
            SelectionLanguage.ResourceCulture.SetCurrentCulture((SelectionLanguage.Language)this._Appvalue.SystemConfig.Language);
            this.SetResourceCulture();

        }

        private AppValue _Appvalue;
        private DeserializeDockContent _deserializeDockContent;
        private frmMain _frmMain;
        private frmSetSys _frmSetSys;
        private Modle.SetImg.frmSetImg _frmSetImg;
        private Modle.SetImg.frmToolBox _frmToolBox;
        private Modle.Cam.frmCam _frmCam;

        private VisionTools.Match.frmSetMatch _frmSetMatch;
        private VisionTools.CheckROI.frmSetCheckROI _frmSetCheckROI;

        private void SetResourceCulture()
        {
            // Set the form title text
            //this.Text = ResourceCulture.GetString("Form1_frmText");

            this.tsmUser.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmUser");
            this.tsmLogin.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmLogin");
            this.tsmLogout.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmLogout");
            this.tsmExit.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmEXIT");

            this.tsmFile.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmFile");

            this.tsmSystem.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmSystem");
            this.tsmCam.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmCam");

            this.tsmSetImage.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmSetImage");

            //this.tsbMain.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsbMain");
            this.tsmSelectionLanguage.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmSelectionLanguage");
            this.tsmCN.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmCN");
            this.tsmUS.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmUS");
            //this.tsbSysSet.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsbSysSet");
            //this.tsmFlowChar.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmFlowChar");
            //this.tsbSetImg.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsbSetImg"); ;
            this.tsmHelp.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmHelp");
            this.tsmHelpDoc.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmHelpDoc");
            this.tsmAbout.Text = SelectionLanguage.ResourceCulture.GetString("frmMDI_tsmAbout");

        }
        

        private void initUI()
        {
            this._frmMain = new frmMain();
            //Form mfrm;
            //setChildWindow(typeof(frmMain), out mfrm);
            //this._frmMain = mfrm as frmMain;
            this._frmMain.Show(this.dockPanel, DockState.Document);
            this._frmMain.BringToFront();
            this._frmMain.Activate();
            this._frmMain.Activated += new EventHandler(_frmMain_Activated);
            //this._winname = WINNAME.main;

            //this._frmMain.SetResourceCulture();
        }
        
        private void changeLogin(bool flag)
        {
            //foreach (ToolStripItem item in this.tsMenu.Items)
            //{
            //    if (!item.Name.Equals("tsbHelp"))
            //    {
            //        item.Enabled = flag;
            //    }
            //}
            
            if (flag)
            {
                this.tsmLogin.Enabled = false;
                this.tsmLogout.Enabled = true;
            }
            else
            {
                this.tsmLogin.Enabled = true;
                this.tsmLogout.Enabled = false;
            }

            this.tsmExit.Enabled = true;
            this.tsmHelpDoc.Enabled = true;

            this._Appvalue.LoginStatus = flag;
        }

        private void _frmMain_Activated(object sender, EventArgs e)
        {
            if (this._frmToolBox != null)
            {
                this._frmToolBox.Hide();
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            //if (persistString == typeof(frmToolBox).ToString())
            //    return this._frmToolBox;
            //else
                return null;
        }

        private void frmMDI_Load(object sender, EventArgs e)
        {
            initUI();
            changeLogin(false);


            this._Appvalue._Station1 = new ImgProcess.Station.Station1(new EventHandler<ImgProcess.Event.EventArgsStation1>(Station1));

            this._Appvalue._ICamera = new CamFile.Camera("cam");
            this._Appvalue._ICamera.NewImageEvent += _ICamera_NewImageEvent;
            //this._Appvalue._ICamera.Open(@"E:\code128");
            this._Appvalue._ICamera.Open(Application.StartupPath+"\\Test_Image");
            this._Appvalue._ICamera.StartGrab();
        }

        void Station1(object sender, ImgProcess.Event.EventArgsStation1 e)
        {
            Console.WriteLine("Station1 : {0}  {1}", e.ImgID, e.Result);
        }

        private void _ICamera_NewImageEvent(object sender, CamCommon.CameraEventArgs e)
        {
            HObject ho_Image;
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.GenImage1(out ho_Image, "byte", e.Width, e.Height, e.Pixels);
            //throw new NotImplementedException();

            this._Appvalue._Station1.addImg(e.ID, ho_Image);

            HTuple hv_Width, hv_Height;
            HTuple hv_Mean, hv_Deviation;

            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.Intensity(ho_Image, ho_Image, out hv_Mean, out hv_Deviation);

            Console.WriteLine("ICamera : {0}    {1}    {2}", e.ID, hv_Mean.D, hv_Deviation.D);
        }

        private void tsbLogin_out_Click(object sender, EventArgs e)
        {
            LoginControl.Login.Instance().NewLoginEvent += new EventHandler<LoginControl.LoginEventArgs>(FrmMDI_NewLoginEvent);
            LoginControl.LoginUI mLoginUI = new LoginControl.LoginUI(sender);
            mLoginUI.ShowDialog();
        }

        private void FrmMDI_NewLoginEvent(object sender, LoginControl.LoginEventArgs e)
        {
            if (e.IsLogin)
            {
                if (((ToolStripMenuItem)e.Sender).Name.Equals("tsmLogin"))
                {
                    changeLogin(true);
                }
                else
                {
                    changeLogin(false);
                }
            }
            
            LoginControl.Login.Instance().NewLoginEvent -= new EventHandler<LoginControl.LoginEventArgs>(FrmMDI_NewLoginEvent);
        }

        private void tsbEXIT_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msgTitle = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("frmMDI_ClosingTitle"));
            string msg = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("frmMDI_ClosingMsg"));
                        
            if (MessageBox.Show(msg, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void tsbMain_Click(object sender, EventArgs e)
        {
            //this._frmMain.BringToFront();
            //this._frmMain.Activate();
        }

        private void tsbSetImg_Click(object sender, EventArgs e)
        {
            if (this._frmSetImg == null)
            {
                this._frmSetImg = new Modle.SetImg.frmSetImg();
                this._frmSetImg.FormClosing += new FormClosingEventHandler(_frmSetImg_FormClosing);
                this._frmSetImg.Activated += new EventHandler(_frmSetImg_Activated);
                //this._frmSetImg.SetResourceCulture();
                this._frmSetImg.Show(this.dockPanel);

                this._frmToolBox = new Modle.SetImg.frmToolBox();                
                SelectionLanguage.ResourceCulture.SetCurrentCulture(SelectionLanguage.ResourceCulture.CurrentLanguage);
                //this._frmToolBox.SetResourceCulture();
                this._frmToolBox.Show(this.dockPanel, DockState.DockLeft);
            }

            this._frmSetImg.BringToFront();
            this._frmSetImg.Activate();
        }

        private void _frmSetImg_Activated(object sender, EventArgs e)
        {
            if (this._frmToolBox != null)
            {
                this._frmToolBox.Show();
            }
        }

        private void _frmSetImg_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            //dockPanel.SaveAsXml(configFile);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this._frmSetImg.Activated -= new EventHandler(_frmSetImg_Activated);
                this._frmSetImg.FormClosing -= new FormClosingEventHandler(_frmSetImg_FormClosing);
                this._frmSetImg = null;
                this._frmToolBox.Close();
                this._frmToolBox = null;
                this.dockPanel.Refresh();
            }
        }

        private void tsmChangeLanguage_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Name.Equals("tsmCN"))
            {
                this._Appvalue.SystemConfig.Language = Convert.ToInt32(SelectionLanguage.Language.zh_CN);
            }
            else if (((ToolStripMenuItem)sender).Name.Equals("tsmUS"))
            {
                this._Appvalue.SystemConfig.Language = Convert.ToInt32(SelectionLanguage.Language.en_US);
            }
            this._Appvalue.updateSystemConfig();
            this._Appvalue.getSystemConfig();
            SelectionLanguage.ResourceCulture.SetCurrentCulture((SelectionLanguage.Language)this._Appvalue.SystemConfig.Language);

            this.SetResourceCulture();
            this._frmMain.SetResourceCulture();
            if (this._frmSetImg!=null)
            {
                this._frmSetImg.SetResourceCulture();
            }

            if (this._frmToolBox!=null)
            {
                this._frmToolBox.SetResourceCulture();
            }            
        }

        private void _frmFlow_Activated(object sender, EventArgs e)
        {
            if (this._frmToolBox != null)
            {
                this._frmToolBox.Hide();
            }
        }

        private void _frmFlow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {

            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
           
        }

        private void tsmCam_Click(object sender, EventArgs e)
        {
            if (this._frmCam == null)
            {
                this._frmCam = new Modle.Cam.frmCam();
                this._frmCam.FormClosing += new FormClosingEventHandler(_frmCam_FormClosing);
                this._frmCam.Activated += new EventHandler(_frmCam_Activated);
                this._frmCam.Show(this.dockPanel);                
            }

            this._frmCam.BringToFront();
            this._frmCam.Activate();
        }

        private void _frmCam_Activated(object sender, EventArgs e)
        {
            if (this._frmToolBox != null)
            {
                this._frmToolBox.Hide();
            }
        }

        private void _frmCam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this._frmCam.Activated -= new EventHandler(_frmSetImg_Activated);
                this._frmCam.FormClosing -= new FormClosingEventHandler(_frmSetImg_FormClosing);
                this._frmCam = null;
                this.dockPanel.Refresh();
            }
        }

        private void tsmMatch_Click(object sender, EventArgs e)
        {
            if (this._frmSetMatch == null)
            {
                this._frmSetMatch = new VisionTools.Match.frmSetMatch();
                this._frmSetMatch.FormClosing += new FormClosingEventHandler(_frmSetMatch_FormClosing);
                this._frmSetMatch.Activated += new EventHandler(_frmSetMatch_Activated);
                this._frmSetMatch.Show(this.dockPanel, DockState.Document);
            }

            this._frmSetMatch.BringToFront();
            this._frmSetMatch.Activate();
        }

        private void _frmSetMatch_Activated(object sender, EventArgs e)
        {
            if (this._frmToolBox != null)
            {
                this._frmToolBox.Hide();
            }
        }

        private void _frmSetMatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this._frmSetMatch.Activated -= new EventHandler(_frmSetMatch_Activated);
                this._frmSetMatch.FormClosing -= new FormClosingEventHandler(_frmSetMatch_FormClosing);
                this._frmSetMatch = null;
                this.dockPanel.Refresh();
            }
        }

        private void tsmCheckRoi_Click(object sender, EventArgs e)
        {

            if (this._frmSetCheckROI == null)
            {
                this._frmSetCheckROI = new VisionTools.CheckROI.frmSetCheckROI();
                this._frmSetCheckROI.FormClosing += new FormClosingEventHandler(_frmSetCheckROI_FormClosing);
                this._frmSetCheckROI.Activated += new EventHandler(_frmSetCheckROI_Activated);
                this._frmSetCheckROI.Show(this.dockPanel, DockState.Document);
            }

            this._frmSetCheckROI.BringToFront();
            this._frmSetCheckROI.Activate();
        }

        private void _frmSetCheckROI_Activated(object sender, EventArgs e)
        {
            if (this._frmToolBox != null)
            {
                this._frmToolBox.Hide();
            }
        }

        private void _frmSetCheckROI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this._frmSetCheckROI.Activated -= new EventHandler(_frmSetCheckROI_Activated);
                this._frmSetCheckROI.FormClosing -= new FormClosingEventHandler(_frmSetCheckROI_FormClosing);
                this._frmSetCheckROI = null;
                this.dockPanel.Refresh();
            }
        }

    }
}
