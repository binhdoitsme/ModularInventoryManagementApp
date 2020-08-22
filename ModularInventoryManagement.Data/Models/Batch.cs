using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Batch
    {
        public Batch()
        {
            Orderline = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public int Quantity { get; set; }
        public int ImportPrice { get; set; }
        public int ProductVariantId { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
        public virtual SupplierShipment Shipment { get; set; }
        public virtual ICollection<OrderLine> Orderline { get; set; }
    }
}
