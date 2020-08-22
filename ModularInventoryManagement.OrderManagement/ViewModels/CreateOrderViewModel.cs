using ModularInventoryManagement.Data.Models;
using ModularInventoryManagement.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ModularInventoryManagement.OrderManagement.ViewModels
{
    public class CreateOrderViewModel : AbstractViewModel
    {
        public IEnumerable<ProductVariant> DataSource { get; set; }
        public decimal mTotal;
        public decimal Total 
        {
            get => mTotal;
            set
            {
                if (mTotal == value) return;
                mTotal = value;
                PaymentMethod.Total = value;
                NotifyPropertyChanged();
            }
        }
        private bool mCanEditOrderLineQuantity;
        public bool CanEditOrderLineQuantity
        {
            get => mCanEditOrderLineQuantity;
            set
            {
                mCanEditOrderLineQuantity = value;
                NotifyPropertyChanged();
            }
        }
        private OrderLineGridViewModel mOrderLineGrid;
        public OrderLineGridViewModel OrderLineGrid
        {
            get => mOrderLineGrid;
            set
            {
                if (mOrderLineGrid == value) return;
                mOrderLineGrid = value;
                mOrderLineGrid.PropertyChanged += (s, e) => {
                    if (e.PropertyName == "SelectedOrderLine")
                    {
                        CanEditOrderLineQuantity = mOrderLineGrid.SelectedOrderLine != null;
                    }
                    UpdateTotal();
                };
            }
        }
        private PaymentMethodViewModel mPaymentMethod;
        public PaymentMethodViewModel PaymentMethod
        {
            get => mPaymentMethod;
            set
            {
                if (mPaymentMethod == value) return;
                mPaymentMethod = value;
                NotifyPropertyChanged();
            }
        }
        private readonly string mUsername;
        public string Username { get => mUsername; }

        public CreateOrderViewModel()
        {
            mStateLocator = StateLocator.GetInstance();
            mUsername = mStateLocator.Get<string>("empFullName");

            PaymentMethod = new PaymentMethodViewModel();
            DataSource = new List<ProductVariant>()
            {
                new ProductVariant()
                {
                    Id = 648512346,
                    Product = new Product()
                    {
                        Name = "Cute Teacup",
                        Unit = 6
                    },
                    ListPrice = 180000
                }
            };
            OrderLineGrid = new OrderLineGridViewModel();
            PropertyChanged += (s, e) => {
                UpdateTotal();
            };
            OrderLineGrid.PropertyChanged += (s, e) => {
                UpdateTotal();
            };
        }

        private void UpdateTotal()
        {
            var orderLines = OrderLineGrid.OrderLines;
            if (orderLines.Any())
            {
                Total = orderLines.Select(line => line.LineSum)
                                .Aggregate((x, y) => x + y);
                return;
            } else
            {
                Total = 0;
                return;
            }
        }

        public void Clear()
        {
            OrderLineGrid.Clear();
            PaymentMethod = new PaymentMethodViewModel();
        }

        public void Logout(object sender, RoutedEventArgs e)
        {
            mStateLocator.Clear(); // not working
        }
    }
}
