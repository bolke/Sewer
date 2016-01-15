using Mod.Configuration.Properties;
using Mod.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface INotify<T>
    {
        [Configure(DefaultValue=true)]
        bool Duplicate { get; set; }
        Func<T, bool> NotifyDelegate { get; set; }
    }
}
