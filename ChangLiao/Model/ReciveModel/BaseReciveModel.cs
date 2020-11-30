using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    class BaseReciveModel:IDisposable
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }

        public void Dispose()
        {
            
        }
    }
}
