using GalaSoft.MvvmLight;
using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
