using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgProcess.Event
{
    public class EventArgsStation1:EventArgs
    {
        public int ID { get; private set; }
        public int ImgID { get;private set; }
        public bool Result { get; private set; }

        public CommandResult ResultCommand { get; private set; }

        public EventArgsStation1(int id, int imgID,CommandResult command, bool result)
        {
            this.ID = id;
            this.ImgID = imgID;
            this.ResultCommand = command;
            this.Result = result;
        }
    }
}
