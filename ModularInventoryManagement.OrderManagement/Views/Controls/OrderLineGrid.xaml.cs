using ModularInventoryManagement.OrderManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModularInventoryManagement.OrderManagement.Views.Controls
{
    /// <summary>
    /// Interaction logic for OrderLineGrid.xaml
    /// </summary>
    public partial class OrderLineGrid : UserControl
    {
        private Action<int> RemoveOrderLine;
        public MouseButtonEventHandler OrderLineDoubleClickHandler { get; set; }

        public OrderLineGrid()
        {
            InitializeComponent();
            OrderLineDoubleClickHandler = (s, e) => { };
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            
            if (!(DataContext is OrderLineGridViewModel dataContext)) return;
            if (dataContext.IsReadOnly)
            {
                RemoveLineColumn.Visibility = Visibility.Collapsed;
            }
            RemoveOrderLine = dataContext.RemoveOrderLine;
        }

        private void OnRemoveOrderLine(object sender, RoutedEventArgs e)
        {
            var source = e.Source as Button;
            if (!(source?.DataContext is int sku)) return;
            RemoveOrderLine(sku);
        }
    }
}
