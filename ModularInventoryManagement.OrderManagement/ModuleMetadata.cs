using ModularInventoryManagement.Infrastructure.Extensibility;
using ModularInventoryManagement.Infrastructure.ViewFactory;
using ModularInventoryManagement.OrderManagement.ViewFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.OrderManagement
{
    public class ModuleMetadata : AbstractModuleMetadata
    {
        public override string Name => "ModularInventoryManagement.OrderManagement";

        public override IViewFactory ViewFactory { get; } = new OrderViewFactory();

        public override IStateObject State { get; }

        public ModuleMetadata()
        {
            State = new StateObject();
            StateLocator.GetInstance().SetInitialState(State);
        }
    }
}
