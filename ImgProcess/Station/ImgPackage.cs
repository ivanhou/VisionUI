using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace ImgProcess.Station
{
    public class ImgPackage
    {
        private HObject _Img;
        public int ID { get; private set; }
        //public HObject Img { get { return this._Img; } }
        public HObject Img { get; private set; }

        public ImgPackage(int id, HObject img)
        {
            this.ID = id;
            //HOperatorSet.CopyImage(img, out this._Img);
            this.Img = img;
        }
    }
}
