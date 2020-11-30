using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Util;
using System.Data.Entity.Validation;
using ChangLiao.Model.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace ChangLiao.DB
{
    class DBHelper
    {
        static DBHelper helper;
        private static readonly object padlock = new object();
        private CLDBContext db;
        private BackgroundQueue queue;
        public static DBHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if(helper == null)
                    {
                        helper = new DBHelper();
                    }
                    return helper;
                }
            }
        }
        private DBHelper()
        {
            DbConnection sqliteCon = SQLiteProviderFactory.Instance.CreateConnection();
            sqliteCon.ConnectionString = "data source="+ System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao\\app\\"+SettingMenager.shard.idCard+"\\db\\cl.db;foreign keys=true";
            db = new CLDBContext(sqliteCon);
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;
            var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingCollection.GenerateViews(new List<EdmSchemaError>());
            queue = new BackgroundQueue();
        }

        public void Save()
        {
            queue.QueueTask(() =>
            {
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    System.Console.WriteLine(dbEx.Message);
                }
            });
        }
        
        private void Disposable()
        {
            db.Dispose();
        }

        public void AddFriend(List<FriendListData> datas)
        {
            queue.QueueTask(() =>
            {
                db.friend.RemoveRange(db.friend.AsEnumerable());
                db.SaveChanges();
                foreach (var f in datas)
                {
                    db.friend.Add(f.toFriend());
                    var sf = db.strongers.Where(p => p.userID.Equals(f.user_id) && p.groupID.Equals("Self")).FirstOrDefault();
                    if (sf != null)
                    {
                        sf.avatar = f.portrait;
                        sf.nickName = f.friend_self_name;
                        sf.idCard = f.id_card;
                    }
                    else
                    {
                        StrongerModel m = new StrongerModel();
                        m.userID = f.user_id;
                        m.groupID = "Self";
                        m.nickName = f.friend_self_name;
                        m.idCard = f.id_card;
                        m.avatar = f.portrait;
                    }
                }
                db.SaveChanges();
            });
        }

        public void addStronger(GetUserData model)
        {
            queue.QueueTask(() =>
            {
                var sf = db.strongers.Where(p => p.userID.Equals(model.user_id) && p.groupID.Equals("Self")).FirstOrDefault();
                if (sf != null)
                {
                    sf.avatar = model.portrait;
                    sf.nickName = model.user_name;
                    sf.idCard = model.id_card;
                }
                else
                {
                    StrongerModel m = new StrongerModel();
                    m.userID = model.user_id;
                    m.groupID = "Self";
                    m.nickName = model.user_name;
                    m.idCard = model.id_card;
                    m.avatar = model.portrait;
                    db.strongers.Add(m);
                }
                db.SaveChanges();
            });
        }
        public void addGroupTemp(List<MyGroupDataModel> datas)
        {
            queue.QueueTask(() =>
            {
                foreach (var g in datas)
                {
                    var gt = db.groupCaches.Where(p => p.groupID.Equals(g.group_id)).FirstOrDefault();
                    if (gt != null)
                    {
                        gt.groupName = g.group_name;
                        gt.avatar = g.group_portrait;
                    }
                    else
                    {
                        GroupCache gn = new GroupCache();
                        gn.groupID = g.group_id;
                        gn.groupName = g.group_name;
                        gn.avatar = g.group_portrait;
                        db.groupCaches.Add(gn);
                    }
                }
                var needDeleteGroup = db.group.ToList();
                foreach (var g in datas)
                {
                    for (int i = needDeleteGroup.Count - 1; i >= 0; i--)
                    {
                        if (needDeleteGroup[i].groupId.Equals(g.group_id))
                        {
                            needDeleteGroup.RemoveAt(i);
                            break;
                        }
                    }
                }
                var needDeleteFocus = db.focus.ToList();
                foreach (var gr in needDeleteGroup)
                {
                    db.group.Remove(gr);
                    foreach (var f in needDeleteFocus)
                    {
                        if (f.groupID.Equals(gr.groupId))
                        {
                            db.focus.Remove(f);
                        }
                    }
                }
            });
        }

        public void addGroupAndFocus(GroupInfoData data)
        {
            queue.QueueTask(() =>
            {
                var g = db.group.Where(p => p.groupId.Equals(data.group_id)).FirstOrDefault();
                if (g != null)
                {
                    g.groupName = data.group_name;
                    g.administrator_id = data.administrator_id;
                    g.avatar = data.group_portrait;
                    g.groupUserSum = data.groupUserSum;
                    g.group_type = data.group_type;
                    g.is_admin = data.is_admin;
                    g.is_all_banned = data.is_all_banned;
                    g.is_menager = data.is_menager;
                    g.is_pingbi = data.is_pingbi;
                    g.notice = data.notice;
                }
                else
                {
                    GroupTable gt = new GroupTable();
                    gt.groupId = data.group_id;
                    gt.groupName = data.group_name;
                    gt.administrator_id = data.administrator_id;
                    gt.avatar = data.group_portrait;
                    gt.groupUserSum = data.groupUserSum;
                    gt.group_type = data.group_type;
                    gt.is_admin = data.is_admin;
                    gt.is_all_banned = data.is_all_banned;
                    gt.is_menager = data.is_menager;
                    gt.is_pingbi = data.is_pingbi;
                    gt.notice = data.notice;
                    db.group.Add(gt);
                }
                db.SaveChanges();
                var list = db.focus.Where(p => p.groupID.Equals(data.group_id)).ToList();
                foreach (var f in list)
                {
                    db.focus.Remove(f);
                }
                db.SaveChanges();
                foreach (var df in data.focusList)
                {
                    FocusTable focus = new FocusTable();
                    focus.groupID = df.group_id;
                    focus.targetID = df.target_user_id;
                    db.focus.Add(focus);
                }
                db.SaveChanges();
            });
        }

        public void addGroupMember(GroupMemberData data)
        {
            queue.QueueTask(() =>
            {
                if (data == null)
                {
                    return;
                }
                var user = db.groupUsers.Where(p => p.groupID == data.group_id && p.userID == data.user_id).FirstOrDefault();
                if (user != null)
                {
                    user.groupID = data.group_id;
                    user.userID = data.user_id;
                    user.avatar = data.portrait;
                    user.friend_name = data.friend_name;
                    user.group_user_nickname = data.group_user_nickname;
                    user.id_card = data.id_card;
                    user.inv_name = data.inv_name;
                    user.is_administrator = data.is_administrator;
                    user.is_manager = data.is_manager;
                    user.is_shield = data.is_shield;
                    user.user_name = data.user_name;
                    db.SaveChanges();
                }
                else
                {
                    GroupUser user1 = new GroupUser();
                    user1.groupID = data.group_id;
                    user1.userID = data.user_id;
                    user1.avatar = data.portrait;
                    user1.friend_name = data.friend_name;
                    user1.group_user_nickname = data.group_user_nickname;
                    user1.id_card = data.id_card;
                    user1.inv_name = data.inv_name;
                    user1.is_administrator = data.is_administrator;
                    user1.is_manager = data.is_manager;
                    user1.is_shield = data.is_shield;
                    user1.user_name = data.user_name;
                    db.groupUsers.Add(user1);
                    db.SaveChanges();
                }
            });
        }

        public void addGroupMember(List<GroupMemberData> data)
        {
            queue.QueueTask(() =>
            {
                if (data.Count < 1)
                {
                    return;
                }
                var da = data[0];
                var oldList = db.groupUsers.Where(p => p.groupID.Equals(da.group_id)).ToList();
                foreach (var om in oldList)
                {
                    db.groupUsers.Remove(om);
                }
                db.SaveChanges();
                foreach (var d in data)
                {
                    GroupUser user = new GroupUser();
                    user.groupID = d.group_id;
                    user.userID = d.user_id;
                    user.avatar = d.portrait;
                    user.friend_name = d.friend_name;
                    user.group_user_nickname = d.group_user_nickname;
                    user.id_card = d.id_card;
                    user.inv_name = d.inv_name;
                    user.is_administrator = d.is_administrator;
                    user.is_manager = d.is_manager;
                    user.is_shield = d.is_shield;
                    user.user_name = d.user_name;
                    queue.QueueTask(() =>
                    {
                        db.groupUsers.Add(user);
                    });
                }
                db.SaveChanges();
            });
        }

        public bool checkFriend(string userid)
        {
            var f = queue.QueueTask<FriendModel>(() =>
            {
                return db.friend.Where(p => p.userID.Equals(userid)).FirstOrDefault();
            },true);
            if (f == null)
            {
                return false;
            }
            return true;
        }
        public FriendListData getFriend(string userId)
        {
            var f = queue.QueueTask<FriendModel>(() =>
            {
                return db.friend.Where(p => p.userID.Equals(userId)).FirstOrDefault();
            },true);
            if (f == null)
            {
                return null;
            }
            FriendListData friend = new FriendListData();
            friend.user_id = f.userID;
            friend.friend_self_name = f.friend_self_name;
            friend.id_card = f.id_Card;
            friend.is_shield = f.is_shild;
            friend.is_star = f.is_star;
            friend.is_yhjf = f.is_yhjf;
            friend.portrait = f.avatar;
            friend.target_user_nickname = f.nickName;
            return friend;
        }
        public List<FriendModel> allFriend()
        {
            return queue.QueueTask<List<FriendModel>>(() =>
            {
                return db.friend.ToList();
            });
        }
        public StrongerModel GetStronger(string userID)
        {
            var s = queue.QueueTask<StrongerModel>(() =>
            {
                return db.strongers.Where(p => p.userID.Equals(userID) && p.groupID.Equals("Self")).FirstOrDefault();
            },true);
            //var s= db.strongers.Where(p => p.userID.Equals(userID) && p.groupID.Equals("Self")).FirstOrDefault();
            if (s == null)
            {
                return queue.QueueTask<StrongerModel>(() =>
                {
                    return db.strongers.Where(p => p.userID.Equals(userID)).FirstOrDefault();
                },true);
                //return db.strongers.Where(p => p.userID.Equals(userID)).FirstOrDefault();
            }
            else
            {
                return s;
            }
        }

        public StrongerModel GetStronger(string userID,string groupID)
        {
            return queue.QueueTask<StrongerModel>(() =>
            {
                return db.strongers.Where(p => p.userID.Equals(userID) && p.groupID.Equals(groupID)).FirstOrDefault();
            },true);
            //return db.strongers.Where(p => p.userID.Equals(userID) && p.groupID.Equals(groupID)).FirstOrDefault();
        }

        public GroupTable GetGroup(string groupID)
        {
            return queue.QueueTask<GroupTable>(() =>
            {
                return db.group.Where(p => p.groupId.Equals(groupID)).FirstOrDefault();
            },true);
            //return db.group.Where(p => p.groupId.Equals(groupID)).FirstOrDefault();
        }

        public List<GroupTable> allGroup()
        {
            return queue.QueueTask<List<GroupTable>>(() =>
            {
                return db.group.ToList();
            });
        }

        public GroupCache GetGroupCache(string groupID)
        {
            return queue.QueueTask<GroupCache>(() =>
            {
                return db.groupCaches.Where(p => p.groupID.Equals(groupID)).FirstOrDefault();
            },true);
            //return db.groupCaches.Where(p => p.groupID.Equals(groupID)).FirstOrDefault();
        }
        public GroupUser GetGroupUser(string userID, string groupID)
        {
            return queue.QueueTask<GroupUser>(() =>
            {
                return db.groupUsers.Where(p => p.userID.Equals(userID) && p.groupID.Equals(groupID)).FirstOrDefault();
            },true);
            //return db.groupUsers.Where(p => p.userID.Equals(userID) && p.groupID.Equals(groupID)).FirstOrDefault();
        }

        public List<GroupUser> getGroupAllUser(string groupId)
        {
            return queue.QueueTask<List<GroupUser>>(() =>
            {
                return db.groupUsers.Where(p => p.groupID == groupId).ToList();
            });
        }

        public void DeleteGroup(string groupId)
        {
            queue.QueueTask(() =>
            {
                db.group.RemoveRange(db.group.Where(p => p.groupId == groupId).AsEnumerable());
                db.groupUsers.RemoveRange(db.groupUsers.Where(p => p.groupID == groupId).AsEnumerable());
                db.focus.RemoveRange(db.focus.Where(p => p.groupID == groupId));
                db.SaveChanges();
            });
        }

        public SearchViewModel mainSearch(string keyword)
        {
            return queue.QueueTask<SearchViewModel>(() =>
            {
                SearchViewModel model = new SearchViewModel();
                var nikename = db.friend.Where(p => p.nickName.Contains(keyword)).ToList();
                foreach (var nn in nikename)
                {
                    if (model.friends.IndexOf(nn) == -1)
                    {
                        model.friends.Add(nn);
                    }
                }
                var username = db.friend.Where(p => p.friend_self_name.Contains(keyword)).ToList();
                foreach (var nn in username)
                {
                    if (model.friends.IndexOf(nn) == -1)
                    {
                        model.friends.Add(nn);
                    }
                }
                var idcard = db.friend.Where(p => p.id_Card.Contains(keyword)).ToList();
                foreach (var nn in idcard)
                {
                    if (model.friends.IndexOf(nn) == -1)
                    {
                        model.friends.Add(nn);
                    }
                }
                var group = db.group.Where(p => p.groupName.Contains(keyword)).ToList();
                foreach (var item in group)
                {
                    if (model.groups.IndexOf(item) == -1)
                    {
                        model.groups.Add(item);
                    }
                }
                return model;
            });
        }
    }
}
