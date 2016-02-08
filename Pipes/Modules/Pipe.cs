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
    public class Pipe<T> : Lockable, IPipe<T>, IInput<T>, IOutput<T> where T : class, IMessage
    {
        private IProducerConsumerCollection<T> queue = null;

        #region IInput<T>
        [Configure(DefaultValue = null)]
        public virtual IInput<T> Input
        {
            get;
            set;
        }

        [Configure(DefaultValue = null)]
        public ConcurrentDictionary<INotify, INotify> InputListeners
        {
            get;
            set;
        }

        public virtual bool Push(T element)
        {
            bool result = false;
            if (element != null)
            {
                if (Input != null && (Input != this))
                    result = Input.Push(element);
                else
                    result = PushObject(element);

                if (result)
                {
                    for (int i = 0; i < InputListeners.Count; i++)
                        InputListeners.ElementAt(i).Value.CallDelegate(this);
                }
            }
            return result;
        }

        object Pipes.Interfaces.IInput<T>.PopObject()
        {
            return Input.PopObject();
        }

        bool Pipes.Interfaces.IInput<T>.PushObject(object element)
        {
            if (Input != null && Input != this)
                return Input.PushObject(element);
            else if (element.GetType().IsAssignableFrom(typeof(T)))
                return queue.TryAdd((T)element);
            return false;
        }
        public virtual void AddInputNotify(INotify inputListener)
        {
            InputListeners[inputListener] = inputListener;
        }

        public virtual bool PushIMessage(IMessage item)
        {
            return Push(item as T);
        }

        #endregion

        #region IOutput<T>
        [Configure(DefaultValue = null)]
        public virtual IOutput<T> Output
        {
            get;
            set;
        }

        [Configure(DefaultValue = null)]
        public ConcurrentDictionary<INotify, INotify> OutputListeners
        {
            get;
            set;
        }

        public virtual T Pop()
        {
            T result = default(T);
            if ((Output != null) && (Output != this))
                result = (T)Output.Pop();
            else
                result = (T)PopObject();
            for (int i = 0; i < OutputListeners.Count; i++)
                OutputListeners.ElementAt(i).Value.CallDelegate(this);
            return result;
        }

        bool Pipes.Interfaces.IOutput<T>.PushObject(object element)
        {
            return Output.PushObject(element);
        }

        object Pipes.Interfaces.IOutput<T>.PopObject()
        {
            return Output.PopObject();
        }

        public virtual void AddOutputNotify(INotify outputListener)
        {
            OutputListeners[outputListener] = outputListener;
        }

        public virtual IMessage PopIMessage()
        {
            return Pop();
        }
        #endregion

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                if (InputListeners == null)
                    InputListeners = new ConcurrentDictionary<INotify, INotify>();
                if (OutputListeners == null)
                    OutputListeners = new ConcurrentDictionary<INotify, INotify>();
                queue = new ConcurrentQueue<T>();
                if (Input == null)
                    Input = this;
                if (Output == null)
                    Output = this;
                return true;
            }
            return false;
        }

        public virtual bool PushObject(object element)
        {
            if (Input != null)
            {
                if (Input == this)
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
                    if (!queue.TryTake(out result))
                        result = default(T);
                    return result;
                }
                else
                    return Output.PopObject();
            }
            return null;
        }
    }
}