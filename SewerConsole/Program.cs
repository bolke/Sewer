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
    public class MagicPlant<T>: Plant<T> where T:IMessage
    {
        public MagicPlant()
        {
            Initialize();
        }

        public override T Process(T element)
        {
            if(element is TextMessage)
                (element as TextMessage).content += "nope";
            return element;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextBucket t1 = new TextBucket();
            Pipe<TextMessage> t2 = new Pipe<TextMessage>();
            Plant<TextMessage> plant = new MagicPlant<TextMessage>();
            Pipe<TextMessage> t3 = new Pipe<TextMessage>();
            TextBucket t4 = new TextBucket();

            t3.Initialize();
            t2.Initialize();

            t1.Input = t2;
            t2.Input = plant;
            plant.Input = t3;
            t3.Input = t4;

            t1.AddInputNotify(t2.FabricateInputNotify());
            t2.AddInputNotify(t2.FabricateInputNotify());
            plant.AddInputNotify(t2.FabricateInputNotify());
            t3.AddInputNotify(t2.FabricateInputNotify());

            t1.AddInputNotify(t4.FabricateInputNotify());
            t2.AddInputNotify(t4.FabricateInputNotify());
            plant.AddInputNotify(t4.FabricateInputNotify());
            t3.AddInputNotify(t4.FabricateInputNotify());

            t1.Push(new TextMessage() { content = "one" });
            t1.Push(new TextMessage() { content = "two" });

             TextMessage tm = t4.Pop();
            TextMessage tm2 = t1.Pop();

            Console.ReadLine();
        }
    }
}
