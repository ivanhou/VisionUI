using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ToolBoxControl
{
    public class ToolBoxItem
    {
        public delegate void ClickHandle(object sender, EventArgs e);
        //定义事件
        public event ClickHandle Clicked;

        private String _name = "";
        private Int32 _imageIndex = -1;
        private ToolBoxItem _parent = null;

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Int32 ImageIndex
        {
            get
            {
                return _imageIndex;
            }
            set
            {
                _imageIndex = value;
            }
        }

        public void OnClickEvent()
        {
            if (this.Clicked != null)
            {
                this.Clicked(this, new EventArgs());
            }
        }

        [Browsable(false)]
        public ToolBoxItem Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
    }

    public class ClickEventArgs : EventArgs
    {
        public String Name { get;private set; }
        public ClickEventArgs()
        {

        }
        public ClickEventArgs(String name)
        {
            this.Name = name;
        }
    }
}
