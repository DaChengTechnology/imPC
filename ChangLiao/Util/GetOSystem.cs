using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Util
{
    public class GetOSystem
    {
        private const string Windows2000 = "5.0";
        private const string WindowsXP = "5.1";
        private const string Windows2003 = "5.2";
        private const string Windows2008 = "6.0";
        private const string Windows7 = "6.1";
        private const string Windows8OrWindows81 = "6.2";
        private const string Windows10 = "10.0";

        private string OSystemName;

        public void setOSystemName(string oSystemName)
        {
            this.OSystemName = oSystemName;
        }
        /// <summary>
        /// 获取
        /// </summary>
        public GetOSystem()
        {
            switch (System.Environment.OSVersion.Version.Major + "." + System.Environment.OSVersion.Version.Minor)
            {
                case Windows2000:
                    setOSystemName("Windows2000");
                    break;
                case WindowsXP:
                    setOSystemName("WindowsXP");
                    break;
                case Windows2003:
                    setOSystemName("Windows2003");
                    break;
                case Windows2008:
                    setOSystemName("Windows2008");
                    break;
                case Windows7:
                    setOSystemName("Windows7");
                    break;
                case Windows8OrWindows81:
                    setOSystemName("Windows8.OrWindows8.1");
                    break;
                case Windows10:
                    setOSystemName("Windows10");
                    break;
            }

            Console.WriteLine(OSystemName);
        }

        /// <summary>
        /// 获取系统
        /// </summary>
        /// <returns></returns>
        public string getSystem()
        {
            return OSystemName;
        }

    }

}
