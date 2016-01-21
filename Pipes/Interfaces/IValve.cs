using Mod.Configuration.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IValve<T>: IInput<T>, IOutput<T> where T: IMessage
    {
        [Configure(DefaultValue=null)]
        IPipe<T> Pipe { get; set; }
        [Configure(DefaultValue=false)]
        bool IsOpen { get; set; }
        bool Open();
        bool Close();
    }
}
