using Pipes.Interfaces;
using Pipes.Interfaces.Containers;
using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewerConsole
{
    class Cake: INotify<IMessage>
    {
        Guid uni = Guid.NewGuid();
        public void Notify(IMessage item)
        {
            Console.Write(uni + ":    ");
            Console.WriteLine(item.UniqueId);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Input<IMessage> test = new Input<IMessage>();
            test.Initialize();
            
            Cake f = new Cake();
            Cake d = new Cake();
            test.InputListeners[f] = f;
            test.InputListeners[d] = d;
            test.Push(new Message());
            test.Push(new Message());

            Output<IMessage> toast = new Output<IMessage>();
            toast.Initialize();
            toast.OutputListeners[f] = f;
            toast.OutputListeners[d] = d;
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
