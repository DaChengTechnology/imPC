using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    class LoginPasswordDataModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string token { get; set; }
    }
    class LoginPasswordReciveModel:BaseReciveModel
    {
        public LoginPasswordDataModel data { get; set; }
    }
}
