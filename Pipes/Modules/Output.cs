using Mod.Configuration.Properties;
using Mod.Modules.Abstracts;
using Pipes.Interfaces;
using Pipes.Interfaces.Containers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Output<T>: Initiator, INotify<T>, IOutput<T> where T: class
    {
        public ConcurrentDictionary<INotify<T>, INotify<T>> OutputListeners
        {
            get;
            set;
        }

        [Configure(InitType = typeof(ConcurrentQueue<>))]
        public ConcurrentQueue<T> Queue { get; set; }

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
            T result = PopObject() as T;
            for(int i = 0; i < OutputListeners.Count; i++)
                OutputListeners.ElementAt(i).Value.Notify(result);
            return result;
        }

        public virtual object PopObject()
        {
            T result;
            if(Queue.TryDequeue(out result))
            {
                return result;
            }
            return default(T);
        }

        public virtual bool PushObject(object element)
        {
            if(element is T)
            {
                Queue.Enqueue(element as T);
                return true;
            }
            return false;
        }


        public virtual void Notify(T element)
        {
            PushObject(element);
        }
    }
}
