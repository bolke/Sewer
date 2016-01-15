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
    public class TextMessage: Message, IClone
    {
        public string content;

        public override object Clone()
        {        
            return new TextMessage(){content = this.content};        
        }
    }
    
    public class TextBucket: Valve<TextMessage>
    {        
        public TextBucket()
        {            
        }    

        [Configure(InitType=typeof(Output<TextMessage>))]
        public override IOutput<TextMessage> Output
        {
            get
            {
                return base.Output;
            }
            set
            {
                base.Output = value;
            }
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                if (Input == null)
                {
                    Input = this;
                } 
                return true;
            }
            return false;
        }

        public override bool PushObject(object element)
        {
            if (element is TextMessage)
            {
                Console.WriteLine(this.UniqueId.ToString()+  ":" + ((element as TextMessage).content));
                return true;
            }
            return false;
        }

        public override object PopObject()
        {
            return base.PopObject();
        }
    }
}
