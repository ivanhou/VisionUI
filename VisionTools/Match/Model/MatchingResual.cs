using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace VisionTools.Match.Model
{
    /// <summary>
    /// 匹配结果
    /// </summary>
    public class MatchingResual
    {
        private int _id;
        private double _row;
        private double _column;
        private double _angle;
        private double _score;

        [XmlElement(ElementName = "ID")]
        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [XmlElement(ElementName = "Row")]
        public double Row
        {
            get { return this._row; }
            set { this._row = value; }
        }

        [XmlElement(ElementName = "Column")]
        public double Column
        {
            get { return this._column; }
            set { this._column = value; }
        }

        [XmlElement(ElementName = "Angle")]
        public double Angle
        {
            get { return this._angle; }
            set { this._angle = value; }
        }

        [XmlElement(ElementName = "Score")]
        public double Score
        {
            get { return this._score; }
            set { this._score = value; }
        }

        public MatchingResual()
        {

        }

        public MatchingResual(int id, double row, double column, double angle, double score)
        {
            this._id = id;
            this._row = row;
            this._column = column;
            this._angle = angle;
            this._score = score;
        }
    }
}
