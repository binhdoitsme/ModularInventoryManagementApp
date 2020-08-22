using ModularInventoryManagement.Infrastructure.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.AuthenticationManagement
{
    internal class StateLocator : AbstractStateLocator
    {
        static IStateLocator instance;
        StateLocator() : base() { }

        internal static IStateLocator GetInstance()
        {
            if (instance is null)
            {
                instance = new StateLocator();
            }
            return instance;
        }
    }
}
