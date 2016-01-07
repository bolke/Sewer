using Mod.Interfaces;
using Mod.Interfaces.Config;
using System;
using Mod.Interfaces.Containers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IInput<T>: IObjectContainer, IUnique where T:IClone<T>
    {  
        void RegisterInputListener(INotify<T> inputListener);
        bool Push(T element);
    }
}
