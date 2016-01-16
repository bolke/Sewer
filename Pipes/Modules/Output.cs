using Mod.Configuration.Properties;
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
    public class Output<T>: Initiator, IOutput<T> where T:IClone
    {
        [Configure]
        public ConcurrentDictionary<INotify<T>, INotify<T>> OutputListeners
        {
            get;
            set;
        }

        [Configure(InitType = typeof(ConcurrentQueue<>))]
        public virtual IProducerConsumerCollection<T> Queue { get; set; }

        [Configure(DefaultValue = null)]
        public virtual IInput<T> Input { get; set; }

        public Output()
        {
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                OutputListeners = new ConcurrentDictionary<INotify<T>, INotify<T>>();
                return true;
            }
            return false;
        }

        public virtual T Pop()
        {
            T result = (T)PopObject();
            for (int i = 0; i < OutputListeners.Count; i++)
            {
                INotify<T> notify = OutputListeners.ElementAt(i).Value;
                if (notify.Duplicate)
                    notify.NotifyDelegate.DynamicInvoke(result.Clone());
                else
                    if ((bool)notify.NotifyDelegate.DynamicInvoke(result))
                        break;
            }
            return result;
        }

        public virtual object PopObject()
        {
            T result;
            if(Queue.TryTake(out result))
            {
                return result;
            }
            return default(T);
        }

        public virtual bool PushObject(object element)
        {
            if(element is T)
            {
                Queue.TryAdd((T)element);
                return true;
            }
            return false;
        }

        public virtual void RegisterOutputListener(INotify<T> outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }
    }
}
