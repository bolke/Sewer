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
    public class Valve<T>:Pipe<T>, IValve<T> where T:IClone<T>
    {
        protected IInput<T> input = null;
        protected IOutput<T> output = null;
        
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


        public override T Pop()
        {
            if(IsOpen && Output != null)
                return Output.Pop();
            return default(T);
        }

        public override bool Push(T element)
        {
            if(IsOpen && Input!=null)
                return Input.Push(element);
            return false;
        }

        public override object PopObject()
        {
            if(IsOpen && Output != null)
                return Output.PopObject();
            return default(T);
        }

        public override bool PushObject(object element)
        {
            if(IsOpen && Input != null)
                return Input.PushObject(element);
            return false;
        }

        [Configure(DefaultValue=null)]
        public virtual IPipe<T> Pipe
        {
            get;
            set;
        }

        [Configure]
        public override IInput<T> Input
        {
            get
            {
                if(Pipe!=null)
                    return Pipe.Input;
                return input;
            }
            set
            {
                if(Pipe != null)
                    Pipe.Input = value;
                else
                    input = value;
            }
        }

        [Configure]
        public override IOutput<T> Output
        {
            get
            {
                if(Pipe!=null)
                    return Pipe.Output;
                return output;
            }
            set
            {
                if(Pipe != null)
                    Pipe.Output = value;
                else
                    output = value;
            }
        }
    }
}
