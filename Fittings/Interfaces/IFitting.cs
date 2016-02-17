using Mod.Interfaces.Config;
using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fittings.Interfaces
{
    public interface IFitting<T> : IValve<T>, IInputListener, IOutputListener, IUnique where T : class, IMessage
    {
    }
}
