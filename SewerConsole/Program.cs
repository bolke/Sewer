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
            Input test = new Input();
            test.Initialize();
            test.Push(new Message());
            test.Push(new Message());

            Output toast = new Output();
            toast.Initialize();
            toast.Queue = test.Queue;
            IMessage a = toast.Pop();
            IMessage b = toast.Pop();
            IMessage c = a.Clone();

            test.Push(a);
            test.Push(b);
            test.Push(c);

            Console.ReadLine();
        }
    }
}
