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

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 群组面板
    /// </summary>
    public partial class GroupPanl : DSkinUserControl
    {
        public GroupTable model { get; set; }
        public GroupPanl()
        {
            InitializeComponent();
        }

        public GroupPanl(GroupTable group)
        {
            model = group;
            InitializeComponent();
        }

        private void GroupPanl_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.avatar))
                {
                    DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (imagePath, b) =>
                    {
                        if (imagePath!=null)
                        {
                            avatarPictureBox.BeginInvoke(() =>
                            {
                                avatarPictureBox.Image = imagePath;
                            });
                        }
                    });
                }
                nameLabel.Text = model.groupName;
                nameLabel.Size = TextRenderer.MeasureText(nameLabel.Text, nameLabel.Font);
            }
            avatarPictureBox.Location = new Point((Width - avatarPictureBox.Width) / 2, 60);
            nameLabel.Location = new Point((Width - nameLabel.Width) / 2, 170);
            sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 230);
        }

        private void GroupPanl_SizeChanged(object sender, EventArgs e)
        {
            avatarPictureBox.Location = new Point((Width - avatarPictureBox.Width) / 2, 60);
            nameLabel.Location = new Point((Width - nameLabel.Width) / 2, 170);
            sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 230);
        }

        private void sendMessageButton_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
            if (frm != null)
            {
                frm.showGroupChat(model);
            }
        }

        private void GroupPanl_Layout(object sender, LayoutEventArgs e)
        {
            avatarPictureBox.Location = new Point((Width - avatarPictureBox.Width) / 2, 60);
            nameLabel.Location = new Point((Width - nameLabel.Width) / 2, 170);
            sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 230);
        }

        public void setGroup(GroupTable group)
        {
            model = group;
            if (model != null)
            {
                this.BeginInvoke(new EventHandler((s, e) => {
                    if (!string.IsNullOrEmpty(model.avatar))
                    {
                        DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (imagePath, b) =>
                        {
                            if (imagePath!=null)
                            {
                                avatarPictureBox.BeginInvoke(() =>
                                {
                                    avatarPictureBox.Image = imagePath;
                                });
                            }
                        });
                    }
                    nameLabel.Text = model.groupName;
                    nameLabel.Size = TextRenderer.MeasureText(nameLabel.Text, nameLabel.Font);
                    avatarPictureBox.Location = new Point((Width - avatarPictureBox.Width) / 2, 60);
                    nameLabel.Location = new Point((Width - nameLabel.Width) / 2, 170);
                    sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 230);
                }));
            }
        }
    }
}
