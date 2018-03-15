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

namespace VisionUI.Modle.Cam
{
    public partial class frmCamName : Form
    {
        public frmCamName()
        {
            InitializeComponent();
            this._TCameraInfo = new DataBaseConfig.Util.TCameraInfo(this._SQLContext);
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public delegate void CamNameDelegate(object sender, string camName);
        public event CamNameDelegate NewCamName;
        private SQLiteContext _SQLContext = new SQLiteContext(new SQLiteConnectionFactory(System.Environment.CurrentDirectory + "\\DataBase", "DataBase.db"));
        private DataBaseConfig.Util.TCameraInfo _TCameraInfo;
        public void SetResourceCulture()
        {
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmCamName_Text");
            
        }

        protected virtual void OnNewCamName(string camName)
        {
            if (this.NewCamName!=null)
            {
                this.NewCamName(this, camName);
            }
        }

        private void frmCamName_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._SQLContext.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtCamName.Text.Trim().Equals(""))
            {
                string mReeor = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("frmCamName_Error1"));
                MessageBox.Show(mReeor);
            }
            else
            {
                //查询数据库，判断相机名称是否重复
                List<CameraInfo> mCameraInfo = this._TCameraInfo.getCams();

                if (mCameraInfo.Count > 0)
                {
                    foreach (CameraInfo item in mCameraInfo)
                    {
                        if (item.Name.Equals(this.txtCamName.Text.Trim()))
                        {
                            string mReeor = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("frmCamName_Error2"));
                            MessageBox.Show(mReeor);
                            return;
                        }
                    }
                }

                OnNewCamName(this.txtCamName.Text.Trim());
                this.DialogResult = DialogResult.OK;
                this.txtCamName.Text = "";
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmCamName_Shown(object sender, EventArgs e)
        {
            SetResourceCulture();
            this.txtCamName.Focus();
        }
    }
}
