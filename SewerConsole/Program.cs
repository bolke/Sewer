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
            TextBucket t2 = new TextBucket();
            TextBucket t3 = new TextBucket();

            t1.RegisterInputListener(new Notify<TextMessage>(t2.Push) { Duplicate = true });
            t1.RegisterInputListener(new Notify<TextMessage>(t3.Push) { Duplicate = true });

            t1.Push(new TextMessage() { content = "one" });

            t2.Close();

            t1.Push(new TextMessage() { content = "two" });

            TextMessage tm = t1.Pop();
            TextMessage tm2 = t1.Pop();

            Console.ReadLine();
        }
    }
}
