using Chloe.SQLite;
using DataBaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CamFile;
using CamCommon;
using ImgProcess;

namespace VisionUI
{
    public class AppValue
    {
        private static AppValue _Instance = null;
        private static object _Lock = new object();

        private AppValue()
        {
            this._TSystemConfig = new DataBaseConfig.Util.TSystemConfig(this._SQLContext);
        }

        public static AppValue Instance()
        {
            if (_Instance == null)
            {
                lock (_Lock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new AppValue();
                    }
                }
            }
            return _Instance;
        }

        private SQLiteContext _SQLContext = new SQLiteContext(new SQLiteConnectionFactory(System.Environment.CurrentDirectory + "\\DataBase", "DataBase.db"));

        private DataBaseConfig.Util.TSystemConfig _TSystemConfig;

        public string FlowChartFile { get{return System.Environment.CurrentDirectory+"\\FlowChartFiles"; } }

        public bool LoginStatus { get; set; }
        public DataBaseConfig.SystemConfig SystemConfig { get; set; }

        public CamCommon.ICamera _ICamera;
        public ImgProcess.Station.Station1 _Station1;

        public void getSystemConfig()
        {
            this.SystemConfig = this._TSystemConfig.getSystemById(1);
        }

        public void updateSystemConfig()
        {
            this._TSystemConfig.updateSystem(this.SystemConfig);
        }


        public  frmMDI App
        {
            get { return _frmMDI; }
        }

        private frmMDI _frmMDI = null;

        public void setMDIForm(frmMDI frm)
        {
            this._frmMDI = frm;
        }

    }
    
}
