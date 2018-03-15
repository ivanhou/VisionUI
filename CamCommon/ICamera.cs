using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamCommon
{
    public class ICamera
    {
        public virtual event EventHandler<CameraEventArgs> NewImageEvent;
        public virtual bool Open(string serialNum)
        {
            return true;
        }

        public virtual void Close(string serialNum)
        {

        }

        public virtual bool StartGrab()
        {
            return true;
        }

        public virtual bool StopGrab()
        {
            return true;
        }

        public virtual bool SetExposure(int value)
        {
            return true;
        }

        public virtual bool SetGain(double value)
        {
            return true;
        }
    }
}
