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
    public interface IInput: IObjectContainer, ILockable
    {
        void AddInputNotify(INotify inputNotify);
        bool PushIMessage(IMessage item);
    }

    public interface IInput<T>: IInput where T: class, IMessage
    {        
        bool Push(T element);
        new bool PushObject(object element);
        new object PopObject();
    }
}
