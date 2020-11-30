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
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Ease;
using ChangLiao.DB;
using EaseMobLib;
using ChangLiao.Util;
using System.Threading;

namespace ChangLiao.windows
{
    public partial class NewGroupNameForm : DSkinForm
    {
        public NewGroupNameForm()
        {
            InitializeComponent();
        }

        private void dSkinRadioButton1_Click(object sender, EventArgs e)
        {
            dSkinRadioButton1.Checked = true;
            dSkinRadioButton2.Checked = false;
        }

        private void dSkinRadioButton2_Click(object sender, EventArgs e)
        {
            dSkinRadioButton1.Checked = false;
            dSkinRadioButton2.Checked = true;
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dSkinTextBox1.Text))
            {
                MessageBox.Show("请输入群名称");
                return;
            }
            NewGroupForm form = new NewGroupForm();
            form.oked += Form_oked;
            form.canceled += Form_canceled;
            form.ShowDialog();
        }

        private void Form_canceled()
        {
            this.Close();
        }

        private void Form_oked(string obj)
        {
            int type = 0;
            if (dSkinRadioButton1.Checked)
            {
                type = 2;
            }else if (dSkinRadioButton2.Checked)
            {
                type = 1;
            }
            CreateGroupSendModel model = new CreateGroupSendModel();
            model.group_name = dSkinTextBox1.Text;
            model.group_portrait = "pc_defalt_group_photo.jpg";
            model.group_type = type;
            HttpUitls.Instance.get<CreateGroupReciveModel>("group/create", model, (json) =>
              {
                  if (json.code == 200)
                  {
                      EaseHelper.shard.createdGroupId = json.data;
                      AddGroupUserSendModel addGroup = new AddGroupUserSendModel();
                      addGroup.group_id = json.data;
                      addGroup.group_user_ids = obj;
                      HttpUitls.Instance.get<BaseReciveModel>("groupUser/addBatch", addGroup, (js) =>
                        {
                            if (js.code == 200)
                            {
                                GroupInfoSendModel m = new GroupInfoSendModel();
                                m.group_id = json.data;
                                HttpUitls.Instance.get<GroupInfoReciveModel>("group/detail", m, (jso) =>
                                {
                                    if (js.code == 200)
                                    {
                                        EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(json.data, EMConversationType.GROUPCHAT, true);
                                        EMTextMessageBody body = new EMTextMessageBody("你创建了群聊");
                                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, json.data, body, EMChatType.GROUP);
                                        conversation.insertMessage(message);
                                        DBHelper.Instance.addGroupAndFocus(jso.data);
                                        ThreadPool.QueueUserWorkItem(getGroupMembers, json.data);
                                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                                        if (main != null)
                                        {
                                            main.refreshConversationList();
                                        }
                                        this.BeginInvoke(new EventHandler((s, e) =>
                                        {
                                            this.Close();
                                        }));
                                    }
                                    else
                                    {
                                        if (js.message.Contains("重新登录"))
                                        {
                                            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                                            if (main != null)
                                            {
                                                main.gotoLogin();
                                            }
                                        }
                                    }
                                }, (s) =>
                                {
                                    if (s < 503 && s > 500)
                                    {
                                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                                        if (main != null)
                                        {
                                            main.gotoLogin();
                                        }
                                    }
                                });
                            }
                            else
                            {
                                if (js.message.Contains("重新登录"))
                                {
                                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                                    if (main != null)
                                    {
                                        main.gotoLogin();
                                    }
                                }
                            }
                        }, (code) =>
                       {
                           if (code < 503 && code > 500)
                           {
                               MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                               if (main != null)
                               {
                                   main.gotoLogin();
                               }
                           }
                       });
                  }
                  else
                  {
                      if (json.message.Contains("重新登录"))
                      {
                          MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                          if (main != null)
                          {
                              main.gotoLogin();
                          }
                      }
                  }
              }, (code) =>
             {
                 if (code < 503 && code > 500)
                 {
                     MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                     if (main != null)
                     {
                         main.gotoLogin();
                     }
                 }
             });
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
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.gotoLogin();
                    }
                }
            }, (s) => {
                if (s < 503 && s > 500)
                {
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.gotoLogin();
                    }
                }
            });
        }
    }
}
