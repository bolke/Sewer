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
            TextBucket s1 = new TextBucket();
            TextBucket s2 = new TextBucket();
            TextBucket s3 = new TextBucket();
            TextBucket s4 = new TextBucket();
            TextBucket s5 = new TextBucket();
            TextBucket s6 = new TextBucket();
            TextBucket s7 = new TextBucket();
            TextBucket s8 = new TextBucket();
            TextBucket s9 = new TextBucket();
            TextBucket s10 = new TextBucket();

            s1.Initialize();
            s2.Initialize();
            s3.Initialize();
            s4.Initialize();
            s5.Initialize();
            s6.Initialize();
            s7.Initialize();
            s8.Initialize();
            s9.Initialize();
            s10.Initialize();

            s1.RegisterInputListener(new Notify<TextMessage>(s2.Push));
            s2.RegisterInputListener(new Notify<TextMessage>(s3.Push));
            s3.RegisterInputListener(new Notify<TextMessage>(s4.Push));
            s4.RegisterInputListener(new Notify<TextMessage>(s5.Push));
            s5.RegisterInputListener(new Notify<TextMessage>(s6.Push));
            s6.RegisterInputListener(new Notify<TextMessage>(s7.Push));
            s7.RegisterInputListener(new Notify<TextMessage>(s8.Push));
            s8.RegisterInputListener(new Notify<TextMessage>(s9.Push));
            s9.RegisterInputListener(new Notify<TextMessage>(s10.Push));
            
            s1.Open();
            s2.Open();
            s3.Open();
            s4.Open();
            s5.Open();
            s6.Open();
            s7.Open();
            s8.Open();
            s9.Open();
            s10.Open();

            int i = 0;
            while (i < 100)
            {
                i++;
                s1.Push(new TextMessage() { content = "1212" });                
                Thread.Sleep(100);
                s1.Pop();
                Thread.Sleep(100);
                if (s2.IsOpen)
                    s2.Close();
                else
                    s2.Open();
            }
            s1.Close();
            Console.ReadLine();
        }
    }
}
