using ModularInventoryManagement.Commons.Views;
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

namespace ModularInventoryManagement.OrderManagement.Views
{
    /// <summary>
    /// Interaction logic for CreateOrderView.xaml
    /// </summary>
    public partial class CreateOrderView : Page
    {
        private readonly Action Clear;

        public CreateOrderView()
        {
            InitializeComponent();
            var dataContext = (CreateOrderViewModel)DataContext;
            SuggestionBox.AddSelectionToSomeList = dataContext.OrderLineGrid.AddOrderLine;
            Clear = dataContext.Clear;
            LogoutButton.Click += dataContext.Logout;
        }

        private void OnCheckout(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
