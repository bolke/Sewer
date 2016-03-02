using GalaSoft.MvvmLight;
using Pipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
