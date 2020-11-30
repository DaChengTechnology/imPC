using ChangLiao.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class LoginedSendModel:BaseSendModel
    {
        public string token { get; set; }
        public LoginedSendModel():base()
        {
            token = SettingMenager.shard.token;
        }
    }
}
