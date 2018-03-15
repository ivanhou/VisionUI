using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cam
{
    public interface ICamera
    {
        event EventHandler<CameraEventArgs> NewImageEvent;

        Dictionary<string, int> DeviceList { get; }
        List<string> Devices { get; }
        Dictionary<string, GrabStatus> GrabingStatus { get; }
        List<bool> IsOpen { get; }
        void DeviceListAcq();
        List<bool> open(List<string> sn);

        List<bool> snap(List<string> sn);
        List<bool> startGrab(List<string> sn);

        void stopGrab(List<string> sn);
        void close(List<string> sn);

        void setTriggerMode(string sn, TriggerMode triggerMode);
        void setTriggerSource(string sn, TriggerSource triggerSource);
    }

    public enum GrabStatus
    {
        Open = 0,
        Close = 1,
        Stop = 2,
        Continuous = 3,
        Snap = 4,
        //ContinuSoftware = 7,
        //Continuous = 8,
    }

    public enum TriggerSource
    {
        //触发源选择:0 - Line0;
        //           1 - Line1;
        //           2 - Line2;
        //           3 - Line3;
        //           4 - Counter;
        //           7 - Software;

        Line0 = 0,
        Line1 = 1,
        Line2 = 2,
        Line3 = 3,
        Counter = 4,
        Software = 7,
    }
    public enum TriggerMode
    {
        Continues = 0,
        Trigger = 1,
    }
}
