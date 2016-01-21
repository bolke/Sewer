using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IPlant<T>: IPipe<T> where T: IMessage
    {
        T Process(T element);
    }
}
