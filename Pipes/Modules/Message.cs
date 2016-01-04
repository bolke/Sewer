using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes.Modules
{
    public class Message: IMessage
    {
        private Guid uniqueId = Guid.Empty;

        public Message()
        {
            UniqueId = Guid.NewGuid();
        }

        public virtual IMessage Clone()
        {
            return new Message() { UniqueId = this.UniqueId };
        }

        public virtual Guid UniqueId
        {
            get { return uniqueId; }
            set { uniqueId = value; }
        }
    }
}
