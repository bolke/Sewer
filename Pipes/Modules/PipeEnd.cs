using Mod.Configuration.Properties;
using Mod.Interfaces.Containers;
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
    public class PipeEnd<T>:Initiator, IPipeEnd<T>
    {
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

        public ConcurrentDictionary<INotify<T>, INotify<T>> OutputListeners
        {
            get { return Output.OutputListeners; }
            set { Output.OutputListeners = value; }
        }

        public virtual ConcurrentDictionary<INotify<T>, INotify<T>> InputListeners
        {
            get { return Input.InputListeners; }
            set { Input.InputListeners = value; }
        }
        
        public Guid UniqueId
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
            return Output.Pop();
        }

        public virtual bool Push(T element)
        {
            return Input.Push(element);
        }
    }
}
