using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    public class GetVersionData
    {
        public string apkUrl { get; set; }
        public string create_time { get; set; }
        public string updateDescription { get; set; }
        public int is_forced { get; set; }
        public string newVersion { get; set; }
    }
    class GetVersionReciveModel:BaseReciveModel
    {
        public GetVersionData data { get; set; }
    }
}
