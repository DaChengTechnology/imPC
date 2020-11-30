using System;
using System.Drawing;

namespace ChangLiao.Model.ViewModel
{
    /// <summary>
    /// 表情模型
    /// </summary>
    public class EmotionModel : ICloneable
    {
        /// <summary>
        /// 表情id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 表情图片
        /// </summary>
        public Image face { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
