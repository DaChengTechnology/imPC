using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ChangLiao.DB;
using ChangLiao.Ease;
using CefSharp;
using ChangLiao.windows;
using System.Windows.Forms;

namespace ChangLiao.Util
{
    class ResultData
    {
        public bool success { get; set; }
        public ResutD data { get; set; }
    }
    class ResutD
    {
        public List<JSData> chat { get; set; }
        public List<JSData> friend { get; set; }
    }
    class JSData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string id_card { get; set; }
    }
    class NewMember
    {
        public bool success { get; set; }
        public NewMemberData data { get; set; }
    }
    class NewMemberData
    {
        public List<JSData> friend { get; set; }
    }
    class NewGroupJSClass
    {
        public void create(IJavascriptCallback javascriptCallback)
        {
            NewGroupForm form = (NewGroupForm)Application.OpenForms["NewGroupForm"];
            using (javascriptCallback)
            {
                javascriptCallback.ExecuteAsync(1);
            }
        }

        public void close(IJavascriptCallback javascriptCallback)
        {
            NewGroupForm form = (NewGroupForm)Application.OpenForms["NewGroupForm"];
            form.Close();
            using (javascriptCallback)
            {
                javascriptCallback.ExecuteAsync(1);
            }
        }
        public void userId(IJavascriptCallback javascriptCallback)
        {
            using (javascriptCallback)
            {
                javascriptCallback.ExecuteAsync(SettingMenager.shard.userID);
            }
        }

        public void token(IJavascriptCallback javascriptCallback)
        {
            using (javascriptCallback)
            {
                javascriptCallback.ExecuteAsync(SettingMenager.shard.token);
            }
        }
        /// <summary>
        /// 浏览器获取群成员
        /// </summary>
        /// <param name="javascriptCallback"></param>
        public void getGroupData(IJavascriptCallback javascriptCallback)
        {
            ChangeGroupOwnerForm form = Application.OpenForms["ChangeGroupOwnerForm"] as ChangeGroupOwnerForm;
            if (form != null)
            {
                var list = DBHelper.Instance.getGroupAllUser(form.groupId);
                using (javascriptCallback)
                {
                    javascriptCallback.ExecuteAsync(JsonConvert.SerializeObject(list));
                }
            }
            else
            {
                using (javascriptCallback)
                {
                    javascriptCallback.ExecuteAsync(null);
                }
            }
        }
        /// <summary>
        /// 邀请群成员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="javascriptCallback"></param>
        public void newGroupMember(string id, IJavascriptCallback javascriptCallback)
        {
            var friend = DBHelper.Instance.allFriend();
            var member = DBHelper.Instance.getGroupAllUser(id);
            foreach (var item in member)
            {
                friend = friend.Where(p => p.userID != item.userID).ToList();
            }
            List<JSData> fed = new List<JSData>();
            foreach (var item in friend)
            {
                var f = new JSData();
                f.id = item.userID;
                f.name = item.friend_self_name;
                f.url = item.avatar;
                f.id_card = item.id_Card;
                fed.Add(f);
            }
            NewMemberData data = new NewMemberData();
            data.friend = fed;
            NewMember result = new NewMember();
            result.success = true;
            result.data = data;
            using (javascriptCallback)
            {
                javascriptCallback.ExecuteAsync(JsonConvert.SerializeObject(result));
            }
        }
        /// <summary>
        /// 浏览器获取好友
        /// </summary>
        /// <param name="javascriptCallback"></param>
        public void getData(IJavascriptCallback javascriptCallback)
        {
            var con = EaseHelper.shard.client.getChatManager().getConversations();
            List<JSData> chat = new List<JSData>();
            foreach (var item in con)
            {
                if(item.conversationType()== EaseMobLib.EMConversationType.CHAT)
                {
                    JSData data = new JSData();
                    data.id = item.conversationId();
                    var f = DBHelper.Instance.getFriend(data.id);
                    if (f != null)
                    {
                        data.name = f.friend_self_name;
                        data.url = f.portrait;
                        data.id_card = f.id_card;
                    }
                    else
                    {
                        var s = DBHelper.Instance.GetStronger(data.id);
                        if (s == null)
                        {
                            continue;
                        }
                        data.name = s.nickName;
                        data.url = s.avatar;
                        data.id_card = s.idCard;
                    }
                    chat.Add(data);
                }
            }
            var friend = DBHelper.Instance.allFriend();
            List<JSData> fed = new List<JSData>();
            foreach (var item in friend)
            {
                var f = new JSData();
                f.id = item.userID;
                f.name = string.IsNullOrEmpty(item.nickName) ? item.friend_self_name : item.nickName;
                f.url = item.avatar;
                f.id_card = item.id_Card;
                fed.Add(f);
            }
            ResutD resutD = new ResutD();
            resutD.chat = chat;
            resutD.friend = fed;
            ResultData result = new ResultData();
            result.success = true;
            result.data = resutD;
            using (javascriptCallback)
            {
                javascriptCallback.ExecuteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
