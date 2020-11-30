using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChangLiao.Util
{
    public class NetworkTaskScheduler : TaskScheduler, IDisposable
    {
        //调用Task的线程
        Thread[] _Threads;

        //Task Collection
        BlockingCollection<Task> _Tasks = new BlockingCollection<Task>();

        int _ConcurrencyLevel;


        //设置schedule并发
        public NetworkTaskScheduler(int concurrencyLevel)
        {

            _Threads = new Thread[concurrencyLevel];
            this._ConcurrencyLevel = concurrencyLevel;


            for (int i = 0; i < concurrencyLevel; i++)
            {
                _Threads[i] = new Thread(() =>
                {
                    foreach (Task task in _Tasks.GetConsumingEnumerable())
                        this.TryExecuteTask(task);

                });

                _Threads[i].Start();
            }

        }

        protected override void QueueTask(Task task)
        {
            _Tasks.Add(task);
        }


        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {

            if (_Threads.Contains(Thread.CurrentThread)) return TryExecuteTask(task);

            return false;
        }

        public override int MaximumConcurrencyLevel
        {
            get
            {
                return _ConcurrencyLevel;
            }
        }


        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _Tasks.ToArray();
        }


        public void Dispose()
        {
            this._Tasks.CompleteAdding();
            foreach (Thread t in _Threads)
            {
                t.Join();
            }

        }
    }
}
