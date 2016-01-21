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
    public abstract class Input<T>: Initiator, IInput<T> where T: IMessage
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
            if(element is T)
            {
                for (int i = 0; i < InputListeners.Count; i++)
                {
                    INotify notify = InputListeners.ElementAt(i).Value;
                    if (notify.Duplicate)
                        notify.NotifyDelegate.DynamicInvoke(element.Clone());
                    else
                        if ((bool)notify.NotifyDelegate.DynamicInvoke(element.Clone()))
                            break;
                }
                return PushObject(element);
            }
            return false;
        }

        bool NotifyPush(IMessage element)
        {
            return Push((T)element);
        }

        public abstract object PopObject();

        public abstract bool PushObject(object element);

        public virtual void AddInputListener(INotify inputListener)
        {
            InputListeners[inputListener] = inputListener;
        }

        public INotify FabricateInputNotifier(bool Duplicate = false)
        {
            return new Notify(NotifyPush) { Duplicate = Duplicate };
        }
    }
}
