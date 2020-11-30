using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Controls;
using ChangLiao.DB;
using ChangLiao.Util;
using ChangLiao.windows;

namespace ChangLiao.Temple
{
    public partial class SearchResultListItem : DSkinListItemTemplate
    {
        private FriendModel friend;
        private GroupTable group;
        public SearchResultListItem()
        {
            InitializeComponent();
        }

        public SearchResultListItem(FriendModel model)
        {
            friend = model;
            InitializeComponent();
        }

        public SearchResultListItem(GroupTable model)
        {
            group = model;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (friend != null)
            {
                DCWebImageMaanager.shard.downloadImageAsync(friend.avatar, (image, b) =>
                {
                    if (image != null)
                    {
                        duiPictureBox1.BeginInvoke(() =>
                        {
                            duiPictureBox1.Image = image;
                        });
                    }
                });
                duiLabel1.Text = string.IsNullOrEmpty(friend.nickName) ? friend.friend_self_name : friend.nickName;
            }else if (group != null)
            {
                DCWebImageMaanager.shard.downloadImageAsync(group.avatar, (image, b) =>
                {
                    if (image != null)
                    {
                        duiPictureBox1.BeginInvoke(() =>
                        {
                            duiPictureBox1.Image = image;
                        });
                    }
                });
                duiLabel1.Text = group.groupName;
            }
        }

        private void SearchResultListItem_IsSelectedChanged(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                BackColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                BackColor = Color.White;
            }
        }

        public void enterChat()
        {
            MainFrm frm = Application.OpenForms["MainFrm"] as MainFrm;
            if (frm != null)
            {
                if (friend != null)
                {
                    frm.showPersonalChat(friend);
                }else if (group != null)
                {
                    frm.showGroupChat(group);
                }
            }
        }
    }
}
