using SewerGui.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace SewerGui.Services
{
    public interface INodeService
    {
        ObservableCollection<NodeViewModel> Nodes { get; }
        Guid CreateNode(NodeViewModel node);
    }

    public class NodeService : INodeService
    {
        public NodeService()
        {
            nodes = new ObservableCollection<NodeViewModel>();
        }

        private ObservableCollection<NodeViewModel> nodes = null;

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return nodes; }                        
        }

        public Guid CreateNode(NodeViewModel node)
        {
            if(node != null)
            {
                node.CreateCommand.Execute(null);
                Nodes.Add(node);
                return node.UniqueId;
            }
            return Guid.Empty;
        }        
    }
}
