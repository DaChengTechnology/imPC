using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class ApplyForFriendSendModel:LoginedSendModel
    {
        public string target_user_id { get; set; }
        public string target_user_name { get; set; }
        public string remark { get; set; }
    }
}
