using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using HalconDotNet;
using System.IO;

namespace VisionTools.Match.Model
{
    public class MatchingConfig
    {
        private int _id;
        private string _name;
        private bool _enabled;
        private List<ViewWindow.Model.RoiData> _ROIParameter;
        private List<Model.CreatModelParameter> _CreatModelParameter;
        private List<Model.FindModelParameter> _FindModelParameter;

        //private static ViewWindow.ViewWindow _windowControl = new ViewWindow.ViewWindow(null);

        [XmlElement(ElementName = "ID")]
        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [XmlElement(ElementName = "Name")]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        [XmlElement(ElementName = "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        [XmlElement(ElementName = "ROIParameter")]
        public List<ViewWindow.Model.RoiData> ROIParameter
        {
            get { return this._ROIParameter; }
            set { this._ROIParameter = value; }
        }

        [XmlElement(ElementName = "CreatModelParameter")]
        public List<Model.CreatModelParameter> CreatModelParameter
        {
            get { return this._CreatModelParameter; }
            set { this._CreatModelParameter = value; }
        }

        [XmlElement(ElementName = "FindModelParameter")]
        public List<Model.FindModelParameter> FindModelParameter
        {
            get { return this._FindModelParameter; }
            set { this._FindModelParameter = value; }
        }

        public MatchingConfig()
        {
        }

        public MatchingConfig(int id, string name, bool enabled, List<ViewWindow.Model.RoiData> roiParameter, List<Model.CreatModelParameter> creatModel, List<Model.FindModelParameter> findModel)
        {
            this._id = id;
            this._name = name;
            this._enabled = enabled;
            this._ROIParameter = roiParameter;
            this._CreatModelParameter = creatModel;
            this._FindModelParameter = findModel;
        }

        public static void saveXML(List<MatchingConfig> matchingConfig, string fileNmae)
        {
            SerializeHelper.Save(matchingConfig, fileNmae);
        }

        public static void loadXML(string fileName, out List<MatchingConfig> matchingConfig)
        {
            matchingConfig = new List<MatchingConfig>();
            if (System.IO.File.Exists(fileName))
            {
                matchingConfig = (List<MatchingConfig>)SerializeHelper.Load(matchingConfig.GetType(), fileName);
            }
        }
        
        public static void delectXML(ref List<MatchingConfig> config, int index, string fileName)
        {
            if ((index >= 0) && (config.Count >= 1))
            {
                config.RemoveAt(index);

                //for (int i = 0; i < config.Count; i++)
                //{
                //    config[i].ID = i;
                //}

                SerializeHelper.Save(config, fileName);
            }
        }

        public static void delectXML(ref List<MatchingConfig> config, int index, string fileName,string modelIdFilePath)
        {
            if ((index >= 0) && (config.Count >= 1))
            {
                string m_fiename = String.Format("{0} {1} {2} {3} {4}", modelIdFilePath, config[index].Name, "_", config[index].ID, ".reg");
                delectModelId(m_fiename);

                config.RemoveAt(index );

                //for (int i = 0; i < config.Count; i++)
                //{
                //    config[i].ID = i;
                //}

                SerializeHelper.Save(config, fileName);
            }
        }

        public static bool addShapeModel(HObject image, ViewWindow.Model.ROI region,ref List<Model.MatchingConfig> matchingConfig, out HTuple modelID)
        {
            bool rct = false;
            if ((region!=null)&&(image!=null))
            {
                List<Model.CreatModelParameter> m_CreatModelParameter = new List<Model.CreatModelParameter>();
                Model.CreatModelParameter m_CreatModel = new Model.CreatModelParameter(1, 0.0, 360.0, 3);
                m_CreatModelParameter.Add(m_CreatModel);
                List<Model.FindModelParameter> m_FindModelParameter = new List<Model.FindModelParameter>();
                Model.FindModelParameter m_FindModel = new Model.FindModelParameter(1, 0.0, 360.0, 0.75, 1, 0.75, 3);
                m_FindModelParameter.Add(m_FindModel);

                List<ViewWindow.Model.RoiData> m_RoiData = new List<ViewWindow.Model.RoiData>();

                m_RoiData.Add(new ViewWindow.Model.RoiData(0, region));
                
                int id = 0;

                if (matchingConfig==null)
                {
                    matchingConfig = new List<MatchingConfig>();
                }
                else if (matchingConfig.Count > 0)
                {
                    id = matchingConfig[matchingConfig.Count - 1].ID + 1;
                }

                Model.MatchingConfig m_matchingConfig = new Model.MatchingConfig(id, m_RoiData[0].Name, true, m_RoiData, m_CreatModelParameter, m_FindModelParameter);
                matchingConfig.Add(m_matchingConfig);

                rct = creatShapeModel(image, m_RoiData[0], m_CreatModelParameter, out modelID);
            }
            else
            {
                rct = false;
                modelID = null;
            }

            return rct;
        }

        public static bool fixMatchingConfig(FixType type,int index, object config, ref List<Model.MatchingConfig> matchingConfig)
        {
            bool rct = false;
            try
            {
                switch (type)
                {
                    case FixType.ROIParameter:
                        matchingConfig[index].ROIParameter[0] = new ViewWindow.Model.RoiData(0, (ViewWindow.Model.ROI)config);
                        rct = true;
                        break;
                    case FixType.CreatModelParameter:
                        matchingConfig[index].CreatModelParameter[0] = (CreatModelParameter)config;
                        rct = true;
                        break;
                    case FixType.FindModelParameter:
                        matchingConfig[index].FindModelParameter[0] = (FindModelParameter)config;
                        rct = true;
                        break;
                    default:
                        rct = false;
                        break;
                }
            }
            catch (Exception)
            {
                rct = false;
            }

            return rct;
        }

        private static bool creatShapeModel(HObject image, ViewWindow.Model.RoiData region, List<CreatModelParameter> parameter, out HTuple modleID)
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

        private static void creatModelRoi(List<ViewWindow.Model.ROI> regions,out List<ViewWindow.Model.RoiData> m_RoiData,out HObject ho_RoiUnion)
        {
            HObject ho_ROIs, ho_modelROI;
            

            HOperatorSet.GenEmptyRegion(out ho_modelROI);
            HOperatorSet.GenEmptyRegion(out ho_ROIs);
            HOperatorSet.GenEmptyRegion(out ho_RoiUnion);
            ho_modelROI.Dispose();

            //ViewWindow.Model.RoiData region;
            m_RoiData = new List<ViewWindow.Model.RoiData>();

            for (int i = 0; i < regions.Count; i++)
            {
                m_RoiData.Add(new ViewWindow.Model.RoiData(i, regions[i]));

            }

            foreach (var region in m_RoiData)
            {
                switch (region.Name)
                {
                    case "Rectangle1":
                        ho_modelROI.Dispose();
                        HOperatorSet.GenRectangle1(out ho_modelROI, region.Rectangle1.Row1, region.Rectangle1.Column1,
                            region.Rectangle1.Row2, region.Rectangle1.Column2);
                        break;
                    case "Rectangle2":
                        ho_modelROI.Dispose();
                        HOperatorSet.GenRectangle2(out ho_modelROI, region.Rectangle2.Row, region.Rectangle2.Column,
                            region.Rectangle2.Phi, region.Rectangle2.Lenth1, region.Rectangle2.Lenth2);
                        break;
                    case "Circle":
                        ho_modelROI.Dispose();
                        HOperatorSet.GenCircle(out ho_modelROI, region.Circle.Row, region.Circle.Column, region.Circle.Radius);
                        break;
                }

                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ROIs, ho_modelROI, out ExpTmpOutVar_0);
                    ho_ROIs.Dispose();
                    ho_ROIs = ExpTmpOutVar_0;
                }
            }

            ho_RoiUnion.Dispose();
            HOperatorSet.Union1(ho_ROIs, out ho_RoiUnion);

            ho_modelROI.Dispose();
        }

        public static bool creatShapeModel(HObject image, List<ViewWindow.Model.ROI> regions,ref List<Model.MatchingConfig> matchingConfig, out HTuple modleID)
        {
            bool m_flag = false;
            HObject ho_TemplateImage;
            HObject ho_RoiUnion;

            HOperatorSet.GenEmptyRegion(out ho_TemplateImage);
            HOperatorSet.GenEmptyRegion(out ho_RoiUnion);            
            ho_TemplateImage.Dispose();

            modleID = new HTuple();
            List<ViewWindow.Model.RoiData> m_RoiData;

            try
            {
                creatModelRoi(regions, out m_RoiData, out ho_RoiUnion);
                
                int id = 0;

                if (matchingConfig == null)
                {
                    matchingConfig = new List<MatchingConfig>();
                }

                if (matchingConfig.Count == 0)
                {
                    List<Model.CreatModelParameter> m_CreatModelParameter = new List<Model.CreatModelParameter>();
                    Model.CreatModelParameter m_CreatModel = new Model.CreatModelParameter(1, 0.0, 360.0, 3);
                    m_CreatModelParameter.Add(m_CreatModel);
                    List<Model.FindModelParameter> m_FindModelParameter = new List<Model.FindModelParameter>();
                    Model.FindModelParameter m_FindModel = new Model.FindModelParameter(1, 0.0, 360.0, 0.75, 1, 0.75, 3);
                    m_FindModelParameter.Add(m_FindModel);

                    Model.MatchingConfig m_matchingConfig = new Model.MatchingConfig(id, m_RoiData[0].Name, true, m_RoiData, m_CreatModelParameter, m_FindModelParameter);
                    matchingConfig.Add(m_matchingConfig);
                }
                
                HOperatorSet.ReduceDomain(image, ho_RoiUnion, out ho_TemplateImage);

                //HObject ho_imagepart;
                //HOperatorSet.GenEmptyObj(out ho_imagepart);
                //ho_imagepart.Dispose();
                //HOperatorSet.CropDomain(ho_TemplateImage, out ho_imagepart);
                //HOperatorSet.WriteRegion(ho_RoiUnion, "d:\\123.reg");
                //HOperatorSet.WriteImage(ho_imagepart, "bmp", 0, "d:\\123.bmp");
              

                HOperatorSet.CreateShapeModel(ho_TemplateImage, matchingConfig[0].CreatModelParameter[0].NumLevels,
                    matchingConfig[0].CreatModelParameter[0].AngleStart, matchingConfig[0].CreatModelParameter[0].AngleExtent,
                    "auto", "auto", "use_polarity", "auto", "auto", out modleID);
                m_flag = true;
                ho_RoiUnion.Dispose();
            }
            catch (Exception)
            {
                m_flag = false;
                modleID = null;
                
                ho_TemplateImage.Dispose();
                ho_RoiUnion.Dispose();
            }

            return m_flag;
        }

        public static bool creatShapeModel(HObject image, ViewWindow.Model.ROI roi, List<CreatModelParameter> parameter, out HTuple modleID)
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

        public static bool findShapeModel(HImage image, HTuple shapeModelID, FindModelParameter parameter, out List<MatchingResual> resual)
        {
            bool m_flag = false;
            resual = new List<MatchingResual>();

            if (shapeModelID == null)
            {
                resual = null;
                return m_flag;
            }

            try
            {
                HTuple m_row = new HTuple(), m_column = new HTuple(), m_angle = new HTuple(), m_score = new HTuple();
                
                HOperatorSet.FindShapeModel(image,shapeModelID,
                    parameter.AngleStart, parameter.AngleExtent, parameter.MinScore, parameter.NumMatches,
                    0.4, "least_squares", parameter.NumLevels, parameter.Greediness,
                    out m_row, out m_column, out m_angle, out m_score);

                if (m_score.TupleLength() > 0)
                {
                    for (int i = 0; i < m_score.TupleLength(); i++)
                    {
                        MatchingResual m_resual = new MatchingResual();
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
            }
            catch (Exception e)
            {
                resual = null;
                m_flag = false;
            }

            return m_flag;
        }

        public static bool saveModelId(HObject image, Model.MatchingConfig matchingConfig, string savePath)
        {
            HTuple modelID = null;
            bool rct = creatShapeModel(image, matchingConfig.ROIParameter[0], matchingConfig.CreatModelParameter, out modelID);
            if (modelID != null)
            {
                string m_fiename = String.Format("{0}{1}{2}{3}{4}", savePath, matchingConfig.Name, "_", matchingConfig.ID, ".shm");
                HOperatorSet.WriteShapeModel(modelID, m_fiename);
            }
            return rct;
        }
             

        public static bool saveModelIdAndImage(HObject image, Model.MatchingConfig matchingConfig, string savePath,bool saveImg)
        {
            HTuple modelID = null;
            bool rct = false;
            creatShapeModel(image, matchingConfig.ROIParameter[0], matchingConfig.CreatModelParameter, out modelID);
            if (modelID != null)
            {
                string m_fiename = String.Format("{0}{1}{2}", savePath, "ModelId", ".shm");
                HOperatorSet.WriteShapeModel(modelID, m_fiename);

                if (saveImg)
                {
                    string m_imagename = String.Format("{0}{1}{2}", savePath, "ModelImage",  ".bmp");
                    HOperatorSet.WriteImage(image, "bmp", 0, m_imagename);
                }

                rct = true;
            }
            return rct;
        }

        public static bool saveModelIdAndImage(HObject image, List<ViewWindow.Model.ROI> regions, List<Model.MatchingConfig> matchingConfig, string savePath, bool saveImg)
        {
            HTuple modelID = null;            

            bool rct = creatShapeModel(image, regions, ref matchingConfig, out modelID);
            if (modelID != null)
            {
                string m_fiename = String.Format("{0}{1}{2}", savePath, "ModelId", ".shm");
                HOperatorSet.WriteShapeModel(modelID, m_fiename);

                if (saveImg)
                {
                    string m_imagename = String.Format("{0}{1}{2}", savePath, "ModelImage", ".bmp");
                    HOperatorSet.WriteImage(image, "bmp", 0, m_imagename);
                }

                rct = true;
            }
            return rct;
        }

        public static int loadModelIdAndImage(string path, out HObject modelImage, out HTuple modelId)
        {
            int mRET = 0;

            string m_fiename = String.Format("{0}{1}{2}", path, "ModelId", ".shm");

            if (System.IO.File.Exists(m_fiename))
            {
                HOperatorSet.ReadShapeModel(m_fiename, out modelId);

            }
            else
            {
                mRET = 1;
                modelId = null;
            }

            string m_imagename = String.Format("{0}{1}{2}", path, "ModelImage", ".bmp");
            if (System.IO.File.Exists(m_imagename))
            {
                HOperatorSet.GenEmptyObj(out modelImage);
                modelImage.Dispose();
                HOperatorSet.ReadImage(out modelImage, m_imagename);

            }
            else
            {
                mRET = 2;
                modelImage = null;
            }

            if ((modelId == null) && (modelImage == null))
            {
                mRET = 3;
            }

            return mRET;
        }

        private static void delectModelId(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public static void tansRoiData(List<ViewWindow.Model.ROI> modelRegion, List<ViewWindow.Model.ROI> regions, Model.MatchingResual matchResual, out List<ViewWindow.Model.RoiData> roiParameterTrans,out HObject roiTrans, out HObject arrow)
        {
            roiParameterTrans = new List<ViewWindow.Model.RoiData>();

            List<ViewWindow.Model.RoiData> m_RoiData;

            HObject ho_ModelRoiUnion;

            HTuple hv_ModelArea = new HTuple(), hv_ModelRow = new HTuple(), hv_ModelCol = new HTuple();
            HTuple hv_homMat2D = null;
            
            HOperatorSet.GenEmptyObj(out ho_ModelRoiUnion);
            HOperatorSet.GenEmptyObj(out roiTrans);
            HOperatorSet.GenEmptyObj(out arrow);
            
            try
            {
                m_RoiData = new List<ViewWindow.Model.RoiData>();
                creatModelRoi(modelRegion, out m_RoiData, out ho_ModelRoiUnion);
               
                HOperatorSet.AreaCenter(ho_ModelRoiUnion, out hv_ModelArea, out hv_ModelRow, out hv_ModelCol);
                
                ho_ModelRoiUnion.Dispose();
                                
                HOperatorSet.VectorAngleToRigid(hv_ModelRow, hv_ModelCol, 0,
                    new HTuple(matchResual.Row), new HTuple(matchResual.Column), new HTuple(matchResual.Angle), out hv_homMat2D);

                m_RoiData = new List<ViewWindow.Model.RoiData>();
                creatModelRoi(regions, out m_RoiData, out ho_ModelRoiUnion);
                ho_ModelRoiUnion.Dispose();

                foreach (var region in m_RoiData)
                {
                    HTuple hv_Rows = null, hv_Cols = null;
                    //HTuple hv_RowsMove = null, hv_ColsMove = null;
                    HTuple hv_RowTrans = null, hv_ColTrans = null;

                    HObject ho_Rectangle, m_modelRoiTrans;
                    HTuple hv_row = null, hv_col = null, hv_phi = null, hv_length1 = null, hv_length2 = null;

                    switch (region.Name)
                    {
                        case "Rectangle1":                            
                            HOperatorSet.GenEmptyObj(out ho_Rectangle);
                            HOperatorSet.GenEmptyObj(out m_modelRoiTrans);

                            ho_Rectangle.Dispose();
                            HOperatorSet.GenRectangle1(out ho_Rectangle, region.Rectangle1.Row1, region.Rectangle1.Column1, region.Rectangle1.Row2, region.Rectangle1.Column2);
                           
                            m_modelRoiTrans.Dispose();
                            HOperatorSet.AffineTransRegion(ho_Rectangle, out m_modelRoiTrans, hv_homMat2D, "nearest_neighbor");
                            
                            HOperatorSet.SmallestRectangle2(m_modelRoiTrans, out hv_row , out hv_col, out hv_phi, out hv_length1, out hv_length2);

                            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                                new ViewWindow.Config.Rectangle2(hv_row.D, hv_col.D, hv_phi.D, hv_length1.D, hv_length2.D)));

                            m_modelRoiTrans.Dispose();
                            
                            break;
                        case "Rectangle2":

                            HOperatorSet.GenEmptyObj(out ho_Rectangle);
                            HOperatorSet.GenEmptyObj(out m_modelRoiTrans);

                            ho_Rectangle.Dispose();
                            HOperatorSet.GenRectangle2(out ho_Rectangle,
                                region.Rectangle2.Row, region.Rectangle2.Column, -region.Rectangle2.Phi, region.Rectangle2.Lenth1, region.Rectangle2.Lenth2);

                            m_modelRoiTrans.Dispose();
                            HOperatorSet.AffineTransRegion(ho_Rectangle, out m_modelRoiTrans, hv_homMat2D, "nearest_neighbor");
                            
                            HOperatorSet.SmallestRectangle2(m_modelRoiTrans, out hv_row, out hv_col, out hv_phi, out hv_length1, out hv_length2);

                            //Console.WriteLine("phi_1 = {0}", hv_phi.D);
                            //Console.WriteLine("phi_2 = {0} {1}", matchResual.Angle, region.Rectangle2.Phi);
                            //Console.WriteLine("phi_3 = {0}", matchResual.Angle - region.Rectangle2.Phi);

                            //roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                            //    new ViewWindow.Config.Rectangle2(hv_row.D, hv_col.D, matchResual.Angle - region.Rectangle2.Phi, hv_length1.D, hv_length2.D)));

                            double m_phi = 0.0;

                            //if (hv_phi.D < 0)
                            //{
                            //    m_phi = -hv_phi.D;
                            //    roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                            //       new ViewWindow.Config.Rectangle2(hv_row.D, hv_col.D, m_phi, hv_length1.D, hv_length2.D)));
                                
                            //}
                            //else
                            //{
                            //    HTuple hv_Rad = null;
                            //    HOperatorSet.TupleRad(180, out hv_Rad);
                            //    m_phi = -hv_phi.D + hv_Rad.D;

                            //    roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                            //       new ViewWindow.Config.Rectangle2(hv_row.D, hv_col.D, m_phi, hv_length1.D, hv_length2.D)));
                            //}

                            Console.WriteLine("phi_1 = {0} {1}", matchResual.Angle, region.Rectangle2.Phi);

                            //double m_phi1 = 0.0;
                            if (matchResual.Angle>=0)
                            {
                                m_phi = -(-matchResual.Angle - region.Rectangle2.Phi);

                                //Console.WriteLine("phi_2 = {0}", -matchResual.Angle - region.Rectangle2.Phi);
                            }
                            else
                            {
                                m_phi = -matchResual.Angle + region.Rectangle2.Phi;

                                //Console.WriteLine("phi_2 = {0}", -matchResual.Angle + region.Rectangle2.Phi);
                            }

                            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                               new ViewWindow.Config.Rectangle2(hv_row.D, hv_col.D, m_phi, hv_length1.D, hv_length2.D)));
                            //Console.WriteLine("phi_3 = {0}", m_phi);

                            m_modelRoiTrans.Dispose();
                            break;
                        case "Circle":
                            hv_Rows = new HTuple();
                            hv_Rows = hv_Rows.TupleConcat(region.Circle.Row);
                            hv_Cols = new HTuple();
                            hv_Cols = hv_Cols.TupleConcat(region.Circle.Column);
                            HOperatorSet.AffineTransPixel(hv_homMat2D, hv_Rows, hv_Cols, out hv_RowTrans, out hv_ColTrans);


                            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                                new ViewWindow.Config.Circle(hv_RowTrans[0].D, hv_ColTrans[0].D, region.Circle.Radius)));
                            break;
                        case "Line":
                            hv_Rows = new HTuple();
                            hv_Rows = hv_Rows.TupleConcat(region.Line.RowBegin);
                            hv_Rows = hv_Rows.TupleConcat(region.Line.RowEnd);

                            hv_Cols = new HTuple();
                            hv_Cols = hv_Cols.TupleConcat(region.Line.ColumnBegin);
                            hv_Cols = hv_Cols.TupleConcat(region.Line.ColumnEnd);

                            HOperatorSet.AffineTransPixel(hv_homMat2D, hv_Rows, hv_Cols, out hv_RowTrans, out hv_ColTrans);


                            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                                new ViewWindow.Config.Line(hv_RowTrans[0].D, hv_ColTrans[0].D, hv_RowTrans[1].D, hv_ColTrans[1].D)));
                            break;
                    }

                    //{
                    //    HObject ExpTmpOutVar_0;
                    //    HOperatorSet.ConcatObj(ho_ROIs, ho_modelROI, out ExpTmpOutVar_0);
                    //    ho_ROIs.Dispose();
                    //    ho_ROIs = ExpTmpOutVar_0;
                    //}
                }

                PublicMethod.genContourXld(roiParameterTrans, out roiTrans, out arrow);


                //regionTrans.Add()                

            }
            catch (Exception ex)
            {
                //regionTrans = null;
                ho_ModelRoiUnion.Dispose();
            }
        }
               
        public static void tansRegions(List<ViewWindow.Model.ROI> regions, Model.MatchingResual matchResual, out HObject modelRoiTrans)
        {
            //regionTrans = new List<ViewWindow.Model.ROI>();
            //roiParameterTrans = new List<ViewWindow.Model.RoiData>();

            List<ViewWindow.Model.RoiData> m_RoiData;

            HObject ho_ModelRoiUnion;
            //HObject ho_ROIs, ho_modelROI;

            HTuple hv_ModelArea = new HTuple(), hv_ModelRow = new HTuple(), hv_ModelCol = new HTuple();
            HTuple hv_homMat2D = null;

            //HOperatorSet.GenEmptyRegion(out ho_modelROI);
            HOperatorSet.GenEmptyRegion(out modelRoiTrans);
            HOperatorSet.GenEmptyObj(out ho_ModelRoiUnion);

            try
            {
                //

                //ho_modelROI.Dispose();

                m_RoiData = new List<ViewWindow.Model.RoiData>();
                creatModelRoi(regions, out m_RoiData, out ho_ModelRoiUnion);
                //ho_modelROI.Dispose();
                //ho_ModelRoiUnion.Dispose();
                //HOperatorSet.Union1(ho_ROIs, out ho_ModelRoiUnion);
                HOperatorSet.AreaCenter(ho_ModelRoiUnion, out hv_ModelArea, out hv_ModelRow, out hv_ModelCol);

                //HOperatorSet.WriteRegion(ho_ModelRoiUnion, "d:\\ho_ModelRoiUnion.reg");

                //Console.WriteLine("aaaaaaa {0}, {1}", hv_ModelRow.D, hv_ModelCol.D);


                //for (int i = 0; i < regions.Count; i++)
                //{
                //    m_RoiData.Add(new ViewWindow.Model.RoiData(i, regions[i]));
                //}

                //HOperatorSet.VectorAngleToRigid(hv_ModelRow, hv_ModelCol, 0, 
                //    new HTuple(matchResual.Row), new HTuple(matchResual.Column), new HTuple(matchResual.Angle), out hv_homMat2D);

                HOperatorSet.VectorAngleToRigid(hv_ModelRow, hv_ModelCol, 0,
                    new HTuple(matchResual.Row), new HTuple(matchResual.Column), new HTuple(matchResual.Angle), out hv_homMat2D);

                modelRoiTrans.Dispose();
                HOperatorSet.AffineTransRegion(ho_ModelRoiUnion, out modelRoiTrans, hv_homMat2D, "nearest_neighbor");

                ho_ModelRoiUnion.Dispose();
                //List<ViewWindow.Model.RoiData> m_ROIParameter = new List<ViewWindow.Model.RoiData>();

                //foreach (var region in m_RoiData)
                //{
                //    HTuple hv_Rows = null, hv_Cols = null;
                //    //HTuple hv_RowsMove = null, hv_ColsMove = null;
                //    HTuple hv_RowTrans = null, hv_ColTrans = null;

                //    switch (region.Name)
                //    {
                //        case "Rectangle1":

                //            hv_Rows = new HTuple();
                //            hv_Rows = hv_Rows.TupleConcat(region.Rectangle1.Row1);
                //            hv_Rows = hv_Rows.TupleConcat(region.Rectangle1.Row2);

                //            hv_Cols = new HTuple();
                //            hv_Cols = hv_Cols.TupleConcat(region.Rectangle1.Column1);
                //            hv_Cols = hv_Cols.TupleConcat(region.Rectangle1.Column2);

                //            HObject ho_Rectangle1;
                //            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
                //            ho_Rectangle1.Dispose();

                //            HOperatorSet.GenRectangle1(out ho_Rectangle1, region.Rectangle1.Row1, region.Rectangle1.Column1, region.Rectangle1.Row2, region.Rectangle1.Column2);

                //            //HOperatorSet.AffineTransPixel(hv_homMat2D, hv_Rows, hv_Cols, out hv_RowTrans, out hv_ColTrans);


                //            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                //                new ViewWindow.Config.Rectangle1(hv_RowTrans[0].D, hv_ColTrans[0].D, hv_RowTrans[1].D, hv_ColTrans[1].D)));

                //            break;
                //        case "Rectangle2":
                //            hv_Rows = new HTuple();
                //            hv_Rows = hv_Rows.TupleConcat(region.Rectangle2.Row);
                //            hv_Cols = new HTuple();
                //            hv_Cols = hv_Cols.TupleConcat(region.Rectangle2.Column);

                //            HOperatorSet.AffineTransPixel(hv_homMat2D, hv_Rows, hv_Cols, out hv_RowTrans, out hv_ColTrans);

                //            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                //                new ViewWindow.Config.Rectangle2(hv_RowTrans[0].D, hv_ColTrans[0].D, region.Rectangle2.Phi + matchResual.Angle,
                //                     region.Rectangle2.Lenth1, region.Rectangle2.Lenth2)));

                //            break;
                //        case "Circle":
                //            hv_Rows = new HTuple();
                //            hv_Rows = hv_Rows.TupleConcat(region.Circle.Row);
                //            hv_Cols = new HTuple();
                //            hv_Cols = hv_Cols.TupleConcat(region.Circle.Column);
                //            HOperatorSet.AffineTransPixel(hv_homMat2D, hv_Rows, hv_Cols, out hv_RowTrans, out hv_ColTrans);


                //            roiParameterTrans.Add(new ViewWindow.Model.RoiData(region.ID,
                //                new ViewWindow.Config.Circle(hv_RowTrans[0].D, hv_ColTrans[0].D, region.Circle.Radius)));
                //            break;
                //    }

                //}

                //regionTrans.Add()                

            }
            catch (Exception ex)
            {
                //regionTrans = null;
                ho_ModelRoiUnion.Dispose();
            }
        }

    }

    public enum FixType
    {
        ROIParameter = 0,
        CreatModelParameter = 1,
        FindModelParameter = 2,
    }
}
