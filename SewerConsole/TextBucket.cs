using Mod.Configuration.Properties;
using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewerConsole
{
    public class TextMessage: Message, IMessage
    {
        public string content;

        public override object Clone()
        {        
            return new TextMessage(){UniqueId = Guid.NewGuid(), content = this.content};        
        }
    }

    public class TextBucket : Valve<TextMessage>
    {
        public TextBucket()
        {
            Initialize();
            Input = this;
            Output = this;
            Open();
        }

        public override bool PushObject(object element)
        {
            Console.WriteLine(((TextMessage)element).content + "--" + ((IMessage)element).UniqueId);
            return base.PushObject(element);
        }
    }
}
