using Mod.Configuration.Properties;
using Mod.Modules.Abstracts;
using Pipes.Interfaces;
using Pipes.Interfaces.Containers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Output: Initiator, IOutput<IMessage>
    {
        [Configure(InitType = typeof(ConcurrentQueue<IMessage>))]
        public ConcurrentQueue<IMessage> Queue { get; set; }

        public Output()
        {
        }

        public virtual IMessage Pop()
        {
            return PopObject() as IMessage;
        }

        public virtual object PopObject()
        {
            IMessage result;
            if(Queue.TryDequeue(out result))
                return result;
            return null;
        }

        public virtual bool PushObject(object element)
        {
            if(element is IMessage)
            {
                Queue.Enqueue(element as IMessage);
                return true;
            }
            return false;
        }
    }
}
