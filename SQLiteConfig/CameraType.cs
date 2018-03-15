using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig
{
    [TableAttribute("CameraType")]
    public class CameraType
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        //[Sequence("USERS_AUTOID")]//For oracle
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
