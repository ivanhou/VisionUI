using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VisionTools.Match.Model
{
    public class CreatModelParameter
    {
        private int _id;
        private double _angleStart;
        private double _angleExtent;
        private int _numLevels;

        [XmlElement(ElementName = "ID")]
        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [XmlElement(ElementName = "AngleStart")]
        public double AngleStart
        {
            get { return this._angleStart; }
            set { this._angleStart = value; }
        }

        [XmlElement(ElementName = "AngleExtent")]
        public double AngleExtent
        {
            get { return this._angleExtent; }
            set { this._angleExtent = value; }
        }

        [XmlElement(ElementName = "NumLevels")]
        public int NumLevels
        {
            get { return this._numLevels; }
            set { this._numLevels = value; }
        }

        public CreatModelParameter()
        {

        }

        public CreatModelParameter(int id, double angleStart, double angleExtent, int numLevels)
        {
            this._id = id;
            this._angleStart = angleStart;
            this._angleExtent = angleExtent;
            this._numLevels = numLevels;
        }
    }
}
