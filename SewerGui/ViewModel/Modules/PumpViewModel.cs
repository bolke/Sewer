using Pipes.Interfaces;
using Pipes.Modules;
using System;

namespace SewerGui.ViewModel
{
    public class PumpViewModel : NodeViewModel
    {
        public Pump<IMessage> Pump
        {
            get
            {
                return BaseItem as Pump<IMessage>;
            }
            set
            {
                BaseItem = value; RaisePropertyChanged(() => Pump);
            }
        }

        public override void DoCreateCommand()
        {
            throw new NotImplementedException();
        }
    }
}
