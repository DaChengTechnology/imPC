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
    /// 陌生人表
    /// </summary>
    class StrongerModel:IDisposable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 群组ID
        /// </summary>
        public string groupID { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 畅聊号
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
