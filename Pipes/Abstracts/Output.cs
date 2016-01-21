﻿using Mod.Configuration.Properties;
using Mod.Modules.Abstracts;
using Pipes.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public abstract class Output<T>: Initiator, IOutput<T> where T: IMessage
    {
        [Configure]
        public ConcurrentDictionary<INotify, INotify> OutputListeners
        {
            get;
            set;
        }

        public Output()
        {
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                OutputListeners = new ConcurrentDictionary<INotify, INotify>();
                return true;
            }
            return false;
        }

        public virtual T Pop()
        {
            T result = (T)PopObject();
            for (int i = 0; i < OutputListeners.Count; i++)
                OutputListeners.ElementAt(i).Value.CallDelegate(result);
            return result;
        }

        public abstract object PopObject();

        public abstract bool PushObject(object element);

        public virtual void AddOutputListener(INotify outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }

        bool NotifyPush(IMessage element)
        {
            return PushObject(element);
        }
        
        public INotify FabricateOutputNotifier(bool Duplicate = true)
        {
            return new Notify(NotifyPush) { Duplicate = Duplicate };
        }
    }
}
