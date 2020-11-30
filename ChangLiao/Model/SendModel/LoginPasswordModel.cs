using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.SendModel
{
    class LoginPasswordModel:BaseSendModel
    {
        public string mobile { get; set; }
        public int way_type { get; set; }
        public string password { get; set; }
    }
}
