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
    public class Valve<T>:Pipe<T>, IValve<T> where T:IClone
    {
        protected IPipe<T> pipe = null;
        
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
            if(IsOpen)
                return base.Pop();
            return default(T);
        }

        public override bool Push(T element)
        {
            if(IsOpen)
                return base.Push(element);
            return false;
        }

        public override object PopObject()
        {
            if(IsOpen)
                return base.PopObject();
            return default(T);
        }

        public override bool PushObject(object element)
        {
            if(IsOpen)
                return base.PushObject(element);
            return false;
        }

        [Configure(DefaultValue=null)]
        public virtual IPipe<T> Pipe
        {
            get { return pipe; }
            set { pipe = value; }
        }

        [Configure]
        public override IInput<T> Input
        {
            get
            {
                if(pipe!=this && pipe != null)
                    return Pipe.Input;
                return base.Input;
            }
            set
            {
                if(pipe != this && pipe != null)
                    Pipe.Input = value;
                else
                    base.Input = value;
            }
        }

        [Configure]
        public override IOutput<T> Output
        {
            get
            {
                if (pipe != this && pipe != null)
                    return Pipe.Output;
                return base.Output;
            }
            set
            {
                if (pipe != this && pipe != null)
                    Pipe.Output = value;
                else
                    base.Output = value;
            }
        }
    }
}
