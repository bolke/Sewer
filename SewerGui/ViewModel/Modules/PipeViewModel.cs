using Pipes.Interfaces;
using System;

namespace SewerGui.ViewModel
{
    public class PipeViewModel : NodeViewModel
    {
        public IPipe<IMessage> Pipe
        {
            get
            {
                return BaseItem as IPipe<IMessage>;
            }
            set
            {
                BaseItem = value; RaisePropertyChanged(() => Pipe);
            }
        }

        public override void DoCreateCommand()
        {
            throw new NotImplementedException();
        }
    }
}
