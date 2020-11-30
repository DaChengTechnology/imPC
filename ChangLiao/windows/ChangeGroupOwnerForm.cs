using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using ChangLiao.Util;
using DSkin.Forms;

namespace ChangLiao.windows
{
    public partial class ChangeGroupOwnerForm : DSkinForm
    {
        private bool isload;
        public ChangeGroupOwnerForm()
        {
            isload = false;
            InitializeComponent();
            chromiumWebBrowser1.JavascriptObjectRepository.Register("desktopAPP", new ChangLiao.Util.NewGroupJSClass());
            chromiumWebBrowser1.JavascriptObjectRepository.ObjectsBoundInJavascript += JavascriptObjectRepository_ObjectsBoundInJavascript;
        }

        private void JavascriptObjectRepository_ObjectsBoundInJavascript(object sender, CefSharp.Event.JavascriptBindingMultipleCompleteEventArgs e)
        {
            if (isload)
                return;
            isload = true;
            chromiumWebBrowser1.ShowDevTools();
            chromiumWebBrowser1.Load("file:///" + System.IO.Directory.GetCurrentDirectory() + "/dist/index.html#/index2");
            //var task = Task.Run(async delegate
            //{
            //    await Task.Delay(1000);
            //    chromiumWebBrowser1.ShowDevTools();
            //    return 1;

            //});
        }

        private void ChangeGroupOwnerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var cookieManager = CefSharp.Cef.GetGlobalCookieManager();

            CookieVisitor visitor = new CookieVisitor();
            visitor.SendCookie += visitor_SendCookie;
            cookieManager.VisitAllCookies(visitor);
        }

        private void visitor_SendCookie(Cookie obj)
        {
            if (obj.Name == "id")
            {
                System.Console.WriteLine(obj.Value);
                this.BeginInvoke(new EventHandler((s, e) =>
                {
                    this.Close();
                }));
            }
        }

        private void cancenButton_Click(object sender, EventArgs e)
        {

        }
    }
}
