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
            Duplicate = true;
            UniqueId = Guid.NewGuid();
        }

        public virtual IMessage Clone()
        {
            if(Duplicate)
                return new Message() { UniqueId = uniqueId };
            else
                return this;
        }

        public virtual Guid UniqueId
        {
            get { return uniqueId; }
            set { uniqueId = value; }
        }

        public virtual bool Duplicate
        {
            get;
            set;
        }
    }
}
