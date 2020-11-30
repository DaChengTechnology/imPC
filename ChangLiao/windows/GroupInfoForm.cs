using System;
using System.Collections.Generic;
using ChangLiao.Ease;
using EaseMobLib;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;
using ChangLiao.DB;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Model.SendModel;
using System.Threading;
using DSkin.Controls;
using DSkin.DirectUI;
using ChangLiao.Util;
using ChangLiao.Model.ViewModel;
using ChangLiao.Temple;

namespace ChangLiao.windows
{
    public partial class GroupInfoForm : DSkinForm
    {
        private GroupTable model;
        private int rightIndex;
        private List<GroupMemberViewModel> menbers;
        private GroupUser mine;
        public GroupInfoForm()
        {
            rightIndex = -1;
            InitializeComponent();
        }

        public GroupInfoForm(GroupTable table)
        {
            GC.AddMemoryPressure(100 * 1024);
            model = table;
            rightIndex = -1;
            InitializeComponent();
            if (model != null)
            {
                
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    var dbGroupMembers = DBHelper.Instance.getGroupAllUser(model.groupId);
                    for (int i = 0; i < dbGroupMembers.Count; i++)
                    {
                        if (dbGroupMembers[i].is_manager == 1)
                        {
                            var data = dbGroupMembers[i];
                            dbGroupMembers.RemoveAt(i);
                            dbGroupMembers.Insert(0, data);
                        }
                    }
                    for (int i = 0; i < dbGroupMembers.Count; i++)
                    {
                        if (dbGroupMembers[i].is_administrator == 1)
                        {
                            var data = dbGroupMembers[i];
                            dbGroupMembers.RemoveAt(i);
                            dbGroupMembers.Insert(0, data);
                            break;
                        }
                    }
                    duiGridList1.BeginInvoke(() =>
                    {
                        this.menbers = new List<GroupMemberViewModel>();
                        foreach (GroupUser item in dbGroupMembers)
                        {
                            this.menbers.Add(new GroupMemberViewModel(item));
                        }
                        if (duiGridList1.Loaded)
                        {
                            loadData();
                        }
                    });
                });
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    refreshGroupMember();
                });
            }
        }

        private void loadData()
        {
            if (menbers == null)
            {
                return;
            }
            InitGridList();
            duiGridList1.Rows.Clear();
            foreach (var item in menbers)
            {
                if (model.is_admin == 1 || model.is_menager == 1)
                {
                    var row = new DuiGridListRow(item);
                    row.Cells.Add(new DuiGridListCell { Text = item.NickName });
                    row.Cells.Add(new DuiGridListCell { Text = item.GroupNickName });
                    row.Cells.Add(new DuiGridListCell { Text = item.IDCard });
                    row.Cells.Add(new DuiGridListCell { Text = item.Leave });
                    duiGridList1.Rows.Add(row);
                }
                else
                {
                    var row = new DuiGridListRow(item);
                    row.Cells.Add(new DuiGridListCell { Text = item.NickName });
                    row.Cells.Add(new DuiGridListCell { Text = item.GroupNickName });
                    row.Cells.Add(new DuiGridListCell { Text = item.Leave });
                    duiGridList1.Rows.Add(row);
                }
            }
        }

        private void refreshGroupMember()
        {
            GroupInfoSendModel model = new GroupInfoSendModel();
            model.group_id = model.group_id;
            HttpUitls.Instance.get<GroupMembersReciveModel>("groupUser/groupUserList", model, (json) =>
            {
                if (json.code == 200)
                {
                    for (int i = 0; i < json.data.Count; i++)
                    {
                        if (json.data[i].is_manager == 1)
                        {
                            var data = json.data[i];
                            json.data.RemoveAt(i);
                            json.data.Insert(0, data);
                        }
                    }
                    for (int i = 0; i < json.data.Count; i++)
                    {
                        if (json.data[i].is_administrator == 1)
                        {
                            var data = json.data[i];
                            json.data.RemoveAt(i);
                            json.data.Insert(0, data);
                            break;
                        }
                    }
                    duiGridList1.BeginInvoke(() =>
                    {
                        this.menbers = new List<GroupMemberViewModel>();
                        foreach (GroupMemberData item in json.data)
                        {
                            this.menbers.Add(new GroupMemberViewModel(item));
                        }
                        if (duiGridList1.Loaded)
                        {
                            loadData();
                        }
                    });
                    DBHelper.Instance.addGroupMember(json.data);
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

        private void GroupInfoForm_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                loadInfo();
                if (menbers != null)
                {
                    loadData();
                }
            }
        }

        private void loadInfo()
        {
            if (model != null)
            {
                if (this.Visible)
                {
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        if (model.is_admin == 1 || model.is_menager == 1)
                        {
                            this.SystemButtons.Clear();
                            this.SystemButtons.Add(systemButton1);
                        }
                        else
                        {
                            this.SystemButtons.Clear();
                        }
                    }));
                }
                else
                {
                    if (model.is_admin == 1 || model.is_menager == 1)
                    {
                        this.SystemButtons.Clear();
                        this.SystemButtons.Add(systemButton1);
                    }
                    else
                    {
                        this.SystemButtons.Clear();
                    }
                }
                DCWebImageMaanager.shard.downloadImageAsync(model.avatar, (image, b) =>
                {
                    if (image != null)
                    {
                        duiPictureBox1.BeginInvoke(() =>
                        {
                            duiPictureBox1.Image = image;
                        });
                    }
                });
                duiLabel1.BeginInvoke(() =>
                {
                    duiLabel1.Text = model.groupName;
                });
                duiLabel2.BeginInvoke(() =>
                {
                    duiLabel2.Text = model.groupUserSum + "人";
                });
                duiLabel3.BeginInvoke(() =>
                {
                    duiLabel3.Text = model.notice.ToString();
                });
            }
        }

        private void InitGridList()
        {
            duiGridList1.BeginInvoke(() =>
            {
                if (duiGridList1.Columns.Count == 0)
                {
                    if (model.is_admin == 1 || model.is_menager == 1)
                    {
                        DuiGridListColumn nickName = new DuiGridListColumn();
                        nickName.Name = "昵称";
                        nickName.Width = 220;
                        nickName.DataPropertyName = "NickName";
                        duiGridList1.Columns.Add(nickName);
                        DuiGridListColumn groupNickName = new DuiGridListColumn();
                        groupNickName.Name = "群昵称";
                        groupNickName.DataPropertyName = "GroupNickName";
                        groupNickName.Width = 220;
                        duiGridList1.Columns.Add(groupNickName);
                        DuiGridListColumn idCard = new DuiGridListColumn();
                        idCard.Name = "畅聊号";
                        idCard.DataPropertyName = "IDCard";
                        idCard.Width = 220;
                        duiGridList1.Columns.Add(idCard);
                        DuiGridListColumn leave = new DuiGridListColumn();
                        leave.Name = "身份";
                        leave.DataPropertyName = "Leave";
                        leave.Width = 80;
                        duiGridList1.Columns.Add(leave);
                    }
                    else
                    {
                        DuiGridListColumn nickName = new DuiGridListColumn();
                        nickName.Name = "昵称";
                        nickName.DataPropertyName = "NickName";
                        nickName.Width = 330;
                        //nickName.CellTemplate = typeof(GroupMemberGridListCell);
                        duiGridList1.Columns.Add(nickName);
                        DuiGridListColumn groupNickName = new DuiGridListColumn();
                        groupNickName.Name = "群昵称";
                        groupNickName.DataPropertyName = "GroupNickName";
                        groupNickName.Width = 330;
                        //groupNickName.CellTemplate = typeof(GroupMemberGridListCell);
                        duiGridList1.Columns.Add(groupNickName);
                        DuiGridListColumn leave = new DuiGridListColumn();
                        leave.Name = "身份";
                        leave.DataPropertyName = "Leave";
                        leave.Width = 80;
                        //leave.CellTemplate = typeof(GroupMemberGridListCell);
                        duiGridList1.Columns.Add(leave);
                    }
                }
            });
        }

        private void duiGridList1_ItemDoubleClick(object sender, DuiGridListMouseEventArgs e)
        {
            var d = duiGridList1.SelectedItem.RowData as GroupMemberViewModel;
            GroupUserInfoForm form = new GroupUserInfoForm(model, d.user);
            form.Show();
        }

        private void duiGridList1_ItemClick(object sender, DuiGridListMouseEventArgs e)
        {

        }

        private void shelidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightIndex == -1)
            {
                return;
            }
            if (menbers != null)
            {
                var member = menbers[rightIndex];
                GroupShieldSingleSendModel m = new GroupShieldSingleSendModel();
                m.group_id = member.user.groupID;
                m.groupUserId = member.user.userID;
                HttpUitls.Instance.get<BaseReciveModel>("groupUser/setShieldSingle", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        this.menbers[rightIndex].user.is_shield = 1;
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            refreshGroupMember();
                        });
                        loadData();
                        EMCmdMessageBody body = new EMCmdMessageBody("");
                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, member.user.userID, body, EMChatType.SINGLE);
                        message.setAttribute("type", "qun_shield");
                        message.setAttribute("id", member.user.groupID);
                        message.setAttribute("userid", member.user.userID);
                        message.setAttribute("qun_shield", "1");
                        EaseHelper.shard.client.getChatManager().sendMessage(message);
                        this.BeginInvoke(new EventHandler((ss, ee) =>
                        {
                            MessageBox.Show("禁言成功");
                        }));
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((ss, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
                        }
                    }
                }, (code) =>
                {
                    if (code > 500 && code < 503)
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                });
            }
            skinContextMenuStrip1.Items.Clear();
        }

        private void tickOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightIndex == -1)
            {
                return;
            }
            if (menbers != null)
            {
                var member = menbers[rightIndex];
                AddGroupUserSendModel m = new AddGroupUserSendModel();
                m.group_id = member.user.groupID;
                m.group_user_ids = member.user.userID;
                HttpUitls.Instance.get<BaseReciveModel>("groupUser/removeBatch", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        this.menbers.Remove(member);
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            refreshGroupMember();
                        });
                        loadData();
                        EMCmdMessageBody body = new EMCmdMessageBody("");
                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, member.user.userID, body, EMChatType.SINGLE);
                        message.setAttribute("type", "qun");
                        message.setAttribute("id", member.user.groupID);
                        EaseHelper.shard.client.getChatManager().sendMessage(message);
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show("踢人成功");
                        }));
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((ss, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
                        }
                    }
                }, (code) =>
                {
                    if (code > 500 && code < 503)
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                });
            }
            skinContextMenuStrip1.Items.Clear();
        }

        private void unShelidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightIndex == -1)
            {
                return;
            }
            if (menbers != null)
            {
                var member = menbers[rightIndex];
                GroupShieldSingleSendModel m = new GroupShieldSingleSendModel();
                m.group_id = member.user.groupID;
                m.groupUserId = member.user.userID;
                HttpUitls.Instance.get<BaseReciveModel>("groupUser/cancelShieldSingle", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        this.menbers[rightIndex].user.is_shield = 1;
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            refreshGroupMember();
                        });
                        loadData();
                        EMCmdMessageBody body = new EMCmdMessageBody("");
                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, member.user.userID, body, EMChatType.SINGLE);
                        message.setAttribute("type", "qun_shield");
                        message.setAttribute("id", member.user.groupID);
                        message.setAttribute("userid", member.user.userID);
                        message.setAttribute("qun_shield", "2");
                        EaseHelper.shard.client.getChatManager().sendMessage(message);
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show("取消禁言成功");
                        }));
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((ss, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
                        }
                    }
                }, (code) =>
                {
                    if (code > 500 && code < 503)
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                });
            }
            skinContextMenuStrip1.Items.Clear();
        }

        private void setManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightIndex == -1)
            {
                return;
            }
            if (menbers != null)
            {
                var member = menbers[rightIndex];
                AddManagerSendModel m = new AddManagerSendModel();
                m.group_id = member.user.groupID;
                m.newManager_id = member.user.userID;
                HttpUitls.Instance.get<BaseReciveModel>("groupUser/addManager", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        this.menbers[rightIndex].user.is_manager = 1;
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            refreshGroupMember();
                        });
                        loadData();
                        EMCmdMessageBody body = new EMCmdMessageBody("");
                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, member.user.userID, body, EMChatType.SINGLE);
                        message.setAttribute("type", "qun");
                        message.setAttribute("id", member.user.groupID);
                        message.setAttribute("qun_auth", "qun_auth");
                        message.setAttribute("auth", "1");
                        EaseHelper.shard.client.getChatManager().sendMessage(message);
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((ss, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
                        }
                    }
                }, (code) =>
                {
                    if (code > 500 && code < 503)
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

        private void cancelManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightIndex == -1)
            {
                return;
            }
            if (menbers != null)
            {
                var member = menbers[rightIndex];
                DeleteManagerSendModel m = new DeleteManagerSendModel();
                m.group_id = member.user.groupID;
                m.oldManager_id = member.user.userID;
                HttpUitls.Instance.get<BaseReciveModel>("groupUser/addManager", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        this.menbers[rightIndex].user.is_manager = 1;
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            refreshGroupMember();
                        });
                        loadData();
                        EMCmdMessageBody body = new EMCmdMessageBody("");
                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, member.user.userID, body, EMChatType.SINGLE);
                        message.setAttribute("type", "qun");
                        message.setAttribute("id", member.user.groupID);
                        message.setAttribute("qun_auth", "qun_auth");
                        message.setAttribute("auth", "2");
                        EaseHelper.shard.client.getChatManager().sendMessage(message);
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((ss, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
                        }
                    }
                }, (code) =>
                {
                    if (code > 500 && code < 503)
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                });
            }
            skinContextMenuStrip1.Items.Clear();
        }

        private void skinContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void duiGridList1_MouseClick(object sender, DuiMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (model.is_admin == 2 && model.is_menager == 2)
                {
                    return;
                }
                skinContextMenuStrip1.Items.Clear();
                if (duiGridList1.SelectedItem == null)
                {
                    skinContextMenuStrip1.Items.Add(邀请群成员ToolStripMenuItem);
                    skinContextMenuStrip1.Show(new Point(e.X + duiGridList1.LocationToScreen.X, e.Y + duiGridList1.LocationToScreen.Y));
                    return;
                }
                var data = duiGridList1.SelectedItem.RowData as GroupMemberViewModel;
                rightIndex = menbers.IndexOf(data);
                int viewIndex = duiGridList1.VisibleControls.IndexOf(duiGridList1.SelectedItem.RowControl);
                if (model.is_menager == 1)
                {
                    var member = data.user;
                    if (member.is_administrator == 1 || member.is_manager == 1)
                    {
                        return;
                    }
                    if (member.is_shield == 1)
                    {
                        skinContextMenuStrip1.Items.Add(unShelidToolStripMenuItem);
                    }
                    else
                    {
                        skinContextMenuStrip1.Items.Add(shelidToolStripMenuItem);
                    }
                    skinContextMenuStrip1.Items.Add(tickOutToolStripMenuItem);
                    skinContextMenuStrip1.Items.Add(邀请群成员ToolStripMenuItem);
                    skinContextMenuStrip1.Show(new Point(e.X + duiGridList1.LocationToScreen.X, e.Y + duiGridList1.LocationToScreen.Y));
                }
                else if (model.is_admin == 1)
                {
                    var me = data.user;
                    if (me.is_shield == 1)
                    {
                        skinContextMenuStrip1.Items.Add(unShelidToolStripMenuItem);
                    }
                    else
                    {
                        skinContextMenuStrip1.Items.Add(shelidToolStripMenuItem);
                    }
                    skinContextMenuStrip1.Items.Add(tickOutToolStripMenuItem);
                    skinContextMenuStrip1.Items.Add(邀请群成员ToolStripMenuItem);
                    skinContextMenuStrip1.Show(new Point(e.X + duiGridList1.LocationToScreen.X, e.Y + duiGridList1.LocationToScreen.Y));
                }
            }
        }

        private void duiGridList1_IsSelectedChanged(object sender, EventArgs e)
        {

        }

        private void 邀请群成员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGroupForm form = new NewGroupForm(model.groupId);
            form.oked += Form_oked;
            form.Show();
        }

        private void Form_oked(string obj)
        {
            AddGroupUserSendModel addGroup = new AddGroupUserSendModel();
            addGroup.group_id = model.groupId;
            addGroup.group_user_ids = obj;
            HttpUitls.Instance.get<BaseReciveModel>("groupUser/addBatch", addGroup, (js) =>
            {
                if (js.code == 200)
                {
                    GroupInfoSendModel m = new GroupInfoSendModel();
                    m.group_id = model.groupId;
                    HttpUitls.Instance.get<GroupInfoReciveModel>("group/detail", m, (jso) =>
                    {
                        if (js.code == 200)
                        {
                            DBHelper.Instance.addGroupAndFocus(jso.data);
                            model = DBHelper.Instance.GetGroup(model.groupId);
                            loadInfo();
                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                refreshGroupMember();
                            });
                            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                            if (main != null)
                            {
                                main.refreshConversationList();
                            }
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

        private void GroupInfoForm_SystemButtonMouseClick(object sender, SystemButtonMouseClickEventArgs e)
        {
            moreContextMenuStrip1.Items.Clear();
            if (model.is_all_banned == 1)
            {
                moreContextMenuStrip1.Items.Add(取消全员禁言ToolStripMenuItem);
            }
            else
            {
                moreContextMenuStrip1.Items.Add(全员禁言ToolStripMenuItem);
            }
            if (model.is_admin == 1)
            {
                moreContextMenuStrip1.Items.Add(解散该群ToolStripMenuItem);
            }
            else
            {
                moreContextMenuStrip1.Items.Add(退出该群ToolStripMenuItem);
            }
            moreContextMenuStrip1.Show(Control.MousePosition);
        }

        private void 全员禁言ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupInfoSendModel m = new GroupInfoSendModel();
            m.group_id = model.groupId;
            HttpUitls.Instance.get<BaseReciveModel>("group/setAllBanned", m, (json) =>
            {
                if (json.code == 200)
                {
                    model.is_all_banned = 1;
                    loadInfo();
                    EMCmdMessageBody body = new EMCmdMessageBody("");
                    EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, model.groupId, body, EMChatType.GROUP);
                    message.setAttribute("type", "qun");
                    message.setAttribute("id", model.groupId);
                    message.setAttribute("grouptype", "1");
                    EaseHelper.shard.client.getChatManager().sendMessage(message);
                    HttpUitls.Instance.get<GroupInfoReciveModel>("group/detail", m, (jso) =>
                    {
                        if (jso.code == 200)
                        {
                            DBHelper.Instance.addGroupAndFocus(jso.data);
                            model = DBHelper.Instance.GetGroup(model.groupId);
                            loadInfo();
                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                refreshGroupMember();
                            });
                            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                            if (main != null)
                            {
                                main.refreshConversationList();
                            }
                        }
                        else
                        {
                            if (jso.message.Contains("重新登录"))
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
                    if (json.message.Contains("重新登录"))
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                    else
                    {
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show(json.message);
                        }));
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

        private void 取消全员禁言ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupInfoSendModel m = new GroupInfoSendModel();
            m.group_id = model.groupId;
            HttpUitls.Instance.get<BaseReciveModel>("group/cancelAllBanned", m, (json) =>
            {
                if (json.code == 200)
                {
                    model.is_all_banned = 2;
                    loadInfo();
                    EMCmdMessageBody body = new EMCmdMessageBody("");
                    EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, model.groupId, body, EMChatType.GROUP);
                    message.setAttribute("type", "qun");
                    message.setAttribute("id", model.groupId);
                    message.setAttribute("grouptype", "2");
                    EaseHelper.shard.client.getChatManager().sendMessage(message);
                    HttpUitls.Instance.get<GroupInfoReciveModel>("group/detail", m, (jso) =>
                    {
                        if (jso.code == 200)
                        {
                            DBHelper.Instance.addGroupAndFocus(jso.data);
                            model = DBHelper.Instance.GetGroup(model.groupId);
                            loadInfo();
                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                refreshGroupMember();
                            });
                            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                            if (main != null)
                            {
                                main.refreshConversationList();
                                main.loadGroup();
                            }
                        }
                        else
                        {
                            if (jso.message.Contains("重新登录"))
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
                    if (json.message.Contains("重新登录"))
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                    else
                    {
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show(json.message);
                        }));
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

        private void 退出该群ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出该群吗?", null, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GroupInfoSendModel m = new GroupInfoSendModel();
                m.group_id = model.groupId;
                HttpUitls.Instance.get<BaseReciveModel>("group/exit", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        DBHelper.Instance.DeleteGroup(model.groupId);
                        EaseHelper.shard.client.getChatManager().removeConversation(model.groupId, true);
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main == null)
                        {
                            return;
                        }
                        main.removeConversation(model.groupId);
                        main.loadGroup();
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show("退出成功");
                            this.Close();
                        }));
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((s, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
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
        }

        private void 解散该群ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要解散该群吗?", null, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GroupInfoSendModel m = new GroupInfoSendModel();
                m.group_id = model.groupId;
                HttpUitls.Instance.get<BaseReciveModel>("group/delete", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        DBHelper.Instance.DeleteGroup(model.groupId);
                        EaseHelper.shard.client.getChatManager().removeConversation(model.groupId, true);
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main == null)
                        {
                            return;
                        }
                        main.removeConversation(model.groupId);
                        main.loadGroup();
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show("解散成功");
                            this.Close();
                        }));
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
                        else
                        {
                            this.BeginInvoke(new EventHandler((s, ee) =>
                            {
                                MessageBox.Show(json.message);
                            }));
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
        }
    }
}
