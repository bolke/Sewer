using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mod.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace SewerGui.ViewModel
{
    public abstract class NodeViewModel : ViewModelBase
    {
        #region variables
        private IInitiator baseItem = null;

        private double top = 0;
        private double left = 0;
        private double right = 0;
        private double bottom = 0;
        private double rotation = 0;

        private ICommand initializeCommand = null;
        private ICommand cleanupCommand = null;
        private ICommand createCommand = null;
        #endregion

        #region properties
        public IInitiator BaseItem { get { return baseItem; } set { baseItem = value; RaisePropertyChanged(() => BaseItem); } }

        public double Top { get { return top; } set { top = value; RaisePropertyChanged(() => Top); } }
        public double Left { get { return left; } set { left = value; RaisePropertyChanged(() => Left); } }
        public double Right { get { return right; } set { right = value; RaisePropertyChanged(() => Right); } }
        public double Bottom { get { return bottom; } set { bottom = value; RaisePropertyChanged(() => Bottom); } }

        public double Width { get { return Right - Left; } set { Right = Left + value; RaisePropertyChanged(() => Width); } }
        public double Height { get { return Bottom - Top; } set { Bottom = Top + value; RaisePropertyChanged(() => Height); } }

        public double Surface { get { return Width * Height; } }

        public double Rotation { get { return rotation; } set { rotation = value; RaisePropertyChanged(() => Rotation); } }

        public Point Center { get { return new Point(Left + (Right - Left) / 2.0f, Top + (Bottom - Top) / 2.0f); } }

        public bool IsInitialized { get { return BaseItem == null ? false : BaseItem.IsInitialized; } }
        public virtual Guid UniqueId { get { return BaseItem == null ? Guid.Empty : BaseItem.UniqueId; } }

        public ICommand InitializeCommand
        {
            get
            {
                if (initializeCommand == null)
                    initializeCommand = new RelayCommand(DoInitializeCommand, CanInitializeCommmand);
                return initializeCommand;
            }
        }

        public ICommand CleanupCommand
        {
            get
            {
                if (cleanupCommand == null)
                    cleanupCommand = new RelayCommand(DoCleanupCommand, CanCleanupCommand);
                return cleanupCommand;
            }
        }
        
        public ICommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                    createCommand = new RelayCommand(DoCreateCommand, CanCreateCommand);
                return createCommand;
            }
        }
        #endregion

        #region functions
        public virtual void DoInitializeCommand()
        {
            if (BaseItem != null)
                BaseItem.Initialize();
        }

        public virtual bool CanInitializeCommmand()
        {
            if (BaseItem != null)
                return !IsInitialized;
            return false;
        }

        public virtual void DoCleanupCommand()
        {
            if (BaseItem != null)
                BaseItem.Cleanup();
        }

        public virtual bool CanCleanupCommand()
        {            
            return IsInitialized;
        }

        public virtual bool CanCreateCommand()
        {
            return baseItem == null;
        }

        public abstract void DoCreateCommand();
        #endregion
    }
}
