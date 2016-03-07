using Pipes.Interfaces;
using System;

namespace SewerGui.ViewModel
{
    public class InputViewModel : NodeViewModel
    {
        public IInput Input
        {
            get { return BaseItem as IInput; }
            set { BaseItem = value; RaisePropertyChanged(() => Input); }
        }

        public virtual bool CanAddInputNotifyCommand()
        {
            return true;
        }

        public virtual bool CanPushIMessage(Pipes.Interfaces.IMessage item)
        {
            return true;
        }

        public virtual bool CanPopObject()
        {
            return true;
        }

        public virtual bool CanPushObject()
        {
            return true;
        }

        public override void DoCreateCommand()
        {
            throw new NotImplementedException();
        }

        //        public virtual void AddInputNotify(INotify inputListener);
        //        public virtual bool PushIMessage(IMessage item);
        //        public virtual object PopObject();
        //        public virtual bool PushObject(object element);
    }
}
