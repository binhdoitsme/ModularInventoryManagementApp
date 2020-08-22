using ModularInventoryManagement.Infrastructure.ViewFactory;
using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    public interface IModuleMetadata
    {
        string Name { get; }
        IViewFactory ViewFactory { get; }
        IStateObject State { get; }
        event EventHandler<object> StateChanged;
    }
}
