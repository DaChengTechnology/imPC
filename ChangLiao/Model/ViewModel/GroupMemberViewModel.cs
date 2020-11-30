using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangLiao.DB;
using ChangLiao.Model.ReciveModel;

namespace ChangLiao.Model.ViewModel
{
    class GroupMemberViewModel
    {
        /// <summary>
        /// 群成员昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 群昵称
        /// </summary>
        public string GroupNickName { get; set; }
        /// <summary>
        /// 畅聊号
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        /// 成员身份
        /// </summary>
        public string Leave { get; set; }
        /// <summary>
        /// 群成员模型
        /// </summary>
        public GroupUser user { get; set; }
        public GroupMemberViewModel(GroupUser groupUser)
        {
            user = groupUser;
            NickName= string.IsNullOrEmpty(user.friend_name) ? user.user_name : user.friend_name;
            GroupNickName= string.IsNullOrEmpty(user.group_user_nickname) ? user.user_name : user.group_user_nickname;
            IDCard = user.id_card;
            if (user.is_administrator == 1)
            {
                Leave = "群主";
            }
            else if (user.is_manager == 1)
            {
                Leave = "管理员";
            }
            else
            {
                Leave = "成员";
            }
        }

        public GroupMemberViewModel(GroupMemberData data)
        {
            user = new GroupUser();
            user.groupID = data.group_id;
            user.userID = data.user_id;
            user.avatar = data.portrait;
            user.friend_name = data.friend_name;
            user.group_user_nickname = data.group_user_nickname;
            user.id_card = data.id_card;
            user.inv_name = data.inv_name;
            user.is_administrator = data.is_administrator;
            user.is_manager = data.is_manager;
            user.is_shield = data.is_shield;
            user.user_name = data.user_name;
            NickName = string.IsNullOrEmpty(user.friend_name) ? user.user_name : user.friend_name;
            GroupNickName = string.IsNullOrEmpty(user.group_user_nickname) ? user.user_name : user.group_user_nickname;
            IDCard = user.id_card;
            if (user.is_administrator == 1)
            {
                Leave = "群主";
            }
            else if (user.is_manager == 1)
            {
                Leave = "管理员";
            }
            else
            {
                Leave = "成员";
            }
        }
    }
}
