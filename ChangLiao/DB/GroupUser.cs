using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    /// <summary>
    /// 群成员表
    /// </summary>
    public class GroupUser:IDisposable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 群ID
        /// </summary>
        public string groupID { get; set; }
        /// <summary>
        /// 群成员ID
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 是否群主
        /// </summary>
        public int is_administrator { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public int is_manager { get; set; }
        /// <summary>
        /// 群昵称
        /// </summary>
        public string group_user_nickname { get; set; }
        /// <summary>
        /// 畅聊号
        /// </summary>
        public string id_card { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 好友备注
        /// </summary>
        public string friend_name { get; set; }
        /// <summary>
        /// 邀请人名称
        /// </summary>
        public string inv_name { get; set; }
        /// <summary>
        /// 是否禁言
        /// </summary>
        public int is_shield { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
