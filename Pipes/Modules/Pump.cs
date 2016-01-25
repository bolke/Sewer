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

        [Configure(InitType = typeof(ConcurrentQueue<Tuple<IObjectContainer, IObjectContainer>>))]
        public ConcurrentQueue<Tuple<IObjectContainer, IObjectContainer>> Flows { get; set; }

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
                    thread.Suspend();
                    IsPaused = true;
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
                    thread.Resume();
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
                    Tuple<IObjectContainer,IObjectContainer> flow = null;
                    if(Flows.TryDequeue(out flow))
                    {       
                        IMessage item = null;
                        Type t = flow.Item1.GetType();
                        if(typeof(Output).IsAssignableFrom(flow.Item1.GetType()))
                            item = (flow.Item1 as Output).PopIMessage();
                        if(item != null)
                        {
                            if(typeof(Input).IsAssignableFrom(flow.Item2.GetType()))
                                (flow.Item2 as Input).PushIMessage(item);
                        }
                        Flows.Enqueue(flow);
                    }
                    //if(Delegated != null)
                    //    Delegated.DynamicInvoke(DelegateParameters);
                }
                Thread.Sleep(1);
            }
        }

        public virtual void AddFlow(IOutput output,IInput input)
        {
            Flows.Enqueue(new Tuple<IObjectContainer, IObjectContainer>(output, input));
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
