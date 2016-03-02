using GalaSoft.MvvmLight;
using Pipes.Interfaces;
using Pipes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
