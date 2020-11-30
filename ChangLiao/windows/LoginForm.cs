using CCWin;
using System;
using System.Windows.Forms;
using ChangLiao.Model.SendModel;
using ChangLiao.Model.ReciveModel;
using ChangLiao.windows;
using ChangLiao.Ease;
using System.Threading;
using ChangLiao.Util;
using ChangLiao.DB;

namespace ChangLiao
{
    public partial class LoginForm : CCSkinMain
    {
        private bool isenter = false;
        public LoginForm()
        {
            InitializeComponent();
            skinButton1.Click += OnLogin;
            skinTextBox2.IsPasswordChat = '*';
            skinTextBox2.IsSystemPasswordChar = true;
            skinTextBox2.MaxLength = 16;
        }

        public void GoToLogin()
        {
            if (isenter)
            {
                return;
            }
            isenter = true;
            var f = Application.OpenForms["MainFrm"] as MainFrm;
            if (f != null)
            {
                return;
            }
            try
            {
                MainFrm form = new MainFrm(true);
                Application.Run(form);
            }
            catch { }
        }

        public void OnLogin (object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            skinButton1.Enabled = false;
            if (skinTextBox1.Text.Length < 1)
            {
                MessageBox.Show("请填写手机号或者畅聊号");
                skinButton1.Enabled = true;
                return;
            }
            if (skinTextBox2.Text.Length < 1)
            {
                MessageBox.Show("请填写密码");
                skinButton1.Enabled = true;
                return;
            }
            skinButton1.Text = "登陆中...";
            LoginPasswordModel model = new LoginPasswordModel();
            model.way_type = 1;
            model.mobile = skinTextBox1.Text;
            model.password = skinTextBox2.Text;
            HttpUitls.Instance.get<LoginPasswordReciveModel>("register/loginPassword", model, (json) =>
            {
                if (json.code == 200)
                {
                   SettingMenager.shard.token = json.data.token;
                    EaseHelper.shard.login(json.data.username, json.data.password, (err) =>
                    {
                        if (err.errorCode == EaseMobLib.EMErrorCode.EM_NO_ERROR)
                        {
                            SettingMenager.shard.userID = json.data.username;
                            skinButton1.BeginInvoke(new EventHandler((s, er) =>
                            {
                                skinButton1.Text = "登录成功";
                            }));
                            HttpUitls.Instance.get<UserInfoReciveModel>("user/userInfo",new LoginedSendModel(),(js) => {
                                //需要存东西
                                SettingMenager.shard.username = js.data.db.user_name;
                                SettingMenager.shard.phone = js.data.db.mobile;
                                SettingMenager.shard.avatar = js.data.db.portrait;
                                SettingMenager.shard.idCard = js.data.db.id_card;
                                new Thread(new ThreadStart(() =>
                                {
                                    _ = DBHelper.Instance;
                                })).Start();
                                this.BeginInvoke(new EventHandler((s, er) =>
                                {
                                    this.Close();
                                }));
                                Thread t = new Thread(new ThreadStart(GoToLogin));
                                t.SetApartmentState(ApartmentState.STA);
                                t.Start();
                            },(s)=> {
                                if (s > 500 && s < 503)
                                {
                                    SettingMenager.shard.token = null;
                                    skinButton1.BeginInvoke(new EventHandler((st, er) =>
                                    {
                                        skinButton1.Text = "登录";
                                        skinButton1.Enabled = true;
                                    }));
                                }
                            });
                        }
                        else
                        {
                            SettingMenager.shard.token = null;
                            skinButton1.BeginInvoke(new EventHandler((s, er) =>
                            {
                                skinButton1.Text = "登录";
                                skinButton1.Enabled = true;
                                MessageBox.Show("聊天服务器登录失败");
                            }));
                        }
                    });
                }
                else
                {
                    SettingMenager.shard.token = null;
                    skinButton1.BeginInvoke(new EventHandler((s, err) =>
                    {
                        skinButton1.Text = "登录";
                        skinButton1.Enabled = true;
                        MessageBox.Show(json.message);
                    }));
                }
            }, (ss) =>
            {
                if (ss < 503 && ss > 500)
                {
                    SettingMenager.shard.token = null;
                    skinButton1.BeginInvoke(new EventHandler((s, err) =>
                    {
                        skinButton1.Text = "登录";
                        skinButton1.Enabled = true;
                    }));
                }
            });
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pc.imchangliao.com/registpc/#/login1?imei="+ Properties.Settings.Default.IMEI + "&deviceinfo="+ new GetOSystem().ToString() + "&client=window&version=1");
        }
    }
}
