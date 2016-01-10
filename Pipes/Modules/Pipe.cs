﻿using Mod.Configuration.Properties;
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
    public class Pipe<T>:Initiator, IPipe<T>, IInput<T>, IOutput<T> where T:IClone<T>
    {
        IProducerConsumerCollection<T> Pipes.Interfaces.IInput<T>.Queue
        {
            get 
            { 
                if(Input != null) 
                    return Input.Queue;
                return null;
            }
            set
            {
                if(Input != null) 
                    Input.Queue = value;
            }
        }
        IProducerConsumerCollection<T> Pipes.Interfaces.IOutput<T>.Queue
        {
            get
            {
                if(Output != null)
                    return Output.Queue;
                return null;
            }
            set
            {
                if(Output != null)
                    Output.Queue = value;
            }
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                if(InputListeners == null)
                    InputListeners = new ConcurrentDictionary<INotify<T>, INotify<T>>();
                if(OutputListeners == null)
                    OutputListeners = new ConcurrentDictionary<INotify<T>, INotify<T>>();
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

        [Configure(InitType = typeof(Input<>))]
        public virtual IInput<T> Input
        {
            get;
            set;
        }

        [Configure(InitType=typeof(Output<>))]
        public virtual IOutput<T> Output
        {
            get;
            set;
        }

        public virtual object PopObject()
        {
            return Output.PopObject();
        }

        public virtual bool PushObject(object element)
        {
            return Input.PushObject(element);
        }

        public virtual T Pop()
        {
            T result = (T)Output.Pop();
            for(int i = 0; i < OutputListeners.Count; i++)
                OutputListeners.ElementAt(i).Value.NotifyDelegate.DynamicInvoke(result.Clone());
            return result;
        }

        public virtual bool Push(T element)
        {
            if(element is T)
            {
                for(int i = 0; i < InputListeners.Count; i++)
                    InputListeners.ElementAt(i).Value.NotifyDelegate.DynamicInvoke(element.Clone());
                return Input.Push(element);
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
