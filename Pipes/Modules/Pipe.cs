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

        public INotify FabricateInputNotifier(bool Duplicate = true)
        {
            if(Input != null && Input != this)
                return Input.FabricateInputNotifier(Duplicate);
            return new Notify(NotifyPush) { Duplicate = Duplicate };
        }

        object Pipes.Interfaces.IInput<T>.PopObject()
        {
            return Input.PopObject();
        }

        bool Pipes.Interfaces.IInput<T>.PushObject(object element)
        {
            if(Input != null && Input != this)
                Input.PushObject(element);
            else
                queue.TryAdd(element);
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

        public INotify FabricateOutputNotifier(bool Duplicate = true)
        {
            if(Output != null && Output != this)
                return Output.FabricateOutputNotifier(Duplicate);
            return new Notify(NotifyPush) { Duplicate = Duplicate };
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
                    InputListeners = new ConcurrentDictionary<INotify, INotify>();
                if(OutputListeners == null)
                    OutputListeners = new ConcurrentDictionary<INotify, INotify>();
                queue = new ConcurrentQueue<T>();
                Input = this;
                Output = this;
                return true;
            }
            return false;
        }

        bool NotifyPush(IMessage element)
        {
            return Push((T)element);
        }

        [Configure(DefaultValue=null)]
        public ConcurrentDictionary<INotify, INotify> InputListeners
        {
            get;
            set;
        }

        [Configure(DefaultValue=null)]
        public ConcurrentDictionary<INotify, INotify> OutputListeners
        {
            get;
            set;
        }

        public virtual bool PushObject(object element)
        {
            if (Input != null)
            {
                if(Input == this)
                    return queue.TryAdd((T)element);
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
            for(int i = 0; i < OutputListeners.Count; i++)
            {
                if(OutputListeners.ElementAt(i).Value.Duplicate && result.Duplicate)
                    OutputListeners.ElementAt(i).Value.CallDelegate(result);
                else if(OutputListeners.ElementAt(i).Value.CallDelegate(result))
                    break;  
            }
            return result;
        }

        public virtual bool Push(T element)
        {
            for(int i = 0; i < InputListeners.Count; i++)
            {
                if(InputListeners.ElementAt(i).Value.Duplicate && element.Duplicate)
                    InputListeners.ElementAt(i).Value.CallDelegate(element);
                else if(InputListeners.ElementAt(i).Value.CallDelegate(element))
                    return true;
            }
            if(Input != null && (Input != this))
                return Input.Push(element);
            return PushObject(element);
        }

        public virtual void AddInputListener(INotify inputListener)
        {
            InputListeners[inputListener] = inputListener;
        }

        public virtual void AddOutputListener(INotify outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }
    }
}
