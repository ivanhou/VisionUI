using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace ViewWindow
{
    public class ViewWindow : Model.IViewWindow
    {
        private Model.HWndCtrl _hWndControl;

        private Model.ROIController _roiController;

        public ViewWindow(HWindowControl window)
        {
            this._hWndControl = new Model.HWndCtrl(window);
            this._roiController = new Model.ROIController();
            this._hWndControl.setROIController(this._roiController);
            this._hWndControl.setViewState(Model.HWndCtrl.MODE_VIEW_NONE);
        }
      

        public void displayImage(HObject img)
        {
            this._hWndControl.addImageShow(img);
            this._roiController.resetWindowImage();
            //this._hWndControl.resetWindow();
            this._hWndControl.resetAll();
            //this._hWndControl.repaint();
        }
        /// <summary>
        /// 复位图像显示
        /// </summary>
        public void resetWindowImage()
        {
            this._hWndControl.resetWindow();
            this._roiController.resetWindowImage();
        }
        /// <summary>
        /// 缩放图像
        /// </summary>
        public void zoomWindowImage()
        {
            this._roiController.zoomWindowImage();
        }
        /// <summary>
        /// 移动图像
        /// </summary>
        public void moveWindowImage()
        {
            this._roiController.moveWindowImage();
        }
        /// <summary>
        /// 取消图像操作
        /// </summary>
        public void noneWindowImage()
        {
            this._roiController.noneWindowImage();
        }

        public void genRect1(double row1, double col1, double row2, double col2, ref List<Model.ROI> rois)
        {
            this._roiController.genRect1(row1, col1, row2, col2, ref rois);
        }

        public void genRect2(double row, double col, double phi, double length1, double length2, ref List<Model.ROI> rois)
        {
            this._roiController.genRect2(row, col, phi, length1, length2, ref rois);
        }

        public void genCircle(double row, double col, double radius, ref List<Model.ROI> rois)
        {
            this._roiController.genCircle(row, col, radius, ref rois);
        }

        public void genLine(double beginRow, double beginCol, double endRow, double endCol, ref List<Model.ROI> rois)
        {
            this._roiController.genLine(beginRow, beginCol, endRow, endCol, ref rois);
        }

        public void genRegions(List<Model.RoiData> roiData, ref List<Model.ROI> rois)
        {
            foreach (var m_roiData in roiData)
            {
                switch (m_roiData.Name)
                {
                    case "Rectangle1":
                        this._roiController.genRect1(m_roiData.Rectangle1.Row1, m_roiData.Rectangle1.Column1, m_roiData.Rectangle1.Row2, m_roiData.Rectangle1.Column2, ref rois);
                        break;
                    case "Rectangle2":                        
                       this._roiController.genRect2(m_roiData.Rectangle2.Row, m_roiData.Rectangle2.Column, m_roiData.Rectangle2.Phi, m_roiData.Rectangle2.Lenth1, m_roiData.Rectangle2.Lenth2, ref rois);
                        break;
                    case "Circle":
                        this._roiController.genCircle(m_roiData.Circle.Row, m_roiData.Circle.Column, m_roiData.Circle.Radius, ref rois);
                        break;
                    case "Line":
                        this._roiController.genLine(m_roiData.Line.RowBegin, m_roiData.Line.ColumnBegin, m_roiData.Line.RowEnd, m_roiData.Line.ColumnEnd, ref rois);
                        break;
                }
            }
        }

        public void genRegions(Model.RoiData roiData, ref List<Model.ROI> rois)
        {
            rois = new List<Model.ROI>();
            if(roiData!=null)
            {
                switch (roiData.Name)
                {
                    case "Rectangle1":
                        this._roiController.genRect1(roiData.Rectangle1.Row1, roiData.Rectangle1.Column1, roiData.Rectangle1.Row2, roiData.Rectangle1.Column2, ref rois);
                        break;
                    case "Rectangle2":
                        this._roiController.genRect2(roiData.Rectangle2.Row, roiData.Rectangle2.Column, roiData.Rectangle2.Phi, roiData.Rectangle2.Lenth1, roiData.Rectangle2.Lenth2, ref rois);
                        break;
                    case "Circle":
                        this._roiController.genCircle(roiData.Circle.Row, roiData.Circle.Column, roiData.Circle.Radius, ref rois);
                        break;
                }
            }
        }

        public void fixROI(int index, Model.ROI roi, ref List<Model.ROI> rois)
        {
            rois[index] = roi;
        }

        public List<double> smallestActiveROI(out string name, out int index)
        {
            List<double> resual = this._roiController.smallestActiveROI(out name,out index);
            return resual;
        }
        
        public Model.ROI smallestActiveROI(out List<double> data, out int index)
        {
            Model.ROI roi = this._roiController.smallestActiveROI(out data, out index);
            return roi;
        }

        public void selectROI(int index)
        {
            this._roiController.selectROI(index);
        }

        public void selectROI( List<Model.ROI> rois, int index)
        {
            //this._roiController.selectROI(index);
            if ((rois.Count > index)&&(index>=0))
            {
                this._hWndControl.resetAll();
                this._hWndControl.repaint();

                HTuple m_roiData = null;
                m_roiData = rois[index].getModelData();

                switch (rois[index].Type)
                {
                    case "ROIRectangle1":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case "ROIRectangle2":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                        }
                        break;
                    case "ROICircle":

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case "ROILine":

                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void displayROI(List<Model.ROI> rois)
        {
            //this._hWndControl.resetAll();
            //this._hWndControl.repaint();

            foreach (var roi in rois)
            {
                HTuple m_roiData = null;
                m_roiData = roi.getModelData();

                switch (roi.Type)
                {
                    case "ROIRectangle1":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case "ROIRectangle2":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                        }
                        break;
                    case "ROICircle":

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case "ROILine":

                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public void displayROI(List<Model.RoiData> roiDatas)
        {
            //this._hWndControl.resetAll();
            //this._hWndControl.repaint();

            List<Model.ROI> rois = new List<Model.ROI>();

            foreach (var roiData in roiDatas)
            {
                switch (roiData.Name)
                {
                    case "Rectangle1":
                        genRect1(roiData.Rectangle1.Row1, roiData.Rectangle1.Column1, roiData.Rectangle1.Row2, roiData.Rectangle1.Column2, ref rois);

                        break;
                    case "Rectangle2":
                        genRect2(roiData.Rectangle2.Row, roiData.Rectangle2.Column, roiData.Rectangle2.Phi,
                            roiData.Rectangle2.Lenth1, roiData.Rectangle2.Lenth2, ref rois);

                        break;
                    case "Circle":
                        genCircle(roiData.Circle.Row, roiData.Circle.Column, roiData.Circle.Radius, ref rois);

                        break;
                    case "Line":
                        genLine(roiData.Line.RowBegin, roiData.Line.ColumnBegin, roiData.Line.RowEnd, roiData.Line.ColumnEnd, ref rois);

                        break;
                }
            }


            foreach (var roi in rois)
            {
                HTuple m_roiData = null;
                m_roiData = roi.getModelData();

                switch (roi.Type)
                {
                    case "ROIRectangle1":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case "ROIRectangle2":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                        }
                        break;
                    case "ROICircle":

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case "ROILine":

                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public void removeActiveROI(ref List<Model.ROI> rois)
        {
            this._roiController.removeActiveROI(ref rois);
        }

        public void saveROI(List<Model.ROI> rois, string fileNmae)
        {
            List<Model.RoiData> m_RoiData = new List<Model.RoiData>();
            for (int i = 0; i < rois.Count; i++)
            {
                m_RoiData.Add(new Model.RoiData(i, rois[i]));
            }

            Config.SerializeHelper.Save(m_RoiData, fileNmae);
        }

        public void loadROI(string fileName, out List<Model.ROI> rois)
        {
            rois = new List<Model.ROI>();
            List<Model.RoiData> m_RoiData = new List<Model.RoiData>();
            m_RoiData = (List<Model.RoiData>)Config.SerializeHelper.Load(m_RoiData.GetType(), fileName);
            for (int i = 0; i < m_RoiData.Count; i++)
            {
                switch (m_RoiData[i].Name)
                {
                    case "Rectangle1":
                        this._roiController.genRect1(m_RoiData[i].Rectangle1.Row1, m_RoiData[i].Rectangle1.Column1,
                            m_RoiData[i].Rectangle1.Row2, m_RoiData[i].Rectangle1.Column2, ref rois);
                        break;
                    case "Rectangle2":
                        this._roiController.genRect2(m_RoiData[i].Rectangle2.Row, m_RoiData[i].Rectangle2.Column,
                            m_RoiData[i].Rectangle2.Phi, m_RoiData[i].Rectangle2.Lenth1, m_RoiData[i].Rectangle2.Lenth2, ref rois);
                        break;
                    case "Circle":
                        this._roiController.genCircle(m_RoiData[i].Circle.Row, m_RoiData[i].Circle.Column, m_RoiData[i].Circle.Radius, ref rois);
                        break;
                    case "Line":
                        this._roiController.genLine(m_RoiData[i].Line.RowBegin, m_RoiData[i].Line.ColumnBegin,
                            m_RoiData[i].Line.RowEnd, m_RoiData[i].Line.ColumnEnd, ref rois);
                        break;
                    default:
                        break;
                }
            }

            this._hWndControl.resetAll();
            this._hWndControl.repaint();
        }        
    }
}
