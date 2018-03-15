using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Cam
{
    public class CameraEventArgs : EventArgs
    {
        //private int _id;
        //private IntPtr _pData;
        //private int _width;
        //private int _height;

        //public int ID { get { return this._id; } }
        //public IntPtr pData { get { return this._pData; } }
        //public int Width { get { return this._width; } }
        //public int Height { get { return this._height; } }
        //public int Channel { get { return this._height; } }

        private PackageHK _HK;

        private PackageBAS _BAS;

        public PackageHK HK { get { return this._HK; } }
        public PackageBAS BAS { get { return this._BAS; } }

        //public CameraEventArgs(int id, IntPtr pData, int width, int height, int channel = 1)
        //{
        //    this._id = id;
        //    this._pData = pData;
        //    this._width = width;
        //    this._height = height;
        //}

        public CameraEventArgs(Type t, object package)
        {
            switch (t.Name)
            {
                case "Basler":
                    this._BAS = package as PackageBAS;
                    break;
                case "CameraMultiple":
                    this._HK = package as PackageHK;
                    break;
                default:
                    break;
            }
        }

    }

    public class PackageHK
    {
        private int _id;
        private string _sn;
        private IntPtr _pData;
        private int _width;
        private int _height;
        private int _mFrames;
        private int _channel;

        public int ID { get { return this._id; } }
        public string SN { get { return this._sn; } }
        public IntPtr pData { get { return this._pData; } }
        public int Width { get { return this._width; } }
        public int Height { get { return this._height; } }
        public int Frames { get { return this._mFrames; } }
        public int Channel { get { return this._channel; } }


        public PackageHK(int id, string sn, IntPtr pData, int width, int height, int frames, int channel = 1)
        {
            this._id = id;
            this._sn = sn;
            this._pData = pData;
            this._width = width;
            this._height = height;
            this._mFrames = frames;
            this._channel = channel;
        }
    }

    public class PackageBAS
    {
        private Bitmap bitmap;
        private byte[] imgData;
        private int _width;
        private int _height;
        private int _channel;

        public Bitmap BitMap { get { return this.bitmap; } }
        public byte[] pData { get { return this.imgData; } }
        public int Width { get { return this._width; } }
        public int Height { get { return this._height; } }
        public int Channel { get { return this._channel; } }

        public PackageBAS(Bitmap bImg, byte[] imageData, int width, int height, int channel = 1)
        {
            this.bitmap = bImg;
            this.imgData = imageData;
            this._width = width;
            this._height = height;
            this._channel = channel;
        }
    }
}
