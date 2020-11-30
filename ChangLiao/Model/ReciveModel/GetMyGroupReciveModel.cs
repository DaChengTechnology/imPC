using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    class MyGroupDataModel
    {
        public string group_id { get; set; }
        public string group_name { get; set; }
        public string group_portrait { get; set; }
    }
    class GetMyGroupReciveModel:BaseReciveModel
    {
        public List<MyGroupDataModel> data { get; set; }
    }
}
