using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class DeleteFriendSendModel:LoginedSendModel
    {
        public string target_user_id { get; set; }
    }
}
