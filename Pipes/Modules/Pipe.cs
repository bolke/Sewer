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
    public class Pipe<T>:Initiator, IPipe<T>, IInput<T>, IOutput<T>, IInputListener where T:IMessage
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
            if(Input != null && Input != this)
                return Input.PushObject(element);
            else if(element.GetType().IsAssignableFrom(typeof(T)))
                return queue.TryAdd((T)element);
            return false;
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
                OutputListeners.ElementAt(i).Value.CallDelegate(this);
            return result;
        }

        public virtual bool Push(T element)
        {
            for(int i = 0; i < InputListeners.Count; i++)
                InputListeners.ElementAt(i).Value.CallDelegate(this);
            if(Input != null && (Input != this))
                return Input.Push(element);
            return PushObject(element);
        }

        public virtual void AddInputNotify(INotify inputListener)
        {
            InputListeners[inputListener] = inputListener;
        }

        public virtual void AddOutputNotify(INotify outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }

        protected bool NotifyInput(IUnique caller)
        {
            Console.WriteLine("STUFF COMES IN " + caller.GetType().Name);
            return true;
        }

        public INotify FabricateInputNotify()
        {
            return new Notify(NotifyInput);
        }
    }
}
