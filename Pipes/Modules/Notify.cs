using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Notify<T>: INotify<T>
    {
        public Notify(Func<T, bool> notifyDelegate)
        {
            NotifyDelegate = notifyDelegate;
        }
      
        public Func<T, bool> NotifyDelegate
        {
            get;
            set;
        }
    }
}
