using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ModularInventoryManagement.Data.Models
{
    public partial class PaymentByCard : IPaymentMethod
    {
        public int Id { get; set; }
        public int ReferenceNo { get; set; }
        public PaymentMethod.Method Method { get; } = PaymentMethod.Method.Card;

        public virtual PaymentMethod IdNavigation { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
