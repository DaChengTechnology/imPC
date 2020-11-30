using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EaseMobLib;
using ChangLiao.Util;
using ChangLiao.windows;
using System.Windows.Forms;
using System.Threading;
using ChangLiao.DB;
using ChangLiao.Model.ViewModel;
using Newtonsoft.Json;

namespace ChangLiao.Ease
{
    class EaseHelper
    {
        static EaseHelper instance;
        private static readonly object padlock = new object();
        public EMClient client;
        private EMConnectionListener ChangLiaoConnection;
        private EMChatManagerListener ChangLiaoChatManager;
        private EMContactListener ContactListener;
        private EMGroupManagerListener GroupManagerListener;
        public delegate void EMCallError(EMError error);
        private BackgroundQueue queue;
        public string createdGroupId { get; set; }
        private bool relogin;
        public static EaseHelper shard
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new EaseHelper();
                    }
                    return instance;
                }
            }
        }
        private EaseHelper()
        {
            EMChatConfigs configs = new EMChatConfigs(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao", /*System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)+ "\\Changliao"*/System.Environment.CurrentDirectory, AppSettingHelper.getAppConfig("hxKey"),0);
            client = EMClient.create(configs);
            queue = new BackgroundQueue();
            ChangLiaoConnection = new EMConnectionListener();
            ChangLiaoConnection.onConntect = onConnect;
            ChangLiaoConnection.onDisconnect = onDisconnect;
            ChangLiaoConnection.onPong = onPong;
            client.addConnectionListener(ChangLiaoConnection);
            ChangLiaoChatManager = new EMChatManagerListener();
            ChangLiaoChatManager.onReceiveMessages = onReciveMessage;
            ChangLiaoChatManager.onReceiveCmdMessages = onReciveCmdMessage;
            ContactListener = new EMContactListener();
            ContactListener.onContactAdded = onContactAdded;
            ContactListener.onContactDeleted = onContactDeleted;
            ContactListener.onContactInvited = onContactInvited;
            client.getChatManager().addListener(ChangLiaoChatManager);
            client.getContactManager().registerContactListener(ContactListener);
            GroupManagerListener = new EMGroupManagerListener();
            GroupManagerListener.onLeaveGroup = onLeaveGroup;
            GroupManagerListener.onReceiveJoinGroupApplication = onReceiveJoinGroupApplication;
            GroupManagerListener.onMemberLeftGroup = onMemberLeaveGroup;
            GroupManagerListener.onMemberJoinedGroup = onMemberJoinedGroup;
            GroupManagerListener.onAutoAcceptInvitationFromGroup = onAutoAcceptInvitationFromGroup;
            GroupManagerListener.onUpdateMyGroupList = onUpdateMyGroupList;
            client.getGroupManager().addListener(GroupManagerListener);
            relogin = false;
        }
        /// <summary>
        /// 环信登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="callError">报错信息</param>
        public void login(string username, string password, EMCallError callError)
        {
            Console.WriteLine("进来了");
            Task t = new Task(() =>
            {
                relogin = false;
                EMError err = client.login(username, password);
                callError(err);
            });
            t.Start();
        }
        /// <summary>
        /// 获取会话列表
        /// </summary>
        /// <returns></returns>
        public List<EMConversation> getAllConversation()
        {
            var con = client.getChatManager().getConversations();
            List<EMConversation> mConversations = new List<EMConversation>();
            foreach (var c in con)
            {
                mConversations.Add(c);
            }
            return mConversations;
        }
        /// <summary>
        /// 登出
        /// </summary>
        public void LoginOut()
        {
            client.logout();
        }
        /// <summary>
        /// 收到普通信息
        /// </summary>
        /// <param name="mMessages"></param>
        private void onReciveMessage(EMMessage[] mMessages)
        {
            AudioPlayer.shard.playTips();
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.refreshConversationList();
        }
        /// <summary>
        /// 接收Cmd消息
        /// </summary>
        /// <param name="messages"></param>
        private void onReciveCmdMessage(EMMessage[] messages)
        {
            foreach (EMMessage message in messages)
            {
                string own = "";
                if (message.getAttribute("own", out own))
                {
                    var data = JsonConvert.DeserializeObject<List<DeleteMessageDataModel>>(own);
                    foreach (var item in data)
                    {
                        EMConversation conversation = client.getChatManager().conversationWithType(item.userid, EMConversationType.CHAT, false);
                        if (conversation != null)
                        {
                            conversation.removeMessage(item.messageId);
                        }
                        MainFrm m = (MainFrm)Application.OpenForms["MainFrm"];
                        if (m == null)
                        {
                            continue;
                        }
                        m.deleteMesssage(item.userid, item.messageId);

                    }
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main == null)
                    {
                        return;
                    }
                    main.refreshConversationList();
                }
                string type;
                if (message.getAttribute("type", out type))
                {
                    if (type == "personMSG")
                    {
                        string userid = "";
                        string nsid = "";
                        message.getAttribute("userid", out userid);
                        message.getAttribute("msgid", out nsid);
                        EMConversation conversation = client.getChatManager().conversationWithType(message.conversationId(), EMConversationType.CHAT, false);
                        if (conversation != null)
                        {
                            EMMessage mMessage = conversation.loadMessage(nsid);
                            if (mMessage != null)
                            {
                                conversation.removeMessage(mMessage);
                            }
                        }
                        MainFrm m = (MainFrm)Application.OpenForms["MainFrm"];
                        if (m == null)
                        {
                            continue;
                        }
                        m.deleteMesssage(userid, nsid);
                        m.refreshConversationList();
                    }
                    if (type == "deleteMSG")
                    {
                        string userid = "";
                        string nsid = "";
                        message.getAttribute("userid", out userid);
                        message.getAttribute("msgid", out nsid);
                        EMConversation conversation = client.getChatManager().conversationWithType(message.conversationId(), EMConversationType.GROUPCHAT, false);
                        if (conversation != null)
                        {
                            EMMessage mMessage = conversation.loadMessage(nsid);
                            if (mMessage != null)
                            {
                                conversation.removeMessage(mMessage);
                            }
                        }
                        MainFrm m = (MainFrm)Application.OpenForms["MainFrm"];
                        if (m == null)
                        {
                            continue;
                        }
                        m.deleteMesssage(userid, nsid);
                        m.refreshConversationList();
                    }
                    if (type == "qunAdmin" || type == "qun" || type == "qun_shield")
                    {
                        MainFrm m = (MainFrm)Application.OpenForms["MainFrm"];
                        if (m == null)
                        {
                            continue;
                        }
                        string id = "";
                        message.getAttribute("id", out id);
                        m.refreshGroupInfo(id);
                    }
                }
            }
        }
        /// <summary>
        /// 好友添加成功
        /// </summary>
        /// <param name="user"></param>
        private void onContactAdded(string user)
        {
            AudioPlayer.shard.playTips();
            EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(user, EMConversationType.CHAT, true);
            EMTextMessageBody body = new EMTextMessageBody("我们成为了好友");
            EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, user, body, EMChatType.GROUP);
            conversation.insertMessage(message);
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.refreshFriendFromServer();
        }
        /// <summary>
        /// 联系人删除
        /// </summary>
        /// <param name="user"></param>
        private void onContactDeleted(string user)
        {
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.removeConversation(user);
            main.refreshFriendFromServer();
        }
        /// <summary>
        /// 收到好友申请
        /// </summary>
        /// <param name="username"></param>
        /// <param name="reason"></param>
        private void onContactInvited(string username, string reason)
        {
            AudioPlayer.shard.playTips();
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.loadAddFriend();
        }
        /// <summary>
        /// 离开群组
        /// </summary>
        /// <param name="group"></param>
        /// <param name="reason"></param>
        private void onLeaveGroup(EMGroup group, EMGroupLeaveReason reason)
        {
            DBHelper.Instance.DeleteGroup(group.groupId());
            client.getChatManager().removeConversation(group.groupId(), true);
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.removeConversation(group.groupId());
        }
        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="group"></param>
        /// <param name="inviter"></param>
        /// <param name="inviteMessage"></param>
        private void onAutoAcceptInvitationFromGroup(EMGroup group, string inviter, string inviteMessage)
        {
            EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(group.groupId(), EMConversationType.GROUPCHAT, true);
            EMTextMessageBody body = new EMTextMessageBody("你加入了群聊");
            EMMessage message = EMMessage.createSendMessage(SettingMenager.shard.userID, group.groupId(), body, EMChatType.GROUP);
            conversation.insertMessage(message);
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.refreshGroupInfo(group.groupId());
        }
        /// <summary>
        /// 废弃
        /// </summary>
        /// <param name="list"></param>
        private void onUpdateMyGroupList(EMGroup[] list)
        {
        }
        /// <summary>
        /// 废弃
        /// </summary>
        /// <param name="group"></param>
        /// <param name="from"></param>
        /// <param name="message"></param>
        private void onReceiveJoinGroupApplication(EMGroup group, string from, string message)
        {
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.refreshGroupInfo(group.groupId());
        }
        /// <summary>
        /// 废弃
        /// </summary>
        /// <param name="group"></param>
        /// <param name="member"></param>
        private void onMemberLeaveGroup(EMGroup group, string member)
        {
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.refreshGroupInfo(group.groupId());
        }
        /// <summary>
        /// 废弃
        /// </summary>
        /// <param name="group"></param>
        /// <param name="member"></param>
        private void onMemberJoinedGroup(EMGroup group, string member)
        {
            MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
            if (main == null)
            {
                return;
            }
            main.refreshGroupInfo(group.groupId());
        }
        /// <summary>
        /// 与环信服务器断开连接
        /// </summary>
        /// <param name="error"></param>
        private void onDisconnect(EMError error)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("em----------------------------------------Discont");
                if (error.errorCode == EMErrorCode.SERVER_UNKNOWN_ERROR)
                {
                    EMError err = client.login(SettingMenager.shard.userID, SettingMenager.shard.userID);
                    if(err.errorCode== EMErrorCode.EM_NO_ERROR)
                    {

                    }
                    else
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main == null)
                        {
                            if (relogin)
                                return;
                            relogin = true;
                            MessageBox.Show("未知错误，请重新登录");
                            return;
                        }
                        else
                        {
                            if (relogin)
                                return;
                            relogin = true;
                            main.BeginInvoke(new EventHandler((s, e) =>
                            {
                                MessageBox.Show("未知错误，请重新登录");
                            }));
                        }
                        
                    }
                }
            });
        }

        private void onConnect()
        {
            Console.WriteLine("EM-----------------------------------------connect");
        }

        private void onPong()
        {
            Console.WriteLine("EM,------------------------------------pong");
        }
    }
}
