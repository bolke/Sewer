using GalaSoft.MvvmLight;
using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewerGui.ViewModel
{
    public class OutputViewModel : NodeViewModel
    {
        public IOutput Output { get { return BaseItem as IOutput; } set { BaseItem = value; RaisePropertyChanged(() => Output); } }

        public virtual bool CanAddInputNotifyCommand() { return true; }

        public virtual bool CanPopIMessage() { return true; }

    }
}
