using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class AddGroupUserSendModel:LoginedSendModel
    {
        public string group_id { get; set; }
        public string group_user_ids { get; set; }
    }
}
