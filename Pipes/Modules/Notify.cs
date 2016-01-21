using Mod.Configuration.Properties;
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
        public Notify(Func<IMessage, bool> notifyDelegate)
        {
            NotifyDelegate = notifyDelegate;
            Duplicate = true;
        }

        public virtual Func<IMessage, bool> NotifyDelegate
        {
            get;
            set;
        }

        [Configure(DefaultValue=true)]
        public virtual bool Duplicate
        {
            get;
            set;
        }

        public virtual bool CallDelegate(IMessage message)
        {
            if(NotifyDelegate != null)
            {
                if(Duplicate && message.Duplicate)
                    return NotifyDelegate.Invoke(message.Clone() as IMessage);
                return NotifyDelegate.Invoke(message);
            }
            return false;
        }
    }
}
