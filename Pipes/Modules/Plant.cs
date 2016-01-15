using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipes.Interfaces;

namespace Pipes.Modules
{
    public class Plant<T>: Pipe<T> where T:IClone
    {
        public override bool Push(T element)
        {
            return base.Push(Process(element));
        }

        public virtual T Process(T element)
        {
            return element;
        }
    }
}
