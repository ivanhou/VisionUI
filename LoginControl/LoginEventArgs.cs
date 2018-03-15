using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginControl
{
    public class LoginEventArgs:EventArgs
    {
        public object Sender { get; private set; }
        public bool IsLogin { get; private set; }
        public User UerInfo { get; private set; }
        public LoginEventArgs(object sender, bool isLogin, User userInfo)
        {
            this.Sender = sender;
            this.IsLogin = isLogin;
            this.UerInfo = userInfo;
        }
    }
}
