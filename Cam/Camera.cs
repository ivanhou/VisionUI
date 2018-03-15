using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cam
{
    public class Camera
    {
        public Camera() { }
        
        public virtual event EventHandler<CameraEventArgs> NewImageEvent;
        public virtual void DeviceListAcq() { }
        public virtual List<bool> open(List<string> sn) { return null; }

        public virtual List<bool> snap(List<string> sn) { return null; }
        public virtual List<bool> startGrab(List<string> sn) { return null; }
        public virtual void stopGrab(List<string> sn) { }
        public virtual void close(List<string> sn) { }
    }
}
