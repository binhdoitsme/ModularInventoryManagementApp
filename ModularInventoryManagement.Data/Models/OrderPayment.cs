using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class OrderPayment
    {
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }

        public virtual Order Order { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
