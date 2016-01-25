using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fittings.Modules
{
    public class TextMessage: Message
    {
        public string Content { get; set; }

        public TextMessage(string content = "")
            : base()
        {
            Content = content;
        }
        public override string ToString()
        {
            return Content;
        }
        public override object Clone()
        {
            TextMessage result = this;
            if(Duplicate)
            {
                result = new TextMessage(Content);
                result.UniqueId = UniqueId;
            }
            return result;
        }
    }

}
