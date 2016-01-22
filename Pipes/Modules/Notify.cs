using Mod.Configuration.Properties;
using Mod.Interfaces.Config;
using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Notify: INotify
    {
        public Notify(Func<IUnique, bool> notifyDelegate)
        {
            NotifyDelegate = notifyDelegate;
        }

        public virtual Func<IUnique, bool> NotifyDelegate
        {
            get;
            set;
        }

        public virtual bool CallDelegate(IUnique caller)
        {
            if(NotifyDelegate != null)
                return NotifyDelegate.Invoke(caller);
            return false;
        }
    }
}
