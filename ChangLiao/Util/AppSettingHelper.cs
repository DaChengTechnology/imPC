using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Util
{
    class AppSettingHelper
    {
        public static string getAppConfig(String key)
        {
            var app = ConfigurationManager.AppSettings;
            if(app.Count == 0)
            {
                return null;
            }
            Boolean ishave = false;
            foreach(String k in app.Keys)
            {
                if (key == k)
                {
                    ishave = true;
                    break;
                }
            }
            if (!ishave)
            {
                return null;
            }
            return app[key];
        }
    }
}