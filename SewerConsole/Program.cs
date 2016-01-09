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

            List<Pipe<IMessage>> pipeLine = new List<Pipe<IMessage>>();

            for(int i = 0; i < 6; i++)
            {
                pipeLine.Add(new Pipe<IMessage>());
                pipeLine[i].Initialize();
            }

            pipeLine[0].Input = pipeLine[1];
        
            pipeLine[1].RegisterInputListener(new Notify<IMessage>(pipeLine[3].Push));
            pipeLine[1].RegisterInputListener(new Notify<IMessage>(pipeLine[2].Push));
            pipeLine[2].RegisterInputListener(new Notify<IMessage>(pipeLine[3].Push));
            pipeLine[3].RegisterInputListener(new Notify<IMessage>(pipeLine[4].Push));
            pipeLine[4].RegisterInputListener(new Notify<IMessage>(pipeLine[5].Push));
            pipeLine[0].Push(new Message());

            for(int i = 0; i < 6; i++)
            {
                Console.Write((i+1).ToString() + ":");
                Input<IMessage> input = null;
                Pipe<IMessage> pipe = pipeLine[i];
                while(pipe.Input is Pipe<IMessage>)
                {
                    pipe = pipe.Input as Pipe<IMessage>;
                }
                Console.Write(":"+pipe.Input.UniqueId + ":");
                Console.WriteLine((pipe.Input as Input<IMessage>).Queue.Count());
            }

            Console.ReadLine();
        }
    }
}
