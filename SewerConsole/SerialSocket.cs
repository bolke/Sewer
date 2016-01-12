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
    public class SerialMessage: Message, IClone<SerialMessage>
    {
        public string content;

        public new SerialMessage Clone()
        {
            throw new NotImplementedException();
        }
    }

    public class SerialSocket: Valve<SerialMessage>
    {
        SerialPort serialPort = null;

        public override bool Open()
        {
            return base.Open();
        }

        public override bool Close()
        {
            return base.Close();
        }

        public override bool Push(SerialMessage element)
        {
            return base.Push(element);
        }

        public override SerialMessage Pop()
        {
            return base.Pop();
        }
    }
}
