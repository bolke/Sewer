using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipes.Interfaces.Containers;
using Mod.Interfaces;
using System.Collections.Concurrent;

namespace Pipes.Interfaces
{
    public interface IPipeEnd<T>:IInput<T>, IOutput<T>
    {
    }
}
