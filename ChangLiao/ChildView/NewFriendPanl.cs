using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Controls;
using DSkin.DirectUI;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Model.SendModel;
using ChangLiao.windows;
using ChangLiao.Temple;

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 新的朋友面板
    /// </summary>
    public partial class NewFriendPanl : DSkinUserControl
    {
        List<GetUserData> datas;
        public NewFriendPanl()
        {
            InitializeComponent();
        }
        public void loadaData()
        {
            HttpUitls.Instance.get<InviteFriendReciveModel>("contacts/inviteList", new LoginedSendModel(), (json) =>
            {
                if (json.code == 200)
                {
                    datas = json.data;
                    duiListBox1.BeginInvoke(() =>
                    {
                        duiListBox1.Items.Clear();
                        foreach (var item in datas)
                        {
                            duiListBox1.Items.Add(new newFriendListItemTemp(item));
                        }
                        duiListBox1.Invalidate();
                    });
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.updateAddFriendCount(json.data.Count);
                    }
                }
                else
                {
                    if (json.message.Contains("重新登录"))
                    {
                        MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                        if (main != null)
                        {
                            main.gotoLogin();
                        }
                    }
                }
            }, (code) =>
            {
                if (code < 503 && code > 500)
                {
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.gotoLogin();
                    }
                }
            });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loadaData();
        }

        private void duiListBox1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                NewAddFriendInfoForm form = new NewAddFriendInfoForm(datas[e.Index]);
                form.Show();
            }
        }

        private void duiListBox1_ItemAdded(object sender, DSkin.DirectUI.DuiControlEventArgs e)
        {
            foreach (DuiBaseControl item in duiListBox1.Items.ToList())
            {
                item.Width = duiListBox1.Width;
            }
        }
    }
}
