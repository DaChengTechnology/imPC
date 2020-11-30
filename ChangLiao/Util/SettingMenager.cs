using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChangLiao.Util
{
    class SettingMenager
    {
        private static object mo = new object();
        private string m_userid;
        private string m_avatar;
        private string m_idcard;
        private string m_token;
        private string m_phone;
        private string m_username;
        public string userID { get => m_userid; set {
                lock (mo)
                {
                    m_userid = value;
                }
            } }
        public string idCard { get => m_idcard; set {
                lock (mo)
                {
                    m_idcard = value;
                }
            } }
        public string token { get=>m_token; set
            {
                lock (mo)
                {
                    m_token = value;
                }
            }
        }
        public string avatar { get=>m_avatar; set
            {
                lock (mo)
                {
                    m_avatar = value;
                }
            }
        }
        public string phone { get => m_phone; set
            {
                lock (mo)
                {
                    m_phone = value;
                }
            }
        }
        public string username { get=>m_username; set
            {
                lock (mo)
                {
                    m_username = value;
                }
            }
        }
        private static SettingMenager menager;
        private static object padlock = new object();
        public static SettingMenager shard
        {
            get
            {
                lock (padlock)
                {
                    if (menager == null)
                    {
                        menager = new SettingMenager();
                    }
                    return menager;
                }
            }
        }

        private SettingMenager()
        {
            
        }
        
        public void ClearLogin()
        {
            lock (padlock)
            {
                m_avatar = null;
                m_idcard = null;
                m_phone = null;
                m_token = null;
                m_userid = null;
                m_username = null;
            }
        }
    }
}
