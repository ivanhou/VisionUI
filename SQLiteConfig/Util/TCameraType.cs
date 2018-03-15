using Chloe;
using Chloe.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig.Util
{
    public class TCameraType
    {
        private SQLiteContext _sqlContext;



        /// <summary>
        /// 获取所有User信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseConfig.CameraType> getCameraTypes()
        {
            List<DataBaseConfig.CameraType> mCameraTypes;
            try
            {
                IQuery<DataBaseConfig.CameraType> q2 = this._sqlContext.Query<DataBaseConfig.CameraType>();
                mCameraTypes = q2.Where(a => a.Id > 0).ToList();
            }
            catch (Exception ex)
            {
                LogRecord.LogHelper.Exception(typeof(TCameraType), ex);
                mCameraTypes = new List<CameraType>();
            }

            return mCameraTypes;
        }

        public DataBaseConfig.CameraType getCameraTypeByName(string typeName)
        {
            DataBaseConfig.CameraType mCameraType;

            try
            {
                IQuery<DataBaseConfig.CameraType> q = this._sqlContext.Query<DataBaseConfig.CameraType>();
                mCameraType = q.Where(a => a.TypeName == typeName).First();
            }
            catch (Exception ex)
            {
                mCameraType = null;
                LogRecord.LogHelper.Exception(typeof(TCameraType), ex);
            }

            return mCameraType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public int addCameraType(string typeName)
        {
            int rct = -1;

            try
            {
                CameraType mUser = getCameraTypeByName(typeName);

                if (mUser == null)
                {
                    if (!typeName.Equals(""))
                    {
                        rct = (int)this._sqlContext.Insert<DataBaseConfig.CameraType>(() => new DataBaseConfig.CameraType()
                        {
                            TypeName = typeName
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                rct = -1;
                LogRecord.LogHelper.Exception(typeof(TCameraType), ex);
            }

            return rct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int updateCameraType(DataBaseConfig.CameraType user)
        {
            int rct = -1;

            try
            {
                rct = this._sqlContext.Update(user);
            }
            catch (Exception ex)
            {
                rct = -1;
                LogRecord.LogHelper.Exception(typeof(TCameraType), ex);
            }

            return rct;
        }

        public TCameraType(SQLiteContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }
    }
}
