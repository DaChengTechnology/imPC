using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangLiao.DB;

namespace ChangLiao.Model.ReciveModel
{
    public class FriendListData:IDisposable
    {
        public string friend_self_name { get; set; }
        public string id_card { get; set; }
        public int is_shield { get; set; }
        public string user_id { get; set; }
        public string target_user_nickname { get; set; }
        public int is_star { get; set; }
        public int is_yhjf { get; set; }
        public string portrait { get; set; }

        public void Dispose()
        {
            
        }

        public FriendModel toFriend()
        {
            var model = new FriendModel();
            model.userID = user_id;
            model.nickName = target_user_nickname;
            model.friend_self_name = friend_self_name;
            model.id_Card = id_card;
            model.avatar = portrait;
            model.is_shild = is_shield;
            model.is_star = is_star;
            model.is_yhjf = is_yhjf;
            return model;
        }
    }
    class FriendListReciveModel:BaseReciveModel
    {
        public List<FriendListData> data { get; set; }
    }
}
