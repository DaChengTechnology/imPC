using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;
using System.Threading;
using ChangLiao.DB;
using ChangLiao.Util;

namespace ChangLiao.windows
{
    public partial class MainForm : CCSkinMain
    {
        public MainForm()
        {
            InitializeComponent();
            MainFrom_SetupUI();
        }

        public MainForm(bool needLoad)
        {
            InitializeComponent();
            if (needLoad)
            {
                LoadData();
            }
            MainFrom_SetupUI();
        }

        private void LoadData()
        {
            HttpUitls.Instance.get<FriendListReciveModel>("contacts/friendList", new LoginedSendModel(),(json)=> {
                if(json.code == 200)
                {
                    DBHelper.Instance.AddFriend(json.data);
                    DBHelper.Instance.Save();
                }
                else
                {
                    if (json.message.Contains("重新登录"))
                    {
                        gotoLogin();
                    }
                }
            },(s)=> {
                if (s < 503 && s > 500)
                {
                    gotoLogin();
                }
            });
            HttpUitls.Instance.get<GetMyGroupReciveModel>("groupUser/getMyGroup", new LoginedSendModel(), (json) =>
             {
                 if(json.code == 200)
                 {
                     DBHelper.Instance.addGroupTemp(json.data);
                     DBHelper.Instance.Save();
                     foreach (var g in json.data)
                     {
                         GroupInfoSendModel m = new GroupInfoSendModel();
                         m.group_id = g.group_id;
                         HttpUitls.Instance.get<GroupInfoReciveModel>("group/detail", m, (js) => {
                            if(js.code == 200)
                             {
                                 DBHelper.Instance.addGroupAndFocus(js.data);
                             }
                             else
                             {
                                 if (js.message.Contains("重新登录"))
                                 {
                                     gotoLogin();
                                 }
                             }
                         }, (s) => {
                             if (s < 503 && s > 500)
                             {
                                 gotoLogin();
                             }
                         });
                         ThreadPool.QueueUserWorkItem(getGroupMembers, g.group_id);
                    }
                 }
                 else
                 {
                     if (json.message.Contains("重新登录"))
                     {
                         gotoLogin();
                     }
                 }
             }, (s) =>
             {
                 if (s < 503 && s > 500)
                 {
                     gotoLogin();
                 }
             });
        }

        private void gotoLogin()
        {
            this.BeginInvoke(new EventHandler((s, err) =>
            {
                this.Close();
            }));
            new Thread(new ThreadStart(() =>
            {
                Application.Run(new LoginForm());
            })).Start();
        }

        private void getGroupMembers(Object state)
        {
            string group_Id = (string)state;
            GroupInfoSendModel model = new GroupInfoSendModel();
            model.group_id = group_Id;
            HttpUitls.Instance.get<GroupMembersReciveModel>("groupUser/groupUserList", model, (json) => {
                if (json.code == 200)
                {
                    DBHelper.Instance.addGroupMember(json.data);
                }
                else
                {
                    if (json.message.Contains("重新登录"))
                    {
                        gotoLogin();
                    }
                }
            }, (s) => {
                if (s < 503 && s > 500)
                {
                    gotoLogin();
                }
            });
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            panel1.Height = Height;
            conversationPanl1.Height = Height;
        }

        private void MainFrom_SetupUI()
        {
            conversationTabButton.BackgroundImage = Properties.Resources.converstation_select;
            contractButton.BackgroundImage = Properties.Resources.contract_normal;
            settingTabButton.BackgroundImage = Properties.Resources.setting_normal;
            HeaderPicture.SizeMode = PictureBoxSizeMode.CenterImage;
            DCWebImageMaanager.shard.downloadImageAsync(SettingMenager.shard.avatar, (image, b) =>
            {
                if(image != null)
                {
                    HeaderPicture.BeginInvoke(new EventHandler((s, e) =>
                    {
                        HeaderPicture.Image = image;
                    }));
                }
            });
            HeaderPicture.Image = Properties.Resources.moren;
        }

        private void conversationTabButton_Click(object sender, EventArgs e)
        {
            conversationTabButton.BackgroundImage = Properties.Resources.converstation_select;
            contractButton.BackgroundImage = Properties.Resources.contract_normal;
            settingTabButton.BackgroundImage = Properties.Resources.setting_normal;
            conversationPanl1.Visible = true;
        }

        private void contractButton_Click(object sender, EventArgs e)
        {
            conversationTabButton.BackgroundImage = Properties.Resources.conversation_normal;
            contractButton.BackgroundImage = Properties.Resources.contract_select;
            settingTabButton.BackgroundImage = Properties.Resources.setting_normal;
            conversationPanl1.Visible = false;
        }

        private void settingTabButton_Click(object sender, EventArgs e)
        {
            conversationTabButton.BackgroundImage = Properties.Resources.conversation_normal;
            contractButton.BackgroundImage = Properties.Resources.contract_normal;
            settingTabButton.BackgroundImage = Properties.Resources.setting_select;
            conversationPanl1.Visible = false;
        }

        public void refreshConversationList()
        {
            conversationPanl1.reFlash();
        }
    }
}
