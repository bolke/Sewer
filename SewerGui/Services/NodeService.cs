using GalaSoft.MvvmLight.Command;
using SewerGui.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
				
		private ICommand testCommand = null;
		
        public ICommand TestCommand
        {
            get
            {
                if (testCommand == null)
                    testCommand = new RelayCommand<string>(DoTestCommand, CanTestCommand);
                return testCommand;
            }
        }
		
		public virtual void DoTestCommand(string testArgument)
        {
            Console.WriteLine(testArgument);
        }

        public virtual bool CanTestCommand(string testArgument)
        {            
            return true;
        }
    }
}
