﻿using Mod.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface INotify<T>
    {
        Func<T, bool> NotifyDelegate { get; set; }
    }
}