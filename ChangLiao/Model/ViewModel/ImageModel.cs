using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ViewModel
{
    class ImageModel
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Image image { get; set; }
    }
}
