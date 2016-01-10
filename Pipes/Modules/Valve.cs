using Mod.Configuration.Properties;
using Mod.Modules.Abstracts;
using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Valve<T>:Initiator ,IPipe<T>, IValve<T> where T:IClone<T>
    {
        [Configure(DefaultValue=false)]
        public virtual bool IsOpen
        {
            get;
            set;
        }

        public virtual bool Open()
        {
            if(!IsOpen)
            {
                IsOpen = true;
                return true;
            }
            return false;
        }

        public virtual bool Close()
        {
            if(IsOpen)
            {
                IsOpen = false;
                return true;
            }
            return false;          
        }


        public virtual T Pop()
        {
            if(IsOpen)
                return Pipe.Pop();
            return default(T);
        }

        public virtual bool Push(T element)
        {
            if(IsOpen)
                return Pipe.Push(element);
            return false;
        }

        public virtual object PopObject()
        {
            if(IsOpen)
                return Pipe.PopObject();
            return default(T);
        }

        public virtual bool PushObject(object element)
        {
            if(IsOpen)
                return Pipe.PushObject(element);
            return false;
        }

        [Configure(DefaultValue=null)]
        public virtual IPipe<T> Pipe
        {
            get;
            set;
        }

        [Configure]
        public virtual IInput<T> Input
        {
            get
            {
                if(Pipe!=null)
                    return Pipe.Input;
                return null;
            }
            set
            {
                if(Pipe!=null)
                    Pipe.Input = value;
            }
        }

        [Configure]
        public virtual IOutput<T> Output
        {
            get
            {
                if(Pipe!=null)
                    return Pipe.Output;
                return null;
            }
            set
            {
                if(Pipe!=null)
                    Pipe.Output = value;
            }
        }

        public virtual void RegisterInputListener(INotify<T> inputListener)
        {
            if(Input != null)
                Input.RegisterInputListener(inputListener);
        }

        public virtual void RegisterOutputListener(INotify<T> outputListener)
        {
            if(Output != null)
                Output.RegisterOutputListener(outputListener);
        }
    }
}
