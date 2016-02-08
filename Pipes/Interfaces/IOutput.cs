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
    public interface IOutput: IObjectContainer, IUnique
    {
        IMessage PopIMessage();
    }

    public interface IOutput<T>: IOutput where T: class, IMessage
    {
        void AddOutputNotify(INotify outputNotify);
        T Pop();
        new bool PushObject(object element);
        new object PopObject();
    }
}
