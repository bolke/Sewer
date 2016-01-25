using Mod.Configuration.Properties;
using Mod.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface INotify
    {
        Func<IUnique,bool> NotifyDelegate { get; set; }
        bool CallDelegate(IUnique caller);
    }
}
