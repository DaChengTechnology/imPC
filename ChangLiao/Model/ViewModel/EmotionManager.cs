using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChangLiao.Model.ViewModel
{
    /// <summary>
    /// 表情管理器
    /// </summary>
    class EmotionManager
    {
        private static EmotionManager manager;
        private static object padlock = new object();
        /// <summary>
        /// 所有表情
        /// </summary>
        private List<EmotionModel> emotions;
        public List<EmotionModel> allEmotions { get => emotions; }
        public static EmotionManager shard
        {
            get
            {
                lock (padlock)
                {
                    if (manager == null)
                    {
                        manager = new EmotionManager();
                    }
                    return manager;
                }
            }
        }

        private EmotionManager()
        {
            emotions = new List<EmotionModel>();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                using (StreamReader sr=new StreamReader(Directory.GetCurrentDirectory()+"\\emotionDB.json"))
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    var dic = serializer.Deserialize<Dictionary<string, string>>(jr);
                    foreach (var key in dic.Keys)
                    {
                        var c = new EmotionModel();
                        c.id = key;
                        c.face = Properties.Resources.ResourceManager.GetObject(dic[key]) as Image;
                        if (c.face != null)
                        {
                            emotions.Add(c);
                        }
                    }
                }
            });
        }

        public EmotionModel GetEmotion(string id)
        {
            foreach (var item in emotions)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
