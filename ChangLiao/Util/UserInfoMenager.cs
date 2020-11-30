using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;
using ChangLiao.windows;
using ChangLiao.DB;
using System.Windows.Forms;
using System.Threading;

namespace ChangLiao.Util
{
    /// <summary>
    /// 用户信息加载器
    /// </summary>
    class UserInfoMenager
    {
        /// <summary>
        /// 加载群成员
        /// </summary>
        struct GroupUser
        {
            public string userid { get; set; }
            public string groupId { get; set; }
        }
        /// <summary>
        /// 加载用户等等模型
        /// </summary>
        struct UserWaait
        {
            public string id { get; set; }
            public UserComplite complite { get; set; }
        }
        /// <summary>
        /// 加载群成员等待模型
        /// </summary>
        struct GroupUserWait
        {
            public GroupUser groupUser { get; set; }
            public GroupUserComplite complite { get; set; }
        }
        /// <summary>
        /// 用户信息加载完成回调
        /// </summary>
        /// <param name="model">用户模型</param>
        public delegate void UserComplite(GetUserData model);
        /// <summary>
        /// 群成员加载完成回调
        /// </summary>
        /// <param name="model">群成员模型</param>
        public delegate void GroupUserComplite(GroupMemberData model);
        private static readonly object padlock = new object();
        private static UserInfoMenager menager;
        private static readonly object ulock = new object();
        private static readonly object guLock = new object();
        private List<string> user;
        private List<UserWaait> userWaaits;
        private List<GroupUser> groupUser;
        private List<GroupUserWait> groupUserWaits;
        public static UserInfoMenager shard
        {
            get
            {
                lock (padlock)
                {
                    if (menager == null)
                    {
                        menager = new UserInfoMenager();
                    }
                    return menager;
                }
            }
        }

        private UserInfoMenager()
        {
            user = new List<string>();
            userWaaits = new List<UserWaait>();
            groupUser = new List<GroupUser>();
            groupUserWaits = new List<GroupUserWait>();
        }
        /// <summary>
        /// 拉去用户数据
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="complite">完成回调</param>
        public void GetUser(string userID,UserComplite complite)
        {
            lock (ulock)
            {
                if (user.Contains(userID))
                {
                    return;
                }
                user.Add(userID);
            }
            ThreadPool.QueueUserWorkItem((o) =>
            {
                GetUserSendModel model = new GetUserSendModel();
                model.user_id = userID;
                HttpUitls.Instance.get<GetUserReciveModel>("user/getUserByUserId", model, (json) =>
                {
                    if (json.code == 200)
                    {
                        complite(json.data);
                        DBHelper.Instance.addStronger(json.data);
                    }
                    else
                    {
                        if (json.message.Contains("重新登录"))
                        {
                            MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
                            frm.gotoLogin();
                        }
                    }
                    lock (ulock)
                    {
                        if (user.IndexOf(userID) > -1)
                        {
                            user.RemoveAt(user.IndexOf(userID));
                        }
                    }
                }, (code) =>
                {
                    if (code > 500 && code < 503)
                    {
                        MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
                        frm.gotoLogin();
                    }
                    lock (ulock)
                    {
                        if (user.IndexOf(userID) > -1)
                        {
                            user.RemoveAt(user.IndexOf(userID));
                        }
                    }
                });
            });
        }
        /// <summary>
        /// 拉去群成员信息
        /// </summary>
        /// <param name="groupID">群ID</param>
        /// <param name="userID">userID</param>
        /// <param name="complite">完成回调</param>
        public void getGroupUser(string groupID,string userID,GroupUserComplite complite)
        {
            GroupUser group = new GroupUser();
            group.groupId = groupID;
            group.userid = userID;
            lock (guLock)
            {
                if (groupUser.Contains(group))
                {
                    return;
                }
                groupUser.Add(group);
            }
            ThreadPool.QueueUserWorkItem((o) =>
            {
                GetOneGroupMemberSendModel model = new GetOneGroupMemberSendModel();
                model.group_id = groupID;
                model.target_user_id = userID;
                HttpUitls.Instance.get<GroupOneMemberReciveModel>("groupUser/getOneGroupUserInfo", model, (json) =>
                {
                    if (json.code == 200)
                    {
                        complite(json.data);
                        DBHelper.Instance.addGroupMember(json.data);
                    }
                    else
                    {
                        if (json.message.Contains("重新登录"))
                        {
                            MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
                            frm.gotoLogin();
                        }
                    }
                    lock (guLock)
                    {
                        if (groupUser.Contains(group))
                        {
                            groupUser.Remove(group);
                        }
                    }
                }, (code) => {
                    if (code > 500 && code < 503)
                    {
                        MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
                        frm.gotoLogin();
                    }
                    lock (guLock)
                    {
                        if (groupUser.Contains(group))
                        {
                            groupUser.Remove(group);
                        }
                    }
                });
            });
        }
    }
}
