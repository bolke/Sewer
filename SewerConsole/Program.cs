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

        public static void PumpTest1()
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

        static void Main(string[] args)
        {
            PumpingRound();            
        }

        public static void PumpingRound()
        {
            Pump<TextMessage> pump = new Pump<TextMessage>();
            pump.Initialize();
            pump.Interval = 1;
            
            Pipe<TextMessage> p1 = new Pipe<TextMessage>();
            Pipe<TextMessage> p2 = new Pipe<TextMessage>();
            Pipe<TextMessage> p3 = new Pipe<TextMessage>();
            Pipe<TextMessage> p4 = new Pipe<TextMessage>();

            p1.Initialize();
            p2.Initialize();
            p3.Initialize();
            p4.Initialize();

            pump.AddFlow(p1, p2);
            pump.AddFlow(p2, p3);
            pump.AddFlow(p3, p4);
            pump.AddFlow(p4, p1);

            p1.AddInputNotify(new Notify(NotifyOutput));
            p2.AddInputNotify(new Notify(NotifyOutput2));
            p3.AddInputNotify(new Notify(NotifyOutput));
            p4.AddInputNotify(new Notify(NotifyOutput2));
            
            p1.Push(new TextMessage("bla bla"));

            pump.Start();            

            Console.ReadLine();
        }
    }
}
