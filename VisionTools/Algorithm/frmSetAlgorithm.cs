using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionTools.Algorithm
{
    public partial class frmSetAlgorithm : DockContent
    {
        public frmSetAlgorithm()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.Dpi;
            this.DockAreas = DockAreas.Document;

            this._AppValue = AppValue.Instance();

            checkConfigDir(this._AppValue._ConfigDir);

        }

        AppValue _AppValue;



        void checkConfigDir(string configDir)
        {
            if (!System.IO.Directory.Exists(configDir))
            {
                System.IO.Directory.CreateDirectory(configDir);
            }
        }

        private void frmSetAlgorithm_Load(object sender, EventArgs e)
        {

        }
    }
}
