using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModularInventoryManagement.Data.Models
{
    public partial class OrderLine : INotifyPropertyChanged
    {
        public int OrderId { get; set; }
        public int BatchId { get; set; }
        private int mQuantity;
        public int Quantity 
        {
            get => mQuantity;
            set
            {
                if (mQuantity == value) return;
                mQuantity = value;
                NotifyPropertyChanged();
            }
        }

        public virtual Batch Batch { get; set; }
        public virtual Order Order { get; set; }
        public decimal mLineSum;
        public decimal LineSum 
        {
            get => mLineSum;
            set
            {
                mLineSum = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderLine()
        {
            PropertyChanged += UpdateLineSum;
        }

        private void UpdateLineSum(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Quantity") return;
            LineSum = mQuantity * Batch.ProductVariant.ListPrice;
        }
    }
}
