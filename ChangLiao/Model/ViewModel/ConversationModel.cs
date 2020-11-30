using ChangLiao.DB;
using ChangLiao.Util;
using EaseMobLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ViewModel
{
    /// <summary>
    /// 会话模型
    /// </summary>
    public class ConversationModel
    {
        public EMConversation conversation;
        /// <summary>
        /// 会话名称 单聊是好友名称 群聊是群名称
        /// </summary>
        public String name;
        /// <summary>
        /// 会话头像 单聊是好友头像 群聊是群头像
        /// </summary>
        public Image avatarImage { get; set; }
        /// <summary>
        /// 最后的一条消息文本
        /// </summary>
        public String lastMessageText;
        /// <summary>
        /// 是否屏蔽
        /// </summary>
        public Boolean isSheild;
        /// <summary>
        /// 最后一条消息时间
        /// </summary>
        public String time;
        /// <summary>
        /// 是否工作群
        /// </summary>
        public Boolean isWorkGroup;
        /// <summary>
        /// 头像url
        /// </summary>
        public String avatarURL;
        /// <summary>
        /// 会话类型
        /// </summary>
        public EMConversationType conversationType;
        public ConversationModel(EMConversation mConversation)
        {
            conversation = mConversation;
            if (conversation == null)
            {
                throw new AggregateException("The conversation not be null");
            }
            conversationType = conversation.conversationType();
            if (conversation.conversationType() == EMConversationType.CHAT)
            {
                var friend = DBHelper.Instance.getFriend(conversation.conversationId());
                if (friend != null)
                {
                    name = string.IsNullOrEmpty(friend.target_user_nickname) ? friend.friend_self_name : friend.target_user_nickname;
                    avatarURL = friend.portrait;
                    DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                    {
                        avatarImage = image;
                    });
                    isWorkGroup = false;
                    if (friend.is_shield == 1)
                    {
                        isSheild = true;
                    }
                    else
                    {
                        isSheild = false;
                    }
                }
                else
                {
                    var stonger = DBHelper.Instance.GetStronger(conversation.conversationId());
                    if (stonger != null)
                    {
                        name = stonger.nickName;
                        avatarURL = stonger.avatar;
                        DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                        {
                            avatarImage = image;
                        });
                        isWorkGroup = false;
                        isSheild = false;
                    }
                    else
                    {
                        avatarImage = Properties.Resources.moren;
                    }
                }
            }
            else
            {
                var group = DBHelper.Instance.GetGroup(conversation.conversationId());
                if (group != null)
                {
                    name = group.groupName;
                    avatarURL = group.avatar;
                    DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                    {
                        avatarImage = image;
                    });
                    if (group.is_pingbi == 1)
                    {
                        isSheild = true;
                    }
                    else
                    {
                        isSheild = false;
                    }
                    if (group.group_type == 1)
                    {
                        isWorkGroup = true;
                    }
                    else
                    {
                        isWorkGroup = false;
                    }
                }
                else
                {
                    var gt = DBHelper.Instance.GetGroupCache(conversation.conversationId());
                    if (gt != null)
                    {
                        name = gt.groupName;
                        avatarURL = gt.avatar;
                        DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                        {
                            avatarImage = image;
                        });
                        isSheild = false;
                        isWorkGroup = false;
                    }
                    else
                    {
                        avatarImage = Properties.Resources.moren;
                    }
                }
            }
            if (avatarImage == null)
            {
                avatarImage = Properties.Resources.moren;
            }
            time = DCUtilTool.GetMessageTime(conversation.latestMessage());
            lastMessageText = DCUtilTool.getMessageShowTest(conversation.latestMessage());
        }
    }
}
