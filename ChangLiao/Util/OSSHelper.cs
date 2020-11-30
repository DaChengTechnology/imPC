using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aliyun.OSS;

namespace ChangLiao.Util
{
    class OSSHelper
    {
        private static object padlock = new object();
        private static OSSHelper helper;
        private OssClient client;
        private HttpClient httpClient;
        public static OSSHelper shard
        {
            get
            {
                lock (padlock)
                {
                    if (helper == null)
                    {
                        helper = new OSSHelper();
                    }
                    return helper;
                }
            }
        }

        private OSSHelper()
        {
            var endpoint = "oss-cn-hongkong.aliyuncs.com";
            var accessKeyId = "LTAIV3Bi486ZiSiB";
            var accessKeySecret = "ps10ksRcHlClI1EMZJPmJ3Q9SWBCnU";
            client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            httpClient = new HttpClient();
        }
        /// <summary>
        /// 上传错误日志
        /// </summary>
        /// <param name="log"></param>
        public void upLoadLog(string log)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    byte[] binaryData = Encoding.UTF8.GetBytes(log);
                    MemoryStream requestContent = new MemoryStream(binaryData);
                    // 上传文件。
                    var objectKey = "clpc/errlog/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".log";
                    client.PutObject("hgjt-oss", objectKey, requestContent);
                    _ = httpClient.GetAsync("http://pclogapi.3nn1.com/api/saveLog?file_path=https://hgjt-oss.oss-cn-hongkong.aliyuncs.com/" + objectKey + "&app_type=1").ContinueWith(async res =>
                    {

                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Put object failed, {0}", ex.Message);
                }
            })).Start();
        }
    }
}
