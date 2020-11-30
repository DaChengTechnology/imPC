using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    /// <summary>
    /// 聊天背景
    /// </summary>
    class ChatBK:IDisposable
    {
        [Key]
        /// <summary>
        /// 回话ID
        /// </summary>
        public string conversationId { get; set; }
        /// <summary>
        /// 图片URL
        /// </summary>
        public string URL { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
