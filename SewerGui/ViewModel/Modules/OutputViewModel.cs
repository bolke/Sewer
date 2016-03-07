using Pipes.Interfaces;
using System;

namespace SewerGui.ViewModel
{
    public class OutputViewModel : NodeViewModel
    {
        public IOutput Output
        {
            get
            {
                return BaseItem as IOutput;
            }
            set
            {
                BaseItem = value; RaisePropertyChanged(() => Output);
            }
        }

        public virtual bool CanAddInputNotifyCommand()
        {
            return true;
        }

        public virtual bool CanPopIMessage()
        {
            return true;
        }

        public override void DoCreateCommand()
        {
            throw new NotImplementedException();
        }

        //void AddOutputNotify(INotify outputNotify);
        //IMessage PopIMessage();
        //new bool PushObject(object element);
        //new object PopObject();
    }
}
