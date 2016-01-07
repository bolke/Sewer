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
        public Notify(Action<T> notifyDelegate)
        {
            NotifyDelegate = notifyDelegate;
        }
      
        public virtual Action<T> NotifyDelegate
        {
            get;
            set;
        }
    }
}
