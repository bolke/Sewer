using Mod.Configuration.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IValve<T>: IPipe<T> where T: class, IMessage
    {
        [Configure(DefaultValue=null)]
        IPipe<T> Pipe { get; set; }
        [Configure(DefaultValue=false)]
        bool IsOpen { get; set; }
        bool Open();
        bool Close();
    }
}
