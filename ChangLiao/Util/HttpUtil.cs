using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Http;
using ChangLiao.Util;
using Newtonsoft.Json;
using ChangLiao.Model.ReciveModel;
using System.Reflection;
using System.Threading;

namespace ChangLiao
{

    public delegate void NetworkCallBack<T>(T jsonres);
    public delegate void NetworkFeild(int code);
    public delegate void DownLoadCallBack(bool complite);
    public delegate void DownloadProgress(float progress);
    /// <summary>
    /// 网络请求库
    /// </summary>
    public class HttpUitls
    {
        static HttpUitls instance;
        private static readonly object padlock = new object();
        private HttpClient httpClient;
        private List<string> downloadUrls;
        private static readonly object dowloadLock = new object();
        private NetworkTaskScheduler request;
        /// <summary>
        /// 获取单例对象
        /// </summary>
        public static HttpUitls Instance
        {
            get{
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new HttpUitls();
                    }
                    return instance;
                }
            }
        }

        private HttpUitls()
        {
            //初始化
            httpClient = new HttpClient();
            var url = AppSettingHelper.getAppConfig("baseUrl");
            if(url != null)
            {
                httpClient.BaseAddress = new Uri(url);
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.IMEI))
            {
                Properties.Settings.Default.IMEI = "windows:" + Guid.NewGuid().ToString().Replace("-", "");
                Properties.Settings.Default.Save();
            }
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            httpClient.DefaultRequestHeaders.Add("dbrand", new GetOSystem().getSystem());
            httpClient.DefaultRequestHeaders.Add("deviceinfo", new GetOSystem().getSystem());
            httpClient.DefaultRequestHeaders.Add("client", "windows");
            httpClient.DefaultRequestHeaders.Add("imei", Properties.Settings.Default.IMEI);
            Assembly currentAssembly = Assembly.LoadFile(Assembly.GetExecutingAssembly().Location);
            httpClient.DefaultRequestHeaders.Add("version", currentAssembly.GetName().Version.ToString());
            downloadUrls = new List<string>();
            request = new NetworkTaskScheduler(Environment.ProcessorCount);
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <typeparam name="T">返回类型模型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="par">请求参数</param>
        /// <param name="callBack">成功回调</param>
        /// <param name="feild">失败回调</param>
        /// <returns></returns>
        public Thread get<T>(String url,object par,NetworkCallBack<T> callBack, NetworkFeild feild)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                String geturl;
                if (par != null)
                {
                    geturl = MapUtil.ModelToUriParam(par, url);
                }
                else
                {
                    geturl = url;
                }
                if (!string.IsNullOrEmpty(SettingMenager.shard.token))
                {
                    if (!httpClient.DefaultRequestHeaders.Contains("imToken"))
                    {
                        httpClient.DefaultRequestHeaders.Add("imToken", SettingMenager.shard.token);
                    }
                }
                try
                {
                    _ = httpClient.GetAsync(geturl).ContinueWith(async res =>
                    {
                        var result = res.Result;
                        if (result.StatusCode == HttpStatusCode.OK)
                        {
                            var body = result.Content.ReadAsStringAsync().ConfigureAwait(true).GetAwaiter().GetResult();
                            callBack(JsonConvert.DeserializeObject<T>(body));
                            return;
                        }
                        else if (result.StatusCode == HttpStatusCode.NotImplemented)
                        {
                            feild(501);
                            throw new ChangLiaoException("你还没有登录");
                        }
                        else if (result.StatusCode == HttpStatusCode.BadGateway)
                        {
                            feild(502);
                            throw new ChangLiaoException("身份验证失败");
                        }
                        else
                        {
                            feild((int)result.StatusCode);
                            throw new ChangLiaoException("链接服务器失败");
                        }
                    });
                }
                catch (Exception e)
                {
                }
                finally
                {

                }
            }));
            t.IsBackground = true;
            t.Start();
            return t;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="localPath">本地地址</param>
        /// <param name="complite">完成回调</param>
        public void downLoadFile(String url,String localPath,DownLoadCallBack complite)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(url, localPath);
                    if (File.Exists(localPath))
                    {
                        complite(true);
                    }
                    else
                    {
                        complite(false);
                    }
                }
                catch
                {
                    complite(false);
                }
            })).Start();
        }

        /// <summary>
        /// 下载文件 进度显示
        /// </summary>
        /// <param name="URL">下载地址</param>
        /// <param name="filename">下载路径</param>
        /// <param name="progress">下载进度</param>
        /// <param name="callBack">完成回调</param>
        public void DownloadFile(string URL, string filename, DownloadProgress progress,DownLoadCallBack callBack)
        {
            float percent = 0;
            Console.WriteLine(URL);
            try
            {
                lock (dowloadLock)
                {
                    if (downloadUrls.Contains(URL))
                    {
                        return;
                    }
                    downloadUrls.Add(URL);
                }
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                var date = DateTime.Now;
                GC.AddMemoryPressure(totalBytes);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    var sp = DateTime.Now - date;
                    date = DateTime.Now;
                    if(sp.Seconds > 1)
                    {
                        percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                        if (progress != null)
                        {
                            Console.WriteLine(percent + "=================================" + filename);
                            progress(percent);
                        }
                    }
                }
                so.Close();
                st.Close();
                if (callBack != null)
                {
                    callBack(true);
                }
                lock (dowloadLock)
                {
                    downloadUrls.Remove(URL);
                }
            }
            catch
            {
                callBack(false);
                lock (dowloadLock)
                {
                    downloadUrls.Remove(URL);
                }
            }
        }
    }

    class ChangLiaoException : ApplicationException
    {
        public ChangLiaoException(string message) : base(message) { }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        public ChangLiaoException()
        {
        }

        public ChangLiaoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
