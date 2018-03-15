using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionUI
{
    public class ToolBoxEventArgs:System.EventArgs
    {
        public string Name { get;private set; }
        public ToolBoxEventArgs(string name)
        {
            this.Name = name;
        }
    }
}
