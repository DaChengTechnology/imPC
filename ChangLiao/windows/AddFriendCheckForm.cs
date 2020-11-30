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
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Util;
using ChangLiao.Ease;
using EaseMobLib;

namespace ChangLiao.windows
{
    public partial class AddFriendCheckForm : DSkinForm
    {
        private GetUserData userData;
        private bool needClose;
        public AddFriendCheckForm()
        {
            needClose = false;
            InitializeComponent();
        }

        public AddFriendCheckForm(GetUserData data)
        {
            InitializeComponent();
            needClose = false;
            userData = data;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (userData != null)
            {
                DCWebImageMaanager.shard.downloadImageAsync(userData.portrait, (image, b) =>
                {
                    if (image != null)
                    {
                        dSkinPictureBox1.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            dSkinPictureBox1.Image = image;
                            dSkinPictureBox1.Invalidate();
                        }));
                    }
                });
                dSkinLabel1.Text = userData.user_name;
                dSkinLabel1.Size = TextRenderer.MeasureText(dSkinLabel1.Text, dSkinLabel1.Font);
                dSkinLabel1.Location = new Point((dSkinNewPanel1.Width - dSkinLabel1.Width) / 2, dSkinLabel1.Location.Y);
                dSkinLabel2.Text = "畅聊号:" + userData.id_card;
                dSkinLabel2.Size = TextRenderer.MeasureText(dSkinLabel2.Text, dSkinLabel2.Font);
                dSkinLabel2.Location = new Point((dSkinNewPanel1.Width - dSkinLabel2.Width) / 2, dSkinLabel2.Location.Y);
            }
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            if (needClose)
            {
                this.BeginInvoke(new EventHandler((s, ee) =>
                {
                    this.Close();
                }));
                return;
            }
            ApplyForFriendSendModel model = new ApplyForFriendSendModel();
            model.target_user_id = userData.user_id;
            model.remark = dSkinTextBox1.Text;
            HttpUitls.Instance.get<BaseReciveModel>("contacts/applyFor", model, (json) =>
            {
                if (json.code == 200)
                {
                    needClose = true;
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        dSkinLabel3.Text = "好友请求已发送";
                        dSkinTextBox1.Visible = false;
                        dSkinButton1.Text = "关闭";
                    }));
                    EMError error = new EMError();
                    EaseHelper.shard.client.getContactManager().inviteContact(userData.user_id, null, error);
                    
                }
                else
                {
                    MessageBox.Show(json.message);
                    if (json.message.Contains("请重新登录"))
                    {
                        MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
                        if (frm != null)
                        {
                            frm.gotoLogin();
                            this.BeginInvoke(new EventHandler((s, ee) =>
                            {
                                this.Close();
                            }));
                        }
                    }
                }
            }, (code) =>
             {
                 if (code > 500 && code < 503)
                 {
                     MessageBox.Show("请重新登录");
                     MainFrm frm = (MainFrm)Application.OpenForms["MainFrm"];
                     if (frm != null)
                     {
                         frm.gotoLogin();
                         this.BeginInvoke(new EventHandler((s, ee) =>
                         {
                             this.Close();
                         }));
                     }
                 }
             });
        }
    }
}
