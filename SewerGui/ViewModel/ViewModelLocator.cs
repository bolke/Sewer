using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SewerGui.Services;

namespace SewerGui.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<INodeService, NodeService>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public INodeService Nodes
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NodeService>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}