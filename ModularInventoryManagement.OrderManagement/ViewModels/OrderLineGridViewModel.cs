using ModularInventoryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModularInventoryManagement.OrderManagement.ViewModels
{
    public class OrderLineGridViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<OrderLine> OrderLines { get; set; }
        private OrderLine mSelectedOrderLine;
        public OrderLine SelectedOrderLine
        {
            get => mSelectedOrderLine;
            set
            {
                if (mSelectedOrderLine == value) return;
                mSelectedOrderLine = value;
                NotifyPropertyChanged();
                if (mSelectedOrderLine == null) return;
                mSelectedOrderLine.PropertyChanged += PropertyChanged;
            }
        }
        private bool mIsReadOnly;
        public bool IsReadOnly
        {
            get => mIsReadOnly;
            set
            {
                if (mIsReadOnly == value) return;
                mIsReadOnly = value;
                NotifyPropertyChanged();
            }
        }

        // Stub initialization
        public OrderLineGridViewModel()
        {
            OrderLines = new ObservableCollection<OrderLine>();
            //IsReadOnly = true;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddOrderLine(object obj)
        {
            if (!(obj is ProductVariant)) throw new InvalidCastException();
            var productVariant = (ProductVariant)obj;
            var existingOrderLine = OrderLines.FirstOrDefault(orderLine => orderLine.Batch.ProductVariant.Id == productVariant.Id);
            if (existingOrderLine is null)
            {
                OrderLines.Add(new OrderLine()
                {
                    Batch = new Batch()
                    {
                        ProductVariant = productVariant
                    },
                    Quantity = 1,
                    LineSum = productVariant.ListPrice
                });
            }
            else
            {
                existingOrderLine.Quantity += 1;
            }
            NotifyPropertyChanged(propertyName: "OrderLines");
        }

        public void RemoveOrderLine(int productSku)
        {
            var orderLine = OrderLines.FirstOrDefault(line => line.Batch.ProductVariant.Id == productSku);
            if (orderLine is null) return;
            var lineSum = orderLine.LineSum;
            OrderLines.Remove(orderLine);
            NotifyPropertyChanged(propertyName: "OrderLines");
        }

        public void Clear()
        {
            OrderLines.Clear();
            NotifyPropertyChanged(propertyName: "OrderLines");
        }
    }
}
