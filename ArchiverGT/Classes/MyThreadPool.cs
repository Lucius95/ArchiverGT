using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ArchiverGT.Classes
{
    class MyThreadPool : IDisposable
    {
        private readonly Thread[] _threads;
        private readonly Queue<Action> _actions;
        private readonly object _syncRoot = new object();

        public MyThreadPool(int maxThread = 4)
        {
            _threads = new Thread[maxThread];
            _actions = new Queue<Action>();
            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i] = new Thread(ThreadProc)
                {       
                    IsBackground = true,
                    Name = $"MyThreadPool Thread {i}"
                };
                _threads[i].Start();
            }
        }

        public void Queue(Action action)
        {
            Monitor.Enter(_syncRoot);
            try
            {
                _actions.Enqueue(action);
                if (_actions.Count == 1)
                {
                    Monitor.Pulse(_syncRoot);
                }
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }              
        }

        public void ThreadProc()
        {
            while (true)
            {
                Action action;
                Monitor.Enter(_syncRoot);
                try
                {

                    if (IsDisposed)
                    {
                        return;
                    }

                    if (_actions.Count > 0)
                    {
                        action = _actions.Dequeue();
                    }
                    else
                    {
                        Monitor.Wait(_syncRoot);
                        continue;
                    }
                    action();
                }
                finally
                {
                    Monitor.Exit(_syncRoot);
                }               
            }
        }

        public bool IsDisposed { get; private set; }    

        public void Dispose()
        {
            bool isDisposing = false;

            Monitor.Enter(_syncRoot);
            try
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                    Monitor.PulseAll(_syncRoot);
                    isDisposing = true;
                }
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }

            if (isDisposing)
            {
                for (int i = 0; i < _threads.Length; i++)
                {
                    _threads[i].Join();
                }
            }
        }
    }
}
