using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChangLiao.Model.ViewModel;
using ChangLiao.windows;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Model.SendModel;
using NLog;
using System.IO;
using System.Text;
using ChangLiao.Util;

namespace ChangLiao
{
    static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal),"Changliao","err.log") };

            // Rules for mapping loggers to targets
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
            new Thread(new ThreadStart(() =>
            {
                _ = EmotionManager.shard;
            })).Start();
            new Thread(new ThreadStart(() =>
            {
                BaseSendModel m = new BaseSendModel();
                m.type = 4;
                HttpUitls.Instance.get<GetVersionReciveModel>("common/getVersion", m, (json) =>
                  {
                      if (json.code == 200)
                      {
                          if (json.data == null)
                          {
                              return;
                          }
                          double newversion = Convert.ToDouble(json.data.newVersion);
                          double oldversion = Convert.ToDouble(Properties.Settings.Default.version);
                          if (newversion > oldversion)
                          {
                              if (json.data != null)
                              {
                                  if (json.data.is_forced == 1)
                                  {

                                      LoginForm loginForm = Application.OpenForms["LoginForm"] as LoginForm;
                                      if (loginForm != null)
                                      {
                                          loginForm.BeginInvoke(new EventHandler((s, e) =>
                                          {
                                              loginForm.Close();
                                          }));
                                          Thread t = new Thread(new ThreadStart(()=>
                                          {
                                              UpdateForm frm = new UpdateForm(json.data.apkUrl);
                                              Application.Run(frm);
                                          }));
                                          t.SetApartmentState(ApartmentState.STA);
                                          t.Start();
                                      }
                                      MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                                      if (main != null)
                                      {
                                          main.BeginInvoke(new EventHandler((s, e) =>
                                          {
                                              main.Close();
                                          }));
                                          Thread t = new Thread(new ThreadStart(() =>
                                          {
                                              UpdateForm frm = new UpdateForm(json.data.apkUrl);
                                              Application.Run(frm);
                                          }));
                                          t.SetApartmentState(ApartmentState.STA);
                                          t.Start();
                                      }
                                  }
                                  else
                                  {
                                      if (MessageBox.Show("发现新版本,是否升级？", "版本更新", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                      {
                                          LoginForm loginForm = Application.OpenForms["LoginForm"] as LoginForm;
                                          if (loginForm != null)
                                          {
                                              loginForm.BeginInvoke(new EventHandler((s, e) =>
                                              {
                                                  loginForm.Close();
                                              }));
                                              Thread t = new Thread(new ThreadStart(() =>
                                              {
                                                  UpdateForm frm = new UpdateForm(json.data.apkUrl);
                                                  Application.Run(frm);
                                              }));
                                              t.SetApartmentState(ApartmentState.STA);
                                              t.Start();
                                          }
                                          MainFrm main = Application.OpenForms["MainFrm"] as MainFrm;
                                          if (main != null)
                                          {
                                              main.BeginInvoke(new EventHandler((s, e) =>
                                              {
                                                  main.Close();
                                              }));
                                              Thread t = new Thread(new ThreadStart(() =>
                                              {
                                                  UpdateForm frm = new UpdateForm(json.data.apkUrl);
                                                  Application.Run(frm);
                                              }));
                                              t.SetApartmentState(ApartmentState.STA);
                                              t.Start();
                                          }
                                      }
                                  }
                              }
                          }
                      }
                  }, (code) =>
                 {
                  });
            })).Start();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new LoginForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            OSSHelper.shard.upLoadLog(str);
            Logger.Error(e.ExceptionObject as Exception, str);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            OSSHelper.shard.upLoadLog(str);
            Logger.Error(e.Exception as Exception, str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }

    }
}
