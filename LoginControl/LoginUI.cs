using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginControl
{
    public partial class LoginUI : Form
    {
        public LoginUI(object sender)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this._Sender = sender;
            SetResourceCulture();
        }

        private object _Sender;

        private void SetResourceCulture()
        {
            this.labUser.Text = SelectionLanguage.ResourceCulture.GetString("LogUI_User_Text");
            this.labPwd.Text = SelectionLanguage.ResourceCulture.GetString("LogUI_Pwd_Text");
            this.btnOK.Text = SelectionLanguage.ResourceCulture.GetString("LogUI_btnOK_Text");
            this.btnCancle.Text = SelectionLanguage.ResourceCulture.GetString("LogUI_btnCancle_Text");
        }

        private void LoginUI_Load(object sender, EventArgs e)
        {

        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtUser.Text.Equals(""))
            {
                this.txtUser.BackColor = Color.Red;
                this.labMesage.Text = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("LoginUI_UserError"));
                return;
            }

            if (this.txtPassword.Text.Equals(""))
            {
                this.txtPassword.BackColor = Color.Red;
                this.labMesage.Text = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("LoginUI_PwdError"));
                return;
            }

            if (Login.Instance().trigger(this._Sender, this.txtUser.Text, this.txtPassword.Text))
            {
                this.Close();
            }
            else
            {
                this.labMesage.Text = string.Format("{0}", SelectionLanguage.ResourceCulture.GetString("LoginUI_UPError"));
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                if (Login.Instance().trigger(this._Sender, this.txtUser.Text, this.txtPassword.Text))
                {
                    this.Close();
                }
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            this.txtUser.BackColor = SystemColors.Window;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.txtPassword.BackColor = SystemColors.Window;
        }
    }
}
