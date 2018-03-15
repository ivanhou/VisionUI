using Chloe;
using Chloe.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseConfig.Util
{
    public class TSystemConfig
    {
        private SQLiteContext _sqlContext;


        public DataBaseConfig.SystemConfig getSystemById(int id =1)
        {
            DataBaseConfig.SystemConfig mSystemConfig;

            try
            {
                IQuery<DataBaseConfig.SystemConfig> q = this._sqlContext.Query<DataBaseConfig.SystemConfig>();
                mSystemConfig = q.Where(a => a.Id == id).First();
            }
            catch (Exception ex)
            {
                mSystemConfig = null;
                LogRecord.LogHelper.Exception(typeof(TSystemConfig), ex);
            }

            return mSystemConfig;
        }

        public int updateSystem(DataBaseConfig.SystemConfig system)
        {
            int rct = -1;

            try
            {
                rct = this._sqlContext.Update(system);
            }
            catch (Exception ex)
            {
                rct = -1;
                LogRecord.LogHelper.Exception(typeof(TSystemConfig), ex);
            }

            return rct;
        }


        public TSystemConfig(SQLiteContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }
    }
}
