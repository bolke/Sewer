using Mod.Configuration.Section;
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
            TextBucket t1 = new TextBucket();
            Pipe<TextMessage> t2 = new Pipe<TextMessage>();
            Pipe<IMessage> t3 = new Pipe<IMessage>();
            TextBucket t4 = new TextBucket();

            t2.Initialize();

            t1.RegisterInputListener(new Notify<TextMessage>(t2.Push) { Duplicate = false });
            t2.RegisterInputListener(new Notify<TextMessage>(t3.Push) { Duplicate = false });
            t3.RegisterInputListener(new Notify<IMessage>(t4.Push) { Duplicate = false });

            t1.Push(new TextMessage() { content = "one" });
            t1.Push(new TextMessage() { content = "two" });

            TextMessage tm = t4.Pop();
            TextMessage tm2 = t1.Pop();

            Console.ReadLine();
        }
    }
}
