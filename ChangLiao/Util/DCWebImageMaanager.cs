using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChangLiao.DB;
using System.IO;
using ChangLiao.Model.ViewModel;
using System.Data.Common;
using System.Data.SQLite.EF6;
using System.Data.Entity.Validation;

namespace ChangLiao.Util
{
    /// <summary>
    /// 等待模型
    /// </summary>
    class WaitModel
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 完成回调
        /// </summary>
        public DCWebImageMaanager.DCImageDownLoadComplite complite { get; set; }
    }
    class DCWebImageMaanager
    {
        public delegate void DCImageDownLoadComplite(Image imageLocal,bool isLoacal);
        public delegate void DCFileDownLoadComplite(string filePath);
        private static DCWebImageMaanager maanager;
        private static readonly object padlock = new object();
        private static readonly object key = new object();
        private static readonly object pkey = new object();
        private CLCacheDBContext db;
        private BackgroundQueue queue;
        private List<ImageModel> imageModels;
        private List<string> loading;
        private List<WaitModel> waits;
        public static DCWebImageMaanager shard
        {
            get
            {
                lock(padlock){
                    if(maanager == null)
                    {
                        maanager = new DCWebImageMaanager();
                    }
                    return maanager;
                }
            }
        }
        private DCWebImageMaanager()
        {
            DbConnection sqliteCon = SQLiteProviderFactory.Instance.CreateConnection();
            sqliteCon.ConnectionString = "data source=" + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\app\\" + SettingMenager.shard.idCard + "\\db\\temp.db;foreign keys=true";
            db = new CLCacheDBContext(sqliteCon);
            queue = new BackgroundQueue();
            imageModels = new List<ImageModel>();
            loading = new List<string>();
            waits = new List<WaitModel>();
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="complite">完成回调</param>
        public void downloadImageAsync(string url, DCImageDownLoadComplite complite)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            lock (key)
            {
                foreach (var item in imageModels)
                {
                    if (item.url == url)
                    {
                        complite(item.image, true);
                        return;
                    }
                }
            }
            var list = queue.QueueTask<List<LocalTempModel>>(() => { return db.localTemps.Where(p => p.url == url).ToList(); });
            if(list.Count > 0)
            {
                if (File.Exists(list[0].localPath))
                {
                    Image image = Image.FromFile(list[0].localPath);
                    complite(image, true);
                    lock (key)
                    {
                        ImageModel m = new ImageModel();
                        m.url = url;
                        m.image = image;
                        imageModels.Add(m);
                    }
                }
            }
            else
            {
                lock (pkey)
                {
                    if (loading.Contains(url))
                    {
                        WaitModel model = new WaitModel();
                        model.url = url;
                        model.complite = complite;
                        waits.Add(model);
                        return;
                    }
                    loading.Add(url);
                }
                ThreadPool.QueueUserWorkItem((s) =>
                {
                    string localPath = DCWebImageMaanager.genratePath(url);
                    HttpUitls.Instance.downLoadFile(url, localPath, (b) =>
                      {
                          if (b)
                          {
                              if (!File.Exists(localPath))
                              {
                                  complite(null, false);
                                  return;
                              }
                              Image image = Image.FromFile(localPath);
                              lock (key)
                              {
                                  GC.AddMemoryPressure(8 * 3 * 1024);
                                  ImageModel m = new ImageModel();
                                  m.url = url;
                                  m.image = image;
                                  imageModels.Add(m);
                              }
                              complite(image, false);
                              lock (pkey)
                              {
                                  List<WaitModel> willDelete=new List<WaitModel>();
                                  foreach (var item in waits)
                                  {
                                      if (item.url == url)
                                      {
                                          item.complite(image, false);
                                          willDelete.Add(item);
                                      }
                                  }
                                  foreach (var item in willDelete)
                                  {
                                      waits.Remove(item);
                                  }
                                  loading.Remove(url);
                              }
                              queue.QueueTask(() =>
                              {
                                  db.localTemps.RemoveRange(db.localTemps.Where(p => p.url == url).AsEnumerable());
                                  db.SaveChanges();
                                  LocalTempModel model = new LocalTempModel();
                                  model.url = url;
                                  model.localPath = localPath;
                                  db.localTemps.Add(model);
                                  try
                                  {
                                      db.SaveChanges();
                                  }
                                  catch (DbEntityValidationException dbEx)
                                  {
                                      System.Console.WriteLine(dbEx.Message);
                                  }
                              });
                          }
                          else
                          {
                              complite(null, false);
                          }
                      });
                });
            }
        }
        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string genratePath(string url)
        {
            string fileName = "";
            string trueURL = url;
            if (trueURL.Contains("?"))
            {
                trueURL = trueURL.Substring(0, trueURL.IndexOf("?"));
            }
            string fileExt = trueURL.Substring(trueURL.LastIndexOf(".")).Trim().ToLower();
            if (fileExt.Length > 4)
            {
                fileExt = string.Empty;
            }
            Random rnd = new Random();
            fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + rnd.Next(10, 99).ToString() + fileExt;
            if(!Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\AppTmp\\"))
            {
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\AppTmp\\");
            }
            fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\AppTmp\\" + fileName;
            return fileName;
        }
    }
}
