using Fittings.Modules;
using Mod.Configuration.Section;
using Mod.Interfaces.Config;
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
        public static bool NotifyOutput(IUnique caller)
        {
            Console.WriteLine("1 " + caller.UniqueId);
            return true;
        }

        public static bool NotifyOutput2(IUnique caller)
        {
            Console.WriteLine("2 " + caller.UniqueId);
            return true;
        }

        static void Main(string[] args)
        {
            Pump<TextMessage> pump = new Pump<TextMessage>();            
            pump.Initialize();
            pump.Interval = 1;
            pump.Start();

            SerialFitting<TextMessage> serial = new SerialFitting<TextMessage>();
            serial.Initialize();
            serial.SerialPort.PortName = "COM3";
            serial.Open();

            SerialFitting<TextMessage> serial2 = new SerialFitting<TextMessage>();
            serial2.Initialize();
            serial2.SerialPort.PortName = "COM7";
            serial2.Open();

            pump.AddFlow(serial, serial2);
            pump.AddFlow(serial2, serial);
            
            serial.AddInputNotify(new Notify(NotifyOutput));
            serial2.AddInputNotify(new Notify(NotifyOutput2));

            serial.Push(new TextMessage("bla bla"));
            serial2.Push(new TextMessage("nib nib"));
            
            Console.ReadLine();
        }
    }
}
