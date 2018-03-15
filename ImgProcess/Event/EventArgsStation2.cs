using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgProcess.Event
{
    public class EventArgsStation2:EventArgs
    {
        public int ID { get; private set; }
        public int ImgID { get; private set; }
        public bool Result { get; private set; }


        public EventArgsStation2(int id, int imgID, bool result)
        {
            this.ID = id;
            this.ImgID = imgID;
            this.Result = result;
        }
    }
}
