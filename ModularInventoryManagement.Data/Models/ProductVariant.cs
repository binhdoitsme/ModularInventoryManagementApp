using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class ProductVariant
    {
        public ProductVariant()
        {
            Batch = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public decimal ListPrice { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Product.Name.ToUpperInvariant()} {Product.Unit}";
        }
    }
}
