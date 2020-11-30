using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class AddManagerSendModel:LoginedSendModel
    {
        public string group_id { get; set; }
        public string newManager_id { get; set; }
    }
}
