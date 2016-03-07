using Pipes.Interfaces;
using System;

namespace SewerGui.ViewModel
{
    public class ValveViewModel : NodeViewModel
    {
        public IValve<IMessage> Valve
        {
            get
            {
                return BaseItem as IValve<IMessage>;
            }
            set
            {
                BaseItem = value; RaisePropertyChanged(() => Valve);
            }
        }

        public override void DoCreateCommand()
        {
            throw new NotImplementedException();
        }
    }
}
