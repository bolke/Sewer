using Mod.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces.Containers
{
    public interface INotify<T>: IUnique
    {
        void Notify(T item);
    }
}
