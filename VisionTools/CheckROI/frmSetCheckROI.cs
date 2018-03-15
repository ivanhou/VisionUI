using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionTools.CheckROI
{
    public partial class frmSetCheckROI : DockContent
    {
        public frmSetCheckROI()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.Dpi;
            this.DockAreas = DockAreas.Document;

            this._AppValue = AppValue.Instance();

            checkConfigDir(this._AppValue._ConfigDir);


            this._contextMenuStrip_ImageControl = new ContextMenuStrip();
            this.viewPort.ContextMenuStrip = this._contextMenuStrip_ImageControl;
            this._ImageControl = new ToolStripMenuItem("图像操作");
            this._ROIControl = new ToolStripMenuItem("ROI操作");

            this._zoomImage = new ToolStripMenuItem("缩放");
            this._zoomImage.Click += new EventHandler(zoomButton_CheckedChanged);
            this._ImageControl.DropDownItems.Add(this._zoomImage);
            this._moveImage = new ToolStripMenuItem("移动");
            this._moveImage.Click += new EventHandler(moveButton_CheckedChanged);
            this._ImageControl.DropDownItems.Add(this._moveImage);
            this._noneImage = new ToolStripMenuItem("None");
            this._noneImage.Click += new EventHandler(noneButton_CheckedChanged);
            this._ImageControl.DropDownItems.Add(this._noneImage);
            this._resetImage = new ToolStripMenuItem("复位");
            this._resetImage.Click += new EventHandler(resetButton_Click);
            this._ImageControl.DropDownItems.Add(this._resetImage);

            this._RoiLine = new ToolStripMenuItem("Line");
            this._RoiLine.Click += new EventHandler(LineButton_Click);
            this._ROIControl.DropDownItems.Add(this._RoiLine);
            this._RoiRectangle2 = new ToolStripMenuItem("Rectangle2");
            this._RoiRectangle2.Click += new EventHandler(Rect2Button_Click);
            this._ROIControl.DropDownItems.Add(this._RoiRectangle2);
            this._RoiCircle = new ToolStripMenuItem("Circle");
            this._RoiCircle.Click += new EventHandler(CircleButton_Click);
            this._ROIControl.DropDownItems.Add(this._RoiCircle);

            this.viewPort.ContextMenuStrip.Items.Add(this._ImageControl);
            this.viewPort.ContextMenuStrip.Items.Add(this._ROIControl);
        }



        AppValue _AppValue;
        private ViewWindow.ViewWindow _windowControl;
        private List<ViewWindow.Model.ROI> _regions;
        private List<Model.CheckRoiConfig> _checkRoiConfig;

        /// <summary>
        /// 当前模板图片
        /// </summary>
        private HObject ho_ModelImage;


        System.Windows.Forms.ContextMenuStrip _contextMenuStrip_ImageControl;

        System.Windows.Forms.ToolStripMenuItem _ImageControl;
        System.Windows.Forms.ToolStripMenuItem _zoomImage;
        System.Windows.Forms.ToolStripMenuItem _moveImage;
        System.Windows.Forms.ToolStripMenuItem _noneImage;
        System.Windows.Forms.ToolStripMenuItem _resetImage;

        System.Windows.Forms.ToolStripMenuItem _ROIControl;
        System.Windows.Forms.ToolStripMenuItem _RoiLine;
        System.Windows.Forms.ToolStripMenuItem _RoiRectangle2;
        System.Windows.Forms.ToolStripMenuItem _RoiCircle;



        #region 窗口图像操作

        private void zoomButton_CheckedChanged(object sender, EventArgs e)
        {
            this._windowControl.zoomWindowImage();
        }

        private void moveButton_CheckedChanged(object sender, EventArgs e)
        {
            this._windowControl.moveWindowImage();
        }

        private void noneButton_CheckedChanged(object sender, EventArgs e)
        {
            this._windowControl.noneWindowImage();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this._windowControl.resetWindowImage();
        }

        #endregion

        #region ROI绘制，创建添加新模板

        private void creatNewROI()
        {
            List<double> m_data;
            int m_index;

            ViewWindow.Model.ROI m_region = this._windowControl.smallestActiveROI(out m_data, out m_index);

            if (m_index > -1)
            {
                bool rct = Model.CheckRoiConfig.addROI( m_region, ref this._checkRoiConfig);

                //if (rct)
                //{
                //    binDataGridViewMatchingConfig(this.dgv_MatchingConfig, this._matchingConfig);
                //    Model.MatchingConfig.saveModelId(ho_ModelImage, this._matchingConfig[m_index], this._ModelIdSavePath);
                //    string msg = string.Format("创建模板成功 {0}", this.hv_ModelId.I);
                //    MessageBox.Show(msg, "系统提示", MessageBoxButtons.OK);
                //}
            }
            else
            {
                MessageBox.Show("请选择ROI", "系统提示", MessageBoxButtons.OK);
            }
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            this._windowControl.genLine(110.0, 110.0, 110.0, 210.0, ref this._regions);
            binDataGridViewROI(this.dgv_ROI, this._regions);
            creatNewROI();
        }

        private void Rect2Button_Click(object sender, EventArgs e)
        {
            this._windowControl.genRect2(200.0, 200.0, 0.0, 60.0, 30.0, ref this._regions);
            binDataGridViewROI(this.dgv_ROI, this._regions);
            creatNewROI();
        }

        private void CircleButton_Click(object sender, EventArgs e)
        {
            this._windowControl.genCircle(200.0, 200.0, 60.0, ref this._regions);
            binDataGridViewROI(this.dgv_ROI, this._regions);
            creatNewROI();
        }

        #endregion




        //显示ROI
        void binDataGridViewROI(DataGridView dgv, List<ViewWindow.Model.ROI> config)
        {
            dgv.DataSource = null;

            //DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            //column1.DataPropertyName = "ID";
            //column1.HeaderText = "编号";
            //column1.Name = "ID";
            //column1.Width = 40;
            //column1.ReadOnly = true;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "Type";
            column1.HeaderText = "类型";
            column1.Name = "Type";
            column1.Width = 90;
            column1.ReadOnly = true;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { column1 });
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AllowUserToDeleteRows = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;//禁止添加行
            dgv.AllowUserToDeleteRows = true;//禁止删除行
            //dgv.ContextMenuStrip = contextMenuStrip;
            dgv.DataSource = config;
            dgv.Refresh();
            if (config.Count > 0)
            {
                dgv.Rows[config.Count - 1].Cells[0].Selected = true;
            }
        }

        //初始化加载显示Matching配置文件
        void initConfig(string configPath)
        {
            this._checkRoiConfig = new List<Model.CheckRoiConfig>();
           
            this._regions = new List<ViewWindow.Model.ROI>();

            if (!System.IO.File.Exists(configPath))
            {
                return;
            }

            Model.CheckRoiConfig.loadXML(configPath, out this._checkRoiConfig);

            foreach (var m_matchingConfig in this._checkRoiConfig)
            {
                this._windowControl.genRegions(m_matchingConfig.ROIParameter, ref this._regions);
            }

            binDataGridViewROI(this.dgv_ROI, this._regions);
            //binDataGridViewMatchingConfig(this.dgv_MatchingConfig, this._matchingConfig);
            

        }

        void checkConfigDir(string configDir)
        {
            if (!System.IO.Directory.Exists(configDir))
            {
                System.IO.Directory.CreateDirectory(configDir);
            }
        }

        private void frmSetCheckROI_Load(object sender, EventArgs e)
        {
            this._windowControl = new ViewWindow.ViewWindow(this.viewPort);

            initConfig(this._AppValue._CheckRoiConfigPath);
        }

        private void frmSetCheckROI_Shown(object sender, EventArgs e)
        {
            int mw = this.gbImageWindow.Size.Width;
            int mh = this.gbImageWindow.Size.Height;

            int y = 18;
            int mh1 = mh - 2 * y;
            int mw1 = 5 * mh1 / 4;
            int x = (mw - mw1) / 2;

            this.viewPort.Location = new System.Drawing.Point(x, y);
            this.viewPort.Size = new System.Drawing.Size(mw1, mh1);
            this.viewPort.WindowSize = new System.Drawing.Size(mw1, mh1);


            int mRet = Model.CheckRoiConfig.loadModelImage(this._AppValue._ModelIdSavePath, out this.ho_ModelImage);

            if (mRet == 0)
            {
                this._windowControl.displayImage(this.ho_ModelImage);

                if (this._regions.Count > 0)
                {
                    this._windowControl.displayROI(this._regions);
                }                
            }
        }

        private void btnLoadModelImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;
            opnDlg.Multiselect = false;
            //opnDlg.InitialDirectory = startImagePath;

            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                string filename = opnDlg.FileName;

                HOperatorSet.GenEmptyObj(out this.ho_ModelImage);
                this.ho_ModelImage.Dispose();
                HOperatorSet.ReadImage(out this.ho_ModelImage, filename);

                this._windowControl.displayImage(this.ho_ModelImage);

                if (this._regions.Count > 0)
                {
                    this._windowControl.displayROI(this._regions);
                }                
            }
        }

        private void viewPort_HMouseUp(object sender, HMouseEventArgs e)
        {
            int index;
            List<double> data;

            ViewWindow.Model.ROI roi = this._windowControl.smallestActiveROI(out data, out index);

            if (index > -1)
            {
                this._windowControl.fixROI(index, roi, ref this._regions);
                Model.CheckRoiConfig.fixCheckRoiConfig(index, roi, ref this._checkRoiConfig);
            }
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            Model.CheckRoiConfig.saveXML(this._checkRoiConfig, this._AppValue._CheckRoiConfigPath);

            List<ViewWindow.Model.RoiData> m_ROIParameters = new List<ViewWindow.Model.RoiData>();
            foreach (var item in this._checkRoiConfig)
            {
                m_ROIParameters.Add(item.ROIParameter[0]);
            }

            HObject roiTrans, ho_arrow;
            PublicMethod.genContourXld(m_ROIParameters, out roiTrans, out ho_arrow);
            this.viewPort.HalconWindow.SetColor("green");
            HOperatorSet.DispObj(roiTrans, this.viewPort.HalconWindow);
            HOperatorSet.DispObj(ho_arrow, this.viewPort.HalconWindow);

            //HObject roiTrans, ho_modelROI;
            //HOperatorSet.GenEmptyObj(out roiTrans);
            //HOperatorSet.GenEmptyObj(out ho_modelROI);

            //foreach (var m_checkRoiConfig in this._checkRoiConfig)
            //{
            //    foreach (var roiData in m_checkRoiConfig.ROIParameter)
            //    {
            //        switch (roiData.Name)
            //        {
            //            //case "Rectangle1":
            //            //    ho_modelROI.Dispose();
            //            //    HOperatorSet.GenRectangle1(out ho_modelROI, roiData.Rectangle1.Row1, roiData.Rectangle1.Column1,
            //            //        roiData.Rectangle1.Row2, roiData.Rectangle1.Column2);
            //            //    break;
            //            case "Rectangle2":
            //                ho_modelROI.Dispose();
            //                HOperatorSet.GenRectangle2ContourXld(out ho_modelROI, roiData.Rectangle2.Row, roiData.Rectangle2.Column,
            //                    -roiData.Rectangle2.Phi, roiData.Rectangle2.Lenth1, roiData.Rectangle2.Lenth2);
            //                Console.WriteLine("phi = {0}", roiData.Rectangle2.Phi);
            //                break;
            //            case "Cicle":
            //                ho_modelROI.Dispose();
            //                HOperatorSet.GenCircleContourXld(out ho_modelROI, roiData.Circle.Row, roiData.Circle.Column, roiData.Circle.Radius,
            //                    0, 6.28318, "positive", 1);
            //                break;
            //            case "Line":

            //                HTuple hv_Rows = new HTuple();
            //                hv_Rows = hv_Rows.TupleConcat(roiData.Line.RowBegin);
            //                hv_Rows = hv_Rows.TupleConcat(roiData.Line.RowEnd);

            //                HTuple hv_Cols = new HTuple();
            //                hv_Cols = hv_Cols.TupleConcat(roiData.Line.ColumnBegin);
            //                hv_Cols = hv_Cols.TupleConcat(roiData.Line.ColumnEnd);

            //                ho_modelROI.Dispose();

            //                HOperatorSet.GenContourPolygonXld(out ho_modelROI, hv_Rows, hv_Cols);
            //                break;
            //        }

            //        {
            //            HObject ExpTmpOutVar_0;
            //            HOperatorSet.ConcatObj(roiTrans, ho_modelROI, out ExpTmpOutVar_0);
            //            roiTrans.Dispose();
            //            roiTrans = ExpTmpOutVar_0;
            //        }
            //    }
            //}

            //this.viewPort.HalconWindow.SetColor("green");
            //HOperatorSet.DispObj(roiTrans, this.viewPort.HalconWindow);

        }

        private void btn_CancleConfig_Click(object sender, EventArgs e)
        {
            Model.CheckRoiConfig.loadXML(this._AppValue._CheckRoiConfigPath, out this._checkRoiConfig);

            this._windowControl.displayImage(this.ho_ModelImage);

            this._regions = new List<ViewWindow.Model.ROI>();
            foreach (var m_matchingConfig in this._checkRoiConfig)
            {
                this._windowControl.genRegions(m_matchingConfig.ROIParameter, ref this._regions);
            }

            if (this._regions.Count > 0)
            {
                this._windowControl.displayROI(this._regions);
            }
        }

    }
}
