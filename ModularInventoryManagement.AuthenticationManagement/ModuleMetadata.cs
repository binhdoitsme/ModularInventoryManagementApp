using ModularInventoryManagement.AuthenticationManagement.ViewFactory;
using ModularInventoryManagement.Infrastructure.Extensibility;
using ModularInventoryManagement.Infrastructure.ViewFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.AuthenticationManagement
{
    public class ModuleMetadata : AbstractModuleMetadata
    {
        public override string Name => "ModularInventoryManagement.AuthenticationManagement";

        public override IViewFactory ViewFactory { get; } = new AuthenticationViewFactory();

        public override IStateObject State { get; }

        public ModuleMetadata()
        {
            State = new StateObject();
            StateLocator.GetInstance().SetInitialState(State);
        }
    }
}
