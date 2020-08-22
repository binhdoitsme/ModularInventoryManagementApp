using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            OrderPayment = new HashSet<OrderPayment>();
        }

        public int Id { get; set; }

        public virtual PaymentByCard PaymentByCard { get; set; }
        public virtual PaymentInCash PaymentInCash { get; set; }
        public virtual ICollection<OrderPayment> OrderPayment { get; set; }

        public class Factory
        {
            public static IPaymentMethod Create(Method method)
            {
                return method switch
                {
                    Method.Card => new PaymentByCard(),
                    Method.Cash => new PaymentInCash(),
                    _ => throw new ArgumentException()
                };
            }
        }

        public enum Method
        {
            Cash = 0, 
            Card = 1
        }
    }
}
