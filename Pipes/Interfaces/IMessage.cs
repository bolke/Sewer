using Mod.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IMessage: IUnique
    {
        IMessage Clone();
    }
}
