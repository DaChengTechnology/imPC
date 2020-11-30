using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;
using CefSharp.WinForms;
using ChangLiao.Util;
using CefSharp;
using CefSharp.SchemeHandler;

namespace ChangLiao.windows
{
    public partial class NewGroupForm : DSkinForm
    {
        private bool isload;
        private List<string> ids;
        private string groupID;
        public NewGroupForm()
        {
            InitializeComponent();

        }


        public NewGroupForm(string groupid)
        {
            groupID = groupid;
            InitializeComponent();
        }

        private void NewGroupForm_Load(object sender, EventArgs e)
        {;
            cancelButton.Location = new Point(Width - 101, Height - 33);
            okButton.Location = new Point(Width - 201, Height - 33);
            chromiumWebBrowser1.ConsoleMessage += ChromiumWebBrowser1_ConsoleMessage;
            chromiumWebBrowser1.JavascriptObjectRepository.ObjectsBoundInJavascript += JavascriptObjectRepository_ObjectsBoundInJavascript;
            var d= new JSDialog();
            chromiumWebBrowser1.JsDialogHandler = d;
            isload = false;
            
        }

        private void JavascriptObjectRepository_ObjectsBoundInJavascript(object sender, CefSharp.Event.JavascriptBindingMultipleCompleteEventArgs e)
        {
            if (isload)
            {
                return;
            }
            isload = true;
            chromiumWebBrowser1.Reload();
        }

        private void ChromiumWebBrowser1_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            System.Console.WriteLine(string.Format("Line: {0}, Source: {1}, Message: {2}", e.Line, e.Source, e.Message));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (groupID == null)
            {
                if (MessageBox.Show("你确定要取消创建吗？", "新的群组", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (canceled != null)
                    {
                        canceled();
                    }
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
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
                string data = obj.Value.Replace("%2C", ",");
                ids = data.Split(',').ToList();
                System.Console.WriteLine(ids);
                if (oked != null)
                {
                    oked(data);
                }
                this.BeginInvoke(new EventHandler((s, e) =>
                {
                    this.Close();
                }));
            }
        }

        private void NewGroupForm_Resize(object sender, EventArgs e)
        {
            cancelButton.Location = new Point(Width - 101, Height - 33);
            okButton.Location = new Point(Width - 201, Height - 33);
        }

        private void NewGroupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            chromiumWebBrowser1.Delete();
        }
    }
}
