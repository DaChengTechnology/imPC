using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    class UserInfoDB
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string mobile { get; set; }
        public string id_card { get; set; }
        public string portrait { get; set; }
    }
    class UserInfoData
    {
        public UserInfoDB db { get; set; }
    }
    class UserInfoReciveModel
    {
        public UserInfoData data { get; set; }
    }
}
