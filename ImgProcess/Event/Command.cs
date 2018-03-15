using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgProcess.Event
{

    public enum CommandResult
    {
        None,
        /// <summary>
        /// 处理结果OK
        /// </summary>
        OK,
        /// <summary>
        /// 处理结果NG
        /// </summary>
        NG,
        /// <summary>
        /// 处理过程报错
        /// </summary>
        ER,
    }

}
