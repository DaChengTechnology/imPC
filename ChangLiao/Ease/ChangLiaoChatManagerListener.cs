using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EaseMobLib;
using ChangLiao.Util;
using ChangLiao.windows;
using System.Windows.Forms;

namespace ChangLiao.Ease
{
    class ChangLiaoChatManagerListener : EMChatManagerListener
    {
        private BackgroundQueue queue;
        public ChangLiaoChatManagerListener():base()
        {
            queue = new BackgroundQueue();
        }
        new void D_onReceiveCmdMessages(EMMessage[] messages)
        {
        }
        new void D_onReceiveMessages(EMMessage[] messages)
        {
            queue.QueueTask(() =>
            {
                MainForm main = (MainForm)Application.OpenForms["MainForm"];
                if (main.IsHandleCreated)
                {
                    main.BeginInvoke(new EventHandler((s, e) =>
                    {
                        main.refreshConversationList();
                    }));
                }
            });
        }
    }
}
