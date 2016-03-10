using GalaSoft.MvvmLight;
using SewerGui.Services;
using System.Collections.ObjectModel;

namespace SewerGui.ViewModel
{    
    public class MainViewModel : ViewModelBase
    {
        INodeService nodeService;
        ObservableCollection<NodeViewModel> nodes;

        ObservableCollection<NodeViewModel> Nodes
        {
            get { return nodes; }
            set
            {
                nodes = value;
                RaisePropertyChanged("Nodes");
            }
        }

        NodeViewModel node;

        public NodeViewModel Node
        {
            get { return node; }
            set
            {
                node = value;
                RaisePropertyChanged("Node");
            }
        }

        public MainViewModel(INodeService nodeService)
        {
            this.nodeService = nodeService;
            Nodes = new ObservableCollection<NodeViewModel>();
        }
        
        public override void Cleanup()
        {        
        }
    }
}
