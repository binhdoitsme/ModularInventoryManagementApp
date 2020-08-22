using ModularInventoryManagement.Infrastructure.Extensibility;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModularInventoryManagement.Infrastructure.ViewModels
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        protected IStateLocator mStateLocator;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
