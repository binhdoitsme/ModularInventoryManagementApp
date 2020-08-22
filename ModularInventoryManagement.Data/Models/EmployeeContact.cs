using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class EmployeeContact
    {
        public int EmployeeId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
