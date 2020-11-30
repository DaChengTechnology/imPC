using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    /// <summary>
    /// 图片缓存表
    /// </summary>
    class LocalTempModel:IDisposable
    {
        /// <summary>
        /// 图片url
        /// </summary>
        [Key]
        [MaxLength(600)]
        public string url { get; set; }
        /// <summary>
        /// 本地地址
        /// </summary>
        public string localPath { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
