using Chloe;
using Chloe.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig.Util
{
    public class TCameraInfo
    {
        private SQLiteContext _sqlContext;



        /// <summary>
        /// 获取所有Camera信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseConfig.CameraInfo> getCams()
        {
            List<DataBaseConfig.CameraInfo> mUsers;
            try
            {
                IQuery<DataBaseConfig.CameraInfo> q2 = this._sqlContext.Query<DataBaseConfig.CameraInfo>();
                mUsers = q2.Where(a => a.Id > 0).ToList();
            }
            catch (Exception ex)
            {
                LogRecord.LogHelper.Exception(typeof(TCameraInfo), ex);
                mUsers = new List<CameraInfo>();
            }

            return mUsers;
        }

        public DataBaseConfig.CameraInfo getCamByName(string camName)
        {
            DataBaseConfig.CameraInfo mCameraInfo;

            try
            {
                IQuery<DataBaseConfig.CameraInfo> q = this._sqlContext.Query<DataBaseConfig.CameraInfo>();
                mCameraInfo = q.Where(a => a.Name == camName).First();
            }
            catch (Exception ex)
            {
                mCameraInfo = null;
                LogRecord.LogHelper.Exception(typeof(TCameraInfo), ex);
            }

            return mCameraInfo;
        }

        public int addCameraInfo(string name, string type="File", int stationId=0, string serialNumber="0000", int exposure=2000, double gain=0.0, bool use=false)
        {
            int rct = -1;

            try
            {
                if ((!name.Equals("")) && (!serialNumber.Equals("")))
                {
                    rct = (int)this._sqlContext.Insert<DataBaseConfig.CameraInfo>(() => new DataBaseConfig.CameraInfo()
                    {
                        StationId = stationId,
                        Name = name,
                        SerialNumber = serialNumber,
                        TypeName = type,
                        Exposure = exposure,
                        Gain = gain,
                        Use = use
                    });
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                LogRecord.LogHelper.Exception(typeof(Tools), ex);
            }

            return rct;
        }
        
        public int delectCameraInfo(string name)
        {
            int rct = -1;

            try
            {
                if (!name.Equals(""))
                {
                    IQuery<DataBaseConfig.CameraInfo> q2 = this._sqlContext.Query<DataBaseConfig.CameraInfo>();
                    DataBaseConfig.CameraInfo mCam = q2.Where(a => a.Name == name).First();
                    rct = this._sqlContext.Delete(mCam);
                }
            }
            catch (Exception ex)
            {
                LogRecord.LogHelper.Exception(typeof(Tools), ex);
            }

            return rct;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int updateCam(DataBaseConfig.CameraInfo cam)
        {
            int rct = -1;

            try
            {
                rct = this._sqlContext.Update(cam);
            }
            catch (Exception ex)
            {
                rct = -1;
                LogRecord.LogHelper.Exception(typeof(TCameraInfo), ex);
            }

            return rct;
        }

        public TCameraInfo(SQLiteContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }
    }
}
