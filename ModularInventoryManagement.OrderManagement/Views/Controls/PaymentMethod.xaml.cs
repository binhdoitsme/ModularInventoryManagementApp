using ModularInventoryManagement.Data.Models;
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
using static ModularInventoryManagement.Data.Models.PaymentMethod;

namespace ModularInventoryManagement.OrderManagement.Views.Controls
{
    /// <summary>
    /// Interaction logic for CheckoutMethod.xaml
    /// </summary>
    public partial class PaymentMethodControl : UserControl
    {
        public PaymentMethodControl()
        {
            InitializeComponent();
            CheckoutMethodSelect.SelectionChanged += OnCheckoutMethodSelectionChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SetDefaultSelection();
        }

        private void OnCheckoutMethodSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataContext = (PaymentMethodViewModel)DataContext;
            string selectedValue = CheckoutMethodSelect.SelectedValue.ToString();
            dataContext.ChangePaymentMethod(selectedValue);

            switch (dataContext.Instance.Method)
            {
                case Method.Cash:
                    CashPaymentMethod.Visibility = Visibility.Visible;
                    CardPaymentMethod.Visibility = Visibility.Collapsed;
                    break;
                case Method.Card:
                    CardPaymentMethod.Visibility = Visibility.Visible;
                    CashPaymentMethod.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public void SetDefaultSelection()
        {
            CheckoutMethodSelect.SelectedIndex = 0;
        }

        private void PaidAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(DataContext);
        }
    }
}
