using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    public class GetUserData
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string portrait { get; set; }
        public string id_card { get; set; }
        public string remark { get; set; }
    }
    class GetUserReciveModel : BaseReciveModel
    {
        public GetUserData data { get; set; }
    }
}
