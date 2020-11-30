using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections;

namespace ChangLiao.Util
{
    struct Workers<T>
    {
        public Action action { get; set; }
        public Func<T> func { get; set; }
        public Complite<T> t { get; set; }
        public bool ace { get; set; }
        public string uuid { get; set; }

    }
    delegate void Complite<T>(T data,string uuid);
    /// <summary>
    /// 线程合并
    /// </summary>
    class BackgroundQueue
    {
        private object key = new object();
        private object pkey = new object();
        private BackgroundWorker main;
        private ArrayList workers;
        private Timer timer;
        public BackgroundQueue()
        {
            //workers = new ArrayList();
            //main = new BackgroundWorker();
            //main.DoWork += Main_DoWork;
            //timer = new Timer(new TimerCallback(timerExcute));
            //timer.Change(30, 5);
        }

        private void Main_DoWork(object sender, DoWorkEventArgs e)
        {
            //Workers<T> a = (Workers<T>)
        }

        private void timerExcute(object o)
        {
            if (!main.IsBusy)
            {
                if(workers.Count > 0)
                {
                    main.RunWorkerAsync(workers[0]);
                    workers.RemoveAt(0);
                }
            }
        }
        /// <summary>
        /// 合并无返回值线程
        /// </summary>
        /// <param name="action">运行函数</param>
        public void QueueTask(Action action)
        {
            //Workers<Type> w = new Workers<Type>();
            //w.ace = false;
            //w.action = action;
            //w.uuid = new Guid().ToString();
            //object wait = null;
            //w.t = (js, uuid) =>
            //{
            //    if (uuid == w.uuid)
            //    {
            //        wait = false;
            //    }
            //};
            //workers.Add(w);
            //while (wait == null)
            //{
            //    Thread.Sleep(10);
            //}
            lock (key)
            {
                action();
            }
        }
        /// <summary>
        /// 合并有返回值线程
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="work">运行函数</param>
        /// <returns></returns>
        public T QueueTask<T>(Func<T> work)
        {
            //T obj = default(T);
            //Workers<T> w = new Workers<T>();
            //w.ace = false;
            //w.func = work;
            //w.uuid = new Guid().ToString();
            //bool wait = true;
            //w.t = (js, uuid) =>
            //{
            //    if (uuid == w.uuid)
            //    {
            //        obj = (T)js;
            //        wait = false;
            //    }
            //};
            //workers.Add(w);
            //while (wait)
            //{
            //    Thread.Sleep(10);
            //}
            //return obj;
            lock (key)
            {
                return work();
            }
        }
        public void QueueTaskAsync(Action action,bool ace)
        {
            //Workers<Type> w = new Workers<Type>();
            //w.ace = ace;
            //w.action = action;
            //w.uuid = new Guid().ToString();
            //workers.Add(w);
            lock (key)
            {
                action();
            }
        }
        /// <summary>
        /// 合并有返回值线程
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="work">线程运行函数</param>
        /// <param name="ace">是否优先</param>
        /// <returns></returns>
        public T QueueTask<T>(Func<T> work, bool ace)
        {
            //T obj = default(T);
            //Workers<T> w = new Workers<T>();
            //w.ace = ace;
            //w.func = work;
            //w.uuid = new Guid().ToString();
            //bool wait = true;
            //w.t = (js, uuid) =>
            //{
            //    if (uuid == w.uuid)
            //    {
            //        obj = (T)js;
            //        wait = false;
            //    }
            //};
            //workers.Add(w);
            //while (wait)
            //{
            //    Thread.Sleep(10);
            //}
            //return obj;
            lock (key)
            {
                return work();
            }
        }
    }
}
