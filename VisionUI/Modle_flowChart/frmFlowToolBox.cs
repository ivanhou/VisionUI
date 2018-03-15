using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionUI.Modle
{
    public partial class frmFlowToolBox : DockContent
    {
        public frmFlowToolBox()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            SetResourceCulture();

            //InitializeCatalog();
        }

        private void InitializeCatalog()
        {
            this.goPalette1.Document.Clear();
            GoComment c = new GoComment();
            c.Text = "Enter your comments here";
            this.goPalette1.Document.Add(c);
            GraphNode n;
            n = new GraphNode(GraphNodeKind.TestKind);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Start);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Step);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Input);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Output);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Decision);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Read);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Write);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.ManualOperation);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.Database);
            this.goPalette1.Document.Add(n);
            n = new GraphNode(GraphNodeKind.End);
            this.goPalette1.Document.Add(n);
        }

        public void SetResourceCulture()
        {
            InitializeCatalog();
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmFlowToolBox_Text");

            //this.tsbLogin.Text = ResourceCulture.GetString("frmMDI_tsbLogin");
            //this.tsbLogout.Text = ResourceCulture.GetString("frmMDI_tsbLogout");
            //this.tsbEXIT.Text = ResourceCulture.GetString("frmMDI_tsbEXIT");
            //this.tsbMain.Text = ResourceCulture.GetString("frmMDI_tsbMain");
            //this.tsbSelectionLanguage.Text = ResourceCulture.GetString("frmMDI_tsbSelectionLanguage");
            //this.tsmCN.Text = ResourceCulture.GetString("frmMDI_tsmCN");
            //this.tsmUS.Text = ResourceCulture.GetString("frmMDI_tsmUS");
            //this.tsbSetImg.Text = ResourceCulture.GetString("frmMDI_tsbSetImg"); ;
            //this.tsbHelp.Text = ResourceCulture.GetString("frmMDI_tsbHelp");
            //this.tsmHelp.Text = ResourceCulture.GetString("frmMDI_tsmHelp");
            //this.tsmAbout.Text = ResourceCulture.GetString("frmMDI_tsmAbout");

        }

        private void goPalette1_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Insert)
            {
                GraphView view = frmMDI.App.GetCurrentGraphView();
                GoObject obj = this.goPalette1.Selection.Primary;
                if (view != null && obj != null)
                {
                    view.StartTransaction();
                    GoObject newobj = view.Doc.AddCopy(obj, view.Doc.NextNodePosition());
                    view.FinishTransaction("Insert Node From Palette");
                }
            }*/
        }

        private void frmFlowToolBox_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
