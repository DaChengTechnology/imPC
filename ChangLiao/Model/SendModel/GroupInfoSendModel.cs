using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class GroupInfoSendModel:LoginedSendModel
    {
        public string group_id { get; set; }
        public GroupInfoSendModel() : base()
        {

        }
    }
}
