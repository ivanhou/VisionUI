namespace VisionUI.Modle
{
    partial class frmFlow
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
            this.myView = new VisionUI.Modle.GraphView();
            this.SuspendLayout();
            // 
            // myView
            // 
            this.myView.AllowEdit = false;
            this.myView.ArrowMoveLarge = 10F;
            this.myView.ArrowMoveSmall = 1F;
            this.myView.BackColor = System.Drawing.Color.White;
            this.myView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myView.GridCellSizeHeight = 10F;
            this.myView.GridCellSizeWidth = 5F;
            this.myView.GridSnapDrag = Northwoods.Go.GoViewSnapStyle.Jump;
            this.myView.Location = new System.Drawing.Point(0, 0);
            this.myView.Name = "myView";
            this.myView.PortGravity = 30F;
            this.myView.Size = new System.Drawing.Size(535, 367);
            this.myView.TabIndex = 0;
            this.myView.Text = "graphView1";
            this.myView.ObjectDoubleClicked += new Northwoods.Go.GoObjectEventHandler(this.myView_ObjectDoubleClicked);
            // 
            // frmFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 367);
            this.Controls.Add(this.myView);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Name = "frmFlow";
            this.Text = "FlowChart";
            this.ResumeLayout(false);

        }

        #endregion

        private GraphView myView;
    }
}