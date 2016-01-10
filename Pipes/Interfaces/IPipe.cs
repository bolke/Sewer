using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Interfaces;
using System.Collections.Concurrent;
using Mod.Configuration.Properties;

namespace Pipes.Interfaces
{
    public interface IPipe<T>:IInput<T>, IOutput<T> where T:IClone<T>
    {
        [Configure(DefaultValue=null)]
        IInput<T> Input { get; set; }

        [Configure(DefaultValue = null)]
        IOutput<T> Output { get; set; }
    }
}
