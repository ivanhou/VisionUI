using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig
{
    [TableAttribute("User")]
    public class User
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        //[Sequence("USERS_AUTOID")]//For oracle
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }

    }
}
