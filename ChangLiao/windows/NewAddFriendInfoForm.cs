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
using ChangLiao.Model.ReciveModel;
using ChangLiao.Util;
using ChangLiao.Model.SendModel;

namespace ChangLiao.windows
{
    public partial class NewAddFriendInfoForm : DSkinForm
    {
        private GetUserData model;
        public NewAddFriendInfoForm()
        {
            InitializeComponent();
        }

        public NewAddFriendInfoForm(GetUserData data)
        {
            model = data;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DCWebImageMaanager.shard.downloadImageAsync(model.portrait, (image, b) =>
            {
                if (image != null)
                {
                    duiPictureBox1.BeginInvoke(() =>
                    {
                        duiPictureBox1.Image = image;
                        duiPictureBox1.Invalidate();
                    });
                }
            });
            duiLabel1.Text = model.user_name;
            duiLabel2.Text = "畅聊号:" + model.id_card;
            duiLabel4.Text = model.remark;
            duiLabel4.Size = TextRenderer.MeasureText(duiLabel4.Text, duiLabel4.Font);
        }

        private void duiButton1_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            DeleteFriendSendModel model = new DeleteFriendSendModel();
            model.target_user_id = this.model.user_id;
            HttpUitls.Instance.get<BaseReciveModel>("contacts/agree", model, (json) =>
            {
                if (json.code == 200)
                {
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.loadAddFriend();
                        main.refreshFriendFromServer();
                    }
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        this.Close();
                    }));
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

        private void duiButton2_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            DeleteFriendSendModel model = new DeleteFriendSendModel();
            model.target_user_id = this.model.user_id;
            HttpUitls.Instance.get<BaseReciveModel>("contacts/refuse", model, (json) =>
            {
                if (json.code == 200)
                {
                    MainFrm main = (MainFrm)Application.OpenForms["MainFrm"];
                    if (main != null)
                    {
                        main.loadAddFriend();
                        main.refreshFriendFromServer();
                    }
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        this.Close();
                    }));
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
    }
}
