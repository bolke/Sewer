using Mod.Configuration.Properties;
using Mod.Interfaces;
using Mod.Interfaces.Containers;
using Mod.Modules.Abstracts;
using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Pump<T>: Lockable, IRunnable where T: class, IMessage
    {
        #region Variables

        private Thread thread = null;

        private bool stopped = false;
        private bool started = false;
        private bool running = false;
        private bool paused = false;
        private bool autoStart = false;

        private object[] delegateParameters = null;
        private Delegate delegated = null;

        #endregion Variables

        #region Properties
        [Configure(InitType = typeof(ConcurrentQueue<Tuple<IOutput, IInput>>))]
        public ConcurrentQueue<Tuple<IOutput, IInput>> Flows { get; set; }

        [Configure(DefaultValue = false)]
        public virtual bool AutoStart
        {
            get
            {
                lock(Padlock) 
                    return autoStart;
            }
            set
            {
                lock(Padlock)
                    autoStart = value;
            }
        }

        [Configure(DefaultValue=1)]
        public virtual int Interval
        {
            get;
            set;
        }

        public virtual Delegate Delegated
        {
            get
            {
                lock(Padlock) return delegated;
            }
            set
            {
                if(!IsRunning)
                {
                    lock(Padlock)
                    {
                        if(!IsRunning)
                        {
                            delegated = value;
                        }
                    }
                }
            }
        }

        public virtual object[] DelegateParameters
        {
            get
            {
                lock(Padlock) 
                    return delegateParameters;
            }
            set
            {
                lock(Padlock) 
                    delegateParameters = value;
            }
        }

        public virtual bool IsStarted
        {
            get
            {
                lock(Padlock) 
                    return started;
            }
            set
            {
                lock(Padlock)
                    started = value;
            }
        }

        public virtual bool IsStopped
        {
            get
            {
                lock(Padlock) 
                    return stopped;
            }
            set
            {
                lock(Padlock)
                    stopped = value;
            }
        }

        public virtual bool IsRunning
        {
            get
            {
                lock(Padlock) 
                    return (running);
            }
            set
            {
                lock(Padlock)
                    running = value;
            }
        }

        public virtual bool IsPaused
        {
            get
            {
                lock(Padlock) 
                    return paused;
            }
            set
            {
                lock(Padlock)
                    paused = value;
            }
        }
        #endregion

        public virtual bool Start()
        {
            lock(Padlock)
            {
                if(!IsStarted)
                {
                    IsRunning = true;
                    IsStarted = true;
                    thread.Start();
                    return true;
                }
            }
            return false;
        }

        public virtual bool Pause()
        {
            lock(Padlock)
            {
                if((IsStarted) && (!IsPaused))
                {                    
                    IsPaused = true;
                    Monitor.Wait(thread);
                    return true;
                }
            }
            return false;
        }

        public virtual bool Resume()
        {
            lock(Padlock)
            {
                if(IsPaused)
                {
                    Monitor.Pulse(thread);
                    IsPaused = false;
                    return true;
                }
            }
            return false;
        }

        public virtual bool Stop()
        {
            lock(Padlock)
            {
                if(!IsStopped)
                {
                    IsStopped = true;
                    IsStarted = false;
                    IsRunning = false;
                    return true;
                }
            }
            return false;
        }

        protected void ThreadMain()
        {
            while(IsRunning)
            {
                if(!IsPaused)
                {
                    Tuple<IOutput,IInput> flow = null;
                    if(Flows.TryDequeue(out flow))
                    {       
                        IMessage item = null;
                        Type t = flow.Item1.GetType();                        
                        item = flow.Item1.PopIMessage();
                        if(item != null)                        
                            flow.Item2.PushIMessage(item);                                                    
                        Flows.Enqueue(flow);
                    }
                    if(Delegated != null)
                        Delegated.DynamicInvoke(DelegateParameters);
                }
                Thread.Sleep(Interval);
            }
        }

        public virtual void AddFlow(IOutput item1, IInput item2)
        {
            Flows.Enqueue(new Tuple<IOutput, IInput>(item1, item2));
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                IsStarted = false;
                IsStopped = false;
                IsPaused = false;
                IsRunning = false;
                thread = new Thread(this.ThreadMain);
                return true;
            }
            return false;
        }

        public override bool Cleanup()
        {
            if(base.Cleanup())
            {
                IsStarted = false;
                IsStopped = false;
                IsPaused = false;
                IsRunning = false;
                return true;
            }
            return false;
        }
    
    }
}
