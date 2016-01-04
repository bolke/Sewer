using Mod.Interfaces;
using Mod.Interfaces.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces.Containers
{
    public interface IInput<T>: IObjectContainer
    {
        bool Push(T item);
    }
}
