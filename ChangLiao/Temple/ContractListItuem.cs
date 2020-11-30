using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSkin.Controls;
using ChangLiao.DB;
using ChangLiao.Util;

namespace ChangLiao.Temple
{
    public partial class ContractListItuem : DSkinListItemTemplate
    {
        private FriendModel model;
        public ContractListItuem()
        {
            InitializeComponent();
        }
        public ContractListItuem(FriendModel friend)
        {
            InitializeComponent();
            model = friend;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (model == null)
            {
                model = (FriendModel)RowData;
            }
            if (model != null)
            {
                DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (imagePath, b) =>
                {
                    Image image = imagePath;
                    if (image != null)
                    {
                        avatarPictureBox.BeginInvoke(() =>
                        {
                            avatarPictureBox.BackgroundImage = image;
                        });
                    }
                });
                this.BeginInvoke(() =>
                {
                    nameLabel.Text = string.IsNullOrWhiteSpace(model.nickName) ? model.friend_self_name : model.nickName;
                    idLabel.Text = "畅聊号:" + model.id_Card;
                });
            }
        }

        private void ContractListItuem_IsSelectedChanged(object sender, EventArgs e)
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

        private void ContractListItuem_MouseEnter(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.FromArgb(192, 192, 192);
        }

        private void ContractListItuem_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.White;
        }
    }
}
