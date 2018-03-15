using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CamCommon;
using System.IO;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CamFile
{
    public class Camera:ICamera
    {        
        public override event EventHandler<CameraEventArgs> NewImageEvent;

        /// <summary>
        /// 用来存储图片
        /// </summary>
        private System.Collections.Hashtable _listImages = new System.Collections.Hashtable();
        /// <summary>
        /// 用来存储图片路径
        /// </summary>
        private string[] _fileKey;

        private Dictionary<string, System.Drawing.Imaging.ImageFormat> _imgFormat;

        private int _index = 0;
        private int _count;
        private bool _ThreadFlag = false;
        private bool _isStart = false;
        private System.Threading.AutoResetEvent _ent;
        private string _camName = "";

        private GCHandle hObject;
        System.Diagnostics.Stopwatch _Stopwatch = new System.Diagnostics.Stopwatch();

        int m_ReadImageCount;
        bool m_WatchTime = false;

        public override bool Open(string serialNum)
        {
            this._fileKey = (string[])traverse(serialNum).ToArray(typeof(string)); ;
            this._count = this._fileKey.Length;
            if (this._count == 0)
            {
                return false;
            }

            readImages(this._fileKey, ref this._listImages);

            return base.Open(serialNum);
        }

        public override void Close(string serialNum)
        {
            this._isStart = false;
            base.Close(serialNum);
        }

        public override bool StartGrab()
        {
            this._isStart = true;
            this._ent.Set();
            return base.StartGrab();
        }

        public override bool StopGrab()
        {
            this._isStart = false;
            this._ent.Reset();
            return base.StopGrab();
        }

        private ArrayList traverse(string findPath)
        {
            ArrayList m_FilePth = new ArrayList();
            this._imgFormat = new Dictionary<string, System.Drawing.Imaging.ImageFormat>();

            Hashtable fileExtensions = new Hashtable(10);
            fileExtensions.Add(".jpg", null);
            fileExtensions.Add(".bmp", null);
            fileExtensions.Add(".jpeg", null);
            fileExtensions.Add(".tif", null);
            fileExtensions.Add(".png", null);

            DirectoryInfo di = new DirectoryInfo(findPath);
            FileInfo[] fiels = di.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo fi in fiels)
            {
                if (fileExtensions.Contains(fi.Extension.ToLower()))
                {
                    string mFullPath = string.Format("{0}\\{1}", findPath, fi.Name);

                    m_FilePth.Add(mFullPath);

                    if (!this._imgFormat.ContainsKey(mFullPath))
                    {
                        switch (fi.Extension.ToLower())
                        {
                            case ".jpg":
                                this._imgFormat.Add(mFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case ".jpeg":
                                this._imgFormat.Add(mFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case ".bmp":
                                this._imgFormat.Add(mFullPath, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            case ".tif":
                                this._imgFormat.Add(mFullPath, System.Drawing.Imaging.ImageFormat.Tiff);
                                break;
                            case ".png":
                                this._imgFormat.Add(mFullPath, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return m_FilePth;
        }

       
        /// <summary>
        /// 读取本地图片
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="bImages"></param>
        private void readImages(string[] fileKey, ref System.Collections.Hashtable bImages)
        {
            if (bImages.ContainsKey(fileKey))
            {
                return;
            }

            try
            {
                foreach (string str in fileKey)
                {
                    System.Drawing.Bitmap curBitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(str);
                    bImages.Add(str, curBitmap);

                    //HObject ho_image;
                    //HOperatorSet.GenEmptyObj(out ho_image);
                    //ho_image.Dispose();
                    //HOperatorSet.ReadImage(out ho_image, str);
                    //bImages.Add(str, ho_image);
                }
            }
            catch (Exception e)
            {
                return;
            }

        }

        private void snapImage()
        {
            do
            {
                if ((this._count > 0) && this._isStart)
                {
                    if (this._ent.WaitOne(0))
                    {

                        if (!this.m_WatchTime)
                        {
                            this.m_WatchTime = !this.m_WatchTime;
                            this._Stopwatch.Restart();
                        }
                        else
                        {
                            this._Stopwatch.Stop();
                            Console.WriteLine("Spacing interval grab image {0}", this._Stopwatch.ElapsedMilliseconds);
                            this._Stopwatch.Restart();
                        }


                        string key = this._fileKey[this._index];
                        System.Drawing.Bitmap m_Bimage;
                        m_Bimage = (System.Drawing.Bitmap)this._listImages[key];

                        int mWidth = m_Bimage.Width;
                        int mHeight = m_Bimage.Height;

                        //IntPtr pPixels = new IntPtr();
                        //pPixels = IntPtr.Zero;
                        byte[] m_Pixels = Bitmap2Byte(m_Bimage, this._imgFormat[key]);
                       // Marshal.Copy(m_Pixels, 0, pPixels, m_Pixels.Length);
                        //Marshal.FreeHGlobal(pPixels);


                        //byte[] test = new byte[5];
                        //this.hObject = GCHandle.Alloc(m_Pixels, GCHandleType.Pinned);
                        GCHandle mhObject = GCHandle.Alloc(m_Pixels, GCHandleType.Pinned);
                        IntPtr pPixels = mhObject.AddrOfPinnedObject();

                        //if (hObject.IsAllocated)
                        //    hObject.Free();

                        //Bitmap bImage24;
                        //System.Drawing.Imaging.BitmapData bmData = null;
                        //System.Drawing.Rectangle rect;
                        //IntPtr pPixels;

                        //rect = new Rectangle(0, 0, m_Bimage.Width, m_Bimage.Height);
                        //bImage24 = new Bitmap(m_Bimage.Width, m_Bimage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        //System.Drawing.Graphics g = Graphics.FromImage(bImage24);
                        //g.DrawImage(m_Bimage, rect);
                        //g.Dispose();

                        //bmData = bImage24.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        //pPixels = bmData.Scan0;                        
                        //bImage24.UnlockBits(bmData);


                        OnNewImageEvent(new CameraEventArgs(this._index, "File", key, pPixels, mWidth, mHeight));

                        if (mhObject.IsAllocated)
                        {
                            mhObject.Free();
                        }
                        GC.Collect();
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            } while (this._ThreadFlag);
        }

        ///// <summary>
        ///// 将Bitmap图像转换成HObject图像
        ///// </summary>
        ///// <param name="bImage"></param>
        ///// <returns></returns>
        //private HObject bitmap2HImage_24(System.Drawing.Bitmap bImage)
        //{
        //    Bitmap bImage24;
        //    System.Drawing.Imaging.BitmapData bmData = null;
        //    System.Drawing.Rectangle rect;
        //    IntPtr pBitmap;
        //    IntPtr pPixels;
        //    HObject hImage;

        //    rect = new Rectangle(0, 0, bImage.Width, bImage.Height);
        //    bImage24 = new Bitmap(bImage.Width, bImage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        //    System.Drawing.Graphics g = Graphics.FromImage(bImage24);
        //    g.DrawImage(bImage, rect);
        //    g.Dispose();

        //    bmData = bImage24.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        //    pBitmap = bmData.Scan0;
        //    pPixels = pBitmap;

        //    HOperatorSet.GenEmptyObj(out hImage);
        //    hImage.Dispose();
        //    HOperatorSet.GenImageInterleaved(out hImage, pPixels, "bgr", bImage.Width, bImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);

        //    bImage24.UnlockBits(bmData);

        //    GC.Collect();

        //    return hImage;
        //}


        private byte[] Bitmap2Byte(Bitmap bitmap, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, format);// System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        protected virtual void OnNewImageEvent(CameraEventArgs e)
        {
            EventHandler<CameraEventArgs> handler = this.NewImageEvent;

            if (handler != null)
            {
                AsyncCallback nn = new AsyncCallback(eventCallback);
                handler.BeginInvoke(handler, e, nn, e);
                //handler(this, e);
            }
        }
        
        /// <summary>
        /// 异步回调函数
        /// </summary>
        /// <param name="ar"></param>
        void eventCallback(IAsyncResult ar)
        {
            //if (hObject.IsAllocated)
            //    hObject.Free();

            this._index += 1;
            if (this._index >= this._count)
            {
                //this.m_ReadImageCount++;
                //Console.WriteLine("Waite for restart read image: {0}", this.m_ReadImageCount);
                //Thread.Sleep(5000);
                //this._index = 0;
            }
            else
            {
                this._ent.Set();
            }
        }


        public Camera(string camName)
        {
            this._camName = camName;
            this._ThreadFlag = true;
            this._ent = new AutoResetEvent(false);
            TaskFactory factory = new TaskFactory();
            Task a = factory.StartNew(snapImage);
        }

        ~Camera()
        {

            this._ThreadFlag = false;
        }
    }
}
