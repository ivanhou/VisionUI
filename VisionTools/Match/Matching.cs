using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.IO;

namespace VisionTools.Match
{
    public class Matching
    {

        public event EventHandler<MatchingResualArgs> MatchingResualCallback;

        public Matching()
        {
        }


        public bool creatShapeModel(HObject image, List<ViewWindow.Model.RoiData> region, List<Model.CreatModelParameter> parameter, out HTuple modleID)
        {
            bool m_flag = false;
            HObject ho_TemplateImage;
            HObject ho_modelROI;

            HOperatorSet.GenEmptyRegion(out ho_modelROI);
            HOperatorSet.GenEmptyRegion(out ho_TemplateImage);
            ho_modelROI.Dispose();
            ho_TemplateImage.Dispose();

            modleID = new HTuple();

            try
            {
                switch (region[0].Name)
                {
                    case "Rectangle1":
                        HOperatorSet.GenRectangle1(out ho_modelROI, region[0].Rectangle1.Row1, region[0].Rectangle1.Column1,
                            region[0].Rectangle1.Row2, region[0].Rectangle1.Column2);
                        break;
                    case "Rectangle2":
                        HOperatorSet.GenRectangle2(out ho_modelROI, region[0].Rectangle2.Row, region[0].Rectangle2.Column,
                            region[0].Rectangle2.Phi, region[0].Rectangle2.Lenth1, region[0].Rectangle2.Lenth2);
                        break;
                    case "Circle":
                        HOperatorSet.GenCircle(out ho_modelROI, region[0].Circle.Row, region[0].Circle.Column, region[0].Circle.Radius);
                        break;
                }

                HOperatorSet.ReduceDomain(image, ho_modelROI, out ho_TemplateImage);
                HOperatorSet.CreateShapeModel(ho_TemplateImage, parameter[0].NumLevels, parameter[0].AngleStart, parameter[0].AngleExtent, "auto",
                    "none", "use_polarity", "auto", "auto", out modleID);
                m_flag = true;
            }
            catch (Exception)
            {
                m_flag = false;
                modleID = null;
                ho_modelROI.Dispose();
                ho_TemplateImage.Dispose();
            }

            return m_flag;
        }

        private bool creatShapeModel(HObject image, ViewWindow.Model.ROI roi, List<Model.CreatModelParameter> parameter, out HTuple modleID)
        {
            bool m_flag = false;
            HObject ho_TemplateImage;
            HObject ho_modelROI;

            HOperatorSet.GenEmptyRegion(out ho_modelROI);
            HOperatorSet.GenEmptyRegion(out ho_TemplateImage);
            ho_modelROI.Dispose();
            ho_TemplateImage.Dispose();

            modleID = new HTuple();

            try
            {
                ViewWindow.Model.RoiData region = new ViewWindow.Model.RoiData(0, roi);

                switch (region.Name)
                {
                    case "Rectangle1":
                        HOperatorSet.GenRectangle1(out ho_modelROI, region.Rectangle1.Row1, region.Rectangle1.Column1,
                            region.Rectangle1.Row2, region.Rectangle1.Column2);
                        break;
                    case "Rectangle2":
                        HOperatorSet.GenRectangle2(out ho_modelROI, region.Rectangle2.Row, region.Rectangle2.Column,
                            region.Rectangle2.Phi, region.Rectangle2.Lenth1, region.Rectangle2.Lenth2);
                        break;
                    case "Circle":
                        HOperatorSet.GenCircle(out ho_modelROI, region.Circle.Row, region.Circle.Column, region.Circle.Radius);
                        break;
                }

                HOperatorSet.ReduceDomain(image, ho_modelROI, out ho_TemplateImage);
                HOperatorSet.CreateShapeModel(ho_TemplateImage, parameter[0].NumLevels, parameter[0].AngleStart, parameter[0].AngleExtent, "auto",
                    "none", "use_polarity", "auto", "auto", out modleID);
                m_flag = true;
            }
            catch (Exception)
            {
                m_flag = false;
                modleID = null;
                ho_modelROI.Dispose();
                ho_TemplateImage.Dispose();
            }

            return m_flag;
        }

        public bool findShapeModel(HObject image, HTuple shapeModelID, Model.FindModelParameter parameter, out List<Model.MatchingResual> resual)
        {
            bool m_flag = false;
            resual = new List<Model.MatchingResual>();

            if (shapeModelID == null)
            {
                resual = null;
                return m_flag;
            }

            try
            {
                HTuple m_row = new HTuple(), m_column = new HTuple(), m_angle = new HTuple(), m_score = new HTuple();

                HOperatorSet.FindShapeModel(image, shapeModelID,
                    parameter.AngleStart, parameter.AngleExtent, parameter.MinScore, parameter.NumMatches,
                    0.4, "least_squares", parameter.NumLevels, parameter.Greediness,
                    out m_row, out m_column, out m_angle, out m_score);

                if (m_score.TupleLength() > 0)
                {
                    for (int i = 0; i < m_score.TupleLength(); i++)
                    {
                        Model.MatchingResual m_resual = new Model.MatchingResual();
                        m_resual.ID = i + 1;
                        m_resual.Row = m_row.D;
                        m_resual.Column = m_column.D;
                        m_resual.Angle = m_angle.D;
                        m_resual.Score = m_score.D;
                        resual.Add(m_resual);
                    }

                    m_flag = true;
                }
                else
                {
                    resual = null;
                    m_flag = false;
                }

                //HOperatorSet.WriteImage(image, "tiff", 0, "D:/111.tiff");
            }
            catch (Exception e)
            {
                resual = null;
                m_flag = false;
            }

            OnMatcihngEvent(new MatchingResualArgs(resual));

            return m_flag;
        }
        
        protected virtual void OnMatcihngEvent(MatchingResualArgs e)
        {
            EventHandler<MatchingResualArgs> handler = this.MatchingResualCallback;

            if (handler != null)
            {
                handler.BeginInvoke(this, e, null, e);
            }
        }

        public bool loadModleId(Model.MatchingConfig matchingConfig, string savePath,out HTuple modleId)
        {
            bool rct = false;
            modleId = null;
            string m_fiename = String.Format("{0}{1}{2}{3}{4}", savePath, matchingConfig.Name, "_", matchingConfig.ID, ".reg");
            if (File.Exists(@m_fiename))
            {
                HOperatorSet.ReadShapeModel(m_fiename, out modleId);
                rct = true;
            }

            return rct;
        }

    }
}
