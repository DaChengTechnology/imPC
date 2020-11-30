using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class BaseSendModel:IDisposable
    {
        public int type { get; set; }

        public BaseSendModel()
        {
            type = 1;
        }

        public void Dispose()
        {
            
        }
    }
}
