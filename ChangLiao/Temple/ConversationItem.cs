using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSkin.DirectUI;
using EaseMobLib;
using ChangLiao.Util;
using ChangLiao.DB;
using System.Windows.Forms;
using ChangLiao.Model.ViewModel;

namespace ChangLiao.Temple
{
    public partial class ConversationItem : DuiBaseControl
    {
        public ConversationModel model;
        public ConversationItem()
        {
            InitializeComponent();
        }

        public ConversationItem(ConversationModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            duiButton1.Visible = false;
            if (model.avatarImage != null)
            {
                avatarImageBtn.Image = model.avatarImage;
            }
            else
            {
                DCWebImageMaanager.shard.downloadImageAsync(model.avatarURL, (image, b) =>
                {
                    if (image != null)
                    {
                        avatarImageBtn.BeginInvoke(() =>
                        {
                            model.avatarImage = image;
                            avatarImageBtn.Image = image;
                        });
                    }
                });
            }
            if (Parent.InnerDuiControl != null)
            {
                if (Width != Parent.InnerDuiControl.Width)
                {
                    Width = Parent.InnerDuiControl.Width;
                }
            }
            Size size = TextRenderer.MeasureText(model.time, timeLabel.Font);
            timeLabel.Width = size.Width;
            timeLabel.Location = new Point(Width - 5 - size.Width, timeLabel.Location.Y);
            nameLabel.Width = Width - 60 - size.Width - 5;
            nameLabel.Text = model.name;
            timeLabel.Text = model.time;
            showMessageLabel.Text = model.lastMessageText;
            int unread = model.conversation.unreadMessagesCount();
            if (unread == 0)
            {
                duiButton1.Visible = false;
            }
            else
            {
                duiButton1.Visible = true;
                if (unread < 100)
                {
                    duiButton1.Text = unread.ToString();
                    duiButton1.Size = new Size(20, 20);
                }
                else
                {
                    duiButton1.Text = "99+";
                    duiButton1.Size = new Size(TextRenderer.MeasureText(duiButton1.Text, duiButton1.Font).Width + 2, 20);
                }
            }
        }

        private void ConversationItem_SizeChanged(object sender, EventArgs e)
        {
            Size size = TextRenderer.MeasureText(model.lastMessageText, timeLabel.Font);
            timeLabel.Width = size.Width;
            timeLabel.Location = new Point(Width - 5 - size.Width, timeLabel.Location.Y);
            nameLabel.Width = Width - 60 - timeLabel.Location.X - 5;
            showMessageLabel.Width = Width - 65;
        }

        private void ConversationItem_IsSelectedChanged(object sender, EventArgs e)
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

        private void ConversationItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.FromArgb(192, 192, 192);
        }

        private void ConversationItem_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.White;
        }
    }
}
