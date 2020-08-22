using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductVariant = new HashSet<ProductVariant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductVariant> ProductVariant { get; set; }
    }
}
