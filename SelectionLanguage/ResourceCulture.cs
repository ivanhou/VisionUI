using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace SelectionLanguage
{
    public class ResourceCulture
    {
        public static Language CurrentLanguage { get; set; }

        /// <summary>
        /// Set current culture by name
        /// </summary>
        /// <param name="name">name</param>
        public static void SetCurrentCulture(Language name)
        {
            string mName = "zh-CN";

            switch (name)
            {
                case Language.zh_CN:
                    mName = "zh-CN";
                    break;
                case Language.en_US:
                    mName = "en-US";
                    break;
                default:
                    mName = "zh-CN";
                    break;
            }
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo(mName);
        }
        /// <summary>
        /// Get string by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>current language string</returns>
        public static string GetString(string id)
        {
            string strCurLanguage = "";
            try
            {
                ResourceManager rm = new ResourceManager("SelectionLanguage.Resource.Resource", Assembly.GetExecutingAssembly());
                CultureInfo ci = Thread.CurrentThread.CurrentCulture;
                strCurLanguage = rm.GetString(id, ci);
            }
            catch (Exception ex)
            {
                strCurLanguage = "No id:" + id + ", please add.";
            }
            return strCurLanguage;
        }
    }


    public enum Language
    {
        zh_CN = 0,
        en_US = 1,
    }
}
