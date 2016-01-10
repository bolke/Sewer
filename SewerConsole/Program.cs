using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Valve<IMessage> valve = new Valve<IMessage>();
            Pipe<IMessage> pipe = new Pipe<IMessage>();

            valve.Initialize();
            pipe.Initialize();
            valve.Pipe = pipe;

            valve.Push(new Message());
            valve.Push(new Message());

            valve.Open();

            valve.Push(new Message());
            valve.Push(new Message());

            valve.Pop();
            valve.Close();

            valve.Pop();
            Console.ReadLine();
        }
    }
}
