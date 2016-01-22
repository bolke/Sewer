﻿using Mod.Configuration.Properties;
using Mod.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface ITransfer
    {
        [Configure(DefaultValue = true)]
        bool Duplicate { get; set; }
        Func<IMessage, bool> TransferDelegate { get; set; }
        bool CallDelegate(IMessage message);
    }

    public interface INotify
    {
        Func<IUnique,bool> NotifyDelegate { get; set; }
        bool CallDelegate(IUnique caller);
    }
}
