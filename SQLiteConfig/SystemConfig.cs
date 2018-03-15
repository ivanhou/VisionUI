using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig
{
    [TableAttribute("SystemConfig")]
    public class SystemConfig
    {

        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        //[Sequence("USERS_AUTOID")]//For oracle
        public int Id { get; set; }

        public int Language { get; set; }

    }
}
