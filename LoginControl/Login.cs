using Chloe.SQLite;
using DataBaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginControl
{
    public class Login
    {
        public static Login _Login = null;
        public static object _Lock = new object();

        public event EventHandler<LoginEventArgs> NewLoginEvent = null;

        private SQLiteContext _SQLContext = new SQLiteContext(new SQLiteConnectionFactory(System.Environment.CurrentDirectory + "\\DataBase", "DataBase.db"));
        private DataBaseConfig.Util.TUser _TUser;

        public static Login Instance()
        {
            if (_Login == null)
            {
                lock (_Lock)
                {
                    if (_Login== null)
                    {
                        _Login = new Login();
                    }
                }
            }

            return _Login;
        }

        public bool trigger(object sender, string userName, string password)
        {
            bool mRct = false;

            //通过userName查找数据库user表格
            DataBaseConfig.User mUser = this._TUser.getUserByName(userName);
            User mU = new User();

            if (mUser!=null)
            {
                if (mUser.UserName.Equals(userName)&&mUser.Password.Equals(password))
                {
                    mRct = true;
                    mU = new User(mUser.UserName, mUser.Password, mUser.Level);
                }
            }

            OnNewLogionEvent(new LoginEventArgs(sender, mRct, mU));

            return mRct;
        }

        protected virtual void OnNewLogionEvent(LoginEventArgs e)
        {
            EventHandler<LoginEventArgs> mLoginEvent = this.NewLoginEvent;

            if (mLoginEvent!=null)
            {
                mLoginEvent(this, e);
            }
        }

        private Login()
        {
            this._TUser = new DataBaseConfig.Util.TUser(this._SQLContext);
        }
    }
}
