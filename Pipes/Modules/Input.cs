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
    public class Input<T>: Initiator, IInput<T> where T:class
    {
        public ConcurrentDictionary<INotify<T>, INotify<T>> InputListeners
        {
            get;
            set;
        }

        [Configure(InitType=typeof(ConcurrentQueue<>))]
        public ConcurrentQueue<T> Queue { get; set; }

        public Input()
        {
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                InputListeners = new ConcurrentDictionary<INotify<T>, INotify<T>>();
                return true;
            }
            return false;
        }

        public virtual bool Push(T item)
        {
            return PushObject(item);
        }

        public virtual object PopObject()
        {
            T result;
            if(Queue.TryDequeue(out result))
                return result;
            return null;
        }

        public virtual bool PushObject(object element)
        { 
            if(element is T)
            {
                for(int i = 0; i < InputListeners.Count; i++)
                    InputListeners.ElementAt(i).Value.Notify(element as T);
                Queue.Enqueue(element as T);
                return true;
            }
            return false;
        }
    }
}
