using ModularInventoryManagement.Infrastructure.ViewFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    public abstract class AbstractModuleMetadata : IModuleMetadata
    {
        public abstract string Name { get; }
        public abstract IViewFactory ViewFactory { get; }
        public abstract IStateObject State { get; }

        public event EventHandler<object> StateChanged
        {
            add { State.StateChanged += value; }
            remove { State.StateChanged -= value; }
        }
    }
}
