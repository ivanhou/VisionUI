using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionUI
{
    public partial class frmMain : DockContent
    {
        public frmMain()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            SetResourceCulture();
        }
        public void SetResourceCulture()
        {
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmMain_Text");
            
        }

    }
}
