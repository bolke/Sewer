using Mod.Configuration.Properties;
using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fittings.Modules
{
    public class BufferedInput<T>: Input<T> where T: class, IMessage
    {
        [Configure(InitType = typeof(ConcurrentQueue<>))]
        public IProducerConsumerCollection<T> Queue { get; set; }

        public override object PopObject()
        {
            T result;
            if(Queue.TryTake(out result))
            {
                return result;
            }
            return default(T);
        }

        public override bool PushObject(object element)
        {
            return Queue.TryAdd((T)element);
        }
    }

}
