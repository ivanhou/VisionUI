using Chloe;
using Chloe.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig.Util
{
    public class TUser
    {
        private SQLiteContext _sqlContext;



        /// <summary>
        /// 获取所有User信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseConfig.User> getUsers()
        {
            List<DataBaseConfig.User> mUsers;
            try
            {
                IQuery<DataBaseConfig.User> q2 = this._sqlContext.Query<DataBaseConfig.User>();
                mUsers = q2.Where(a => a.Id > 0).ToList();
            }
            catch (Exception ex)
            {
                LogRecord.LogHelper.Exception(typeof(TUser), ex);
                mUsers = new List<User>();
            }

            return mUsers;
        }

        public DataBaseConfig.User getUserByName(string userName)
        {
            DataBaseConfig.User mUser;

            try
            {
                IQuery<DataBaseConfig.User> q = this._sqlContext.Query<DataBaseConfig.User>();
                mUser = q.Where(a => a.UserName == userName).First();
            }
            catch (Exception ex)
            {
                mUser = null;
                LogRecord.LogHelper.Exception(typeof(TUser), ex);
            }

            return mUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public int addUser(string userName, string password, int level=1)
        {
            int rct = -1;

            try
            {
                User mUser = getUserByName(userName);

                if (mUser == null)
                {
                    if ((!userName.Equals("")) && (!password.Equals("")))
                    {
                        rct = (int)this._sqlContext.Insert<DataBaseConfig.User>(() => new DataBaseConfig.User()
                        {
                            UserName = userName,
                            Password = password,
                            Level = level
                        });
                    }
                }
               
            }
            catch (Exception ex)
            {
                rct = -1;
                LogRecord.LogHelper.Exception(typeof(TUser), ex);
            }

            return rct;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int updateUser(DataBaseConfig.User user)
        {
            int rct = -1;

            try
            {
                rct = this._sqlContext.Update(user);
            }
            catch (Exception ex)
            {
                rct = -1;
                LogRecord.LogHelper.Exception(typeof(TUser), ex);
            }

            return rct;
        }

        public TUser(SQLiteContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }
    }
}
