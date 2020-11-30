using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    class FocusTable:IDisposable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 群ID
        /// </summary>
        public string groupID { get; set; }
        /// <summary>
        /// 关注人ID
        /// </summary>
        public string targetID { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
