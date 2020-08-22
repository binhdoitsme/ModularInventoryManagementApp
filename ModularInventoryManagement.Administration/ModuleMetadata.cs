using ModularInventoryManagement.Infrastructure.Extensibility;
using ModularInventoryManagement.Infrastructure.ViewFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Administration
{
    public class ModuleMetadata : AbstractModuleMetadata
    {
        public override string Name => "ModularInventoryManagement.Administration";

        public override IViewFactory ViewFactory { get; }

        public override IStateObject State { get; }

        public ModuleMetadata()
        {
            State = new StateObject();
            StateLocator.GetInstance().SetInitialState(State);
        }
    }
}
