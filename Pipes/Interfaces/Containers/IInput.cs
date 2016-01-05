using Mod.Interfaces;
using Mod.Interfaces.Config;
using Mod.Interfaces.Containers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces.Containers
{
    public interface IInput<T>: IObjectContainer, IUnique
    {
        ConcurrentDictionary<INotify<T>, INotify<T>> InputListeners { get; set; }
        bool Push(T element);
    }
}
