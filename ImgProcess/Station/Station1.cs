using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HalconDotNet;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Collections.Concurrent;

namespace ImgProcess.Station
{
    public class Station1
    {
        private System.Threading.Thread _runThread;

        private event EventHandler<Event.EventArgsStation1> _ImgProcessStation1 = null;

        private System.Threading.AutoResetEvent _are = new System.Threading.AutoResetEvent(true);

        //private Queue<ImgPackage> _ImgInfo = new Queue<ImgPackage>();
        private ConcurrentQueue<ImgPackage> _ImgInfos = new ConcurrentQueue<ImgPackage>();

        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
        private int _index = 0;
        private bool _ThreadFlag = false;
        private Vision.ImageProcessing _ImageProcessing;

        public void addImg(int id, HObject img)
        {
            //this._ImgInfo.Enqueue(new ImgPackage(id, img.Clone()));
            this._ImgInfos.Enqueue(new ImgPackage(id, img.Clone()));
        }

        private void runProcess()
        {
            ImgPackage mImgInfo = null;
            bool mFlag = false;
            HTuple hv_Height = new HTuple(), hv_Width = new HTuple();

            do
            {
                if (!this._ImgInfos.IsEmpty)
                {
                    try
                    {
                        this._are.WaitOne();
                        this._stopwatch.Restart();
                        this._index++;
                        mFlag = this._ImgInfos.TryDequeue(out mImgInfo);
                        if (mFlag)
                        {                            
                            Event.EventArgsStation1 mResult;
                            //imgProcess(1, mImgInfo.ID, mImgInfo.Img, out mResult);
                            this._ImageProcessing.ImgProcess(1, mImgInfo.ID, mImgInfo.Img, out mResult);
                            OnNewEvent(mResult);                            
                        }
                    }
                    catch (Exception ex)
                    {
                        LogRecord.LogHelper.Error(typeof(Station1), string.Format("{0}", ex));
                    }

                    
                    if (mImgInfo != null)
                    {
                        mImgInfo.Img.Dispose();
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }

            } while (this._ThreadFlag);

            
        }

        private void imgProcess(int id, int imgID, HObject ho_Image, out Event.EventArgsStation1 result)
        {
            bool mResult = false;
            HTuple hv_Height = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
            Event.CommandResult mCommand = Event.CommandResult.NG;
            try
            {
                for (int i = 0; i < 300000000; i++)
                {
                    int ii = i;
                }

                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.Intensity(ho_Image, ho_Image, out hv_Mean, out hv_Deviation);

                if (hv_Height.I > 100)
                {
                    mCommand = Event.CommandResult.OK;
                    mResult = true;
                }
            }
            catch (Exception ex)
            {
                mCommand = Event.CommandResult.ER;
                LogRecord.LogHelper.Error(typeof(Station1), string.Format("{0}", ex));
            }

            result = new Event.EventArgsStation1(id, imgID, mCommand, mResult);
        }

        //private void runProcessA()
        //{
        //    int mID = -1;
        //    int mStepID = 0;
        //    bool mResult = false;
        //    ImgPackage mImgInfo = null;

        //    HObject ho_Image = null;

        //    HTuple hv_Height = new HTuple(), hv_Width = new HTuple();

        //    var tokenSource = new CancellationTokenSource();
        //    var token = tokenSource.Token;

        //    Random mRandom = new Random(100);
        //    HOperatorSet.GenEmptyObj(out ho_Image);

        //    do
        //    {
        //        if (this._ImgInfo.Count > 0)
        //        {

        //            try
        //            {
        //                this._are.WaitOne();
        //                this._stopwatch.Restart();
        //                this._index++;
        //                mImgInfo = this._ImgInfo.Dequeue();
        //                //ho_Image.Dispose();
        //                //HOperatorSet.CopyImage(mImgInfo.Img, out ho_Image);

        //                //HOperatorSet.GetImageSize(mImgInfo.Img, out hv_Width, out hv_Height);

        //                //if (hv_Height.I > 100)
        //                //{
        //                //    mResult = true;
        //                //}

        //                Task tasks = Task.Factory.StartNew(() =>
        //                        DoSomeWork(this._index, mImgInfo.ID, mImgInfo.Img.Clone(), token), token);

        //                /*for (int i = 0; i < 1000000; i++)
        //                {
        //                    int ii = i;
        //                }
                        
        //                HOperatorSet.GetImageSize(mImgInfo.Img, out hv_Width, out hv_Height);

        //                if (hv_Height.I > 100)
        //                {
        //                    mResult = true;
        //                }*/
        //            }
        //            catch (Exception ex)
        //            {
        //                //mID = -1;
        //                LogRecord.LogHelper.Error(typeof(Station1), string.Format("{0}", ex));
        //            }

        //            //OnNewEvent(new Event.EventArgsStation1(mImgInfo.ID, mStepID, mResult));

        //            //if (mImgInfo != null)
        //            //{
        //            //    mImgInfo.Img.Dispose();
        //            //}

        //            //ho_Image.Dispose();
        //            this._are.Set();
        //        }
        //        else
        //        {
        //            System.Threading.Thread.Sleep(10);
        //        }

        //    } while (this._ThreadFlag);


        //}

        //private void DoSomeWork(int id, int imgID, HObject img, CancellationToken ct)
        //{
        //    bool mResult = false;
        //    HTuple hv_Height = new HTuple(), hv_Width = new HTuple();
        //    HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
        //    HObject ho_Image = null;
        //    HOperatorSet.GenEmptyObj(out ho_Image);

        //    try
        //    {
        //        ho_Image = img.Clone();

        //        for (int i = 0; i < 300000000; i++)
        //        {
        //            int ii = i;
        //        }

        //        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        //        HOperatorSet.Intensity(ho_Image, ho_Image, out hv_Mean, out hv_Deviation);
                
        //        if (hv_Height.I > 100)
        //        {
        //            mResult = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogRecord.LogHelper.Error(typeof(Station1), string.Format("{0}", ex));
        //    }

        //    OnNewEvent(new Event.EventArgsStation1(id, imgID, mResult));

        //    ho_Image.Dispose();
        //    img.Dispose();
        //}

        /// <summary>
        /// 事件触发方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNewEvent(Event.EventArgsStation1 e)
        {
            EventHandler<Event.EventArgsStation1> handler = this._ImgProcessStation1;

            if (handler != null)
            {
                //获取所有注册了的委托事件
                Delegate[] m_handlers = handler.GetInvocationList();

                foreach (var item in m_handlers)
                {
                    //遍历判断事件列表，执行当前触发的事件
                    if (item.Target.ToString().Equals(handler.Target.ToString()))
                    {
                        EventHandler<Event.EventArgsStation1> mhandler = (EventHandler<Event.EventArgsStation1>)item;
                        if (mhandler != null)
                        {
                            AsyncCallback EventDone = new AsyncCallback(CallBack);
                            //异步执行
                            mhandler.BeginInvoke(this, e, EventDone, e);
                        }
                    }
                }
            }
        }
        private void CallBack(IAsyncResult result)
        {
            //AsyncResult ar = (AsyncResult)result;
            //Event.EventArgsStation1 mResult = (Event.EventArgsStation1)ar.AsyncDelegate;
            //var mResult = (Event.EventArgsStation1)result.AsyncState;
            //Console.WriteLine(" {0}  {1}  {2}", mResult.ID, mResult.ImgID, this._ImgInfo.Count);

            this._stopwatch.Stop();
            Console.WriteLine("ElapsedTime {0}  {1} ", this._stopwatch.ElapsedMilliseconds, this._index);
            this._are.Set();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        public Station1(EventHandler<Event.EventArgsStation1> callback)
        {
            this._ImageProcessing = new Vision.ImageProcessing();
            this._ThreadFlag = true;
            this._ImgProcessStation1 = callback;
            this._runThread = new Thread(new ThreadStart(runProcess));
            this._runThread.IsBackground = true;
            this._runThread.Name = "Station1";
            this._runThread.Start();
            
        }

        ~Station1()
        {
            this._ThreadFlag = false;
            //foreach (var item in this._ImgInfo)
            //{
            //    item.Img.Dispose();
            //}

            foreach (var item in this._ImgInfos)
            {
                item.Img.Dispose();
            }
        }
    }
    
}
