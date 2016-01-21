using Mod.Configuration.Properties;
using Mod.Interfaces;
using Mod.Interfaces.Config;
using Mod.Interfaces.Containers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IOutput<T>: IObjectContainer, IUnique where T: IMessage
    {
        [Configure(DefaultValue=null)]
        void RegisterOutputListener(INotify<T> outputListener);
        T Pop();
        bool PushObject(object element);
        object PopObject();
    }
}
