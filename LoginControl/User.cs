using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginControl
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string EMailAddress { get; set; }
        public string ImageSourcePath { get; set; }

        public User()
        {

        }

        public User(string userName, string password, int level)
        {
            this.UserName = userName;
            this.Password = password;
            this.Level = level;
        }
    }
}
