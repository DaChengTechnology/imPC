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
using ChangLiao.Util;
using System.IO;

namespace ChangLiao.windows
{
    public partial class UpdateForm : DSkinForm
    {
        public string host { get; set; }
        public UpdateForm(string url)
        {
            host = url;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            starDownLoad();
        }

        private void starDownLoad()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ChangLiao", "AutoUpdate");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "Autosetup.exe");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            HttpUitls.Instance.DownloadFile(host, path, (p) =>
            {
                Application.DoEvents();
                this.BeginInvoke(new EventHandler((s, ee) =>
                {
                    this.dSkinLabel1.Text = string.Format("{0:0.00}", p);
                    dSkinProgressBar1.Value = Convert.ToInt32(p * 100);
                }));
            }, (b) =>
            {
                if (b)
                {
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        this.dSkinLabel1.Text = "下载完成";
                        dSkinProgressBar1.Value = 100;
                    }));
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(path);
                    startInfo.Verb = "runas";
                    try
                    {
                        System.Diagnostics.Process.Start(startInfo);
                    }
                    catch
                    {
                        this.BeginInvoke(new EventHandler((s, ee) =>
                        {
                            MessageBox.Show("启动自动安装程序失败，请手动启动\n程序位置：" + path);
                            Application.Exit();
                        }));
                        return;

                    }
                    Application.Exit();
                }
                else
                {
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        if (MessageBox.Show("文件下载失败是否重试？", "自动更新", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            starDownLoad();
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }));
                }
            });
        }
    }
}
