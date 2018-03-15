using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionTools
{
    internal class AppValue
    {
        protected static AppValue _instance = null;
        protected static object _lock = new object();

        private AppValue()
        {

        }
        
        public static AppValue Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppValue();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// config文件夹
        /// </summary>
        public string _ConfigDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config\\";
        /// <summary>
        /// match配置文件存放路径
        /// </summary>
        public string _MatchingConfigPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config\\" + "MatchinConfig.xml";
        /// <summary>
        /// match模板存放路径
        /// </summary>
        public string _ModelIdSavePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config\\" + "MatchModel\\";
        
        /// <summary>
        /// checkRoi配置文件存放路径
        /// </summary>
        public string _CheckRoiConfigPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config\\" + "CheckRoiConfig.xml";

    }
}
