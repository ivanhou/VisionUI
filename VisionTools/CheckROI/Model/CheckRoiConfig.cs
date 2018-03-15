using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VisionTools.CheckROI.Model
{
    public class CheckRoiConfig
    {
        private int _id;
        private string _name;
        private bool _enabled;
        private List<ViewWindow.Model.RoiData> _ROIParameter;

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

        public CheckRoiConfig()
        {
        }

        public CheckRoiConfig(int id, string name, bool enabled, List<ViewWindow.Model.RoiData> roiParameter)
        {
            this._id = id;
            this._name = name;
            this._enabled = enabled;
            this._ROIParameter = roiParameter;
        }

        public static void saveXML(List<CheckRoiConfig> checkRoiConfig, string fileNmae)
        {
            SerializeHelper.Save(checkRoiConfig, fileNmae);
        }

        public static void loadXML(string fileName, out List<CheckRoiConfig> matchingConfig)
        {
            matchingConfig = new List<CheckRoiConfig>();

            if (System.IO.File.Exists(fileName))
            {
                matchingConfig = (List<CheckRoiConfig>)SerializeHelper.Load(matchingConfig.GetType(), fileName);
            }
        }

        public static void delectXML(ref List<CheckRoiConfig> config, int index, string fileName)
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

        public static bool fixCheckRoiConfig(int index, ViewWindow.Model.ROI region, ref List<Model.CheckRoiConfig> checkRoiConfig)
        {
            bool rct = false;
            try
            {
                if (checkRoiConfig.Count>0)
                {

                //    List<ViewWindow.Model.RoiData> m_RoiData = new List<ViewWindow.Model.RoiData>();
                //    m_RoiData.Add(new ViewWindow.Model.RoiData(0, region));
                //    checkRoiConfig.Add
                //}
                //else
                //{
                    checkRoiConfig[index].ROIParameter[0] = new ViewWindow.Model.RoiData(0, region);
                }
                rct = true;
            }
            catch (Exception)
            {
                rct = false;
            }

            return rct;
        }

        public static bool addROI(ViewWindow.Model.ROI region, ref List<Model.CheckRoiConfig> checkRoiConfig)
        {
            bool mRET = false;

            try
            {
                List<ViewWindow.Model.RoiData> m_RoiData = new List<ViewWindow.Model.RoiData>();
                m_RoiData.Add(new ViewWindow.Model.RoiData(0, region));

                int id = 0;
                if (checkRoiConfig == null)
                {
                    checkRoiConfig = new List<CheckRoiConfig>();
                }
                else if(checkRoiConfig.Count>0)
                {
                    id = checkRoiConfig[checkRoiConfig.Count - 1].ID + 1;
                }

                checkRoiConfig.Add(new CheckRoiConfig(id, m_RoiData[0].Name, true, m_RoiData));

                mRET = true;
            }
            catch (Exception ex)
            {
                
            }


            return mRET;
        }

        public static int loadModelImage(string path, out HObject modelImage)
        {
            int mRET = 0;
            
            string m_imagename = String.Format("{0}{1}{2}", path, "ModelImage", ".bmp");
            if (System.IO.File.Exists(m_imagename))
            {
                HOperatorSet.GenEmptyObj(out modelImage);
                modelImage.Dispose();
                HOperatorSet.ReadImage(out modelImage, m_imagename);

            }
            else
            {
                mRET = 1;
                modelImage = null;
            }
            
            return mRET;
        }

    }
}
