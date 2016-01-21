using Mod.Interfaces;
using Mod.Interfaces.Config;
using System;
using Mod.Interfaces.Containers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Configuration.Properties;

namespace Pipes.Interfaces
{
    public interface IInput<T>: IObjectContainer, IUnique where T:IMessage
    {
        INotify FabricateInputNotifier(bool Duplicate = false);
        void AddInputListener(INotify inputListener);
        bool Push(T element);
        bool PushObject(object element);
        object PopObject();
    }
}
