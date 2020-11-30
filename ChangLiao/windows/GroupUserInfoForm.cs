using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;
using ChangLiao.DB;
using ChangLiao.Util;
using ChangLiao.Model.ReciveModel;

namespace ChangLiao.windows
{
    public partial class GroupUserInfoForm : DSkinForm
    {
        private GroupUser member;
        private GroupTable group;
        public GroupUserInfoForm()
        {
            InitializeComponent();
        }

        public GroupUserInfoForm(GroupTable group, GroupUser user)
        {
            this.group = group;
            member = user;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(group==null|| member == null)
            {
                return;
            }

            DCWebImageMaanager.shard.downloadImageAsync(member.avatar, (image, b) =>
             {
                 if (image != null)
                 {
                     duiPictureBox1.BeginInvoke(() =>
                     {
                         duiPictureBox1.Image = image;
                     });
                 }
             });
            if (group.group_type == 1)
            {
                if (DBHelper.Instance.checkFriend(member.userID))
                {
                    nameLabel1.Text = string.IsNullOrEmpty(member.friend_name) ? (string.IsNullOrEmpty(member.group_user_nickname) ? member.user_name : member.group_user_nickname) : member.friend_name + "(" + member.id_card + ")";
                    nameLabel1.Size = TextRenderer.MeasureText(nameLabel1.Text, nameLabel1.Font);
                    nameLabel1.Location = new Point((Width - nameLabel1.Width) / 2, nameLabel1.Location.Y);
                    if (string.IsNullOrEmpty(member.group_user_nickname))
                    {
                        groupNameLabel.Visible = false;
                    }
                    else
                    {
                        groupNameLabel.Text = "群昵称:" + member.group_user_nickname;
                        groupNameLabel.Visible = true;
                        groupNameLabel.Size = TextRenderer.MeasureText(groupNameLabel.Text, groupNameLabel.Font); ;
                        groupNameLabel.Location = new Point((Width - groupNameLabel.Width) / 2, groupNameLabel.Location.Y);
                    }
                    comeFromLabel.Text = "来自:" + group.groupName;
                    comeFromLabel.Size = TextRenderer.MeasureText(comeFromLabel.Text, comeFromLabel.Font);
                    comeFromLabel.Location = new Point((Width - comeFromLabel.Width) / 2, comeFromLabel.Location.Y);
                    if (group.is_admin == 1)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }else if(group.is_menager==1 && (member.is_administrator == 1 || member.is_manager == 1))
                    {
                        comevarLabel1.Visible = false;
                    }else if (group.is_menager == 1 && member.is_administrator == 2 && member.is_manager == 2)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else
                    {
                        comevarLabel1.Visible = false;
                    }
                    sendMessaageButton.Visible = true;
                    sendMessaageButton.Location = new Point((Width - sendMessaageButton.Width) / 2, sendMessaageButton.Location.Y);
                    addFriendButton.Visible = false;
                }
                else
                {
                    nameLabel1.Text = string.IsNullOrEmpty(member.group_user_nickname) ? member.user_name : member.group_user_nickname + "(" + member.id_card + ")";
                    nameLabel1.Size = TextRenderer.MeasureText(nameLabel1.Text, nameLabel1.Font);
                    nameLabel1.Location = new Point((Width - nameLabel1.Width) / 2, nameLabel1.Location.Y);
                    if (string.IsNullOrEmpty(member.group_user_nickname))
                    {
                        groupNameLabel.Visible = false;
                    }
                    else
                    {
                        groupNameLabel.Text = "群昵称:" + member.group_user_nickname;
                        groupNameLabel.Visible = true;
                        groupNameLabel.Size = TextRenderer.MeasureText(groupNameLabel.Text, groupNameLabel.Font); ;
                        groupNameLabel.Location = new Point((Width - groupNameLabel.Width) / 2, groupNameLabel.Location.Y);
                    }
                    comeFromLabel.Text = "来自:" + group.groupName;
                    comeFromLabel.Size = TextRenderer.MeasureText(comeFromLabel.Text, comeFromLabel.Font);
                    comeFromLabel.Location = new Point((Width - comeFromLabel.Width) / 2, comeFromLabel.Location.Y);
                    if (group.is_admin == 1)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else if (group.is_menager == 1 && (member.is_administrator == 1 || member.is_manager == 1))
                    {
                        comevarLabel1.Visible = false;
                    }
                    else if (group.is_menager == 1 && member.is_administrator == 2 && member.is_manager == 2)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else
                    {
                        comevarLabel1.Visible = false;
                    }
                    sendMessaageButton.Visible = true;
                    sendMessaageButton.Location = new Point((Width - sendMessaageButton.Width) / 2, sendMessaageButton.Location.Y);
                    addFriendButton.Visible = true;
                    addFriendButton.Location = new Point((Width - addFriendButton.Width) / 2, addFriendButton.Location.Y);
                }
            }
            else
            {
                if (DBHelper.Instance.checkFriend(member.userID))
                {
                    nameLabel1.Text = string.IsNullOrEmpty(member.friend_name) ? (string.IsNullOrEmpty(member.group_user_nickname) ? member.user_name : member.group_user_nickname) : member.friend_name;
                    nameLabel1.Size = TextRenderer.MeasureText(nameLabel1.Text, nameLabel1.Font);
                    nameLabel1.Location = new Point((Width - nameLabel1.Width) / 2, nameLabel1.Location.Y);
                    if (string.IsNullOrEmpty(member.group_user_nickname))
                    {
                        groupNameLabel.Visible = false;
                    }
                    else
                    {
                        groupNameLabel.Text = "群昵称:" + member.group_user_nickname;
                        groupNameLabel.Visible = true;
                        groupNameLabel.Size = TextRenderer.MeasureText(groupNameLabel.Text, groupNameLabel.Font); ;
                        groupNameLabel.Location = new Point((Width - groupNameLabel.Width) / 2, groupNameLabel.Location.Y);
                    }
                    comeFromLabel.Text = "来自:" + group.groupName;
                    comeFromLabel.Size = TextRenderer.MeasureText(comeFromLabel.Text, comeFromLabel.Font);
                    comeFromLabel.Location = new Point((Width - comeFromLabel.Width) / 2, comeFromLabel.Location.Y);
                    if (group.is_admin == 1)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else if (group.is_menager == 1 && (member.is_administrator == 1 || member.is_manager == 1))
                    {
                        comevarLabel1.Visible = false;
                    }
                    else if (group.is_menager == 1 && member.is_administrator == 2 && member.is_manager == 2)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else
                    {
                        comevarLabel1.Visible = false;
                    }
                    sendMessaageButton.Visible = true;
                    sendMessaageButton.Location = new Point((Width - sendMessaageButton.Width) / 2, sendMessaageButton.Location.Y);
                    addFriendButton.Visible = false;
                }
                else
                {
                    nameLabel1.Text = string.IsNullOrEmpty(member.group_user_nickname) ? member.user_name : member.group_user_nickname + "(" + member.id_card + ")";
                    nameLabel1.Size = TextRenderer.MeasureText(nameLabel1.Text, nameLabel1.Font);
                    nameLabel1.Location = new Point((Width - nameLabel1.Width) / 2, nameLabel1.Location.Y);
                    if (string.IsNullOrEmpty(member.group_user_nickname))
                    {
                        groupNameLabel.Visible = false;
                    }
                    else
                    {
                        groupNameLabel.Text = "群昵称:" + member.group_user_nickname;
                        groupNameLabel.Visible = true;
                        groupNameLabel.Size = TextRenderer.MeasureText(groupNameLabel.Text, groupNameLabel.Font); ;
                        groupNameLabel.Location = new Point((Width - groupNameLabel.Width) / 2, groupNameLabel.Location.Y);
                    }
                    comeFromLabel.Text = "来自:" + group.groupName;
                    comeFromLabel.Size = TextRenderer.MeasureText(comeFromLabel.Text, comeFromLabel.Font);
                    comeFromLabel.Location = new Point((Width - comeFromLabel.Width) / 2, comeFromLabel.Location.Y);
                    if (group.is_admin == 1)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else if (group.is_menager == 1 && (member.is_administrator == 1 || member.is_manager == 1))
                    {
                        comevarLabel1.Visible = false;
                    }
                    else if (group.is_menager == 1 && member.is_administrator == 2 && member.is_manager == 2)
                    {
                        comevarLabel1.Visible = true;
                        comevarLabel1.Text = "通过\"" + member.inv_name + "\"进群";
                        comevarLabel1.Size = TextRenderer.MeasureText(comevarLabel1.Text, comevarLabel1.Font);
                        comevarLabel1.Location = new Point((Width - comevarLabel1.Width) / 2, comevarLabel1.Location.Y);
                    }
                    else
                    {
                        comevarLabel1.Visible = false;
                    }
                    if (group.is_admin == 1 || group.is_menager == 1 || member.is_manager == 1 || member.is_administrator == 1)
                    {
                        sendMessaageButton.Visible = true;
                        sendMessaageButton.Location = new Point((Width - sendMessaageButton.Width) / 2, sendMessaageButton.Location.Y);
                        addFriendButton.Visible = true;
                        addFriendButton.Location = new Point((Width - addFriendButton.Width) / 2, addFriendButton.Location.Y);
                    }
                    else
                    {
                        sendMessaageButton.Visible = false;
                        addFriendButton.Visible = false;
                    }
                }
            }
        }

        private void GroupUserInfoForm_Load(object sender, EventArgs e)
        {

        }

        private void addFriendButton_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            if (member == null)
            {
                return;
            }
            GetUserData data = new GetUserData();
            data.user_id = member.userID;
            data.portrait = member.avatar;
            data.user_name = member.user_name;
            data.id_card = member.id_card;
            AddFriendCheckForm form = new AddFriendCheckForm(data);
            form.Show();
            this.Close();
        }

        private void sendMessaageButton_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
            if (frm != null)
            {
                if (!frm.Visible)
                {
                    frm.Visible = true;
                }
                FriendModel f = new FriendModel();
                f.userID = member.userID;
                f.friend_self_name = member.user_name;
                frm.showPersonalChat(f);
                frm.BringToFront();
                this.Close();
            }
        }
    }
}
