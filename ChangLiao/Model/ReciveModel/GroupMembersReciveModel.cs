using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    class GroupMemberData
    {
        public string group_id { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public int is_administrator { get; set; }
        public string portrait { get; set; }
        public int is_shield { get; set; }
        public int is_manager { get; set; }
        public string id_card { get; set; }
        public string inv_name { get; set; }
        public string group_user_nickname { get; set; }
        public string friend_name { get; set; }
    }
    class GroupMembersReciveModel:BaseReciveModel
    {
        public List<GroupMemberData> data { get; set; }
    }
}
