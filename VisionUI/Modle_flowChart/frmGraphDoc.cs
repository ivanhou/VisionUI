using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionUI.Modle
{
    public partial class frmGraphDoc : Form
    {
        public frmGraphDoc()
        {
            InitializeComponent();
        }

        protected void EnableControls(bool enable)
        {
            this.nameTextBox.Enabled = enable;
            this.okButton.Enabled = enable;
        }

        protected void UpdateDialog()
        {
            if (this.Doc == null) return;
            this.nameTextBox.Text = this.Doc.Name;
            EnableControls(!this.Doc.IsReadOnly);
        }

        protected void UpdateObject()
        {
            if (this.Doc == null) return;
            this.Doc.StartTransaction();
            this.Doc.Name = this.nameTextBox.Text;
            this.Doc.FinishTransaction("Change Document Properties");
        }

        public GraphDoc Doc
        {
            get { return myDoc; }
            set
            {
                if (value == null) return;
                myDoc = value;
                UpdateDialog();
            }
        }

        private GraphDoc myDoc = null;

        private void okButton_Click(object sender, EventArgs e)
        {
            UpdateObject();
        }
    }
}
