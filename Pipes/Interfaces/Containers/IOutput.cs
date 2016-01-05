﻿using Mod.Interfaces;
using Mod.Interfaces.Containers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces.Containers
{
    public interface IOutput<T>: IObjectContainer
    {
        ConcurrentDictionary<INotify<T>, INotify<T>> OutputListeners { get; set; }
        T Pop();
    }
}
