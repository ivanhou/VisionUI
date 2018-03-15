using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VisionTools.Match.Model
{
    public class FindModelParameter
    {
        private int _id;
        private double _angleStart;
        private double _angleExtent;
        private double _minScore;
        private int _numMatches;
        private double _greediness;
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

        [XmlElement(ElementName = "MinScore")]
        public double MinScore
        {
            get { return this._minScore; }
            set { this._minScore = value; }
        }

        [XmlElement(ElementName = "NumMatches")]
        public int NumMatches
        {
            get { return this._numMatches; }
            set { this._numMatches = value; }
        }

        [XmlElement(ElementName = "Greediness")]
        public double Greediness
        {
            get { return this._greediness; }
            set { this._greediness = value; }
        }

        [XmlElement(ElementName = "NumLevels")]
        public int NumLevels
        {
            get { return this._numLevels; }
            set { this._numLevels = value; }
        }

        public FindModelParameter()
        {

        }

        public FindModelParameter(int id, double angleStart, double angleExtent, double minScore, int numMatches, double greediness, int numLevels)
        {
            this._id = id;
            this._angleStart = angleStart;
            this._angleExtent = angleExtent;
            this._minScore = minScore;
            this._numMatches = numMatches;
            this._greediness = greediness;
            this._numLevels = numLevels;
        }
    }
}
