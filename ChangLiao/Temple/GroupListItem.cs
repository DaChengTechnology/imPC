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

namespace ChangLiao.Temple
{
    public partial class GroupListItem : DSkinListItemTemplate
    {
        private GroupTable group;
        public GroupListItem()
        {
            InitializeComponent();
        }
        public GroupListItem(GroupTable table)
        {
            InitializeComponent();
            group = table;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (group == null)
            {
                group = (GroupTable)RowData;
            }
            if (group != null)
            {
                DCWebImageMaanager.shard.downloadImageAsync(group.avatar, (imagePath, b) =>
                {
                    Image image = imagePath;
                    if (image != null)
                    {
                        headPictureBox.BeginInvoke(() =>
                        {
                            headPictureBox.Image = image;
                        });
                    }
                });
                this.BeginInvoke(() =>
                {
                    nameLabel.Text = group.groupName;
                    this.Invalidate();
                }); 
            }
        }

        private void GroupListItem_IsSelectedChanged(object sender, EventArgs e)
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

        private void GroupListItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.FromArgb(192, 192, 192);
        }

        private void GroupListItem_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.White;
        }
    }
}
