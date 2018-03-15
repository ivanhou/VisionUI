using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamCommon
{
    public class CameraEventArgs : EventArgs
    {

        public int ID { get;private set; }

        public string CamType { get;private set; }

        public string CamName { get;private set; }

        public IntPtr Pixels { get;private set; }

        public int Width { get;private set; }

        public int Height { get;private set; }


        public CameraEventArgs(int id, string camType, string camName, IntPtr pixels, int width, int height)
        {
            this.ID = id;
            this.CamType = camType;
            this.CamName = camName;
            this.Pixels = pixels;
            this.Width = width;
            this.Height = height;
        }

    }
}
