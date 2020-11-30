using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class CreateGroupSendModel:LoginedSendModel
    {
        public string group_name { get; set; }
        public string group_portrait { get; set; }
        public int group_type { get; set; }
    }
}
