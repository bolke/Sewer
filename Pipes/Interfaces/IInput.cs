﻿using Mod.Interfaces;
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
    public interface IInput<T>: IObjectContainer, IUnique where T:IClone<T>
    {
        [Configure(InitType = typeof(ConcurrentQueue<>))]
        IProducerConsumerCollection<T> Queue { get; set; }
        void RegisterInputListener(INotify<T> inputListener);
        bool Push(T element);
        bool PushObject(object element);
        object PopObject();
    }
}
