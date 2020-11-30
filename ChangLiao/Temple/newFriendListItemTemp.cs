using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using DSkin.Controls;
using System.Windows.Forms;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Util;

namespace ChangLiao.Temple
{
    public partial class newFriendListItemTemp : DSkinListItemTemplate
    {
        private GetUserData data;
        public GetUserData Data { get => data; }
        public newFriendListItemTemp()
        {
            InitializeComponent();
        }

        public newFriendListItemTemp(GetUserData userData)
        {
            data = userData;
            InitializeComponent();
            duiLabel1.Text = data.user_name + "(" + data.id_card + ")";
            DCWebImageMaanager.shard.downloadImageAsync(data.portrait, (image, b) =>
            {
                if (image != null)
                {
                    this.BeginInvoke(() =>
                    {
                        duiPictureBox1.Image = image;
                        this.Invalidate();
                    });
                }
            });
        }

        private void newFriendListItemTemp_IsSelectedChanged(object sender, EventArgs e)
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

        private void newFriendListItemTemp_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.FromArgb(192, 192, 192);
        }

        private void newFriendListItemTemp_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.White;
        }

        private void newFriendListItemTemp_SizeChanged(object sender, EventArgs e)
        {
            duiBaseControl1.Width = Width;
        }
    }
}
