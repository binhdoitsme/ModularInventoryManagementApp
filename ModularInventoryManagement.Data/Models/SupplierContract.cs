using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class SupplierContract
    {
        public int SupplierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte EarlyEnded { get; set; }
        public int ContractMaker { get; set; }

        public virtual Employee ContractMakerNavigation { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
