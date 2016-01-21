using Mod.Configuration.Properties;
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
    public class Pipe<T>:Initiator, IPipe<T>, IInput<T>, IOutput<T> where T:IMessage
    {
        private IProducerConsumerCollection<T> queue = null;

        #region IInput<T>
        [Configure(DefaultValue=null)]
        public virtual IInput<T> Input
        {
            get;
            set;
        }

        object Pipes.Interfaces.IInput<T>.PopObject()
        {
            return Input.PopObject();
        }

        bool Pipes.Interfaces.IInput<T>.PushObject(object element)
        {
            return Input.PushObject(element);
        }

        #endregion

        #region IOutput<T>
        [Configure(DefaultValue=null)]
        public virtual IOutput<T> Output
        {
            get;
            set;
        }

        bool Pipes.Interfaces.IOutput<T>.PushObject(object element)
        {
            return Output.PushObject(element);
        }

        object Pipes.Interfaces.IOutput<T>.PopObject()
        {
            return Output.PopObject();
        }

        #endregion

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                if(InputListeners == null)
                    InputListeners = new ConcurrentDictionary<INotify<T>, INotify<T>>();
                if(OutputListeners == null)
                    OutputListeners = new ConcurrentDictionary<INotify<T>, INotify<T>>();
                queue = new ConcurrentQueue<T>();
                Input = this;
                Output = this;
                return true;
            }
            return false;
        }

        [Configure(DefaultValue=null)]
        public ConcurrentDictionary<INotify<T>, INotify<T>> InputListeners
        {
            get;
            set;
        }

        [Configure(DefaultValue=null)]
        public ConcurrentDictionary<INotify<T>, INotify<T>> OutputListeners
        {
            get;
            set;
        }

        public virtual bool PushObject(object element)
        {
            if (Input != null)
            {
                if(Input == this)
                    queue.TryAdd((T)element);
                else
                    return Input.PushObject(element);
            }
            return false;
        }

        public virtual object PopObject()
        {
            if (Output != null)
            {
                if (Output == this)
                {
                    T result;
                    if(!queue.TryTake(out result))
                        result = default(T);
                    return result;
                }
                else
                    return Output.PopObject();
            }
            return null;            
        }

        public virtual T Pop()
        {
            T result = default(T);
            if ((Output != null) && (Output != this))
                result = (T)Output.Pop();
            else
                result = (T)PopObject();
            for (int i = 0; i < OutputListeners.Count; i++)
            {
                INotify<T> notifier = OutputListeners.ElementAt(i).Value;
                if (notifier.Duplicate)
                    notifier.NotifyDelegate.DynamicInvoke(result.Clone());
                else
                    if ((bool)notifier.NotifyDelegate.DynamicInvoke(result))
                        break;
            }
            return result;
        }

        public virtual bool Push(T element)
        {
            if(element is T)
            {
                for (int i = 0; i < InputListeners.Count; i++)
                {
                    INotify<T> notifier = InputListeners.ElementAt(i).Value;
                    if (notifier.Duplicate)
                        notifier.NotifyDelegate.DynamicInvoke(element.Clone());
                    else
                        if ((bool)notifier.NotifyDelegate.DynamicInvoke(element))
                            break;
                }
                if (Input != null)
                {
                    if (Input != this)
                        return Input.Push(element);
                }
                return PushObject(element);
            }
            return false;
        }

        public virtual void RegisterInputListener(INotify<T> inputListener)
        {
            InputListeners[inputListener] = inputListener;
        }

        public virtual void RegisterOutputListener(INotify<T> outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }
    }
}
