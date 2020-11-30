using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangLiao.DB;

namespace ChangLiao.Model.ViewModel
{
    public class SearchViewModel
    {
        /// <summary>
        /// 好友
        /// </summary>
        public List<FriendModel> friends { get; set; }
        /// <summary>
        /// 群组
        /// </summary>
        public List<GroupTable> groups { get; set; }
        public SearchViewModel()
        {
            friends = new List<FriendModel>();
            groups = new List<GroupTable>();
        }
    }
}
