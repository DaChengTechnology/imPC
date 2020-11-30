using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.DirectUI;
using EaseMobLib;
using ChangLiao.Util;
using ChangLiao.DB;

namespace ChangLiao.Temple
{
    public partial class UnreadTipsItem : DuiBaseControl
    {
        public UnreadTipsItem()
        {
            InitializeComponent();
        }

        public UnreadTipsItem(EMConversation conversation)
        {
            InitializeComponent();
            if(conversation.conversationType()== EMConversationType.GROUPCHAT)
            {
                var group = DBHelper.Instance.GetGroup(conversation.conversationId());
                if (group != null)
                {
                    DCWebImageMaanager.shard.downloadImageAsync(group.avatar, (image, b) =>
                     {
                         if (image != null)
                         {
                             this.BeginInvoke(() =>
                             {
                                 duiPictureBox1.Image = image;
                             });
                         }
                     });
                    this.BeginInvoke(() =>
                    {
                        duiLabel1.Text = group.groupName;
                        duiLabel2.Text = DCUtilTool.getMessageShowTest(conversation.latestMessage());
                    });
                }
                else
                {
                    var g = DBHelper.Instance.GetGroupCache(conversation.conversationId());
                    if (g != null)
                    {
                        DCWebImageMaanager.shard.downloadImageAsync(g.avatar, (image, b) =>
                        {
                            if (image != null)
                            {
                                this.BeginInvoke(() =>
                                {
                                    duiPictureBox1.Image = image;
                                });
                            }
                        });
                        this.BeginInvoke(() =>
                        {
                            duiLabel1.Text = g.groupName;
                            duiLabel2.Text = DCUtilTool.getMessageShowTest(conversation.latestMessage());
                        });
                    }
                }
            }
            else
            {
                var friend = DBHelper.Instance.getFriend(conversation.conversationId());
                if (friend != null)
                {
                    DCWebImageMaanager.shard.downloadImageAsync(friend.portrait, (image, b) =>
                    {
                        if (image != null)
                        {
                            this.BeginInvoke(() =>
                            {
                                duiPictureBox1.Image = image;
                            });
                        }
                    });
                    this.BeginInvoke(() =>
                    {
                        duiLabel1.Text = string.IsNullOrEmpty(friend.target_user_nickname) ? friend.friend_self_name : friend.target_user_nickname;
                        duiLabel2.Text = DCUtilTool.getMessageShowTest(conversation.latestMessage());
                    });
                }
                else
                {
                    var user = DBHelper.Instance.GetStronger(conversation.conversationId());
                    if (user != null)
                    {
                        DCWebImageMaanager.shard.downloadImageAsync(user.avatar, (image, b) =>
                        {
                            if (image != null)
                            {
                                this.BeginInvoke(() =>
                                {
                                    duiPictureBox1.Image = image;
                                });
                            }
                        });
                        this.BeginInvoke(() =>
                        {
                            duiLabel1.Text = user.nickName;
                            duiLabel2.Text = DCUtilTool.getMessageShowTest(conversation.latestMessage());
                        });
                    }
                }
            }
        }

        private void UnreadTipsItem_IsSelectedChanged(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                BackColor = Color.FromArgb(204, 204, 204);
            }
            else
            {
                BackColor = Color.White;
            }
        }

        private void UnreadTipsItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.FromArgb(192, 192, 192);
        }

        private void UnreadTipsItem_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.White;
        }
    }
}
