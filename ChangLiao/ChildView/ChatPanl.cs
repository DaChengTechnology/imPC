using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using ChangLiao.DB;
using System.Linq;
using ChangLiao.Model.ReciveModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Controls;
using EaseMobLib;
using ChangLiao.Ease;
using ChangLiao.Temple;
using ChangLiao.Model.ViewModel;
using DSkin.DirectUI;
using ChangLiao.Util;
using System.Threading;
using ChangLiao.windows;
using System.IO;
using ChangLiao.Model.SendModel;
using System.Diagnostics;

namespace ChangLiao.ChildView
{
    public partial class ChatPanl : DSkinUserControl
    {
        /// <summary>
        /// 环信会话对象
        /// </summary>
        public EMConversation conversation { get; set; }
        /// <summary>
        /// 消息列表
        /// </summary>
        private List<EMMessage> datasouce;
        /// <summary>
        /// 左右边距
        /// </summary>
        public static int PaddingLorR = 10;
        /// <summary>
        /// 上下边距
        /// </summary>
        public static int PaddingUorD = 5;
        /// <summary>
        /// 气泡和头像的上边距
        /// </summary>
        public static int PaddingBandAUP = 5;
        /// <summary>
        /// 头像大小
        /// </summary>
        public static int avatarSize = 35;
        /// <summary>
        /// 时间戳行高
        /// </summary>
        public static int timeRowHeight = 30;
        /// <summary>
        /// 聊天字体
        /// </summary>
        public Font textMessageFont { get; set; }
        /// <summary>
        /// 名字字体
        /// </summary>
        public Font nameFont { get; set; }
        /// <summary>
        /// 气泡边距
        /// </summary>
        public static int BubblePadding = 3;
        /// <summary>
        /// 时间字体
        /// </summary>
        public Font timeFont { get; set; }
        /// <summary>
        /// 气泡最大宽度
        /// </summary>
        public int maxBubbleWith { get; set; }
        /// <summary>
        /// 后台处理线程
        /// </summary>
        private BackgroundWorker messageQueue;
        /// <summary>
        /// 线程队列
        /// </summary>
        private List<Action> actions;
        private static object lockobj = new object();
        /// <summary>
        /// 时间间隔
        /// </summary>
        private long timeSpan = 60 * 1000;
        /// <summary>
        /// 第一个时间
        /// </summary>
        [DefaultValue(0)]
        private long firsttime;
        /// <summary>
        /// 输入框内容
        /// </summary>
        private CLChatContent ChatContent;
        /// <summary>
        /// 消息监听
        /// </summary>
        private EMChatManagerListener listener;
        /// <summary>
        /// 是否需要滚动到底部
        /// </summary>
        [DefaultValue(false)]
        private bool needScrollToBottm;
        /// <summary>
        /// 自己是否禁言
        /// </summary>
        private bool isSinlge = false;
        /// <summary>
        /// 我自己的成员信息
        /// </summary>
        private GroupUser mine;
        /// <summary>
        /// 提示消息
        /// </summary>
        private ChatTipsPanl tipsPanl;
        /// <summary>
        /// 点击消息
        /// </summary>
        private ChatHistoryListItem selectRightItem;
        /// <summary>
        /// 显示的第一个消息
        /// </summary>
        private ChatHistoryListItem willVisableFistItem;
        /// <summary>
        /// 是否正在加载
        /// </summary>
        private bool isLoadding = false;
        public ChatPanl()
        {
            InitializeComponent();
            messageQueue = new BackgroundWorker();
            messageQueue.DoWork += MessageQueue_DoWork;
            nameFont = new Font("微软雅黑", 8);
            textMessageFont = new Font("微软雅黑", 10);
            timeFont = new Font("微软雅黑", 7);
            ChatContent = new CLChatContent();
            topLeave = true;
            listener = new EMChatManagerListener();
            listener.onReceiveMessages = onMessageRecive;
            listener.onReceiveCmdMessages = onCmdMessageRecive;
            listener.onReceiveHasDeliveredAcks = onReceiveHasDeliveredAcks;
            listener.onReceiveHasReadAcks = onReciveHasReadAcks;
            listener.onMessageStatusChanged = onMessageStateChanged;
            EaseHelper.shard.client.getChatManager().addListener(listener);
        }

        public ChatPanl(EMConversation mConversation)
        {
            InitializeComponent();
            conversation = mConversation;
            messageQueue = new BackgroundWorker();
            messageQueue.DoWork += MessageQueue_DoWork;
            actions = new List<Action>();
            if (!isLoadding)
            {
                isLoadding = true;
                addActionToMessageQueue(loadMessage);
            }
            nameFont = new Font("微软雅黑", 8);
            textMessageFont = new Font("微软雅黑", 10);
            timeFont = new Font("微软雅黑", 7);
            ChatContent = new CLChatContent();
            topLeave = true;
            Task.Run(() =>
            {
                if (conversation.conversationType() == EMConversationType.CHAT)
                {
                    friend = DBHelper.Instance.getFriend(conversation.conversationId());
                    if (friend == null)
                    {
                        var stranger = DBHelper.Instance.GetStronger(conversation.conversationId());
                        if (stranger != null)
                        {
                            friend = new FriendListData();
                            friend.user_id = stranger.userID;
                            friend.friend_self_name = stranger.nickName;
                            friend.id_card = stranger.idCard;
                            friend.is_shield = 2;
                            friend.portrait = stranger.avatar;
                            friend.is_star = 2;
                            friend.is_yhjf = 2;
                        }
                        else
                        {
                            UserInfoMenager.shard.GetUser(conversation.conversationId(), (user) =>
                            {
                                if (user != null)
                                {
                                    friend = new FriendListData();
                                    friend.user_id = user.user_id;
                                    friend.friend_self_name = user.user_name;
                                    friend.id_card = user.id_card;
                                    friend.is_shield = 2;
                                    friend.portrait = user.portrait;
                                    friend.is_star = 2;
                                    friend.is_yhjf = 2;
                                }
                            });
                        }
                    }
                }
                else
                {
                    group = DBHelper.Instance.GetGroup(conversation.conversationId());
                    mine = DBHelper.Instance.GetGroupUser(SettingMenager.shard.userID, conversation.conversationId());
                }
            });
            listener = new EMChatManagerListener();
            listener.onReceiveMessages = onMessageRecive;
            listener.onReceiveCmdMessages = onCmdMessageRecive;
            listener.onReceiveHasDeliveredAcks = onReceiveHasDeliveredAcks;
            listener.onReceiveHasReadAcks = onReciveHasReadAcks;
            listener.onMessageStatusChanged = onMessageStateChanged;
            EaseHelper.shard.client.getChatManager().addListener(listener);
        }

        public ChatPanl(string conversationId,EMConversationType conversationType)
        {
            InitializeComponent();
            conversation = EaseHelper.shard.client.getChatManager().conversationWithType(conversationId, conversationType, true);
            messageQueue = new BackgroundWorker();
            actions = new List<Action>();
            if (!isLoadding)
            {
                isLoadding = true;
                addActionToMessageQueue(loadMessage);
            }
            messageQueue.DoWork += MessageQueue_DoWork;
            nameFont = new Font("微软雅黑", 8);
            textMessageFont = new Font("微软雅黑", 10);
            timeFont = new Font("微软雅黑", 7);
            ChatContent = new CLChatContent();
            topLeave = true;
            Task.Run(() =>
            {
                if (conversation.conversationType() == EMConversationType.CHAT)
                {
                    friend = DBHelper.Instance.getFriend(conversation.conversationId());
                    if (friend == null)
                    {
                        var stranger = DBHelper.Instance.GetStronger(conversationId);
                        if (stranger != null)
                        {
                            friend = new FriendListData();
                            friend.user_id = stranger.userID;
                            friend.friend_self_name = stranger.nickName;
                            friend.id_card = stranger.idCard;
                            friend.is_shield = 2;
                            friend.portrait = stranger.avatar;
                            friend.is_star = 2;
                            friend.is_yhjf = 2;
                        }
                        else
                        {
                            UserInfoMenager.shard.GetUser(conversationId, (user) =>
                            {
                                if (user != null)
                                {
                                    friend = new FriendListData();
                                    friend.user_id = user.user_id;
                                    friend.friend_self_name = user.user_name;
                                    friend.id_card = user.id_card;
                                    friend.is_shield = 2;
                                    friend.portrait = user.portrait;
                                    friend.is_star = 2;
                                    friend.is_yhjf = 2;
                                    if (friend != null)
                                    {
                                        MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                                        if (main != null)
                                        {
                                            main.setTitle(string.IsNullOrEmpty(friend.target_user_nickname) ? friend.friend_self_name : friend.target_user_nickname);
                                        }
                                    }
                                }
                            });
                        }
                    }
                    if (friend != null)
                    {
                        MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                        if (main != null)
                        {
                            main.setTitle(string.IsNullOrEmpty(friend.target_user_nickname) ? friend.friend_self_name : friend.target_user_nickname);
                        }
                    }
                }
                else
                {
                    MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                    if (main != null)
                    {
                        main.refreshGroupInfo(conversationId);
                        main.getGroupMembers(conversationId);
                    }
                    updateGroup();
                }
            });
            listener = new EMChatManagerListener();
            listener.onReceiveMessages = onMessageRecive;
            listener.onReceiveCmdMessages = onCmdMessageRecive;
            listener.onReceiveHasDeliveredAcks = onReceiveHasDeliveredAcks;
            listener.onReceiveHasReadAcks = onReciveHasReadAcks;
            listener.onMessageStatusChanged = onMessageStateChanged;
            EaseHelper.shard.client.getChatManager().addListener(listener);
        }
        /// <summary>
        /// 群组信息刷
        /// </summary>
        public void updateGroup()
        {
            if(conversation.conversationType()!= EMConversationType.GROUPCHAT)
            {
                return;
            }
            group = DBHelper.Instance.GetGroup(conversation.conversationId());
            mine = DBHelper.Instance.GetGroupUser(SettingMenager.shard.userID, conversation.conversationId());
        }

        private void ChatPanl_SizeChanged(object sender, EventArgs e)
        {
            chathistoryListBox.Height = Height - sendButton.Height - chatTextBox.Height - chatToolbar.Height;
            chathistoryListBox.Width = Width;
            chatToolbar.Location = new Point(0, chathistoryListBox.Height);
            chatToolbar.Width = Width;
            chatTextBox.Location = new Point(0, chathistoryListBox.Height + chatToolbar.Height);
            chatTextBox.Width = Width;
            sendButton.Location = new Point(Width - 100, Height-30);
            maxBubbleWith = Width - avatarSize * 2 - PaddingLorR * 4;
            foreach (DuiBaseControl item in chathistoryListBox.Items)
            {
                item.Width = chathistoryListBox.Width;
            }
            chathistoryListBox.Invalidate();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatPanl_Load(object sender, EventArgs e)
        {
            GC.AddMemoryPressure(8 * 1024 * 300);
            chathistoryListBox.InnerScrollBar.ShowArrow = false;
            chathistoryListBox.LayoutContented += ChathistoryListBox_LayoutContented;
            chatTextBox.Items.ItemRemoved += Items_ItemRemoved;
            chatTextBox.Items.ItemAdded += Items_ItemAdded;
            needScrollToBottm = false;
            datasouce = new List<EMMessage>();
            chathistoryListBox.Height = Height - sendButton.Height - chatTextBox.Height - chatToolbar.Height;
            chathistoryListBox.Width = Width;
            chatToolbar.Location = new Point(0, chathistoryListBox.Height);
            chatToolbar.Width = Width;
            chatTextBox.Location = new Point(0, chathistoryListBox.Height + chatToolbar.Height);
            chatTextBox.Width = Width;
            sendButton.Location = new Point(Width - 100, Height - 30);
            this.BeginInvoke(new EventHandler((s, ee) =>
            {
                this.Invalidate();
            }));
        }
        /// <summary>
        /// 输入字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Items_ItemAdded(object sender, DSkin.Common.CollectionEventArgs<ILayoutElement> e)
        {
            if(e.Item is DuiChar)
            {
                var c = e.Item as DuiChar;
                ChatContent.insertChar(c.Char, e.Index);
            }
        }
        /// <summary>
        /// 输入框删除（文字、图片）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Items_ItemRemoved(object sender, DSkin.Common.CollectionEventArgs<ILayoutElement> e)
        {
            if (chatTextBox.SelectionLength > 0)
            {
                ChatContent.removeAt(chatTextBox.SelectionStart);
            }
            else
            {
                ChatContent.removeAt(chatTextBox.CaretIndex);
            }
        }

        private void ChathistoryListBox_LayoutContented(object sender, EventArgs e)
        {
            maxBubbleWith = Width - avatarSize * 2 - PaddingLorR * 4;
            foreach (DuiBaseControl item in chathistoryListBox.Items)
            {
                item.Width = chathistoryListBox.Width;
            }
            chathistoryListBox.Invalidate();
            if (needScrollToBottm)
            {
                chathistoryListBox.BeginInvoke(() =>
                {
                    chathistoryListBox.Value = 1;
                });
            }
        }

        private void fileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            fileButton.BackgroundImage = Properties.Resources.file_over;
        }

        private void fileButton_MouseLeave(object sender, EventArgs e)
        {
            fileButton.BackgroundImage = Properties.Resources.file_normal;
        }

        private void faceButton_MouseEnter(object sender, MouseEventArgs e)
        {
            faceButton.BackgroundImage = Properties.Resources.face_over;
        }

        private void faceButton_MouseLeave(object sender, EventArgs e)
        {
            faceButton.BackgroundImage = Properties.Resources.face_normal;
        }
        /// <summary>
        /// message线程执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void messageQueueTimer_Tick(object sender, EventArgs e)
        {
            if (!messageQueue.IsBusy)
            {
                if (actions.Count > 0)
                {
                    lock (lockobj)
                    {
                        Action act = actions[0];
                        actions.RemoveAt(0);
                        messageQueue.RunWorkerAsync(act);
                    }
                }
            }
        }

        private void MessageQueue_DoWork(object sender, DoWorkEventArgs e)
        {
            Action action = e.Argument as Action;
            if (action != null)
            {
                action();
            }
        }

        private void addActionToMessageQueue(Action a)
        {
            lock (lockobj)
            {
                actions.Add(a);
            }
        }
        /// <summary>
        /// 加载消息记录
        /// </summary>
        private void loadMessage()
        {
            Console.WriteLine("开始读取");
            DateTime b = DateTime.Now;
            EMMessage firstMessage = null;
            if (datasouce.Count > 0)
            {
                firstMessage = datasouce[0];
                willVisableFistItem = chathistoryListBox.Items[0] as ChatHistoryListItem
                    ;
            }
            EMMessage[] newMesage;
            if (firstMessage != null)
            {
                needScrollToBottm = false;
                newMesage = conversation.loadMoreMessages(firstMessage.msgId(), 20, EMMessageSearchDirection.UP);
            }
            else
            {
                needScrollToBottm = true;
                newMesage = conversation.loadMoreMessages(null, 20, EMMessageSearchDirection.UP);
            }
            Console.WriteLine("读取" + (DateTime.Now - b));
            if (newMesage.Length > 0)
            {
                for (int i = 0; i < newMesage.Count(); i++)
                {
                    GC.AddMemoryPressure(8 * 1024 * 4);
                    datasouce.Insert(i, newMesage[i]);
                }
            }
            else
            {
                return;
            }
            var list = formatMessage(newMesage, false);
            if (list.Count == 0)
            {
                return;
            }
            Console.WriteLine("读取+解析" + (DateTime.Now - b));
            chathistoryListBox.Invoke(() =>
            {
                List<ChatHistoryListItem> items = new List<ChatHistoryListItem>();
                foreach (var item in list)
                {
                    ChatHistoryListItem i = new ChatHistoryListItem(item, this);
                    if (!item.isTime && !item.isshowTest)
                    {
                        i.messageClick += Item_messageClick;
                        i.messageRightClick += Item_messageRightClick;
                        i.headClick += Item_headClick;
                        i.headRightClick += Item_headRightClick;
                    }
                    items.Add(i);
                }
                var newlast = newMesage[newMesage.Length - 1];
                if (firstMessage != null)
                {
                    if (firstMessage.timestamp() - newlast.timestamp() < timeSpan || firstMessage.timestamp() - newlast.timestamp() > -timeSpan)
                    {
                        chathistoryListBox.Items.RemoveAt(0);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    chathistoryListBox.Items.Insert(i, items[i]);
                }
                Console.WriteLine(DateTime.Now - b);
                if (firstMessage == null || needScrollToBottm)
                {
                    needScrollToBottm = true;
                    chathistoryListBox.Value = 1;
                    chathistoryListBox.Invalidate();
                    isLoadding = false;
                }
                else
                {
                    int h = 0;
                    foreach (var item in items)
                    {
                        h -= item.Height;
                    }
                    chathistoryListBox.ListTop = h;
                    chathistoryListBox.Invalidate();
                    willVisableFistItem = null;
                    isLoadding = false;
                }
            });
            MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
            if (conversation.unreadMessagesCount() > 0)
            {
                conversation.markAllMessagesAsRead();
            }
            if (main != null)
            {
                main.refreshConversationList();
            }
        }
        /// <summary>
        /// 生成消息模型
        /// </summary>
        /// <param name="messages">消息</param>
        /// <param name="isAppend">是否添加</param>
        /// <returns></returns>
        private List<MessageModel> formatMessage(EMMessage[] messages, bool isAppend)
        {
            List<MessageModel> listItems = new List<MessageModel>();
            if (messages.Length < 1)
            {
                return listItems;
            }
            for (int i = 0; i < messages.Length; i++)
            {
                if (i == 0)
                {
                    if (isAppend)
                    {
                        EMMessage last = null;
                        if (datasouce.Count > 0)
                        {
                            last = datasouce[datasouce.Count - 1];
                        }
                        if (last == null)
                        {
                            listItems.Add(new MessageModel(messages[i].timestamp()));
                            var item = new MessageModel(messages[i]);
                            listItems.Add(item);
                        }
                        else
                        {
                            if (messages[i].timestamp() - last.timestamp() > timeSpan || messages[i].timestamp() - last.timestamp() < -timeSpan)
                            {
                                listItems.Add(new MessageModel(messages[i].timestamp()));
                                var item = new MessageModel(messages[i]);
                                listItems.Add(item);
                            }
                            else
                            {
                                var item = new MessageModel(messages[i]);
                                listItems.Add(item);
                            }
                        }
                    }
                    else
                    {
                        listItems.Add(new MessageModel(messages[i].timestamp()));
                        var item = new MessageModel(messages[i]);
                        listItems.Add(item);
                    }
                }
                else
                {
                    if (messages[i].timestamp() - messages[i - 1].timestamp() > timeSpan || messages[i].timestamp() - messages[i - 1].timestamp() < -timeSpan)
                    {
                        listItems.Add(new MessageModel(messages[i].timestamp()));
                        var item = new MessageModel(messages[i]);
                        listItems.Add(item);
                    }
                    else
                    {
                        var item = new MessageModel(messages[i]);
                        listItems.Add(item);
                    }
                }
            }
            return listItems;
        }
        /// <summary>
        /// 头像右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_headRightClick(object sender, EventArgs e)
        {
            headContextMenuStrip.Items.Clear();
            if(conversation.conversationType() == EMConversationType.GROUPCHAT)
            {
                if (mine.is_administrator == 2 && mine.is_manager == 2)
                {
                    return;
                }
                selectRightItem = sender as ChatHistoryListItem;
                if (selectRightItem == null)
                {
                    return;
                }
                if (selectRightItem.model.isTime)
                {
                    return;
                }
                if (selectRightItem.model.isshowTest)
                {
                    return;
                }
                DuiMouseEventArgs args = e as DuiMouseEventArgs;
                if (args == null)
                {
                    return;
                }
                var user = DBHelper.Instance.GetGroupUser(selectRightItem.model.message.from(), conversation.conversationId());
                if (user != null)
                {
                    if(mine.is_manager==1)
                    {
                        if (user.is_administrator == 1 || user.is_manager == 1)
                            return;
                        else
                        {
                            if (user.is_shield == 1)
                            {
                                headContextMenuStrip.Items.Add(解除禁言ToolStripMenuItem);
                            }
                            else
                            {
                                headContextMenuStrip.Items.Add(禁言ToolStripMenuItem);
                            }
                            headContextMenuStrip.Items.Add(踢人ToolStripMenuItem);
                            headContextMenuStrip.Show(new Point(selectRightItem.headX + args.X, selectRightItem.headY + args.Y));
                        }
                    }
                    if (mine.is_administrator == 1)
                    {
                        if (user.is_manager == 1)
                        {
                            headContextMenuStrip.Items.Add(踢人ToolStripMenuItem);
                            headContextMenuStrip.Show(new Point(selectRightItem.headX + args.X, selectRightItem.headY + args.Y));
                        }
                        else
                        {
                            if (user.is_shield == 1)
                            {
                                headContextMenuStrip.Items.Add(解除禁言ToolStripMenuItem);
                            }
                            else
                            {
                                headContextMenuStrip.Items.Add(禁言ToolStripMenuItem);
                            }
                            headContextMenuStrip.Items.Add(踢人ToolStripMenuItem);
                            headContextMenuStrip.Show(new Point(selectRightItem.headX + args.X, selectRightItem.headY + args.Y));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 消息右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_messageRightClick(object sender, EventArgs e)
        {
            selectRightItem = sender as ChatHistoryListItem;
            if (selectRightItem == null)
            {
                return;
            }
            if (selectRightItem.model.isTime)
            {
                return;
            }
            if (selectRightItem.model.isshowTest)
            {
                return;
            }
            DuiMouseEventArgs args = e as DuiMouseEventArgs;
            if (args == null)
            {
                return;
            }
            chatContextMenuStrip.Items.Clear();
            if(selectRightItem.model.bodyType== EMMessageBodyType.TEXT&&!selectRightItem.model.isshowTest&&!selectRightItem.model.isIDCard&&!selectRightItem.noSupport&&!selectRightItem.model.isGifFace)
            {
                chatContextMenuStrip.Items.Add(复制ToolStripMenuItem);
                chatContextMenuStrip.Items.Add(删除消息ToolStripMenuItem);
                chatContextMenuStrip.Show(new Point(selectRightItem.bubbleX + args.X + 5, selectRightItem.bubbleY + args.Y + 5));
            }
            else
            {
                chatContextMenuStrip.Items.Add(删除消息ToolStripMenuItem);
                chatContextMenuStrip.Show(new Point(selectRightItem.bubbleX + args.X, selectRightItem.bubbleY + args.Y));
            }
        }
        /// <summary>
        /// 头像左键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_headClick(object sender, EventArgs e)
        {
            selectRightItem = sender as ChatHistoryListItem;
            if (selectRightItem == null)
            {
                return;
            }
            if (selectRightItem.model.isTime)
            {
                return;
            }
            if (selectRightItem.model.isshowTest)
            {
                return;
            }
            if (selectRightItem.model.isSender)
            {
                return;
            }
            if(conversation.conversationType() == EMConversationType.CHAT)
            {
                if (DBHelper.Instance.checkFriend(conversation.conversationId()))
                {
                    UserInfoForm userInfo = new UserInfoForm(friend.toFriend());
                    
                    userInfo.Show();
                }
            }
            else
            {
                var user = DBHelper.Instance.GetGroupUser(selectRightItem.model.message.from(), conversation.conversationId());
                if (user != null)
                {
                    GroupUserInfoForm form = new GroupUserInfoForm(group, user);
                    form.Show();
                }
                else
                {
                    UserInfoMenager.shard.getGroupUser(conversation.conversationId(), selectRightItem.model.message.from(), (d) =>
                    {
                        if (d != null)
                        {
                            GroupUser u = new GroupUser();
                            u.groupID = d.group_id;
                            u.userID = d.user_id;
                            u.avatar = d.portrait;
                            u.friend_name = d.friend_name;
                            u.group_user_nickname = d.group_user_nickname;
                            u.id_card = d.id_card;
                            u.inv_name = d.inv_name;
                            u.is_administrator = d.is_administrator;
                            u.is_manager = d.is_manager;
                            u.is_shield = d.is_shield;
                            u.user_name = d.user_name;
                            GroupUserInfoForm form = new GroupUserInfoForm(group, u);
                            form.Show();
                        }
                    });
                }
            }
        }
        /// <summary>
        /// 消息点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_messageClick(object sender, EventArgs e)
        {
            selectRightItem = sender as ChatHistoryListItem;
            if (selectRightItem == null)
            {
                return;
            }
            if (selectRightItem.model.isTime)
            {
                return;
            }
            if (selectRightItem.model.isshowTest)
            {
                return;
            }
            if(selectRightItem.model.bodyType== EMMessageBodyType.FILE)
            {
                if (File.Exists(selectRightItem.model.fileLocalPath))
                {
                    try
                    {
                        ProcessStartInfo info = new ProcessStartInfo(selectRightItem.model.fileLocalPath);
                        Process.Start(info);
                    }
                    catch
                    {
                        MessageBox.Show("不支持该格式");
                    }
                }
                else
                {
                    DownLoadChatFileManager.shard.downloadFileAttchment(selectRightItem.model.message, (msg) =>
                     {
                         EMFileMessageBody body = msg.bodies()[0] as EMFileMessageBody;
                         if (body != null)
                         {
                             selectRightItem.model.fileLocalPath = body.localPath();
                             if (File.Exists(selectRightItem.model.fileLocalPath))
                             {
                                 try
                                 {
                                     ProcessStartInfo info = new ProcessStartInfo(selectRightItem.model.fileLocalPath);
                                     Process.Start(info);
                                 }
                                 catch
                                 {
                                     this.BeginInvoke(new EventHandler((s, ee) =>
                                     {
                                         MessageBox.Show("不支持该格式");
                                     }));
                                 }
                             }
                             else
                             {
                                 MessageBox.Show("下载文件失败");
                             }
                         }
                         else
                         {
                             MessageBox.Show("数据错误");
                         }
                     });
                }
            }
            if(selectRightItem.model.bodyType== EMMessageBodyType.VOICE)
            {
                if (!selectRightItem.model.isSender)
                {
                    conversation.markMessageAsRead(selectRightItem.model.messageId, true);
                    selectRightItem.model.message.setIsRead(true);
                    selectRightItem.updateMessgeState(selectRightItem.model.message);
                    conversation.updateMessage(selectRightItem.model.message);
                    MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                    if (main != null)
                    {
                        main.refreshConversationList();
                    }
                }
                if (File.Exists(selectRightItem.model.fileLocalPath))
                {
                    AudioPlayer.shard.playAMR(selectRightItem.model.fileLocalPath);
                }
                else
                {
                    DownLoadChatFileManager.shard.downloadThumbnailFileAttchment(selectRightItem.model.message, (msg) =>
                    {
                        EMVoiceMessageBody body = msg.bodies()[0] as EMVoiceMessageBody;
                        if (body != null)
                        {
                            selectRightItem.model.fileLocalPath = body.localPath();
                            if (File.Exists(selectRightItem.model.fileLocalPath))
                            {
                                AudioPlayer.shard.playAMR(selectRightItem.model.fileLocalPath);
                            }
                            else
                            {
                                MessageBox.Show("下载文件失败");
                            }
                        }
                        else
                        {
                            MessageBox.Show("数据错误");
                        }
                    });
                }
            }
            if(selectRightItem.model.isGifFace || selectRightItem.model.bodyType== EMMessageBodyType.IMAGE)
            {
                List<ChatHistoryListItem> l = new List<ChatHistoryListItem>();
                foreach(ChatHistoryListItem i in chathistoryListBox.Items.ToList())
                {
                    if(i.model.isGifFace||i.model.bodyType== EMMessageBodyType.IMAGE)
                    {
                        l.Add(i);
                    }
                }
                PicturePreviewForm form = new PicturePreviewForm(l, l.IndexOf(selectRightItem));
                form.ShowDialog();
            }
        }
        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendButton_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            sendText();
        }

        private void ChatPanl_ParentChanged(object sender, EventArgs e)
        {
            if (Parent == null)
            {
                return;
            }
            if(Parent is DSkinTabPage)
            {
                maxBubbleWith = Parent.Width - 180 - avatarSize * 2 - PaddingLorR * 4;
            }
            else
            {
                maxBubbleWith = Parent.Width - avatarSize * 2 - PaddingLorR * 4;
            }
        }

        private void chatTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (chatTextBox.Items.Count > ChatContent.items.Count)
            //{
            //    while (chatTextBox.Items.Count > ChatContent.items.Count)
            //    {
            //        DuiChar cha = chatTextBox.Items[ChatContent.items.Count] as DuiChar;
            //        if (cha != null)
            //        {
            //            ChatContent.insertChar(cha.Char, ChatContent.items.Count);
            //        }
            //    }
            //}
            //if(chatTextBox.Items.Count< ChatContent.items.Count)
            //{
            //    if (chatTextBox.CaretIndex < ChatContent.items.Count && chatTextBox.CaretIndex < ChatContent.items.Count)
            //    {
            //        ChatContent.removeAt(chatTextBox.CaretIndex);
            //    }
            //}
            //chatTextBox.Invalidate();
        }
        /// <summary>
        /// 发送文字
        /// </summary>
        private void sendText()
        {
            if(conversation.conversationType()== EMConversationType.GROUPCHAT)
            {
                if (mine == null)
                {
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        MessageBox.Show("加载数据中,请稍后...");
                    }));
                    return;
                }
            }
            if (ChatContent.items.Count < 1)
            {
                chatTextBox.Items.Clear();
                chatTextBox.Text = string.Empty;
                chatTextBox.LayoutContent();
                ChatContent = new CLChatContent();
                return;
            }
            string normal = ChatContent.GetNormalString();
            if (string.IsNullOrEmpty(normal))
            {
                chatTextBox.Items.Clear();
                chatTextBox.Text = string.Empty;
                chatTextBox.LayoutContent();
                chatTextBox.Invalidate();
                ChatContent = new CLChatContent();
                return;
            }
            var encrypted = DCEncrypt.Encrypt(normal, DCEncrypt.key) + "_encode";
            chatTextBox.Items.Clear();
            chatTextBox.Text = string.Empty;
            chatTextBox.LayoutContent();
            ChatContent = new CLChatContent();
            using (EMTextMessageBody body = new EMTextMessageBody(encrypted))
            {
                EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, conversation.conversationId(), body, DCUtilTool.GetChatType(conversation));
                addActionToMessageQueue(() =>
                {
                    sendMessage(message);
                });
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        private void sendMessage(EMMessage message)
        {
            if(conversation.conversationType()== EMConversationType.GROUPCHAT)
            {
                if (mine.is_administrator == 2 && mine.is_manager == 2)
                {
                    if (group.is_all_banned == 1)
                    {
                        this.BeginInvoke(new EventHandler((s, e) =>
                        {
                            MessageBox.Show("全员禁言中...");
                        }));
                        return;
                    }
                    if (mine.is_shield == 1)
                    {
                        this.BeginInvoke(new EventHandler((s, e) =>
                        {
                            MessageBox.Show("你已被禁言");
                        }));
                        return;
                    }
                    long now = DCUtilTool.nowTimeSpan();
                    var msg = conversation.loadMoreMessages(null, 300, EMMessageSearchDirection.UP).ToList();
                    var sendedMSG = new List<EMMessage>();
                    foreach (EMMessage m in msg)
                    {
                        if (m.from() == SettingMenager.shard.userID)
                        {
                            System.Console.WriteLine(m.timestamp());
                            if (m.timestamp() > now - 60000)
                            {
                                sendedMSG.Add(m);
                            }
                        }
                    }
                    System.Console.WriteLine(sendedMSG.Count);
                    
                    if (sendedMSG.Count >= 10)
                    {
                        this.BeginInvoke((new EventHandler((s, e) =>
                        {
                            if (tipsPanl == null)
                            {
                                tipsPanl = new ChatTipsPanl();
                                tipsPanl.Location = new Point(0, chathistoryListBox.Height - 40);
                                tipsPanl.Size = new Size(chathistoryListBox.Width, 40);
                                this.DUIControls.Add(tipsPanl);
                            }
                            else
                            {
                                tipsPanl.DisposeCanvas();
                                this.DUIControls.Remove(tipsPanl);
                                tipsPanl = null;
                                tipsPanl = new ChatTipsPanl();
                                tipsPanl.Location = new Point(0, chathistoryListBox.Height - 40);
                                tipsPanl.Size = new Size(chathistoryListBox.Width, 40);
                                this.DUIControls.Add(tipsPanl);
                                this.Invalidate();
                            }
                            new Thread(new ThreadStart(() =>
                            {
                                Thread.Sleep(3000);
                                this.BeginInvoke(new EventHandler((ss, ee) =>
                                {
                                    if (tipsPanl != null)
                                    {
                                        this.DUIControls.Remove(tipsPanl);
                                        tipsPanl = null;
                                        this.Invalidate();
                                    }
                                }));
                            })).Start();
                        })));
                        return;
                    }
                }
            }
            message.setStatus(EMMessageStatus.DELIVERING);
            conversation.updateMessage(message);
            ChatHistoryListItem chatHistoryListItem = new ChatHistoryListItem(new MessageModel(message), this);
            chatHistoryListItem.messageClick += Item_messageClick;
            chatHistoryListItem.headClick += Item_headClick;
            chatHistoryListItem.headRightClick += Item_headRightClick;
            chatHistoryListItem.messageRightClick += Item_messageRightClick;
            if (datasouce.Count > 0)
            {
                EMMessage lastMessage = datasouce[datasouce.Count - 1];
                if (message.timestamp() - lastMessage.timestamp() < timeSpan)
                {
                    chathistoryListBox.BeginInvoke(() =>
                    {
                        chathistoryListBox.Items.Add(chatHistoryListItem);
                        chathistoryListBox.Value = 1;
                    });
                    needScrollToBottm = true;
                    chathistoryListBox.BeginInvoke(() =>
                    {
                        chathistoryListBox.Invalidate();
                    });
                }
                else
                {
                    ChatHistoryListItem time = new ChatHistoryListItem(new MessageModel(message.timestamp()), this);
                    chathistoryListBox.BeginInvoke(() =>
                    {
                        chathistoryListBox.Items.Add(time);
                        chathistoryListBox.Items.Add(chatHistoryListItem);
                        chathistoryListBox.Value = 1;
                    });
                    needScrollToBottm = true;
                    chathistoryListBox.BeginInvoke(() =>
                    {
                        chathistoryListBox.Invalidate();
                    });
                }
            }
            else
            {
                ChatHistoryListItem time = new ChatHistoryListItem(new MessageModel(message.timestamp()), this);
                chathistoryListBox.BeginInvoke(() =>
                {
                    chathistoryListBox.Items.Add(time);
                    chathistoryListBox.Items.Add(chatHistoryListItem);
                    chathistoryListBox.InnerScrollBar.Value = 1;
                });
                chathistoryListBox.BeginInvoke(() =>
                {
                    chathistoryListBox.Invalidate();
                });
            }
            datasouce.Add(message);
            EMCallback callback = new EMCallback();
            callback.onSuccessCallback = () =>
            {
                message.setStatus(EMMessageStatus.SUCCESS);
                conversation.updateMessage(message);
            };
            callback.onProgressCallback = (p) =>
            {

            };
            callback.onFailCallback = (e) =>
            {
                message.setStatus(EMMessageStatus.FAIL);
                conversation.updateMessage(message);
            };
            message.setCallback(callback);
            EaseHelper.shard.client.getChatManager().sendMessage(message);
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            main.refreshConversationList();
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="messages"></param>
        private void onMessageRecive(EMMessage[] messages)
        {
            addActionToMessageQueue(() =>
            {
                bool ishave = false;
                if (chathistoryListBox.Value > 0.98)
                {
                    ishave = true;
                }
                List<EMMessage> own = new List<EMMessage>();
                foreach (EMMessage message in messages)
                {
                    if (message.conversationId() == conversation.conversationId())
                    {
                        ishave = true;
                        own.Add(message);
                        GC.AddMemoryPressure(8 * 1024 * 4);
                        datasouce.Add(message);
                    }
                }
                var data = formatMessage(own.ToArray(), true);
                if (data.Count < 1)
                {
                    return;
                }
                chathistoryListBox.BeginInvoke(() =>
                {
                    List<ChatHistoryListItem> items = new List<ChatHistoryListItem>();
                    foreach (var item in data)
                    {
                        ChatHistoryListItem i = new ChatHistoryListItem(item, this);
                        if (!item.isTime && !item.isshowTest)
                        {
                            i.messageClick += Item_messageClick;
                            i.messageRightClick += Item_messageRightClick;
                            i.headClick += Item_headClick;
                            i.headRightClick += Item_headRightClick;
                        }
                        items.Add(i);
                    }
                    chathistoryListBox.Items.AddRange(items);
                });
                if (topLeave)
                {
                    if (Visible)
                    {
                        conversation.markAllMessagesAsRead();
                    }
                    MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                    if (main != null)
                    {
                        main.refreshConversationList();
                    }
                }
                if (ishave)
                {
                    chathistoryListBox.BeginInvoke(() =>
                    {
                        chathistoryListBox.Value = 1;
                        chathistoryListBox.Invalidate();
                    });
                }
                else
                {
                    chathistoryListBox.BeginInvoke(() =>
                    {
                        chathistoryListBox.Invalidate();
                    });
                }
            });
        }
        /// <summary>
        /// 接收cmd
        /// </summary>
        /// <param name="cmdMessages"></param>
        private void onCmdMessageRecive(EMMessage[] cmdMessages)
        {
            addActionToMessageQueue(() =>
            {
                foreach (EMMessage item in cmdMessages)
                {
                    if (item.conversationId() == conversation.conversationId())
                    {
                        EMCmdMessageBody body = item.bodies()[0] as EMCmdMessageBody;
                        if (body.action() == "TypingBegin")
                        {

                        }
                        else if (body.action() == "TypingEnd")
                        {

                        }
                    }
                }
            });
        }
        /// <summary>
        /// 送达回执
        /// </summary>
        /// <param name="messages"></param>
        private void onReceiveHasDeliveredAcks(EMMessage[] messages)
        {

        }
        /// <summary>
        /// 已读回执
        /// </summary>
        /// <param name="messages"></param>
        private void onReciveHasReadAcks(EMMessage[] messages)
        {

        }
        /// <summary>
        /// 消息状态改变
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        private void onMessageStateChanged(EMMessage message,EMError error)
        {
            addActionToMessageQueue(() =>
            {
                bool ishaave = false;
                for (int i = 0; i < datasouce.Count; i++)
                {
                    if (datasouce[i].msgId() == message.msgId())
                    {
                        datasouce[i] = message;
                        ishaave = true;
                        break;
                    }
                }
                if (!ishaave)
                {
                    return;
                }
                for (int i = 0; i < chathistoryListBox.Items.Count; i++)
                {
                    ChatHistoryListItem m = chathistoryListBox.Items[i] as ChatHistoryListItem;
                    if (m.model.messageId == message.msgId())
                    {
                        MessageModel model = new MessageModel(message);
                        chathistoryListBox.BeginInvoke(() =>
                        {
                            ChatHistoryListItem nm = new ChatHistoryListItem(model, this);
                            nm.messageClick += Item_messageClick;
                            nm.headClick += Item_headClick;
                            nm.headRightClick += Item_headRightClick;
                            nm.messageRightClick += Item_messageRightClick;
                            chathistoryListBox.Items.RemoveAt(i);
                            chathistoryListBox.Items.Insert(i, nm);
                            chathistoryListBox.Invalidate();
                        });
                        break;
                    }
                }
            });
        }

        private void ChatPanl_Enter(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 消息列表变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chathistoryListBox_SizeChanged(object sender, EventArgs e)
        {
            maxBubbleWith = chathistoryListBox.Width - avatarSize * 2 - PaddingLorR * 4;
            foreach (DuiBaseControl item in chathistoryListBox.Items)
            {
                item.Width = chathistoryListBox.Width;
            }
            chathistoryListBox.Invalidate();
        }
        /// <summary>
        /// 鼠标进入消息列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chathistoryListBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chathistoryListBox.Height < chathistoryListBox.ContentLength)
            {
                chathistoryListBox.InnerScrollBar.Visible = true;
            }
        }
        /// <summary>
        /// 鼠标离开消息列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chathistoryListBox_MouseLeave(object sender, EventArgs e)
        {
            chathistoryListBox.InnerScrollBar.Visible = false;
        }
        /// <summary>
        /// 表情按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void faceButton_MouseClick(object sender, DuiMouseEventArgs e)
        {
            Point point = new Point(chatToolbar.Location.X + fileButton.Location.X, chatToolbar.Location.Y + fileButton.Location.Y);
            FacePanl facePanl = new FacePanl();
            point.Y -= facePanl.Height;
            facePanl.Location = point;
            facePanl.click = faceClick;
            facePanl.FocusedChanged += FacePanl_FocusedChanged;
            this.DUIControls.Add(facePanl);
            this.BeginInvoke(new EventHandler((s, ee) =>
            {
                facePanl.Focus();
                this.Invalidate();
            }));
        }

        private void FacePanl_FocusedChanged(object sender, EventArgs e)
        {
            var pan = sender as FacePanl;
            this.BeginInvoke(new EventHandler((s, se) =>
            {
                if (pan != null)
                {
                    if (!pan.Focused)
                    {
                        DUIControls.Remove(pan);
                        this.Invalidate();

                    }
                }
            }));
        }
        /// <summary>
        /// 表情点击
        /// </summary>
        /// <param name="emotion"></param>
        private void faceClick(EmotionModel emotion)
        {
            foreach (DuiBaseControl item in DUIControls.ToList())
            {
                if(item is FacePanl)
                {
                    DUIControls.Remove(item);
                    break;
                }
            }
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                this.Invalidate();
            }));
            chatTextBox.BeginInvoke(() =>
            {
                DuiPictureBox p = chatTextBox.InsertImage(chatTextBox.CaretIndex, emotion.face);
                if (p != null)
                {
                    p.Size = DuiChar.MeasureChar('啊', chatTextBox.Font);
                    p.Width = p.Height;
                    chatTextBox.LayoutContent();
                    chatTextBox.Invalidate();
                    ChatContent.insertEmotion(emotion, chatTextBox.CaretIndex);
                    chatTextBox.CaretIndex+=1;
                }
                chatTextBox.Focus();
            });
        }
        /// <summary>
        /// 文件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileButton_MouseClick(object sender, DuiMouseEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "所有文件(*.*)|*.*";
            openFileDialog.Title = "请选择文件";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var fi = new FileInfo(filePath);
                if (fi.Length >1024 * 1024 * 10)
                {
                    MessageBox.Show("文件大于10M不能发送");
                    return;
                }
                try
                {
                    Image img = Image.FromFile(filePath);
                    sendImage(img);
                }catch(OutOfMemoryException ex)
                {
                    EMFileMessageBody body = new EMFileMessageBody(filePath, EMMessageBodyType.FILE);
                    body.setFileLength(fi.Length);
                    sendFile(body);
                }
            }
        }
        /// <summary>
        /// 文件发送
        /// </summary>
        /// <param name="body"></param>
        private void sendFile(EMMessageBody body)
        {
            EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, conversation.conversationId(), body, DCUtilTool.GetChatType(conversation));
            addActionToMessageQueue(() =>
            {
                sendMessage(message);
            });
        }
        /// <summary>
        /// 消息列表鼠标滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chathistoryListBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (needScrollToBottm)
            {
                needScrollToBottm = false;
            }
            if (chathistoryListBox.Value == 0 && e.Delta > 0)
            {
                if (!isLoadding)
                {
                    isLoadding = true;
                    addActionToMessageQueue(loadMessage);
                }
            }
            if (chathistoryListBox.Height < chathistoryListBox.ContentLength)
            {
                chathistoryListBox.InnerScrollBar.Visible = true;
            }
        }
        /// <summary>
        /// 至于最上层
        /// </summary>
        public void goToTop()
        {
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main != null)
            {
                main.allChatToBtton();
            }
            topLeave = true;
            chathistoryListBox.LayoutContent();
            BringToFront();
            this.Invalidate();
        }

        private void chathistoryListBox_LayoutContented_1(object sender, EventArgs e)
        {
            if (needScrollToBottm)
            {
                chathistoryListBox.Value = 1;
            }
            chathistoryListBox.Invalidate();
        }

        private void 删除消息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectRightItem != null)
            {
                if(MessageBox.Show("你确定删除这条消息（本地删除）吗?","提示",MessageBoxButtons.OKCancel)== DialogResult.OK)
                {
                    addActionToMessageQueue(() =>
                    {
                        conversation.removeMessage(selectRightItem.model.message);
                        int index = datasouce.IndexOf(selectRightItem.model.message);
                        if (index > -1)
                        {
                            datasouce.RemoveAt(index);
                        }
                        chathistoryListBox.BeginInvoke(() =>
                        {
                            int idx = chathistoryListBox.Items.IndexOf(selectRightItem);
                            if (idx > -1)
                            {
                                chathistoryListBox.Items.RemoveAt(idx);
                            }
                            if (idx > 0)
                            {
                                ChatHistoryListItem it = chathistoryListBox.Items[idx - 1] as ChatHistoryListItem;
                                if (it.model.isTime)
                                {
                                    chathistoryListBox.Items.RemoveAt(idx - 1);
                                }
                            }
                            chathistoryListBox.Invalidate();
                        });
                    });
                }
            }
        }

        private void 禁言ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectRightItem != null)
            {
                if(MessageBox.Show("你确定要禁言Ta吗？","提示", MessageBoxButtons.OKCancel)== DialogResult.OK)
                {
                    GroupShieldSingleSendModel m = new GroupShieldSingleSendModel();
                    m.group_id = selectRightItem.model.message.conversationId();
                    m.groupUserId = selectRightItem.model.message.from();
                    HttpUitls.Instance.get<BaseReciveModel>("groupUser/setShieldSingle", m, (json) =>
                    {
                        if (json.code == 200)
                        {
                            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                            if (main != null)
                            {
                                ThreadPool.QueueUserWorkItem(main.getGroupMembers, selectRightItem.model.message.conversationId());
                            }
                            EMCmdMessageBody body = new EMCmdMessageBody("");
                            EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, selectRightItem.model.message.from(), body, EMChatType.SINGLE);
                            message.setAttribute("type", "qun_shield");
                            message.setAttribute("id", selectRightItem.model.message.conversationId());
                            message.setAttribute("userid", selectRightItem.model.message.from());
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
            }
        }

        private void 踢人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectRightItem != null)
            {
                if(MessageBox.Show("你确定要踢出Ta吗?","提示", MessageBoxButtons.OKCancel)== DialogResult.OK)
                {
                    AddGroupUserSendModel m = new AddGroupUserSendModel();
                    m.group_id = selectRightItem.model.message.conversationId();
                    m.group_user_ids = selectRightItem.model.message.from();
                    HttpUitls.Instance.get<BaseReciveModel>("groupUser/removeBatch", m, (json) =>
                    {
                        if (json.code == 200)
                        {
                            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                            if (main != null)
                            {
                                ThreadPool.QueueUserWorkItem(main.getGroupMembers, selectRightItem.model.message.conversationId());
                            }
                            EMCmdMessageBody body = new EMCmdMessageBody("");
                            EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, selectRightItem.model.message.from(), body, EMChatType.SINGLE);
                            message.setAttribute("type", "qun");
                            message.setAttribute("id", selectRightItem.model.message.conversationId());
                            EaseHelper.shard.client.getChatManager().sendMessage(message);
                            this.BeginInvoke(new EventHandler((ss, ee) =>
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
            }
        }

        private void 解除禁言ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectRightItem != null)
            {
                GroupShieldSingleSendModel m = new GroupShieldSingleSendModel();
                m.group_id = selectRightItem.model.message.conversationId();
                m.groupUserId = selectRightItem.model.message.from();
                HttpUitls.Instance.get<BaseReciveModel>("groupUser/cancelShieldSingle", m, (json) =>
                {
                    if (json.code == 200)
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            ThreadPool.QueueUserWorkItem(main.getGroupMembers, selectRightItem.model.message.conversationId());
                        }
                        EMCmdMessageBody body = new EMCmdMessageBody("");
                        EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, selectRightItem.model.message.from(), body, EMChatType.SINGLE);
                        message.setAttribute("type", "qun_shield");
                        message.setAttribute("id", selectRightItem.model.message.conversationId());
                        message.setAttribute("userid", selectRightItem.model.message.from());
                        message.setAttribute("qun_shield", "2");
                        EaseHelper.shard.client.getChatManager().sendMessage(message);
                        this.BeginInvoke(new EventHandler((ss, ee) =>
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
        }
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="msgId"></param>
        public void deleteMessgae(string msgId)
        {
            addActionToMessageQueue(() =>
            {
                for (int i = 0; i < datasouce.Count; i++)
                {
                    if (datasouce[i].msgId() == msgId)
                    {
                        datasouce.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < chathistoryListBox.Items.Count; i++)
                {
                    ChatHistoryListItem item = chathistoryListBox.Items[i] as ChatHistoryListItem;
                    if (item != null)
                    {
                        if (item.model.messageId == msgId)
                        {
                            chathistoryListBox.BeginInvoke(() =>
                            {
                                chathistoryListBox.Items.RemoveAt(i);
                                if (i > 0)
                                {
                                    ChatHistoryListItem orTime = chathistoryListBox.Items[i-1] as ChatHistoryListItem;
                                    if (orTime.model.isTime)
                                    {
                                        chathistoryListBox.Items.RemoveAt(i - 1);
                                    }
                                }
                            });
                            break;
                        }
                    }
                }
            });
        }

        private void chatTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void chatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.S && e.Alt)
            {
                sendText();
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectRightItem != null)
            {
                
                var t = new Thread(new ThreadStart(copyTreadSub));
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void chatTextBox_onPasteImage(object sender, Image e)
        {
            sendImage(e);
        }

        private void chatTextBox_onSend(object sender, EventArgs e)
        {
            sendText();
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="image"></param>
        private void sendImage(Image image)
        {
            addActionToMessageQueue(() =>
            {
                try
                {
                    string dir = Directory.GetCurrentDirectory() + "\\ChangLiao\\" + SettingMenager.shard.idCard;
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string path = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff.jpg");
                    image.Save(path, ImageFormat.Jpeg);
                    string tPath = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff.jpg");
                    if (image.Width > image.Height)
                    {
                        var dt = ZoomImage(image, Convert.ToInt32((double)image.Height / (double)image.Width * 170.0), 170);
                        dt.Save(tPath, ImageFormat.Jpeg);
                    }
                    else
                    {
                        var dt = ZoomImage(image, Convert.ToInt32((double)image.Height / (double)image.Width * 170.0), 170);
                        dt.Save(tPath, ImageFormat.Jpeg);
                    }
                    if (File.Exists(path) && File.Exists(tPath))
                    {
                        EMImageMessageBody body = new EMImageMessageBody(path, tPath);
                        sendFile(body);
                    }
                    else
                    {
                        MessageBox.Show("图片处理失败");
                    }
                }
                catch
                {

                }
            });
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        /// <returns></returns>
        private Image ZoomImage(Image bitmap, int destHeight, int destWidth)
        {
            try
            {
                System.Drawing.Image sourImage = bitmap;
                int width = 0, height = 0;
                //按比例缩放           
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                Bitmap destBitmap = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);
                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                //设置压缩质量     
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }
        /// <summary>
        /// 文字粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatTextBox_onPasteText(object sender, string e)
        {
            CLChatContent content = new CLChatContent(e);
            chatTextBox.BeginInvoke(() =>
            {
                foreach (var item in content.items)
                {
                    if (item.type == 2)
                    {
                        DuiPictureBox p = chatTextBox.InsertImage(chatTextBox.CaretIndex, item.emotion.face);
                        p.Size = DuiChar.MeasureChar('啊', chatTextBox.Font);
                        p.Width = p.Height;
                        ChatContent.insertEmotion(item.emotion, chatTextBox.CaretIndex);
                        chatTextBox.CaretIndex += 1;
                    }
                    else
                    {
                        chatTextBox.InsertText(item.ch.ToString());
                    }
                }
                chatTextBox.LayoutContent();
                chatTextBox.Invalidate();
            });
        }
        /// <summary>
        /// 复制消息
        /// </summary>
        [STAThread]
        void copyTreadSub()
        {
            if (selectRightItem.textMSGLable.startIndex == -1 || selectRightItem.textMSGLable.endIndex == -1)
            {
                selectRightItem.textMSGLable.selectAll();
            }
            string text = selectRightItem.textMSGLable.getSelectText();
            try
            {
                Clipboard.SetText(text, TextDataFormat.UnicodeText);
                selectRightItem.textMSGLable.clearSelect();
            }
            catch
            {
                try
                {
                    Application.DoEvents();
                    Clipboard.SetText(text, TextDataFormat.UnicodeText);
                    selectRightItem.textMSGLable.clearSelect();
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 消息列表右键菜单消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if(e.CloseReason!= ToolStripDropDownCloseReason.ItemClicked)
            {
                if (selectRightItem != null)
                {
                    if (selectRightItem.textMSGLable.Visible)
                    {
                        selectRightItem.textMSGLable.clearSelect();
                    }
                }
                selectRightItem = null;
            }
        }

        private void chathistoryListBox_ItemAdded(object sender, DuiControlEventArgs e)
        {
            
        }

        private void chathistoryListBox_ItemRemoved(object sender, DuiControlEventArgs e)
        {

        }

        private void chatTextBox_ControlRemoved(object sender, DuiControlEventArgs e)
        {
            
        }
        /// <summary>
        /// 输入框复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatTextBox_onCopy(object sender, EventArgs e)
        {
            var t = new Thread(new ThreadStart(copyTreadSub2));
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
        }
        /// <summary>
        /// 复制线程
        /// </summary>
        private void copyTreadSub2()
        {
            var s = ChatContent.GetSelectionText(chatTextBox.SelectionStart, chatTextBox.SelectionLength);
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            try
            {
                Clipboard.SetText(s, TextDataFormat.UnicodeText);
            }
            catch
            {
                try
                {
                    Application.DoEvents();
                    Clipboard.SetText(s, TextDataFormat.UnicodeText);
                }
                catch { }
            }
        }

        private void ChatPanl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                addActionToMessageQueue(() =>
                {
                    foreach (var item in datasouce)
                    {
                        if (!item.isRead())
                        {
                            conversation.markMessageAsRead(item.msgId(), true);
                        }
                    }
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.refreshConversationList();
                    }
                });
            }
        }

        private void chathistoryListBox_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
