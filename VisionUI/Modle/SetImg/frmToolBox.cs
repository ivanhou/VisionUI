using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ToolBoxControl;

namespace VisionUI.Modle.SetImg
{
    public partial class frmToolBox : DockContent
    {
        public frmToolBox()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            SetResourceCulture();

            foreach (ToolBoxControl.ToolBoxCategory item in this.toolBox1.Categories)
            {
                foreach (ToolBoxItem OnToolBoxItem in item.Items)
                {
                    OnToolBoxItem.Clicked += new ToolBoxItem.ClickHandle(OnToolBoxItem_Clicked);
                }
            }
        }

        private void OnToolBoxItem_Clicked(object sender, EventArgs e)
        {
            string mName = ((ToolBoxItem)sender).Name;
            OnNewToolBoxClickEvent(new ToolBoxEventArgs(mName));
            //Console.WriteLine(((ToolBoxItem)sender).Name);
        }
        
        public static event EventHandler<ToolBoxEventArgs> NewToolBoxClickEvent;
                
        public void SetResourceCulture()
        {
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmToolBox_Text");

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
        
        protected virtual void OnNewToolBoxClickEvent(ToolBoxEventArgs e)
        {
            EventHandler<ToolBoxEventArgs> m_NewToolBoxClickEvent = NewToolBoxClickEvent;
            if (m_NewToolBoxClickEvent != null)
            {
                AsyncCallback EventDone = new AsyncCallback(CallBack);
                m_NewToolBoxClickEvent.BeginInvoke(this, e, null, e);
            }
        }
        private void CallBack(IAsyncResult result)
        {
            //AsyncResult ar = (AsyncResult)result;
            //Event.EventArgsStation1 mResult = (Event.EventArgsStation1)ar.AsyncDelegate;
            //var mResult = (Event.EventArgsStation1)result.AsyncState;
            //Console.WriteLine(" {0}  {1}  {2}", mResult.ID, mResult.ImgID, this._ImgInfo.Count);

            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //string mName = ((Label)sender).Name;
            //OnNewToolBoxClickEvent(new ToolBoxEventArgs(mName));
        }
    }
}
