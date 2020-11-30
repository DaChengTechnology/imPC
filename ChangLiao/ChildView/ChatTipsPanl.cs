using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.DirectUI;

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 禁言提示
    /// </summary>
    public partial class ChatTipsPanl : DuiBaseControl
    {
        public ChatTipsPanl()
        {
            InitializeComponent();
        }

        private void ChatTipsPanl_SizeChanged(object sender, EventArgs e)
        {
            duiLabel1.Location = new Point((Width - duiLabel1.Width) / 2, (Height - duiLabel1.Height) / 2);
        }

        private void ChatTipsPanl_VisibleChanged(object sender, EventArgs e)
        {
            duiLabel1.Location = new Point((Width - duiLabel1.Width) / 2, (Height - duiLabel1.Height) / 2);
        }
    }
}
