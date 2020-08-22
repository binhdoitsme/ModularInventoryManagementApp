using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ModularInventoryManagement.Data.Models
{
    public partial class PaymentInCash : IPaymentMethod
    {
        public int Id { get; set; }
        private decimal mPaidAmount;
        public decimal PaidAmount
        {
            get => mPaidAmount;
            set
            {
                mPaidAmount = value;
                NotifyPropertyChanged();
            }
        }
        private decimal mReturned;
        public decimal Returned
        {
            get => mReturned;
            set
            {
                mReturned = value;
                NotifyPropertyChanged();
            }
        }
        public PaymentMethod.Method Method { get; } = PaymentMethod.Method.Cash;

        public virtual PaymentMethod IdNavigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
