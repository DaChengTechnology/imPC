using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    /// <summary>
    /// 自定义表情表
    /// </summary>
    class FaceTable:IDisposable
    {
        [Key]
        /// <summary>
        /// 表情ID
        /// </summary>
        public string faceID { get; set; }
        /// <summary>
        /// 表情URL
        /// </summary>
        public string faceURL { get; set; }
        /// <summary>
        /// 表情高度
        /// </summary>
        public int faceH { get; set; }
        /// <summary>
        /// 表情宽度
        /// </summary>
        public int faceW { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
