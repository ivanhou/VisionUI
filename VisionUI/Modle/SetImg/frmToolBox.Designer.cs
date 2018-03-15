namespace VisionUI.Modle.SetImg
{
    partial class frmToolBox
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
            ToolBoxControl.ToolBoxCategory toolBoxCategory1 = new ToolBoxControl.ToolBoxCategory();
            ToolBoxControl.ToolBoxItem toolBoxItem1 = new ToolBoxControl.ToolBoxItem();
            ToolBoxControl.ToolBoxItem toolBoxItem2 = new ToolBoxControl.ToolBoxItem();
            ToolBoxControl.ToolBoxItem toolBoxItem3 = new ToolBoxControl.ToolBoxItem();
            ToolBoxControl.ToolBoxItem toolBoxItem4 = new ToolBoxControl.ToolBoxItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToolBox));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolBox1 = new ToolBoxControl.ToolBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "General";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.ImageIndex = 0;
            this.label2.Location = new System.Drawing.Point(0, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "         Pointer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // toolBox1
            // 
            toolBoxCategory1.ImageIndex = -1;
            toolBoxCategory1.IsOpen = false;
            toolBoxItem1.ImageIndex = -1;
            toolBoxItem1.Name = "Rectangel1";
            toolBoxItem1.Parent = null;
            toolBoxItem2.ImageIndex = -1;
            toolBoxItem2.Name = "Rectangle2";
            toolBoxItem2.Parent = null;
            toolBoxItem3.ImageIndex = -1;
            toolBoxItem3.Name = "Circle";
            toolBoxItem3.Parent = null;
            toolBoxItem4.ImageIndex = -1;
            toolBoxItem4.Name = "Line";
            toolBoxItem4.Parent = null;
            toolBoxCategory1.Items.Add(toolBoxItem1);
            toolBoxCategory1.Items.Add(toolBoxItem2);
            toolBoxCategory1.Items.Add(toolBoxItem3);
            toolBoxCategory1.Items.Add(toolBoxItem4);
            toolBoxCategory1.Name = "ROI";
            toolBoxCategory1.Parent = null;
            this.toolBox1.Categories.Add(toolBoxCategory1);
            this.toolBox1.CategoryBackColor = System.Drawing.Color.DarkGray;
            this.toolBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBox1.Location = new System.Drawing.Point(0, 38);
            this.toolBox1.Name = "toolBox1";
            this.toolBox1.Size = new System.Drawing.Size(220, 430);
            this.toolBox1.TabIndex = 10;
            this.toolBox1.Text = "toolBox1";
            // 
            // frmToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 468);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.toolBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmToolBox";
            this.Text = "ToolBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ToolBoxControl.ToolBox toolBox1;
    }
}