using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ReciveModel
{
    class FocusData
    {
        public string group_id { get; set; }
        public string target_user_id { get; set; }
    }
    class GroupInfoData
    {
        /// <summary>
        /// 群ID
        /// </summary>
        public string group_id { get; set; }
        /// <summary>
        /// 群名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 群头像
        /// </summary>
        public string group_portrait { get; set; }
        /// <summary>
        /// 群主ID
        /// </summary>
        public string administrator_id { get; set; }
        /// <summary>
        /// 是否群主
        /// </summary>
        public int is_admin { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public int is_menager { get; set; }
        /// <summary>
        /// 群公告
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 群类型
        /// </summary>
        public int group_type { get; set; }
        /// <summary>
        /// 是否屏蔽
        /// </summary>
        public int is_pingbi { get; set; }
        /// <summary>
        /// 是否全员禁言
        /// </summary>
        public int is_all_banned { get; set; }
        /// <summary>
        /// 群成员总数
        /// </summary>
        public int groupUserSum { get; set; }
        public List<FocusData> focusList { get; set; }
    }
    class GroupInfoReciveModel:BaseReciveModel
    {
        public GroupInfoData data { get; set; }
    }
}
