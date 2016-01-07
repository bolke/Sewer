using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Interfaces
{
    public interface IClone<T>
    {
        bool Duplicate { get; set; }
        T Clone();
    }
}
