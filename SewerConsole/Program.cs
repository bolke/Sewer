using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewerConsole
{
    class Cake
    {
        public void Notify(IMessage item)
        {
            Console.Write("LALALALALALA" + ":    ");
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
            test.RegisterInputListener(new Notify<IMessage>(new Action<IMessage>(d.Notify)));
            test.RegisterInputListener(new Notify<IMessage>(new Action<IMessage>(f.Notify)));
            test.Push(new Message());
            test.Push(new Message());

            Output<IMessage> toast = new Output<IMessage>();
            toast.Initialize();
            toast.RegisterOutputListener(new Notify<IMessage>(new Action<IMessage>(d.Notify)));
            toast.RegisterOutputListener(new Notify<IMessage>(new Action<IMessage>(f.Notify)));
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
