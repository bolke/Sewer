using Fittings.Interfaces;
using Mod.Configuration.Properties;
using Mod.Interfaces.Config;
using Mod.Interfaces.Containers;
using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fittings.Modules
{
    public class SerialFitting<T>: Valve<T>, IFitting<T>, IInputListener, IOutputListener where T: TextMessage{
    
        [Configure(InitType=typeof(SerialPort))]
        public SerialPort SerialPort { get; set; }

        [Configure(InitType = typeof(BufferedInput<>))]
        public override IInput<T> Input
        {
            get
            {
                return base.Input;
            }
            set
            {
                base.Input = value;
            }
        }

        [Configure(InitType = typeof(BufferedOutput<>))]
        public override IOutput<T> Output
        {
            get
            {
                return base.Output;
            }
            set
            {
                base.Output = value;
            }
        }

        public override bool Initialize()
        {
            if(base.Initialize())
            {
                Input.AddInputNotify(this.FabricateInputNotify());
                SerialPort.DataReceived += SerialPortDataReceived;
                return true;
            }
            return false;
        }

        public override bool Open()
        {
            if(base.Open())
            {
                SerialPort.Open();
                if(SerialPort.IsOpen)
                    return true;
            }
            Close();
            return false;
        }

        public override bool Close()
        {
            if(SerialPort != null && SerialPort.IsOpen)
                SerialPort.Close();
            return base.Close();
        }

        protected bool NotifyOutput(IUnique caller)
        {
            if(caller is IObjectContainer)
                Console.Write((caller as IObjectContainer).PopObject().ToString());
            return true;
        }

        protected bool NotifyInput(IUnique caller)
        {
            if(caller is IObjectContainer)
                SerialPort.Write((caller as IObjectContainer).PopObject().ToString());
            return true;
        }

        public INotify FabricateOutputNotify()
        {
            return new Notify(NotifyOutput);
        }

        public INotify FabricateInputNotify()
        {
            return new Notify(NotifyInput);
        }

        void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            TextMessage result = new TextMessage();
            while(SerialPort.BytesToRead > 0) 
                result.Content += SerialPort.ReadExisting();
            Output.PushObject(result);
        }

    }
}
