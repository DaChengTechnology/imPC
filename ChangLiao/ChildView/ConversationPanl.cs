using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChangLiao.Ease;
using EaseMobLib;
using ChangLiao.DIYListBox.ListItem;
using ChangLiao.Util;

namespace ChangLiao.ChildView
{
    public partial class ConversationPanl : UserControl
    {
        private List<EMConversation> conversations;

        public ConversationPanl()
        {
            InitializeComponent();
            conversationListBox1.selectColor = Color.FromArgb(204, 204, 204);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true); // 调整大小时重绘
        }

        private void ConversationPanl_SizeChanged(object sender, EventArgs e)
        {
            conversationListBox1.Height = this.Height - 48;
        }

        public void reFlash()
        {
            BackgroundWorker rf = new BackgroundWorker();
            rf.DoWork += Rf_DoWork;
            rf.RunWorkerAsync();
        }

        private void Rf_DoWork(object sender, DoWorkEventArgs e)
        {
            conversations = EaseHelper.shard.getAllConversation();
            for (int i = conversations.Count - 1; i > 0; i--)
            {
                if (conversations[i].latestMessage() == null)
                {
                    conversations.RemoveAt(i);
                }
                else if (conversations[i].conversationId() == SettingMenager.shard.userID)
                {
                    conversations.RemoveAt(i);
                }
            }
            conversations.Sort(delegate (EMConversation e1, EMConversation e2) {
                if (e1.latestMessage() == null)
                {
                    return 1;
                }
                if (e2.latestMessage() == null)
                {
                    return 0;
                }
                long et1 = e1.latestMessage().timestamp();
                long et2 = e2.latestMessage().timestamp();
                if (et1 == et2)
                {
                    return 0;
                }
                if (et2 > et1)
                {
                    return 1;
                }
                return 0;
            });
            List<ConversationListItem> models = new List<ConversationListItem>();
            foreach (var item in conversations)
            {
                models.Add(new ConversationListItem(item));
            }
            if (conversationListBox1.IsHandleCreated)
            {
                conversationListBox1.BeginInvoke(new EventHandler((s, ee) =>
                {
                    conversationListBox1.Items.Add(models);
                }));
            }
            else
            {
                conversationListBox1.Items.Add(models);
            }
        }

        private void ConversationPanl_Load(object sender, EventArgs e)
        {
            reFlash();
        }
    }
}
