using ModularInventoryManagement.Infrastructure.ViewFactory;
using ModularInventoryManagement.OrderManagement.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ModularInventoryManagement.OrderManagement.ViewFactory
{
    internal class OrderViewFactory : AbstractViewFactory
    {
        public override string Title => "Manage orders";

        public OrderViewFactory()
        {
            PageCreators = new Dictionary<string, Func<Page>>()
            {
                { nameof(TestOrderView), () => new TestOrderView() },
                { nameof(CreateOrderView), () => new CreateOrderView() }
            };
        }
    }
}
