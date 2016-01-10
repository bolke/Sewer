using Mod.Configuration.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IClone<T>
    {
        [Configure(DefaultValue=true)]
        bool Duplicate { get; set; }
        T Clone();
    }
}
