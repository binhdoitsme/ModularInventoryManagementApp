using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderPayment = new HashSet<OrderPayment>();
            Orderline = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset Created { get; set; }
        public int CashierId { get; set; }

        public virtual User Cashier { get; set; }
        public virtual ICollection<OrderPayment> OrderPayment { get; set; }
        public virtual ICollection<OrderLine> Orderline { get; set; }
    }
}
