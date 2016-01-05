using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    interface IPipe<T>
    {
        ConcurrentDictionary<int, IPipeEnd<T>> LeftPipeEnds { get; set; }
        ConcurrentDictionary<int, IPipeEnd<T>> RightPipeEnds { get; set; }
    }
}
