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
using ChangLiao.DB;
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;

namespace ChangLiao.windows
{
    public partial class AddFriendForm : DSkinForm
    {
        public AddFriendForm()
        {
            InitializeComponent();
        }

        private void searchPerson()
        {
            if (string.IsNullOrEmpty(dSkinTextBox1.Text))
            {
                MessageBox.Show("请填写手机号或者畅聊号");
                return;
            }
            GetUserByMobileOrIdCardSendModel model = new GetUserByMobileOrIdCardSendModel();
            model.mobile = dSkinTextBox1.Text;
            HttpUitls.Instance.get<GetUserByMobileOrIdCardReciveModel>("user/getUserByMobile", model, (json) =>
            {
                if (json.code == 200)
                {
                    FriendListData friend = DBHelper.Instance.getFriend(json.data.user_id);
                    if (friend == null)
                    {
                        MainFrm frm = Application.OpenForms["MainFrm"] as MainFrm;
                        if (frm != null)
                        {
                            GetUserData data = new GetUserData();
                            data.user_id = json.data.user_id;
                            data.user_name = json.data.user_name;
                            data.portrait = json.data.portrait;
                            data.id_card = json.data.id_card;
                            UserInfoForm form = new UserInfoForm(data);
                            frm.BeginInvoke(new EventHandler((s, e) =>
                            {
                                this.FormClosed += new FormClosedEventHandler((ss, ee) =>
                                {
                                    form.BeginInvoke(new EventHandler((sss, sse) =>
                                    {
                                        form.BringToFront();
                                        form.Activate();
                                    }));
                                });
                                this.Close();
                                form.Show();
                            }));
                            DBHelper.Instance.addStronger(data);
                        }
                        else
                        {
                            this.BeginInvoke(new EventHandler((s, e) =>
                            {
                                GetUserData data = new GetUserData();
                                data.user_id = json.data.user_id;
                                data.user_name = json.data.user_name;
                                data.portrait = json.data.portrait;
                                data.id_card = json.data.id_card;
                                UserInfoForm form = new UserInfoForm(data);
                                form.Show();
                                DBHelper.Instance.addStronger(data);
                                this.Close();
                                form.BringToFront();
                            }));
                        }
                    }
                    else
                    {
                        MainFrm frm = Application.OpenForms["MainFrm"] as MainFrm;
                        if (frm != null)
                        {
                            UserInfoForm form = new UserInfoForm(friend.toFriend());
                            frm.BeginInvoke(new EventHandler((s, e) =>
                            {
                                this.FormClosed += new FormClosedEventHandler((ss, ee) =>
                                {
                                    form.BeginInvoke(new EventHandler((sss, sse) =>
                                    {
                                        form.BringToFront();
                                        form.Activate();
                                    }));
                                });
                                this.Close();
                                form.Show();
                            }));
                            GetUserData data = new GetUserData();
                            data.user_id = json.data.user_id;
                            data.user_name = json.data.user_name;
                            data.portrait = json.data.portrait;
                            data.id_card = json.data.id_card;
                            DBHelper.Instance.addStronger(data);
                        }
                        else
                        {
                            this.BeginInvoke(new EventHandler((s, e) =>
                            {
                                UserInfoForm form = new UserInfoForm(friend.toFriend());
                                form.Show();
                                GetUserData data = new GetUserData();
                                data.user_id = json.data.user_id;
                                data.user_name = json.data.user_name;
                                data.portrait = json.data.portrait;
                                data.id_card = json.data.id_card;
                                DBHelper.Instance.addStronger(data);
                                this.Close();
                                form.BringToFront();
                            }));
                        }
                    }
                }
                else
                {
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        MessageBox.Show(json.message);
                    }));
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
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        MessageBox.Show("请重新登录");
                    }));
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

        private void dSkinTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchPerson();
            }
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            searchPerson();
        }
    }
}
