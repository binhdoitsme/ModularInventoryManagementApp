using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class SupplierShipment
    {
        public SupplierShipment()
        {
            Batch = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public DateTimeOffset ImportTime { get; set; }
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
    }
}
