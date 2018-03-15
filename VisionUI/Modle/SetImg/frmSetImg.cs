using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionUI.Modle.SetImg
{
    public partial class frmSetImg : DockContent
    {
        public frmSetImg()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            frmToolBox.NewToolBoxClickEvent += new EventHandler<ToolBoxEventArgs>(FrmToolBox_NewToolBoxClickEvent);
            SetResourceCulture();
        }

        public void SetResourceCulture()
        {
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmSetImg_Text");

        }

        private void FrmToolBox_NewToolBoxClickEvent(object sender, ToolBoxEventArgs e)
        {

            Console.WriteLine(e.Name);
        }

        private void frmSetImg_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmToolBox.NewToolBoxClickEvent -= new EventHandler<ToolBoxEventArgs>(FrmToolBox_NewToolBoxClickEvent);
        }
    }
}
