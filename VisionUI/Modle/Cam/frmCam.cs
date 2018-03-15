using Chloe.SQLite;
using DataBaseConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionUI.Modle.Cam
{
    public partial class frmCam : DockContent
    {
        public frmCam()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            SetResourceCulture();            
        }

        frmCamName _frmCamName;
        private SQLiteContext _SQLContext = new SQLiteContext(new SQLiteConnectionFactory(System.Environment.CurrentDirectory + "\\DataBase", "DataBase.db"));
        private DataBaseConfig.Util.TCameraInfo _TCameraInfo;
        private DataBaseConfig.Util.TCameraType _TCameraType;
        

        public void SetResourceCulture()
        {
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmCam_Text");

        }

        private void showCamInfo(CameraInfo cameraInfo)
        {
            this.gbSetCam.Enabled = true;
            this.txtCamName.Text = cameraInfo.Name;
            this.txtCamSerialNumber.Text = cameraInfo.SerialNumber;
            this.cmbCamTypeList.Text = cameraInfo.TypeName;
            this.numUDExposure.Value = Convert.ToDecimal(cameraInfo.Exposure);
            this.numUDGain.Value = Convert.ToDecimal(cameraInfo.Gain);
            this.cbCamUse.Checked = cameraInfo.Use;
        }

        private void saveCamInfo()
        {
            CameraInfo mCameraInfo = this._TCameraInfo.getCamByName(this.txtCamName.Text);

            mCameraInfo.Name = this.txtCamName.Text;
            mCameraInfo.SerialNumber = this.txtCamSerialNumber.Text.Trim();
            mCameraInfo.TypeName = this.cmbCamTypeList.Text;
            mCameraInfo.Exposure = Convert.ToInt32(this.numUDExposure.Value);
            mCameraInfo.Gain = Convert.ToDouble(this.numUDGain.Value);
            mCameraInfo.Use = this.cbCamUse.Checked;

            int rct = this._TCameraInfo.updateCam(mCameraInfo);

            string msg = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("frmCam_SaveInfo_Error")); 
            if (rct ==1)
            {
                msg = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("frmCam_SaveInfo_OK"));
            }

            MessageBox.Show(msg);
        }

        private void tv_CamList_MouseUp(object sender, MouseEventArgs e)
        {
            TreeView treev = sender as TreeView;            
            Point point = treev.PointToClient(Cursor.Position);
            TreeViewHitTestInfo info = treev.HitTest(point.X, point.Y);
            TreeNode node = info.Node;

            if (node == null)
            {
                this.tsmDelectCam.Enabled = false;
            }
            else
            {
                this.tsmDelectCam.Enabled = true;
            }

            if (node != null && MouseButtons.Right == e.Button)
            {

                treev.SelectedNode = node;//关键的一句话，右键点击菜单的时候，会选中右键点击的项
                
            }
            else if(node != null && MouseButtons.Left == e.Button)
            {
                CameraInfo mCameraInfo = this._TCameraInfo.getCamByName(node.Text);

                if (mCameraInfo != null)
                {
                    showCamInfo(mCameraInfo);
                }
            }
        }

        private void tsmAddCam_Click(object sender, EventArgs e)
        {
            this._frmCamName = new frmCamName();
            this._frmCamName.NewCamName += new frmCamName.CamNameDelegate(_frmCamName_NewCamName);
            this._frmCamName.ShowDialog();
        }

        private void tsmDelectCam_Click(object sender, EventArgs e)
        {
            if (this.tv_CamList.SelectedNode != null)
            {
                int rct = this._TCameraInfo.delectCameraInfo(this.tv_CamList.SelectedNode.Text);

                if (rct != -1)
                {
                    this.tv_CamList.SelectedNode.Remove();
                }
            }
        }

        private void _frmCamName_NewCamName(object sender, string camName)
        {
            int rct = this._TCameraInfo.addCameraInfo(camName);
            if (rct != -1)
            {
                this.tv_CamList.Nodes.Add(camName);
            }

            this._frmCamName.NewCamName -= new frmCamName.CamNameDelegate(_frmCamName_NewCamName);
        }

        private void frmCam_Load(object sender, EventArgs e)
        {
            this._TCameraInfo = new DataBaseConfig.Util.TCameraInfo(this._SQLContext);
            this._TCameraType = new DataBaseConfig.Util.TCameraType(this._SQLContext);

            List<CameraInfo> mCameraInfos = this._TCameraInfo.getCams();
            foreach (CameraInfo item in mCameraInfos)
            {
                this.tv_CamList.Nodes.Add(item.Name);
            }
            
            List<CameraType> mCameraTypes = this._TCameraType.getCameraTypes();

            foreach (CameraType item in mCameraTypes)
            {
                this.cmbCamTypeList.Items.Add(item.TypeName);
            }

            this.gbSetCam.Enabled = false;
        }

        private void frmCam_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._SQLContext.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveCamInfo();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            CameraInfo mCameraInfo = this._TCameraInfo.getCamByName(this.txtCamName.Text);

            if (mCameraInfo != null)
            {
                showCamInfo(mCameraInfo);
            }
        }

        private void cmbCamTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbCamTypeList.Text.Equals("File"))
            {
                this.pnlFilePath.Enabled = true;
                this.pnlSerialNum.Enabled = false;
                this.pnlExposure.Enabled = false;
                this.pnlGain.Enabled = false;
            }
            else
            {
                this.pnlFilePath.Enabled = false;
                this.pnlSerialNum.Enabled = true;
                this.pnlExposure.Enabled = true;
                this.pnlGain.Enabled = true;
            }
            
        }
    }
}
