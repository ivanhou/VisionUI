using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VisionTools.Match
{
    public class MatchingResualArgs:EventArgs
    {
        private List<Model.MatchingResual> _resual;
        public List<Model.MatchingResual> Resual
        {
            get { return this._resual; }
            set { this._resual = value; }
        }

        public MatchingResualArgs()
        {

        }

        public MatchingResualArgs(List<Model.MatchingResual> resual)
        {
            this._resual = resual;
        }
    }
}
