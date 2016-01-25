using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipes.Interfaces;

namespace Pipes.Modules
{
    public abstract class Plant<T>: Pipe<T> where T: class, IMessage
    {
        public abstract T Process(T element);

        public override bool Push(T element)
        {
            return base.Push(Process(element));
        }
    }
}
