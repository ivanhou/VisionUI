using Chloe;
using Chloe.SQLite;
using System;
using System.Collections.Generic;

namespace DataBaseConfig
{
    public class Tools
    {
        private SQLiteContext _sqlContext;

        #region CameraInfo

        ///// <summary>
        ///// 获取所有CameraInfo信息
        ///// </summary>
        ///// <returns></returns>
        //public List<DataBaseConfig.CameraInfo> getCameraInfo()
        //{
        //    List<DataBaseConfig.CameraInfo> mCameraInfo;
        //    try
        //    {
        //        IQuery<DataBaseConfig.CameraInfo> q2 = this._sqlContext.Query<DataBaseConfig.CameraInfo>();
        //        mCameraInfo = q2.Where(a => a.Id > 0).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogRecord.LogHelper.Exception(typeof(Tools), ex);
        //        mCameraInfo = new List<CameraInfo>();
        //    }

        //    return mCameraInfo;
        //}
        ///// <summary>
        ///// 添加新CameraInfo
        ///// </summary>
        ///// <param name="name">名称</param>
        ///// <param name="serialNumber">相机序列号</param>
        ///// <returns></returns>
        //public int addCameraInfo(int stationId, string name, string serialNumber, int exposure, double gain, bool use)
        //{
        //    int rct = -1;

        //    try
        //    {
        //        if ((!name.Equals(""))&&(!serialNumber.Equals("")))
        //        {
        //            rct = (int)this._sqlContext.Insert<DataBaseConfig.CameraInfo>(() => new DataBaseConfig.CameraInfo()
        //            {
        //                StationId = stationId,
        //                Name = name,
        //                SerialNumber = serialNumber,
        //                Exposure = exposure,
        //                Gain = gain,
        //                Use = use
        //            });
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogRecord.LogHelper.Exception(typeof(Tools), ex);
        //    }

        //    return rct;
        //}
        ///// <summary>
        ///// 更新CameraInfo信息
        ///// </summary>
        ///// <param name="cameraInfo">需要更新的CameraInfo内容</param>
        ///// <returns></returns>
        //public int updateCameraInfo(DataBaseConfig.CameraInfo cameraInfo)
        //{
        //    int rct = -1;

        //    try
        //    {
        //        rct = this._sqlContext.Update(cameraInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogRecord.LogHelper.Exception(typeof(Tools), ex);
        //    }

        //    return rct;
        //}

        #endregion


        //public Tools(SQLiteContext sqlContext)
        //{
        //    this._sqlContext = sqlContext;
        //}

    }
}
