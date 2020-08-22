using ModularInventoryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModularInventoryManagement.OrderManagement.ViewModels
{
    public class PaymentMethodViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IPaymentMethod mInstance;
        public IPaymentMethod Instance
        {
            get => mInstance;
            set
            {
                if (mInstance == value) return;
                mInstance = value;
                mInstance.PropertyChanged += (s, e) => {
                    if (e.PropertyName == "PaidAmount") UpdateReturnedValue();
                };
                NotifyPropertyChanged();
            }
        }
        // property for display only
        private decimal mTotal;
        public decimal Total
        {
            get => mTotal;
            set
            {
                if (mTotal == value) return;
                mTotal = value;
                UpdateReturnedValue();
                NotifyPropertyChanged();
            }
        }
        private bool mCanCreate;
        public bool CanCreate
        {
            get => mCanCreate;
            set
            {
                mCanCreate = value;
                NotifyPropertyChanged();
            }
        }

        // Default payment method is Cash
        // Default constructor create default payment method
        public PaymentMethodViewModel()
        {
            mInstance = PaymentMethod.Factory.Create(PaymentMethod.Method.Cash);
            mInstance.PropertyChanged += (s, e) => {
                if (e.PropertyName != "PaidAmount") return;
                UpdateReturnedValue();
            };
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateReturnedValue()
        {
            if (Instance.Method != PaymentMethod.Method.Cash) return;
            var paymentInCash = (PaymentInCash)Instance;
            paymentInCash.Returned = paymentInCash.PaidAmount < mTotal ? 0 : paymentInCash.PaidAmount - mTotal;
            CanCreate = paymentInCash.PaidAmount >= mTotal && mTotal > 0;
        }

        public void ChangePaymentMethod(string newPaymentMethod)
        {
            var paymentMethod = Enum.Parse<PaymentMethod.Method>(newPaymentMethod);
            Instance = PaymentMethod.Factory.Create(paymentMethod);
        }
    }
}
