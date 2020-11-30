using System;
using System.Collections.Generic;
using System.ComponentModel;
using DSkin.Controls;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChangLiao.Ease;
using DSkin.Forms;
using EaseMobLib;
using ChangLiao.Temple;
using ChangLiao.Util;
using System.Threading;
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;
using ChangLiao.DB;
using ChangLiao.ChildView;
using ChangLiao.Model.ViewModel;

namespace ChangLiao.windows
{
    public partial class MainFrm : DSkinForm
    {
        private BackgroundWorker conversationRefreshQueue;
        private bool needRefreshConversation;
        private List<EMConversation> conversations;
        private bool needLoadData;
        private BackgroundWorker loadDataQueue;
        private int conversationSelectIndex;
        private List<ChatPanl> openConversation;
        [DefaultValue(1)]
        private int contractOrGroup;
        private BackgroundQueue loadContractOrGroupQueue;
        private List<FriendModel> friends;
        private List<GroupTable> groups;
        private DSkinUserControl contractInfoPanl;
        private int contractRightIndex;
        private int addFriendCount = 0;
        private SearchResultBox resultBox;
        private bool showClearContentMenu = false;
        private string deleteFriendID;
        private bool nflag = true;
        private Icon clear;
        public MainFrm()
        {
            InitializeComponent();
            needLoadData = false;
            conversationRefreshQueue = new BackgroundWorker();
            conversationRefreshQueue.DoWork += ConversationRefreshQueue_DoWork;
            conversationRefreshQueue.RunWorkerCompleted += ConversationRefreshQueue_RunWorkerCompleted;
            needRefreshConversation = false;
            loadDataQueue = new BackgroundWorker();
            loadDataQueue.DoWork += LoadData;
            conversationSelectIndex = -1;
            loadContractOrGroupQueue = new BackgroundQueue();
        }

        public MainFrm(bool loadData)
        {
            needLoadData = loadData;
            InitializeComponent();
            conversationRefreshQueue = new BackgroundWorker();
            conversationRefreshQueue.DoWork += ConversationRefreshQueue_DoWork;
            needRefreshConversation = false;
            loadDataQueue = new BackgroundWorker();
            loadDataQueue.DoWork += LoadData;
            conversationSelectIndex = -1;
            loadContractOrGroupQueue = new BackgroundQueue();
        }
        protected override void OnLoad(EventArgs e)
        {
            openConversation = new List<ChatPanl>();
            base.OnLoad(e);
            if (needLoadData)
            {
                loadDataQueue.RunWorkerAsync();
            }
            contractOrGroup = 1;
            dSkinButton2.Visible = false;
            CLnotifyIcon.Visible = true;
            CLnotifyIcon.Text = SettingMenager.shard.username + "(" + SettingMenager.shard.idCard + ")";
            if (!conversationRefreshQueue.IsBusy)
            {
                conversationRefreshQueue.RunWorkerAsync();
            }
            try
            {
                clear = Icon.FromHandle(Properties.Resources.clear.GetHicon());
            }
            catch { }
            titleLabel.Visible = false;
            DCWebImageMaanager.shard.downloadImageAsync(SettingMenager.shard.avatar, (image, b) =>
            {
                if (image != null)
                {
                    userHeadBtn.BeginInvoke(() =>
                    {
                        userHeadBtn.BackgroundImage = image;
                        userHeadBtn.Invalidate();
                    });
                    var ico = DCUtilTool.ConvertToIcon(image);
                    if (ico != null)
                    {
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            this.Icon = ico;
                        }));
                    }
                }
            });
            contractRightIndex = -1;
            versionLabel.Text = "V" + Properties.Settings.Default.version;
        }
        #region 回话列表刷新
        private void ConversationRefreshQueue_DoWork(object sender, DoWorkEventArgs e)
        {
            conversations = EaseHelper.shard.getAllConversation();
            for (int i = conversations.Count - 1; i > 0; i--)
            {
                if (conversations[i].latestMessage() == null)
                {
                    conversations.RemoveAt(i);
                }
                else if (conversations[i].conversationId() == SettingMenager.shard.userID)
                {
                    conversations.RemoveAt(i);
                }
            }
            conversations = conversations.OrderByDescending(p => p.latestMessage().timestamp()).ToList();
            int unread = 0;
            List<ConversationModel> models = new List<ConversationModel>();
            foreach (var conversaton in conversations)
            {
                unread += conversaton.unreadMessagesCount();
                var c = new ConversationModel(conversaton);
                models.Add(c);
            }
            conversationListBox.Invoke(new EventHandler((s, ee) => {
                List<ConversationItem> items = new List<ConversationItem>();
                foreach (ConversationModel item in models)
                {
                    ConversationItem c = new ConversationItem(item);
                    foreach (var i in openConversation)
                    {
                        if (c.model.conversation.conversationId() == i.conversation.conversationId() && i.topLeave)
                        {
                            c.IsSelected = true;
                        }
                    }
                    items.Add(c);
                }
                conversationListBox.Items.Clear();
                conversationListBox.Items.AddRange(items);
            }));
            dSkinButton1.Invoke(new EventHandler((s, ee) =>
            {
                if (unread > 0)
                {
                    dSkinButton1.Visible = true;
                    if (unread > 99)
                    {
                        dSkinButton1.Text = "99+";
                        dSkinButton1.Size = new Size(TextRenderer.MeasureText(dSkinButton1.Text, dSkinButton1.Font).Width + 2, 24);
                    }
                    else
                    {
                        dSkinButton1.Text = unread.ToString();
                        dSkinButton1.Size = new Size(24, 24);
                    }
                }
                else
                {
                    dSkinButton1.Visible = false;
                }
                if(!Visible && conversations.Count > 0)
                {
                    if (conversations[0].unreadMessagesCount() > 0)
                    {
                        timer1.Start();
                    }
                }
            }));
            Thread.Sleep(800);
        }
        public void refreshConversationList()
        {
            if (conversationRefreshQueue.IsBusy)
            {
                needRefreshConversation = true;
            }
            else
            {
                conversationRefreshQueue.RunWorkerAsync();
            }
        }

        private void ConversationRefreshQueue_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (conversationSelectIndex>-1&&conversationSelectIndex<conversationListBox.Items.Count)
            {
                conversationListBox.BeginInvoke(new EventHandler((s, ee) =>
                {
                    conversationListBox.SelectedItem = conversationListBox.Items[conversationSelectIndex];
                }));
            }
            if (needRefreshConversation)
            {
                needRefreshConversation = false;
                conversationRefreshQueue.RunWorkerAsync();
            }
        }
        #endregion

        private void dSkinTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }
        #region 用户数据拉取
        private void LoadData(object sender, DoWorkEventArgs e)
        {
            loadFriend();
            loadGroup();
            refreshFriendFromServer();
            loadAddFriend();
            HttpUitls.Instance.get<GetMyGroupReciveModel>("groupUser/getMyGroup", new LoginedSendModel(), (json) =>
            {
                if (json.code == 200)
                {
                    DBHelper.Instance.addGroupTemp(json.data);
                    DBHelper.Instance.Save();
                    foreach (var g in json.data)
                    {
                        ThreadPool.QueueUserWorkItem(refreshGroupInfo, g.group_id);
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

        public void refreshGroupInfo(object groupid)
        {
            GroupInfoSendModel m = new GroupInfoSendModel();
            m.group_id = (string)groupid;
            HttpUitls.Instance.get<GroupInfoReciveModel>("group/detail", m, (js) =>
            {
                if (js.code == 200)
                {
                    DBHelper.Instance.addGroupAndFocus(js.data);
                    loadGroup();
                    foreach (var item in openConversation)
                    {
                        if (item.conversation.conversationId() == (string)groupid)
                        {
                            item.updateGroup();
                        }
                    }
                }
                else
                {
                    if (js.message.Contains("重新登录"))
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
            //getGroupMembers(groupid);
        }

        public void loadAddFriend()
        {
            HttpUitls.Instance.get<InviteFriendReciveModel>("contacts/inviteList", new LoginedSendModel(), (json) =>
              {
                  if (json.code == 200)
                  {
                      updateAddFriendCount(json.data.Count);
                      this.BeginInvoke(new EventHandler((s, e) =>
                      {
                          if (mainTabControl.SelectedIndex == 1)
                          {
                              var panl = contractInfoPanl as NewFriendPanl;
                              if (panl != null)
                              {
                                  panl.loadaData();
                              }
                          }
                      }));
                  }
                  else
                  {
                      if (json.message.Contains("重新登录"))
                      {
                          gotoLogin();
                      }
                  }
              }, (code) =>
             {
                 if (code < 503 && code > 500)
                 {
                     gotoLogin();
                 }
             }).Join();
        }

        public void updateAddFriendCount(int count)
        {
            addFriendCount = count;
            if (count > 0)
            {
                this.BeginInvoke(new EventHandler((s, e) =>
                {
                    dSkinButton2.Visible = true;
                    dSkinButton2.Text = count.ToString();
                }));
            }
            else
            {
                this.BeginInvoke(new EventHandler((s, e) =>
                {
                    dSkinButton2.Visible = false;
                }));
            }
        }

        public void refreshFriendFromServer()
        {
            HttpUitls.Instance.get<FriendListReciveModel>("contacts/friendList", new LoginedSendModel(), (json) =>
            {
                if (json.code == 200)
                {
                    DBHelper.Instance.AddFriend(json.data);
                    DBHelper.Instance.Save();
                    Thread.Sleep(1000);
                    loadFriend();
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

        public void gotoLogin()
        {
            SettingMenager.shard.ClearLogin();
            EaseHelper.shard.LoginOut();
            this.BeginInvoke(new EventHandler((s, err) =>
            {
                this.Close();
            }));
            new Thread(new ThreadStart(() =>
            {
                Application.Run(new LoginForm());
            })).Start();
        }

        public void getGroupMembers(Object state)
        {
            string group_Id = (string)state;
            GroupInfoSendModel model = new GroupInfoSendModel();
            model.group_id = group_Id;
            HttpUitls.Instance.get<GroupMembersReciveModel>("groupUser/groupUserList", model, (json) =>
            {
                if (json.code == 200)
                {
                    DBHelper.Instance.addGroupMember(json.data);
                    foreach (var item in openConversation)
                    {
                        if (item.conversation.conversationId() == group_Id)
                        {
                            item.updateGroup();
                        }
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
            }).Join();
        }
        #endregion
        #region 读取本地信息
        private void loadFriend()
        {
            friends = DBHelper.Instance.allFriend();
            if (dSkinTabBar1.SelectedIndex == 1)
            {
                if (contractOrGroup == 1)
                {
                    if (friends != null)
                    {
                        List<ContractListItuem> items = new List<ContractListItuem>(); 
                        foreach (var f in friends)
                        {
                            items.Add(new ContractListItuem(f));
                        }
                        contractOrGroupListBox.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            contractOrGroupListBox.Items.Clear();
                            contractOrGroupListBox.Items.Add(new newFriendListItem(addFriendCount));
                            var tabTitle = new ContractOrGroupListItem(1);
                            tabTitle.SelectTab = contractTabSelect;
                            contractOrGroupListBox.Items.Add(tabTitle);
                            contractOrGroupListBox.Items.AddRange(items);
                            contractOrGroupListBox.Invalidate();
                        }));
                    }
                }
            }
        }

        public void loadGroup()
        {
            groups = DBHelper.Instance.allGroup();
            if (dSkinTabBar1.SelectedIndex == 1)
            {
                if (contractOrGroup == 2)
                {
                    if (groups != null)
                    {
                        List<GroupListItem> items = new List<GroupListItem>();
                        foreach (var item in groups)
                        {
                            items.Add(new GroupListItem(item));
                        }
                        contractOrGroupListBox.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            contractOrGroupListBox.Items.Clear();
                            contractOrGroupListBox.Items.Add(new newFriendListItem(addFriendCount));
                            var tabTitle = new ContractOrGroupListItem(2);
                            tabTitle.SelectTab = contractTabSelect;
                            contractOrGroupListBox.Items.Add(tabTitle);
                            contractOrGroupListBox.Items.AddRange(items);
                            contractOrGroupListBox.Invalidate();
                        }));
                    }
                }
            }
        }
        #endregion

        private void conversationListBox_ItemClick(object sender, DSkin.Controls.ItemClickEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                conversationSelectIndex = e.Index;
                ConversationItem conversation = conversationListBox.Items[conversationSelectIndex] as ConversationItem;
                bool ishave = false;
                foreach (var item in openConversation)
                {
                    if (item.conversation.conversationId() == conversation.model.conversation.conversationId())
                    {
                        ishave = true;
                        item.goToTop();
                        item.Invalidate();
                        break;
                    }
                }
                if (!ishave)
                {
                    ChatPanl chat = new ChatPanl(conversation.model.conversation);
                    chat.Location = new Point(180, 0);
                    chat.Size = new Size(conversationTablePage.Width - 180, conversationTablePage.Height);
                    openConversation.Add(chat);
                    conversationTablePage.Controls.Add(chat);
                    chat.goToTop();
                    conversationTablePage.Invalidate();
                }
                titleLabel.Visible = true;
                titleLabel.Text = conversation.model.name;
            }
        }

        protected override void DestroyHandle()
        {
            conversationRefreshQueue.Dispose();
            loadDataQueue.Dispose();
            base.DestroyHandle();
        }

        private void CLnotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (string.IsNullOrEmpty(SettingMenager.shard.token))
                {
                    LoginForm login = Application.OpenForms["LoginForm"] as LoginForm;
                    if (login == null)
                    {
                        new Thread(new ThreadStart(() =>
                        {
                            Application.Run(new LoginForm());
                        })).Start();
                    }
                    else
                    {
                        if (!login.Visible)
                        {
                            login.Visible = true;
                        }
                        login.BringToFront();
                        login.Activate();
                    }
                }
                else
                {
                    if (!this.Visible)
                    {
                        //系统任务栏显示图标
                        this.Visible = true;
                        timer1.Stop();
                        CLnotifyIcon.Icon = Properties.Resources.icon1;
                    }
                    //激活窗体并获取焦点
                    this.Activate();
                }
            }
        }

        private void MainFrm_SizeChanged(object sender, EventArgs e)
        {
            userInfoPanl.Height = Height;
            mainTabControl.Height = Height - 53;
            mainTabControl.Width = Width - 70;
        }

        private void conversationTablePage_SizeChanged(object sender, EventArgs e)
        {
            conversationPanel.Height = conversationTablePage.Height;
            conversationListBox.Height = conversationTablePage.Height;
            foreach (var item in openConversation)
            {
                item.Height = conversationTablePage.Height;
                item.Width = conversationTablePage.Width - 180;
            }
            contractOrGroupListBox.Height = contractTabPage.Height;
        }

        private void dSkinTabBar1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dSkinTabBar1_TabControlSelectedIndexChanged(object sender, DSkin.Controls.DSkinTabBarEventArgs e)
        {
            if (e.DSkinTabItem.TabIndex == 0)
            {
                searchPanel.BeginInvoke(new EventHandler((s, ee) =>
                {
                    searchPanel.Visible = true;
                }));
                foreach (ChatPanl item in openConversation)
                {
                    if (item.topLeave)
                    {
                        if(item.conversation.conversationType()== EMConversationType.CHAT)
                        {
                            if (item.friend != null)
                            {
                                setTitle(string.IsNullOrEmpty(item.friend.target_user_nickname) ? item.friend.friend_self_name : item.friend.target_user_nickname);
                            }
                        }
                        else
                        {
                            if (item.group != null)
                            {
                                setTitle(item.group.groupName);
                            }
                        }
                    }
                }
                if (openConversation.Count < 1)
                {
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        titleLabel.Visible = false;
                    }));
                }
                refreshConversationList();
            }
            else if (e.DSkinTabItem.TabIndex == 1)
            {
                if (contractOrGroup == 1)
                {
                    loadFriend();
                }else if (contractOrGroup == 2)
                {
                    loadGroup();
                }
                if (contractInfoPanl == null)
                {
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        titleLabel.Visible = false;
                    }));
                }
                else
                {
                    if(contractInfoPanl is NewFriendPanl)
                    {
                        setTitle("新的好友");
                    }else if(contractInfoPanl is PersonalPanl)
                    {
                        var p = contractInfoPanl as PersonalPanl;
                        if (p != null)
                        {
                            setTitle(string.IsNullOrEmpty(p.model.nickName) ? p.model.friend_self_name : p.model.nickName);
                        }
                    }else if(contractInfoPanl is GroupPanl)
                    {
                        var p = contractInfoPanl as GroupPanl;
                        if (p != null)
                        {
                            setTitle(p.model.groupName);
                        }
                    }
                }
                this.BeginInvoke(new EventHandler((s, ee) =>
                {
                    searchPanel.Visible = true;
                }));
            }
            else if (e.DSkinTabItem.TabIndex == 2)
            {
                this.BeginInvoke(new EventHandler((s, ee) =>
                {
                    titleLabel.Visible = false;
                    searchPanel.Visible = false;
                }));
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddFriendForm form = new AddFriendForm();
            form.Show();
            //ChangeGroupOwnerForm form = new ChangeGroupOwnerForm();
            //form.groupId = "97938377342977";
            //form.Show();
        }

        private void contractTabSelect(int index)
        {
            contractOrGroup = index;
            if (dSkinTabBar1.SelectedIndex == 1)
            {
                if (contractOrGroup == 1)
                {
                    loadFriend();
                }
                else if (contractOrGroup == 2)
                {
                    loadGroup();
                }
            }
        }

        public void showPersional(FriendModel friend)
        {
            if (contractInfoPanl == null)
            {
                PersonalPanl panl = new PersonalPanl(friend);
                panl.Location = new Point(180, 0);
                panl.Size = new Size(contractTabPage.Width - 180, contractTabPage.Height);
                contractTabPage.BeginInvoke(new EventHandler((s, e) =>
                {
                    contractTabPage.Controls.Add(panl);
                }));
                contractInfoPanl = panl;
            }
            else if(contractInfoPanl is PersonalPanl)
            {
                PersonalPanl panl = (PersonalPanl)contractInfoPanl;
                panl.setFriend(friend);
            }else if(contractInfoPanl is GroupPanl)
            {
                contractTabPage.Controls.Remove(contractInfoPanl);
                PersonalPanl panl = new PersonalPanl(friend);
                panl.Location = new Point(180, 0);
                panl.Size = new Size(contractTabPage.Width - 180, contractTabPage.Height);
                contractTabPage.BeginInvoke(new EventHandler((s, e) =>
                {
                    contractTabPage.Controls.Add(panl);
                }));
                contractInfoPanl = panl;
            }
            else
            {
                contractTabPage.Controls.Remove(contractInfoPanl);
                PersonalPanl panl = new PersonalPanl(friend);
                panl.Location = new Point(180, 0);
                panl.Size = new Size(contractTabPage.Width - 180, contractTabPage.Height);
                contractTabPage.BeginInvoke(new EventHandler((s, e) =>
                {
                    contractTabPage.Controls.Add(panl);
                }));
                contractInfoPanl = panl;
            }
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                if (!titleLabel.Visible)
                {
                    titleLabel.Visible = true;
                }
                titleLabel.Text = string.IsNullOrEmpty(friend.nickName) ? friend.friend_self_name : friend.nickName;
            }));
        }

        public void showGroup(GroupTable group)
        {
            if (contractInfoPanl == null)
            {
                GroupPanl panl = new GroupPanl(group);
                panl.Location = new Point(180, 0);
                panl.Size = new Size(contractTabPage.Width - 180, contractTabPage.Height);
                contractTabPage.BeginInvoke(new EventHandler((s, e) =>
                {
                    contractTabPage.Controls.Add(panl);
                }));
                contractInfoPanl = panl;
            }else if(contractInfoPanl is PersonalPanl)
            {
                contractTabPage.Controls.Remove(contractInfoPanl);
                GroupPanl panl = new GroupPanl(group);
                panl.Location = new Point(180, 0);
                panl.Size = new Size(contractTabPage.Width - 180, contractTabPage.Height);
                contractTabPage.BeginInvoke(new EventHandler((s, e) =>
                {
                    contractTabPage.Controls.Add(panl);
                }));
                contractInfoPanl = panl;
            }else if(contractInfoPanl is GroupPanl)
            {
                GroupPanl panl = (GroupPanl)contractInfoPanl;
                panl.setGroup(group);
            }
            else
            {
                contractTabPage.Controls.Remove(contractInfoPanl);
                GroupPanl panl = new GroupPanl(group);
                panl.Location = new Point(180, 0);
                panl.Size = new Size(contractTabPage.Width - 180, contractTabPage.Height);
                contractTabPage.BeginInvoke(new EventHandler((s, e) =>
                {
                    contractTabPage.Controls.Add(panl);
                }));
                contractInfoPanl = panl;
            }
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                if (!titleLabel.Visible)
                {
                    titleLabel.Visible = true;
                }
                titleLabel.Text = group.groupName;
            }));
        }

        private void contractOrGroupListBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            showClearContentMenu = true;
            if (e.Button== MouseButtons.Left)
            {
                if (e.Index == 0)
                {
                    showAddFriend();
                    return;
                }
                if (e.Index == 1)
                {
                    return;
                }
                if (contractOrGroup == 1)
                {
                    showPersional(friends[e.Index - 2]);
                }
                else if (contractOrGroup == 2)
                {
                    showGroup(groups[e.Index - 2]);
                }
            }
            else
            {
                if (e.Index < 2)
                {
                    return;
                }
                if (contractOrGroup == 1)
                {
                    contractRightIndex = e.Index - 2;
                    friendContextMenuStrip.Show(new Point(e.Location.X + contractOrGroupListBox.Items[e.Index].LocationToScreen.X, e.Location.Y + contractOrGroupListBox.Items[e.Index].LocationToScreen.Y));
                }else if (contractOrGroup == 2)
                {
                    contractRightIndex = e.Index - 2;
                    groupContextMenuStrip.Show(new Point(e.Location.X + contractOrGroupListBox.Items[e.Index].LocationToScreen.X, e.Location.Y + contractOrGroupListBox.Items[e.Index].LocationToScreen.Y));
                }
            }
        }

        public void showAddFriend()
        {
            if (mainTabControl.SelectedIndex != 1)
            {
                mainTabControl.SelectedIndex = 1;
            }
            var pan = contractInfoPanl as NewFriendPanl;
            if (pan != null)
            {
                pan.loadaData();
            }
            else
            {
                contractTabPage.Controls.Remove(contractInfoPanl);
                contractInfoPanl = new NewFriendPanl();
                contractTabPage.Controls.Add(contractInfoPanl);
                contractInfoPanl.Location = new Point(180, 0);
                contractInfoPanl.Width = contractTabPage.Width - 180;
                contractInfoPanl.Height = contractTabPage.Height;
                titleLabel.Visible = true;
                titleLabel.Text = "新的朋友";
            }
        }

        public void showPersonalChat(FriendModel userID)
        {
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                if (resultBox != null)
                {
                    this.Controls.Remove(resultBox);
                    resultBox = null;
                }
            }));
            if (mainTabControl.SelectedIndex != 0)
            {
                mainTabControl.SelectedIndex = 0;
            }
            foreach (var item in openConversation)
            {
                if (item.conversation.conversationId() == userID.userID)
                {
                    item.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        item.BringToFront();
                    }));
                    this.BeginInvoke((new EventHandler((s, ee) =>
                    {
                        titleLabel.Text = string.IsNullOrEmpty(userID.nickName) ? userID.friend_self_name : userID.nickName;
                    })));
                    return;
                }
            }
            conversationTablePage.BeginInvoke(new EventHandler((ss, err) =>
            {
                ChatPanl chat = new ChatPanl(userID.userID,EMConversationType.CHAT);
                chat.Location = new Point(180, 0);
                chat.Size = new Size(conversationTablePage.Width - 180, conversationTablePage.Height);
                openConversation.Add(chat);
                conversationTablePage.Controls.Add(chat);
                chat.BringToFront();
                this.BeginInvoke((new EventHandler((s, ee) =>
                {
                    titleLabel.Text = string.IsNullOrEmpty(userID.nickName) ? userID.friend_self_name : userID.nickName;
                })));
                conversationTablePage.Invalidate();
            }));
        }

        public void showGroupChat(GroupTable groupID)
        {
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                if (resultBox != null)
                {
                    this.Controls.Remove(resultBox);
                    resultBox = null;
                }
            }));
            if (mainTabControl.SelectedIndex != 0)
            {
                mainTabControl.SelectedIndex = 0;
            }
            foreach (var item in openConversation)
            {
                if (item.conversation.conversationId() == groupID.groupId)
                {
                    item.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        item.BringToFront();
                    }));
                    this.BeginInvoke((new EventHandler((s, ee) =>
                    {
                        titleLabel.Text = groupID.groupName;
                    })));
                    return;
                }
            }
            conversationTablePage.BeginInvoke(new EventHandler((ss, err) =>
            {
                ChatPanl chat = new ChatPanl(groupID.groupId, EMConversationType.GROUPCHAT);
                chat.Location = new Point(180, 0);
                chat.Size = new Size(conversationTablePage.Width - 180, conversationTablePage.Height);
                openConversation.Add(chat);
                conversationTablePage.Controls.Add(chat);
                chat.BringToFront();
                this.BeginInvoke((new EventHandler((s, ee) =>
                {
                    titleLabel.Text = groupID.groupName;
                })));
                conversationTablePage.Invalidate();
            }));
        }

        private void friendContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (contractRightIndex == -1)
            {
                return;
            }
            if (e.ClickedItem.Text == "发送消息")
            {
                showPersonalChat(friends[contractRightIndex]);
            }else if (e.ClickedItem.Text == "设置备注")
            {
                SetBacupNameForm form = new SetBacupNameForm();
                form.userID = friends[contractRightIndex].userID;
                form.ChangeBackupName = changeBackupName;
                form.ShowDialog();
            }
            else if (e.ClickedItem.Text == "删除好友")
            {
                string id = friends[contractRightIndex].userID;
                if (MessageBox.Show("你确定删除他吗？",null,MessageBoxButtons.OKCancel)== DialogResult.OK)
                {
                    deleteFriend(id);
                }
            }
            contractRightIndex = -1;
        }

        private void changeBackupName(string id,string name)
        {
            SetFriendNickNameSendModel model = new SetFriendNickNameSendModel();
            model.target_user_id = id;
            model.newName = name;
            HttpUitls.Instance.get<BaseReciveModel>("contacts/editFriendNickname", model, (json) =>
              {
                  if (json.code == 200)
                  {
                      refreshFriendFromServer();
                      MessageBox.Show("设置成功");
                  }
                  else
                  {
                      if (json.message.Contains("重新登录"))
                      {
                          gotoLogin();
                      }
                  }
              }, (code) =>
              {
                  if (code < 503 && code > 500)
                  {
                      gotoLogin();
                  }
              });
        }

        private void deleteFriend(string id)
        {
            EMError error = new EMError();
            EaseHelper.shard.client.getContactManager().deleteContact(id, error, false);
            DeleteFriendSendModel model = new DeleteFriendSendModel();
            model.target_user_id = id;
            deleteFriendID = id;
            HttpUitls.Instance.get<BaseReciveModel>("contacts/delFriend", model, (json) =>
            {
                if (json.code == 200)
                {
                    foreach (ChatPanl item in openConversation)
                    {
                        if (item.conversation.conversationId() == id)
                        {
                            conversationTablePage.BeginInvoke(new EventHandler((s, e) =>
                            {
                                conversationTablePage.Controls.Remove(item);
                            }));
                            openConversation.Remove(item);
                            if (openConversation.Count > 0)
                            {
                                if (openConversation.Last() != null)
                                {
                                    openConversation.Last().topLeave = true;
                                    this.BeginInvoke(new EventHandler((s, e) =>
                                    {
                                        openConversation.Last().goToTop();
                                    }));
                                }
                            }
                            else
                            {
                                conversationTablePage.BeginInvoke(new EventHandler((s, e) =>
                                {
                                    conversationTablePage.Invalidate();
                                }));
                                this.BeginInvoke(new EventHandler((ss, ee) =>
                                {
                                    titleLabel.Visible = false;
                                }));
                            }
                            break;
                        }
                    }
                    refreshFriendFromServer();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    if (json.message.Contains("重新登录"))
                    {
                        gotoLogin();
                    }
                }
            }, (code) =>
             {
                 if (code < 503 && code > 500)
                 {
                     gotoLogin();
                 }
             });
        }

        private void groupContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Console.WriteLine(e.ClickedItem.MergeIndex);
            if (contractRightIndex == -1)
            {
                return;
            }
            if(e.ClickedItem.Text == "发送消息")
            {
                showGroupChat(groups[contractRightIndex]);
            }
            if (e.ClickedItem.Text == "新的群组")
            {
                NewGroupNameForm form = new NewGroupNameForm();
                form.Show();
            }
            if(e.ClickedItem.Text== "群详情")
            {
                GroupInfoForm form = new GroupInfoForm(groups[contractRightIndex]);
                form.Show();
            }
            contractRightIndex = -1;
        }

        private void dSkinContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contractTabPage_SizeChanged(object sender, EventArgs e)
        {
            contractOrGroupListBox.Height = contractTabPage.Height;
            if (contractInfoPanl != null)
            {
                contractInfoPanl.Width = contractTabPage.Width - 180;
                contractInfoPanl.Height = contractTabPage.Height;
            }
        }
        
        public void allChatToBtton()
        {
            foreach (var item in openConversation)
            {
                item.topLeave = false;
            }
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason== CloseReason.UserClosing)
            {
                try
                {
                    //取消关闭窗口
                    e.Cancel = true;
                    this.Visible = false;
                }
                catch
                {

                }
                finally
                {

                }
            }
            else
            {
                EaseHelper.shard.LoginOut();
            }
        }

        private void CLnotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (string.IsNullOrEmpty(SettingMenager.shard.token))
                {
                    LoginForm login = Application.OpenForms["LoginForm"] as LoginForm;
                    if (login == null)
                    {
                        new Thread(new ThreadStart(() =>
                        {
                            Application.Run(new LoginForm());
                        })).Start();
                    }
                    else
                    {
                        if (!login.Visible)
                        {
                            login.Visible = true;
                        }
                        login.BringToFront();
                        login.Activate();
                    }
                }
                else
                {
                    if (!this.Visible)
                    {
                        //系统任务栏显示图标
                        this.Visible = true;
                        timer1.Stop();
                        CLnotifyIcon.Icon = Properties.Resources.icon1;
                    }
                    //激活窗体并获取焦点
                    this.Activate();
                }
            }
            if(e.Button== MouseButtons.Right)
            {
                //notificatiionContextMenuStrip.Show(new Point(CLnotifyIcon.l);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EaseHelper.shard.LoginOut();
            try
            {
                Application.Exit();
            }
            catch
            {
                Application.Exit();
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new EventHandler((s, ee) =>
            {
                DCWebImageMaanager.shard.downloadImageAsync(SettingMenager.shard.avatar, (image, b) =>
                {
                    this.BeginInvoke(new EventHandler((ss, se) =>
                    {
                        dSkinPictureBox1.Image = image;
                    }));
                });
                dSkinLabel1.Text = SettingMenager.shard.username;
                dSkinLabel2.Text = "畅聊号:" + SettingMenager.shard.idCard;
            }));
        }

        private void userHeadBtn_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                if (mainTabControl.SelectedIndex != 2)
                {
                    mainTabControl.SelectedIndex = 2;
                }
            }
        }

        private void settingTabPage_SizeChanged(object sender, EventArgs e)
        {
            dSkinPictureBox1.Location = new Point((settingTabPage.Width - dSkinPictureBox1.Width) / 2, dSkinPictureBox1.Location.Y);
            dSkinLabel1.Location = new Point((settingTabPage.Width - dSkinLabel1.Width) / 2, dSkinLabel1.Location.Y);
            dSkinLabel2.Location = new Point((settingTabPage.Width - dSkinLabel2.Width) / 2, dSkinLabel2.Location.Y);
            dSkinButton3.Location = new Point((settingTabPage.Width - dSkinButton3.Width) / 2, dSkinButton3.Location.Y);
            versionLabel.Location = new Point((settingTabPage.Width - versionLabel.Width) / 2, versionLabel.Location.Y);
        }

        private void settingTabPage_Layout(object sender, LayoutEventArgs e)
        {
            dSkinPictureBox1.Location = new Point((settingTabPage.Width - dSkinPictureBox1.Width) / 2, dSkinPictureBox1.Location.Y);
            dSkinLabel1.Location = new Point((settingTabPage.Width - dSkinLabel1.Width) / 2, dSkinLabel1.Location.Y);
            dSkinLabel2.Location = new Point((settingTabPage.Width - dSkinLabel2.Width) / 2, dSkinLabel2.Location.Y);
            dSkinButton3.Location = new Point((settingTabPage.Width - dSkinButton3.Width) / 2, dSkinButton3.Location.Y);
            versionLabel.Location = new Point((settingTabPage.Width - versionLabel.Width) / 2, versionLabel.Location.Y);
        }

        private void dSkinButton3_MouseClick(object sender, MouseEventArgs e)
        {
            EaseHelper.shard.LoginOut();
            Application.Exit();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        public void setTitle(string text)
        {
            titleLabel.BeginInvoke(new EventHandler((s, e) =>
            {
                titleLabel.Visible = true;
                titleLabel.Text = text;
            }));
        }

        public void deleteMesssage(string conversationId,string messaageId)
        {
            foreach (var item in openConversation)
            {
                if (item.conversation.conversationId() == conversationId)
                {
                    item.deleteMessgae(messaageId);
                }
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                if (string.IsNullOrEmpty(searchTextBox.Text))
                {
                    return;
                }
                var data = DBHelper.Instance.mainSearch(searchTextBox.Text);
               if (resultBox == null)
                {
                    resultBox = new SearchResultBox();
                    resultBox.Location = new Point(searchPanel.Location.X, searchTextBox.Location.Y + searchTextBox.Height);
                    this.Invoke(new EventHandler((s, ee) =>
                    {
                        this.Controls.Add(resultBox);
                        resultBox.refreshData(data);
                    }));
                }
            })).Start();
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            this.BeginInvoke(new EventHandler((s, ee) =>
            {
                if (resultBox != null)
                {
                    this.Controls.Remove(resultBox);
                    resultBox = null;
                }
            }));
        }

        public void removeConversation(string conversationId)
        {
            if (conversationId == deleteFriendID) return;
            EaseHelper.shard.client.getChatManager().removeConversation(conversationId, true);
            foreach (ChatPanl item in openConversation)
            {
                if (item.conversation.conversationId() == conversationId)
                {
                    conversationTablePage.BeginInvoke(new EventHandler((s, e) =>
                    {
                        conversationTablePage.Controls.Remove(item);
                        if (item.topLeave)
                        {
                            if(item.conversation.conversationType()== EMConversationType.GROUPCHAT)
                            {
                                MessageBox.Show("您已被移除该群");
                            }
                            else
                            {
                                MessageBox.Show("您已被对方删除");
                            }
                        }
                    }));
                    openConversation.Remove(item);
                    if (openConversation.Count > 0)
                    {
                        if (openConversation.Last() != null)
                        {
                            openConversation.Last().topLeave = true;
                            this.BeginInvoke(new EventHandler((s, e) =>
                            {
                                openConversation.Last().goToTop();
                            }));
                        }
                    }
                    else
                    {
                        conversationTablePage.BeginInvoke(new EventHandler((s, e) =>
                        {
                            conversationTablePage.Invalidate();
                        }));
                        this.BeginInvoke(new EventHandler((ss, ee) =>
                        {
                            titleLabel.Visible = false;
                        }));
                    }
                    break;
                }
            }
            refreshConversationList();
            this.BeginInvoke(new EventHandler((ss, ee) =>
            {
                if (mainTabControl.SelectedIndex == 2)
                {
                    titleLabel.Visible = false;
                }
            }));
            var per = contractInfoPanl as PersonalPanl;
            if (per != null)
            {
                if (per.model.userID == conversationId)
                {
                    this.BeginInvoke(new EventHandler((ss, ee) =>
                    {
                        contractTabPage.Controls.Remove(per);
                        if (mainTabControl.SelectedIndex == 1)
                        {
                            titleLabel.Visible = false;
                        }
                    }));
                }
            }
            var g = contractInfoPanl as GroupPanl;
            if (g != null)
            {
                if (g.model.groupId == conversationId)
                {
                    this.BeginInvoke(new EventHandler((ss, ee) =>
                    {
                        contractTabPage.Controls.Remove(g);
                        if (mainTabControl.SelectedIndex == 1)
                        {
                            titleLabel.Visible = false;
                        }
                    }));
                }
            }
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Up && !e.Control && !e.Alt && !e.Shift)
            {
                if (resultBox != null)
                {
                    resultBox.up();
                }
            }
            if (e.KeyCode == Keys.Down && !e.Control && !e.Alt && !e.Shift)
            {
                if (resultBox != null)
                {
                    resultBox.down();
                }
            }
            if (e.KeyCode == Keys.Enter && !e.Control && !e.Alt && !e.Shift)
            { 
                if (resultBox != null)
                {
                    resultBox.EnterChat();
                }
                searchTextBox.BeginInvoke(new EventHandler((s, ee) =>
                {
                    searchTextBox.Text = string.Empty;
                }));
            }
        }

        private void contractOrGroupListBox_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void contractOrGroupListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (showClearContentMenu)
            {
                showClearContentMenu = false;
            }
            else
            {
                if (contractOrGroup == 2)
                {
                    clearGroupContextMenuStrip1.Show(Control.MousePosition);
                }
            }
        }

        private void 新的群组ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            NewGroupNameForm form = new NewGroupNameForm();
            form.Show();
        }

        private void CLnotifyIcon_BalloonTipShown(object sender, EventArgs e)
        {
            Console.WriteLine("-----------------------");
        }

        private void CLnotifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("-----------------------move--------------------");
        }

        public void ShowWithChat(EMConversation conversation)
        {
            bool ishave = false;
            foreach (var item in openConversation)
            {
                if (item.conversation.conversationId() == conversation.conversationId())
                {
                    ishave = true;
                    item.goToTop();
                    item.Invalidate();
                    break;
                }
            }
            if (!ishave)
            {
                ChatPanl chat = new ChatPanl(conversation);
                chat.Location = new Point(180, 0);
                chat.Size = new Size(conversationTablePage.Width - 180, conversationTablePage.Height);
                openConversation.Add(chat);
                conversationTablePage.Controls.Add(chat);
                chat.goToTop();
                conversationTablePage.Invalidate();
            }
            titleLabel.Visible = true;
            Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (nflag)
            {
                if (clear != null)
                {
                    CLnotifyIcon.Icon = clear;
                }
                nflag = !nflag;
            }
            else
            {
                CLnotifyIcon.Icon = Properties.Resources.icon1;
                nflag = !nflag;
            }
        }
    }
}
