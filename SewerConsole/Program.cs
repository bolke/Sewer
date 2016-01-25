using Fittings.Modules;
using Mod.Configuration.Section;
using Mod.Interfaces.Containers;
using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SewerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BufferedInput<TextMessage> bufferIn = new BufferedInput<TextMessage>();
            BufferedOutput<TextMessage> bufferOut = new BufferedOutput<TextMessage>();

            bufferIn.Initialize();
            bufferOut.Initialize();

            Pump<TextMessage> pump = new Pump<TextMessage>();
            pump.Initialize();
            pump.Start();

            SerialFitting<TextMessage> serial = new SerialFitting<TextMessage>();
            serial.Initialize();
            serial.SerialPort.PortName = "COM11";
            serial.Open();

            pump.AddFlow(serial.Output, bufferIn);// Enqueue(new Tuple<IObjectContainer, IObjectContainer>(serial.Output, bufferIn));
            pump.AddFlow(bufferOut, serial.Input);// Flows.Enqueue(new Tuple<IObjectContainer, IObjectContainer>(bufferOut, serial.Input));

            while(true)
            {
                bufferOut.PushObject(new TextMessage() { Content = "kaas\n\r\0sdfsfd" });
                bufferOut.PushObject(new TextMessage() { Content = "worst\n\r" });

                TextMessage pop = bufferIn.PopObject() as TextMessage;
                if(pop != null)
                {
                    Console.WriteLine(pop.ToString());
                }


                Thread.Sleep(100);
            }

            serial.Close();
            
            Console.ReadLine();
        }
    }
}
