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
    public abstract class Input: Lockable, IObjectContainer
    {
        public abstract bool PushIMessage(IMessage item);
         public abstract object PopObject();

         public abstract bool PushObject(object element);
    }

    public abstract class Input<T>: Input, IInput<T> where T: class, IMessage
    {
        [Configure]
        public ConcurrentDictionary<INotify, INotify> InputListeners
        {
            get;
            set;
        }

        public Input()
        {
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                InputListeners = new ConcurrentDictionary<INotify, INotify>();
                return true;
            }
            return false;
        }

        public virtual bool Push(T element)
        {
            if(element != null)
            {
                if(PushObject(element))
                {
                    for(int i = 0; i < InputListeners.Count; i++)
                        InputListeners.ElementAt(i).Value.CallDelegate(this);
                    return true;
                }
            }
            return false;
        }

        public virtual void AddInputNotify(INotify inputListener)
        {
            InputListeners[inputListener] = inputListener;
        }

        public override bool PushIMessage(IMessage item)
        {
            if(typeof(T).IsAssignableFrom(item.GetType()))
                return Push((T)item);
            return false;
        }
    }
}
