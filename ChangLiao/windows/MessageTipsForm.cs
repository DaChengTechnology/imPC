using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;
using ChangLiao.Ease;
using EaseMobLib;
using ChangLiao.Temple;
using ChangLiao.Util;

namespace ChangLiao.windows
{
    public partial class MessageTipsForm : DSkinForm
    {
        private List<EMConversation> conversations;
        private MainFrm Frm;
        public MessageTipsForm(MainFrm main)
        {
            Frm = main;
            InitializeComponent();
            conversations = EaseHelper.shard.client.getChatManager().getConversations().Where(p => p.unreadMessagesCount() > 0).OrderByDescending(p => p.latestMessage().timestamp()).ToList();
            List<UnreadTipsItem> list = new List<UnreadTipsItem>();
            foreach (var item in conversations)
            {
                list.Add(new UnreadTipsItem(item));
            }
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                Text = SettingMenager.shard.username;
                if (conversations.Count > 6)
                {
                    Size = new Size(150, 6 * 50 + 20);
                }
                else
                {
                    Size = new Size(150, conversations.Count * 50 + 20);
                }
                duiListBox1.Items.AddRange(list);
            }));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0xA3 && m.Msg != 0x0003 && m.WParam != (IntPtr)0xF012)
            {
                base.WndProc(ref m);
            }
        }

        private void duiListBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void MessageTipsForm_MouseLeave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MessageTipsForm_MouseEnter(object sender, EventArgs e)
        {
            Focus();
        }

        private void MessageTipsForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void duiListBox1_ItemClick(object sender, DSkin.Controls.ItemClickEventArgs e)
        {
            this.Close();
            Frm.ShowWithChat(conversations[e.Index]);
        }
    }
}
