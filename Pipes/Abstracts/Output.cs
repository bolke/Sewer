using Mod.Configuration.Properties;
using Mod.Interfaces.Config;
using Mod.Interfaces.Containers;
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
    public abstract class Output: Lockable, IOutput, IObjectContainer
    {
        public abstract void AddOutputNotify(INotify outputListener);
        public abstract IMessage PopIMessage();
        public abstract object PopObject();
        public abstract bool PushObject(object element);
    }

    public abstract class Output<T>: Output, IOutput<T> where T: class, IMessage
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
                OutputListeners.ElementAt(i).Value.CallDelegate(this);
            return result;
        }

        public override void AddOutputNotify(INotify outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }

        public override IMessage PopIMessage()
        {
            return this.Pop();
        }

    }
}
