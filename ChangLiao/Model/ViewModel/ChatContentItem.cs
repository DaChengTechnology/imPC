using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ViewModel
{
    public class ChatContentItem
    {
        /// <summary>
        /// 类型 1字符 2表情
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 字符
        /// </summary>
        public char ch { get; set; }
        /// <summary>
        /// 表情
        /// </summary>
        public EmotionModel emotion { get; set; }
        /// <summary>
        /// 普通字符位置
        /// </summary>
        public int index { get; set; }
    }
}
