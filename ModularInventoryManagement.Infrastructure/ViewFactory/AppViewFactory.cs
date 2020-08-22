using ModularInventoryManagement.Infrastructure.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ModularInventoryManagement.Infrastructure.ViewFactory
{
    internal class AppViewFactory : AbstractViewFactory
    {
        public override string Title => "Inventory Management";

        public AppViewFactory() : base()
        {
            var moduleManager = ModuleManager.GetInstance();
            PageCreators = moduleManager.ModuleMetadataCollection
                                .ToDictionary(x => x.Name,
                                                x => x.ViewFactory.MasterPageFactory);
            MasterPageFactory = moduleManager.GetStartupModule().ViewFactory.MasterPageFactory;
        }
    }
}
