using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    /// <summary>
    /// 好友数据模型
    /// </summary>
    public class FriendModel:IDisposable
    {
        [Key]
        /// <summary>
        /// userID
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 畅聊号
        /// </summary>
        public string id_Card { get; set; }
        /// <summary>
        /// 好友昵称
        /// </summary>
        public string friend_self_name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 是否屏蔽 1是 2不是
        /// </summary>
        public int is_shild { get; set; }
        /// <summary>
        /// 是否星标 1是 2不是
        /// </summary>
        public int is_star { get; set; }
        /// <summary>
        /// 是否阅后即焚 1是 2不是
        /// </summary>
        public int is_yhjf { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
