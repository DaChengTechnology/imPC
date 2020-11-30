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
using ChangLiao.Model.ReciveModel;

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 好友面板
    /// </summary>
    public partial class PersonalPanl : DSkinUserControl
    {
        /// <summary>
        /// 好友模型
        /// </summary>
        public FriendModel model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private GetUserData data;
        public PersonalPanl()
        {
            InitializeComponent();
        }

        public PersonalPanl(FriendModel friend)
        {
            model = friend;
            InitializeComponent();
        }

        private void PersonalPanl_SizeChanged(object sender, EventArgs e)
        {
            avatarPictureBox.Location = new Point(Width / 2 - avatarPictureBox.Width / 2, 60);
            nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
            nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
            idCardLabel.Location = new Point((Width - idCardLabel.Width) / 2, 230);
            sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
        }

        private void PersonalPanl_Load(object sender, EventArgs e)
        {
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.portrait))
                {
                    DCWebImageMaanager.shard.downloadImageAsync(data.portrait, (image, b) =>
                    {
                        if (image != null)
                        {
                            avatarPictureBox.BeginInvoke(() =>
                            {
                                avatarPictureBox.Image = image;
                            });
                        }
                    });
                }
                this.BeginInvoke(new EventHandler((s, ee) =>
                {
                    nameLabel.Text = data.user_name;
                    nameLabel.Size = TextRenderer.MeasureText(nameLabel.Text, nameLabel.Font);
                    nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
                    nickNameLabel.Text = "畅聊号:" + data.id_card;
                    nickNameLabel.Size = TextRenderer.MeasureText(nickNameLabel.Text, nickNameLabel.Font);
                    nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
                    idCardLabel.Visible = false;
                    sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
                    sendMessageButton.Text = "加为好友";
                }));
            }else
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.avatar))
                {
                    DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (imagePath, b) =>
                     {
                         Image image = imagePath;
                         if (image != null)
                         {
                             avatarPictureBox.BeginInvoke(() =>
                             {
                                 avatarPictureBox.Image = image;
                             });
                         }
                     });
                }
                this.BeginInvoke(new EventHandler((s, ee) =>
                {
                    nameLabel.Text = model.friend_self_name;
                    nameLabel.Size = TextRenderer.MeasureText(model.friend_self_name, nameLabel.Font);
                    nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
                    if (string.IsNullOrEmpty(model.nickName))
                    {
                        nickNameLabel.Visible = false;
                    }
                    else
                    {
                        nickNameLabel.Visible = true;
                        nickNameLabel.Text = "备注:" + model.nickName;
                        nickNameLabel.Size = TextRenderer.MeasureText(nickNameLabel.Text, nickNameLabel.Font);
                        nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
                    }
                    idCardLabel.Text = "畅聊号:" + model.id_Card;
                    idCardLabel.Size = TextRenderer.MeasureText(idCardLabel.Text, idCardLabel.Font);
                    idCardLabel.Location = new Point((Width - idCardLabel.Width) / 2, 230);
                    sendMessageButton.Text = "发送消息";
                }));
            }
        }

        private void sendMessageButton_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            if (data != null)
            {
                AddFriendCheckForm form = new AddFriendCheckForm(data);
                form.Show();
                return;
            }
            MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
            if (frm != null)
            {
                frm.showPersonalChat(model);
                frm.BringToFront();
            }
        }

        private void PersonalPanl_Layout(object sender, LayoutEventArgs e)
        {
            avatarPictureBox.Location = new Point(Width / 2 - avatarPictureBox.Width / 2, 60);
            nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
            nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
            idCardLabel.Location = new Point((Width - idCardLabel.Width) / 2, 230);
            sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
        }

        public void setFriend(FriendModel friend)
        {
            model = friend;
            if (model != null)
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        if (!string.IsNullOrEmpty(model.avatar))
                        {
                            DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (imagePath, b) =>
                            {
                                Image image = imagePath;
                                if (image != null)
                                {
                                    avatarPictureBox.BeginInvoke(() =>
                                    {
                                        avatarPictureBox.Image = image;
                                    });
                                }
                            });
                        }
                        nameLabel.Text = model.friend_self_name;
                        nameLabel.Size = TextRenderer.MeasureText(model.friend_self_name, nameLabel.Font);
                        nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
                        if (string.IsNullOrEmpty(model.nickName))
                        {
                            nickNameLabel.Visible = false;
                        }
                        else
                        {
                            nickNameLabel.Text = "昵称:" + model.nickName;
                            nickNameLabel.Size = TextRenderer.MeasureText(nickNameLabel.Text, nickNameLabel.Font);
                            nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
                        }
                        idCardLabel.Text = "畅聊号:" + model.id_Card;
                        idCardLabel.Size = TextRenderer.MeasureText(idCardLabel.Text, idCardLabel.Font);
                        idCardLabel.Location = new Point((Width - idCardLabel.Width) / 2, 230);
                        sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
                        sendMessageButton.Text = "发送消息";
                    }));
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.avatar))
                    {
                        DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (imagePath, b) =>
                        {
                            Image image = imagePath;
                            if (image != null)
                            {
                                avatarPictureBox.BeginInvoke(() =>
                                {
                                    avatarPictureBox.Image = image;
                                });
                            }
                        });
                    }
                    nameLabel.Text = model.friend_self_name;
                    nameLabel.Size = TextRenderer.MeasureText(model.friend_self_name, nameLabel.Font);
                    nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
                    if (string.IsNullOrEmpty(model.nickName))
                    {
                        nickNameLabel.Visible = false;
                    }
                    else
                    {
                        nickNameLabel.Text = "昵称:" + model.nickName;
                        nickNameLabel.Size = TextRenderer.MeasureText(nickNameLabel.Text, nickNameLabel.Font);
                        nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
                    }
                    idCardLabel.Text = "畅聊号:" + model.id_Card;
                    idCardLabel.Size = TextRenderer.MeasureText(idCardLabel.Text, idCardLabel.Font);
                    idCardLabel.Location = new Point((Width - idCardLabel.Width) / 2, 230);
                    sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
                    sendMessageButton.Text = "发送消息";
                }
            }
        }
        public void setDataBeforeLoad(GetUserData data)
        {
            this.data = data;
        }
        public void setData(GetUserData data)
        {
            this.data = data;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.portrait))
                {
                    DCWebImageMaanager.shard.downloadImageAsync(data.portrait, (image, b) =>
                    {
                        if (image != null)
                        {
                            avatarPictureBox.BeginInvoke(() =>
                            {
                                avatarPictureBox.Image = image;
                            });
                        }
                    });
                }
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        nameLabel.Text = data.user_name;
                        nameLabel.Size = TextRenderer.MeasureText(nameLabel.Text, nameLabel.Font);
                        nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
                        nickNameLabel.Text = "畅聊号:" + model.id_Card;
                        nickNameLabel.Size = TextRenderer.MeasureText(nickNameLabel.Text, nickNameLabel.Font);
                        nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
                        idCardLabel.Visible = false;
                        sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
                        sendMessageButton.Text = "加为好友";
                    }));
                }
                else
                {
                    nameLabel.Text = data.user_name;
                    nameLabel.Size = TextRenderer.MeasureText(nameLabel.Text, nameLabel.Font);
                    nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 170);
                    nickNameLabel.Text = "畅聊号:" + model.id_Card;
                    nickNameLabel.Size = TextRenderer.MeasureText(nickNameLabel.Text, nickNameLabel.Font);
                    nickNameLabel.Location = new Point((Width - nickNameLabel.Width) / 2, 200);
                    idCardLabel.Visible = false;
                    sendMessageButton.Location = new Point((Width - sendMessageButton.Width) / 2, 250);
                    sendMessageButton.Text = "加为好友";
                }
            }
        }
    }
}
