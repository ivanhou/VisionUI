using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionUI.Modle
{
    public partial class frmFlow : DockContent
    {
        public frmFlow()
        {
            _frmMain = this;

            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            SetResourceCulture();
        }

        private static frmFlow _frmMain;

        public GraphView View
        {
            get { return this.myView; }
        }
        
        public GraphDoc Doc
        {
            get { return this.myView.Doc; }
        }


        public void SetResourceCulture()
        {
            // Set the form title text
            this.Text = SelectionLanguage.ResourceCulture.GetString("frmFlow_Text");

        }
        protected override void OnLeave(EventArgs evt)
        {
            base.OnLeave(evt);
            this.View.DoEndEdit();
        }


        protected override void OnClosing(CancelEventArgs evt)
        {
            base.OnClosing(evt);
            if (this.Doc.IsModified)
            {
                IList windows = FindWindows(this.MdiParent, this.Doc);
                if (windows.Count <= 1)
                {  // only one left, better ask if we need to save
                    String msg = "Save modified graph?\r\n" + this.Doc.Name;
                    if (this.Doc.Location != "")
                        msg += "\r\n(" + this.Doc.Location + ") ";
                    DialogResult res = MessageBox.Show(this.MdiParent,
                                                       msg,
                                                       "Closing Modified Graph",
                                                       MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Cancel)
                    {
                        evt.Cancel = true;
                    }
                    else if (res == DialogResult.Yes)
                    {
                        if (!Save())
                            evt.Cancel = true;
                    }
                }
            }
        }

        protected override void OnClosed(EventArgs evt)
        {
            base.OnClosed(evt);
            IList windows = FindWindows(this.MdiParent, this.Doc);
            if (windows.Count <= 1)
                GraphDoc.RemoveDocument(this.Doc.Location);
        }

        public virtual bool Save()
        {
            String loc = this.Doc.Location;
            String loc_xml = string.Format("{0}_flowStep.xml", loc);
            int lastslash = loc.LastIndexOf("\\");
            if (loc != "" && !this.Doc.IsReadOnly && lastslash >= 0 && loc.Substring(lastslash + 1) == Doc.Name)
            {
                FileStream file = null;
                FileStream file_xml = null;
                try
                {
                    file = File.Open(loc, FileMode.Create);
                    this.Doc.Store(file, loc);
                    file_xml = File.Open(loc_xml, FileMode.Create);
                    this.Doc.StoreA(file_xml, loc_xml, View);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error saving graph as a file");
                    return false;
                }
                finally
                {
                    if (file != null)
                    {
                        file.Close();
                    }

                    if (file_xml != null)
                    {
                        file_xml.Close();
                    }
                }
                return true;
            }
            else
            {
                return SaveAs();
            }
        }

        public virtual bool SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = AppValue.Instance().FlowChartFile;
            if (Doc.Name != "")
                dlg.FileName = Doc.Name;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                String loc = dlg.FileName;
                String loc_xml = string.Format("{0}_flowStep.xml", loc);
                FileStream file = null;
                FileStream file_xml = null;
                try
                {
                    file = File.Open(loc, FileMode.Create);
                    this.Doc.Store(file, loc);
                    file_xml = File.Open(loc_xml, FileMode.Create);
                    this.Doc.StoreA(file_xml, loc_xml, View);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error saving graph as a file");
                    return false;
                }
                finally
                {
                    if (file != null)
                    {
                        file.Close();
                    }

                    if (file_xml != null)
                    {
                        file_xml.Close();
                    }
                }
            }
            return true;
        }

        public virtual bool Reload()
        {
            String loc = this.Doc.Location;
            if (loc != "")
            {
                FileStream file = File.Open(loc, FileMode.Open);
                GraphDoc olddoc = this.View.Doc;
                GraphDoc newdoc = null;
                try
                {
                    newdoc = GraphDoc.Load(file, loc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error reading graph from a file");
                    return false;
                }
                finally
                {
                    file.Close();
                }
                if (newdoc != null)
                {
                    IList windows = frmFlow.FindWindows(this.MdiParent, olddoc);
                    foreach (Object obj in windows)
                    {
                        frmFlow w = obj as frmFlow;
                        if (w != null)
                        {
                            w.View.Document = newdoc;
                        }
                    }
                }
            }
            return true;
        }


        public static frmFlow Open(Form mdiparent)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                String loc = dlg.FileName;
                GraphDoc olddoc = GraphDoc.FindDocument(loc);
                if (olddoc != null)
                {
                    IList windows = frmFlow.FindWindows(mdiparent, olddoc);
                    if (windows.Count > 0)
                    {
                        frmFlow w = windows[0] as frmFlow;
                        if (w.Reload())
                        {
                            w.Show();
                            w.Activate();
                        }
                        return w;
                    }
                }
                else
                {
                    Stream file = dlg.OpenFile();
                    if (file != null)
                    {
                        GraphDoc doc = null;
                        try
                        {
                            doc = GraphDoc.Load(file, loc);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(mdiparent, ex.Message, "Error reading graph from a file");
                        }
                        finally
                        {
                            file.Close();
                        }
                        if (doc != null)
                        {
                            //frmMain w = new frmMain();
                            //w.View.Document = doc;
                            //w.MdiParent = mdiparent;
                            //w.Show();
                            //w.Activate();
                            //return w;
                            _frmMain.View.Document = doc;
                            _frmMain.Show();
                            _frmMain.Activate();
                            return _frmMain;

                        }
                    }
                }
            }
            return null;
        }
        
        public static IList FindWindows(Form mdiparent, GraphDoc doc)
        {
            ArrayList windows = new ArrayList();
            Form[] children = mdiparent.MdiChildren;
            foreach (Form f in children)
            {
                frmFlow w = f as frmFlow;
                if (w != null && w.Doc == doc)
                {
                    windows.Add(w);
                }
            }
            return windows;
        }

        private void myView_ObjectDoubleClicked(object sender, Northwoods.Go.GoObjectEventArgs e)
        {
            if (e.GoObject.GetType().ToString().Equals("Northwoods.Go.GoLink"))
            {
                return;
            }

            frmDetails details = new frmDetails();

            if (details.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
