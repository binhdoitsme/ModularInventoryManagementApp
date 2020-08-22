using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ModularInventoryManagement.Data.Models
{
    public interface IPaymentMethod : INotifyPropertyChanged
    {
        int Id { get; set; }
        PaymentMethod.Method Method { get; }
    }
}
