using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    /// <summary>
    /// 群组缓存
    /// </summary>
    class GroupCache:IDisposable
    {
        [Key]
        /// <summary>
        /// 群组ID
        /// </summary>
        public string groupID { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 群组头像
        /// </summary>
        public string avatar { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
