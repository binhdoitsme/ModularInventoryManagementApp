using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace ModularInventoryManagement.Infrastructure.ViewFactory
{
    public interface IViewFactory : INotifyPropertyChanged
    {
        string Title { get; }
        Func<Page> MasterPageFactory { get; }
        IEnumerable<string> AllowedPages { set; }
    }
}
