using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EaseMobLib;
using ChangLiao.DB;
using ChangLiao.Util;

namespace ChangLiao.DIYListBox.ListItem
{
    class ConversationListItem : IDisposable
    {
        [DefaultValue(typeof(EMConversation),null)]
        public EMConversation conversation;
        public String name;
        public Image avatarImage;
        public String lastMessageText;
        public Boolean isSheild;
        public EMConversationType conversationType;
        public String time;
        public Boolean isWorkGroup;
        public String avatarURL;
        public void Dispose()
        {
            conversation = null;
        }
        public ConversationListItem()
        {

        }
        public ConversationListItem(EMConversation mConversation)
        {
            conversation = mConversation;
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
                        if (avatarImage == null)
                        {
                            avatarImage = Properties.Resources.moren;
                        }
                    });
                    if (avatarImage == null)
                    {
                        avatarImage = Properties.Resources.moren;
                    }
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
                        DCWebImageMaanager.shard.downloadImageAsync(stonger.avatar, (image, b) => {
                            
                            avatarImage = image;
                            if (avatarImage == null)
                            {
                                avatarImage = Properties.Resources.moren;
                            }
                        });
                        if (avatarImage == null)
                        {
                            avatarImage = Properties.Resources.moren;
                        }
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
                if(group != null)
                {
                    name = group.groupName;
                    avatarURL = group.avatar;
                    DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                    {
                        avatarImage = image;
                        if (avatarImage == null)
                        {
                            avatarImage = Properties.Resources.moren;
                        }
                    });
                    if (avatarImage == null)
                    {
                        avatarImage = Properties.Resources.moren;
                    }
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
                            if (avatarImage == null)
                            {
                                avatarImage = Properties.Resources.moren;
                            }
                        });
                        if (avatarImage == null)
                        {
                            avatarImage = Properties.Resources.moren;
                        }
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
