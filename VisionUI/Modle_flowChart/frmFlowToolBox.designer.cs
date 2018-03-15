namespace VisionUI.Modle
{
    partial class frmFlowToolBox
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
            this.goPalette1 = new Northwoods.Go.GoPalette();
            this.SuspendLayout();
            // 
            // goPalette1
            // 
            this.goPalette1.AllowDelete = false;
            this.goPalette1.AllowEdit = false;
            this.goPalette1.AllowInsert = false;
            this.goPalette1.AllowLink = false;
            this.goPalette1.AllowMove = false;
            this.goPalette1.AllowReshape = false;
            this.goPalette1.AllowResize = false;
            this.goPalette1.ArrowMoveLarge = 10F;
            this.goPalette1.ArrowMoveSmall = 1F;
            this.goPalette1.AutoScrollRegion = new System.Drawing.Size(0, 0);
            this.goPalette1.BackColor = System.Drawing.Color.White;
            this.goPalette1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goPalette1.GridCellSizeHeight = 60F;
            this.goPalette1.GridCellSizeWidth = 60F;
            this.goPalette1.GridOriginX = 20F;
            this.goPalette1.GridOriginY = 5F;
            this.goPalette1.Location = new System.Drawing.Point(0, 0);
            this.goPalette1.Name = "goPalette1";
            this.goPalette1.ShowsNegativeCoordinates = false;
            this.goPalette1.Size = new System.Drawing.Size(199, 455);
            this.goPalette1.TabIndex = 0;
            this.goPalette1.Text = "goPalette1";
            this.goPalette1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.goPalette1_KeyDown);
            // 
            // frmFlowToolBox
            // 
            this.AutoHidePortion = 0.2D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 455);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.goPalette1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.Name = "frmFlowToolBox";
            this.Text = "frmToolBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFlowToolBox_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Northwoods.Go.GoPalette goPalette1;
    }
}