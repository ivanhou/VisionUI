using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using HalconDotNet;
using System.Collections;
using DataGridViewNumericUpDownElements;

namespace VisionTools.Match
{
    public partial class frmSetMatch : DockContent
    {
        public frmSetMatch()
        {
            InitializeComponent();


            AutoScaleMode = AutoScaleMode.Dpi;
            this.DockAreas = DockAreas.Document;

            this._AppValue = AppValue.Instance();

            checkConfigDir(this._AppValue._ConfigDir);


            this._contextMenuStrip_ImageControl = new ContextMenuStrip();
            this.viewPort.ContextMenuStrip = this._contextMenuStrip_ImageControl;
            this._ImageControl = new ToolStripMenuItem("图像操作");
            this._ROIControl = new ToolStripMenuItem("添加模板ROI");

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

            this._RoiRectangle1 = new ToolStripMenuItem("Rectangle1");
            this._RoiRectangle1.Click += new EventHandler(Rect1Button_Click);
            this._ROIControl.DropDownItems.Add(this._RoiRectangle1);
            this._RoiRectangle2 = new ToolStripMenuItem("Rectangle2");
            this._RoiRectangle2.Click += new EventHandler(Rect2Button_Click);
            this._ROIControl.DropDownItems.Add(this._RoiRectangle2);
            this._RoiCircle = new ToolStripMenuItem("Circle");
            this._RoiCircle.Click += new EventHandler(CircleButton_Click);
            this._ROIControl.DropDownItems.Add(this._RoiCircle);

            this.viewPort.ContextMenuStrip.Items.Add(this._ImageControl);
            this.viewPort.ContextMenuStrip.Items.Add(this._ROIControl);
        }

        #region MyRegion

        private ViewWindow.ViewWindow _windowControl;
        private Matching _matching;

        private List<ViewWindow.Model.ROI> _regions;
        private List<Model.MatchingConfig> _matchingConfig;

        private bool _flagNewModelImage = false;
        /// <summary>
        /// 当前模板图片
        /// </summary>
        private HObject ho_ModelImage;
        /// <summary>
        /// 当前模板Id
        /// </summary>
        private HTuple hv_ModelId;
        /// <summary>
        /// 检测结果集合
        /// </summary>
        private List<Model.MatchingResual> _matchingResual = new List<Model.MatchingResual>();

        private List<CheckROI.Model.CheckRoiConfig> _checkRoiConfig;
        private List<ViewWindow.Model.ROI> _regionsCheckRoi;

        AppValue _AppValue;
        
        /// <summary>
        /// 待检测的图像组
        /// </summary>
        Hashtable _listImages_Training = new Hashtable();


        System.Windows.Forms.ContextMenuStrip _contextMenuStrip_ImageControl;

        System.Windows.Forms.ToolStripMenuItem _ImageControl;
        System.Windows.Forms.ToolStripMenuItem _zoomImage;
        System.Windows.Forms.ToolStripMenuItem _moveImage;
        System.Windows.Forms.ToolStripMenuItem _noneImage;
        System.Windows.Forms.ToolStripMenuItem _resetImage;

        System.Windows.Forms.ToolStripMenuItem _ROIControl;
        System.Windows.Forms.ToolStripMenuItem _RoiRectangle1;
        System.Windows.Forms.ToolStripMenuItem _RoiRectangle2;
        System.Windows.Forms.ToolStripMenuItem _RoiCircle;

        #endregion


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

        //创建新模板
        private void creatNewShapeModel()
        {
            if (this._regions.Count>0)
            {
                bool rct = Model.MatchingConfig.creatShapeModel(this.ho_ModelImage, this._regions, ref this._matchingConfig, out this.hv_ModelId);

                if (rct)
                {
                    binDataGridView_CreatModel(this.dgv_CreatModelParameter, this._matchingConfig[0].CreatModelParameter);
                    binDataGridView_FindModel(this.dgv_FindModelParameter, this._matchingConfig[0].FindModelParameter);
                    
                    string msg = string.Format("创建模板成功 {0}", this.hv_ModelId.I);
                    MessageBox.Show(msg, "系统提示", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("请添加ROI", "系统提示", MessageBoxButtons.OK);
            }
        }

        private void Rect1Button_Click(object sender, EventArgs e)
        {
            this._windowControl.genRect1(110.0, 110.0, 210.0, 210.0, ref this._regions);
            //binDataGridViewROI(this.dgv_ROI, this._regions);
            //AddNewShapeModel();
        }

        private void Rect2Button_Click(object sender, EventArgs e)
        {
            this._windowControl.genRect2(200.0, 200.0, 0.0, 60.0, 30.0, ref this._regions);
            //binDataGridViewROI(this.dgv_ROI, this._regions);
            //AddNewShapeModel();
        }

        private void CircleButton_Click(object sender, EventArgs e)
        {
            this._windowControl.genCircle(200.0, 200.0, 60.0, ref this._regions);
            //binDataGridViewROI(this.dgv_ROI, this._regions);
            //AddNewShapeModel();
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
        //显示配置文件集合
        void binDataGridViewMatchingConfig(DataGridView dgv, List<Model.MatchingConfig> config)
        {
            dgv.DataSource = null;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "ID";
            column1.HeaderText = "编号";
            column1.Name = "ID";
            column1.Width = 40;
            column1.ReadOnly = true;

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "Name";
            column2.HeaderText = "类型";
            column2.Name = "Name";
            column2.Width = 70;
            column2.ReadOnly = true;

            DataGridViewCheckBoxColumn column3 = new DataGridViewCheckBoxColumn();
            column3.DataPropertyName = "Enabled";
            column3.HeaderText = "是否启用";
            column3.Name = "Enabled";
            column3.Width = 45;
            column3.ReadOnly = false;

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.DataPropertyName = "RoiParameter";
            column4.HeaderText = "RoiParameter";
            column4.Name = "RoiParameter";
            column4.Width = 60;
            column4.ReadOnly = true;
            column4.Visible = false;

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.DataPropertyName = "CreatModelParameter";
            column5.HeaderText = "CreatModelParameter";
            column5.Name = "CreatModelParameter";
            column5.Width = 60;
            column5.ReadOnly = true;
            column5.Visible = false;

            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.DataPropertyName = "CreatModelParameter";
            column6.HeaderText = "CreatModelParameter";
            column6.Name = "CreatModelParameter";
            column6.Width = 60;
            column6.ReadOnly = true;
            column6.Visible = false;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { column1, column2, column3, column4, column5, column6 });
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
        //显示配置文件数据集合
        void binDataGridView_CreatModel(DataGridView dgv, List<Model.CreatModelParameter> config)
        {
            dgv.DataSource = null;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            //column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column1.DataPropertyName = "ID";
            column1.HeaderText = "编号";
            column1.Name = "ID";
            column1.Width = 40;
            column1.ReadOnly = true;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column2 = new DataGridViewNumericUpDownColumn();
            //column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column2.Minimum = Convert.ToDecimal(-360);
            column2.Maximum = Convert.ToDecimal(360);
            column2.Increment = Convert.ToDecimal(1);
            column2.DataPropertyName = "AngleStart";
            column2.HeaderText = "起始角度";
            column2.Name = "AngleStart";
            column2.Width = 70;
            column2.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column3 = new DataGridViewNumericUpDownColumn();
            //column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column3.Minimum = Convert.ToDecimal(0);
            column3.Maximum = Convert.ToDecimal(360);
            column3.Increment = Convert.ToDecimal(1);
            column3.DataPropertyName = "AngleExtent";
            column3.HeaderText = "角度范围";
            column3.Name = "AngleExtent";
            column3.Width = 70;
            column3.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column4 = new DataGridViewNumericUpDownColumn();
            //column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column4.Minimum = 1;
            column4.Maximum = 6;
            column4.Increment = 1;
            column4.DataPropertyName = "NumLevels";
            column4.HeaderText = "金字塔等级";
            column4.Name = "NumLevels";
            column4.Width = 70;
            column4.ReadOnly = false;


            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { column1, column2, column3, column4 });
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AllowUserToDeleteRows = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            dgv.DataSource = config;

            for (int i = 0; i < config.Count; i++)
            {
                if (i % 2 == 1)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        //显示配置文件数据集合
        void binDataGridView_FindModel(DataGridView dgv, List<Model.FindModelParameter> config)
        {
            dgv.DataSource = null;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            //column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column1.DataPropertyName = "ID";
            column1.HeaderText = "编号";
            column1.Name = "ID";
            column1.Width = 40;
            column1.ReadOnly = true;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column2 = new DataGridViewNumericUpDownColumn();
            //column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column2.Minimum = Convert.ToDecimal(-360);
            column2.Maximum = Convert.ToDecimal(360);
            column2.Increment = Convert.ToDecimal(1);
            column2.DataPropertyName = "AngleStart";
            column2.HeaderText = "起始角度";
            column2.Name = "AngleStart";
            column2.Width = 70;
            column2.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column3 = new DataGridViewNumericUpDownColumn();
            //column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column3.Minimum = Convert.ToDecimal(0);
            column3.Maximum = Convert.ToDecimal(360);
            column3.Increment = Convert.ToDecimal(1);
            column3.DataPropertyName = "AngleExtent";
            column3.HeaderText = "角度范围";
            column3.Name = "AngleExtent";
            column3.Width = 70;
            column3.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column4 = new DataGridViewNumericUpDownColumn();
            //column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column4.Minimum = Convert.ToDecimal(0.0);
            column4.Maximum = Convert.ToDecimal(1.0);
            column4.Increment = Convert.ToDecimal(0.01);
            column4.DecimalPlaces = 2;
            column4.DataPropertyName = "MinScore";
            column4.HeaderText = "最小匹配分数";
            column4.Name = "MinScore";
            column4.Width = 70;
            column4.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column5 = new DataGridViewNumericUpDownColumn();
            //column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column5.Minimum = 0;
            column5.Maximum = 5;
            column5.Increment = 1;
            column5.DataPropertyName = "NumMatches";
            column5.HeaderText = "最大匹配个数";
            column5.Name = "NumMatches";
            column5.Width = 70;
            column5.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column6 = new DataGridViewNumericUpDownColumn();
            //column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column6.Minimum = Convert.ToDecimal(0.0);
            column6.Maximum = Convert.ToDecimal(1.0);
            column6.Increment = Convert.ToDecimal(0.01);
            column6.DecimalPlaces = 2;
            column6.DataPropertyName = "Greediness";
            column6.HeaderText = "贪婪度";
            column6.Name = "Greediness";
            column6.Width = 70;
            column6.ReadOnly = false;

            DataGridViewNumericUpDownElements.DataGridViewNumericUpDownColumn column7 = new DataGridViewNumericUpDownColumn();
            //column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column7.Minimum = 1;
            column7.Maximum = 6;
            column7.Increment = 1;
            column7.DataPropertyName = "NumLevels";
            column7.HeaderText = "金字塔等级";
            column7.Name = "NumLevels";
            column7.Width = 70;
            column7.ReadOnly = false;


            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { column1, column2, column3, column4, column5, column6, column7 });
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AllowUserToDeleteRows = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            dgv.DataSource = config;

            for (int i = 0; i < config.Count; i++)
            {
                if (i % 2 == 1)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        
        //显示配置文件数据集合
        void binDataGridView_MatchResual(DataGridView dgv, List<Model.MatchingResual> config)
        {
            dgv.DataSource = null;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            //column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column1.DataPropertyName = "ID";
            column1.HeaderText = "编号";
            column1.Name = "ID";
            column1.Width = 40;
            column1.ReadOnly = true;

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            //column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column2.DataPropertyName = "Row";
            column2.HeaderText = "行坐标";
            column2.Name = "Row";
            column2.Width = 60;
            column2.ReadOnly = true;

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            //column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column3.DataPropertyName = "Column";
            column3.HeaderText = "列坐标";
            column3.Name = "Column";
            column3.Width = 60;
            column3.ReadOnly = true;

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            //column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column4.DataPropertyName = "Angle";
            column4.HeaderText = "角度";
            column4.Name = "Angle";
            column4.Width = 60;
            column4.ReadOnly = true;

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            //column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column5.DataPropertyName = "Score";
            column5.HeaderText = "分数";
            column5.Name = "Score";
            column5.Width = 60;
            column5.ReadOnly = true;


            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { column1, column2, column3, column4, column5 });
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = true;

            dgv.DataSource = config;

            for (int i = 0; i < config.Count; i++)
            {
                if (i % 2 == 1)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        

        //初始化加载显示Matching配置文件
        void initConfig(string configPath)
        {
            this._matchingConfig = new List<Model.MatchingConfig>();
            this._regions = new List<ViewWindow.Model.ROI>();

            if (!System.IO.File.Exists(configPath))
            {
                return;
            }

            Model.MatchingConfig.loadXML(configPath, out this._matchingConfig);

            foreach (var m_matchingConfig in this._matchingConfig)
            {
                this._windowControl.genRegions(m_matchingConfig.ROIParameter, ref this._regions);
            }

            //binDataGridViewROI(this.dgv_ROI, this._regions);
            //binDataGridViewMatchingConfig(this.dgv_MatchingConfig, this._matchingConfig);

            if (this._matchingConfig.Count>0)
            {
                binDataGridView_CreatModel(this.dgv_CreatModelParameter, this._matchingConfig[0].CreatModelParameter);
                binDataGridView_FindModel(this.dgv_FindModelParameter, this._matchingConfig[0].FindModelParameter);
            }

        }

        void checkConfigDir(string configDir)
        {
            if (!System.IO.Directory.Exists(configDir))
            {
                System.IO.Directory.CreateDirectory(configDir);
            }
        }

        private void frmSetMatch_Load(object sender, EventArgs e)
        {
            this._windowControl = new ViewWindow.ViewWindow(this.viewPort);
            this._matching = new Matching();
            this._matching.MatchingResualCallback += new EventHandler<MatchingResualArgs>(_matching_MatchingResualCallback);
            initConfig(this._AppValue._MatchingConfigPath);
        }

        private void frmSetMatch_Shown(object sender, EventArgs e)
        {

            int mw = this.gbImageWindow.Size.Width;
            int mh = this.gbImageWindow.Size.Height;

            int y = 18;
            int mh1 = mh - 2 * y;
            int mw1 = 5* mh1/4;
            int x = (mw - mw1) / 2;

            this.viewPort.Location = new System.Drawing.Point(x, y);
            this.viewPort.Size = new System.Drawing.Size(mw1, mh1);
            this.viewPort.WindowSize = new System.Drawing.Size(mw1, mh1);


            int mRet = Model.MatchingConfig.loadModelIdAndImage(this._AppValue._ModelIdSavePath, out this.ho_ModelImage, out this.hv_ModelId);

            if (mRet == 0)
            {
                    this._windowControl.displayImage(this.ho_ModelImage);

                    if (this._regions.Count > 0)
                    {
                        this._windowControl.displayROI(this._regions);
                    }

                    this._flagNewModelImage = false;
            }


           //CheckROI.Model.CheckRoiConfig.loadXML(this._AppValue._CheckRoiConfigPath, out this._checkRoiConfig);
           // foreach (var m_checkRoiConfig in this._checkRoiConfig)
           // {
           //     this._windowControl.genRegions(m_checkRoiConfig.ROIParameter, ref this._regionsCheckRoi);
           // }

            tab_Message_SelectedIndexChanged(this, null);
        }

        #region 创建模板按钮

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

                this._flagNewModelImage = true;
            }
        }

        //窗口ROI选择
        private void viewPort_HMouseUp(object sender, HMouseEventArgs e)
        {
            int index;
            List<double> data;

            ViewWindow.Model.ROI roi = this._windowControl.smallestActiveROI(out data, out index);

            if (index > -1)
            {
                this._windowControl.fixROI(index, roi, ref this._regions);
                Model.MatchingConfig.fixMatchingConfig(Model.FixType.ROIParameter, index, roi, ref this._matchingConfig);
            }
        }

        private void btnCreateModel_Click(object sender, EventArgs e)
        {
            creatNewShapeModel();
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            Model.MatchingConfig.saveXML(this._matchingConfig, this._AppValue._MatchingConfigPath);
            Model.MatchingConfig.saveModelIdAndImage(ho_ModelImage, this._regions, this._matchingConfig, this._AppValue._ModelIdSavePath,this._flagNewModelImage);
            this._flagNewModelImage = false;
        }

        private void btn_CancleConfig_Click(object sender, EventArgs e)
        {
            Model.MatchingConfig.loadXML(this._AppValue._MatchingConfigPath, out this._matchingConfig);
            //binDataGridViewMatchingConfig(this.dgv_MatchingConfig, this._matchingConfig);   

            this._regions = new List<ViewWindow.Model.ROI>();
            foreach (var m_matchingConfig in this._matchingConfig)
            {
                this._windowControl.genRegions(m_matchingConfig.ROIParameter, ref this._regions);
            }
            this._windowControl.displayImage(this.ho_ModelImage);

            if (this._regions.Count > 0)
            {
                this._windowControl.displayROI(this._regions);
            }
        }

        #endregion


        #region 检测图片


        private void btnLoadCheckImage_Click(object sender, EventArgs e)
        {
            string[] files;

            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "所有图像文件 | *.bmp; *.png; *.jpg; *.tif";
            opnDlg.Multiselect = true;
            //opnDlg.InitialDirectory = startImagePath;

            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                files = opnDlg.FileNames;
                //startImagePath = opnDlg.FileName.Substring(0, opnDlg.FileName.LastIndexOf('\\'));

                PublicMethod.readImages(files, ref this._listImages_Training);

                foreach (string str in files)
                {
                    this.ListBoxTrainImage.Items.Add(str);
                }

                this.ListBoxTrainImage.SelectedIndex = 0;
            }
        }

        private void btnDelectCheckImage_Click(object sender, EventArgs e)
        {
            int count;
            if ((count = this.ListBoxTrainImage.SelectedIndex) < 0)
            {
                return;
            }

            string fileName = (string)this.ListBoxTrainImage.SelectedItem;

            if ((--count) < 0)
            {
                count += 2;
            }

            if ((count < this.ListBoxTrainImage.Items.Count))
            {
                this.ListBoxTrainImage.SelectedIndex = count;
            }

            this.ListBoxTrainImage.Items.Remove(fileName);

            PublicMethod.removeImages(fileName, ref this._listImages_Training);
        }

        private void btnDelectAllCheckImage_Click(object sender, EventArgs e)
        {
            if (this.ListBoxTrainImage.Items.Count > 0)
            {
                this.ListBoxTrainImage.Items.Clear();
                PublicMethod.removeImagesAll(ref this._listImages_Training);
            }
        }

        private void btnCheckImage_Click(object sender, EventArgs e)
        {
            bool rct = false;



            if (this.ListBoxTrainImage.Items.Count > 0)
            {
                this._matchingResual = new List<Model.MatchingResual>();
                this._resualId = 0;
                this._canChangeTab = false;
                this.gb_ImageOperation.Enabled = false;
                this.gb_Setting.Enabled = false;

                if (!this.cBcontinual.Checked)
                {
                    HObject ho_image;
                    List<Model.MatchingResual> resual;

                    string file = (string)this.ListBoxTrainImage.SelectedItem;
                    HOperatorSet.GenEmptyObj(out ho_image);
                    ho_image = (HObject)this._listImages_Training[file];

                    rct = this._matching.findShapeModel(ho_image, this.hv_ModelId, this._matchingConfig[0].FindModelParameter[0], out resual);

                    this._windowControl.displayImage(ho_image);
                }
                else
                {
                    this.ListBoxTrainImage.SelectedIndex = 0;

                    List<Model.MatchingResual> resual;
                    HObject ho_image;
                    string file = (string)this.ListBoxTrainImage.SelectedItem;
                    HOperatorSet.GenEmptyObj(out ho_image);
                    ho_image = (HObject)this._listImages_Training[file];

                    rct = this._matching.findShapeModel(ho_image, this.hv_ModelId, this._matchingConfig[0].FindModelParameter[0], out resual);

                    this._windowControl.displayImage(ho_image);
                }
            }
            else
            {
                MessageBox.Show("未加载待检测图片！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListBoxTrainImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._windowControl.resetWindowImage();

            if (this.ListBoxTrainImage.SelectedIndex < 0)
            {
                return;
            }

            HObject ho_image;
            string file = (string)this.ListBoxTrainImage.SelectedItem;
            HOperatorSet.GenEmptyObj(out ho_image);
            ho_image = (HObject)this._listImages_Training[file];

            this._windowControl.displayImage(ho_image);
        }

        //int _index = 0;
        int _resualId = 0;
        bool _canChangeTab = true;

        private void _matching_MatchingResualCallback(object sender, MatchingResualArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                if (e.Resual != null)
                {
                    this.viewPort.HalconWindow.SetColor("green");
                    this.viewPort.HalconWindow.WriteString("OK");

                    for (int i = 0; i < e.Resual.Count; i++)
                    {
                        this._resualId++;

                        this.viewPort.HalconWindow.DispCross(e.Resual[i].Row, e.Resual[i].Column, 16.0, e.Resual[i].Angle);
                        //this._matchingResual.Add(e.Resual[i]);
                        this._matchingResual.Add(new Model.MatchingResual(this._resualId, e.Resual[i].Row, e.Resual[i].Column, e.Resual[i].Angle, e.Resual[i].Score));

                        #region roi仿射变换

                        List<ViewWindow.Model.RoiData> m_CheckRoiParameterTrans;
                        HObject m_CheckRoiTrans, m_Arrow;

                        Model.MatchingConfig.tansRoiData(this._regions, this._regionsCheckRoi,
                                new Model.MatchingResual(this._resualId, e.Resual[i].Row, e.Resual[i].Column, e.Resual[i].Angle, e.Resual[i].Score),
                                out m_CheckRoiParameterTrans, out m_CheckRoiTrans,out m_Arrow);

                        if (m_CheckRoiParameterTrans != null)
                        {
                            //if (m_CheckRoiParameterTrans.Count > 0)
                            //{
                            //    this._windowControl.displayROI(m_CheckRoiParameterTrans);
                            //}
                            HOperatorSet.DispObj(m_CheckRoiTrans, this.viewPort.HalconWindow);
                            HOperatorSet.DispObj(m_Arrow, this.viewPort.HalconWindow);
                        }


                        //HObject m_Regions;
                        //HOperatorSet.GenEmptyObj(out m_Regions);
                        //Model.MatchingConfig.tansRegions(this._regions,
                        //    new Model.MatchingResual(this._resualId, e.Resual[i].Row, e.Resual[i].Column, e.Resual[i].Angle, e.Resual[i].Score),
                        //    out m_Regions);
                        //HOperatorSet.DispObj(m_Regions, this.viewPort.HalconWindow);



                        //List<ViewWindow.Model.RoiData> m_RoiParameterTrans;
                        //Model.MatchingConfig.tansRoiData(this._regions,
                        //        new Model.MatchingResual(this._resualId, e.Resual[i].Row, e.Resual[i].Column, e.Resual[i].Angle, e.Resual[i].Score),
                        //        out m_RoiParameterTrans);

                        //if (m_RoiParameterTrans != null)
                        //{
                        //    if (m_RoiParameterTrans.Count > 0)
                        //    {
                        //        this._windowControl.displayROI(m_RoiParameterTrans);

                        //    }
                        //}

                        #endregion
                    }
                }
                else
                {
                    this.viewPort.HalconWindow.SetColor("red");
                    this.viewPort.HalconWindow.WriteString("NG");
                }

                if ((this.cBcontinual.Checked) && (ListBoxTrainImage.SelectedIndex < this._listImages_Training.Count - 1))
                {
                    System.Threading.Thread.Sleep(200);
                    List<Model.MatchingResual> resual;
                    HObject ho_image;
                    HOperatorSet.GenEmptyObj(out ho_image);
                    ho_image.Dispose();

                    ListBoxTrainImage.SelectedIndex++;
                    string file = (string)ListBoxTrainImage.SelectedItem;

                    ho_image = (HObject)this._listImages_Training[file];

                    bool rct = this._matching.findShapeModel(ho_image, this.hv_ModelId, this._matchingConfig[0].FindModelParameter[0], out resual);

                    this._windowControl.displayImage(ho_image);
                }
                else
                {
                    this._canChangeTab = true;
                    this.gb_ImageOperation.Enabled = true;
                    this.gb_Setting.Enabled = true;
                }

            }));
        }

        #endregion

        private void tab_Message_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tab_Message.SelectedIndex == 0)
            {
                this._windowControl.displayImage(ho_ModelImage);
                this._windowControl.displayROI(this._regions);
            }
            else if (this.tab_Message.SelectedIndex == 1)
            {
                this._windowControl.displayImage(ho_ModelImage);
                ListBoxTrainImage_SelectedIndexChanged(sender, e);
            }
            else if (this.tab_Message.SelectedIndex == 2)
            {
                binDataGridView_MatchResual(this.dgv_Resual, this._matchingResual);
            }
        }

        private void tab_Message_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !this._canChangeTab;
        }

        private void frmSetMatch_Activated(object sender, EventArgs e)
        {
            CheckROI.Model.CheckRoiConfig.loadXML(this._AppValue._CheckRoiConfigPath, out this._checkRoiConfig);
            this._regionsCheckRoi = new List<ViewWindow.Model.ROI>();
            foreach (var m_checkRoiConfig in this._checkRoiConfig)
            {
                this._windowControl.genRegions(m_checkRoiConfig.ROIParameter, ref this._regionsCheckRoi);
            }

        }
    }
}
