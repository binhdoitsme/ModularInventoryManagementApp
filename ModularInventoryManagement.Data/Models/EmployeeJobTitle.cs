using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class EmployeeJobTitle
    {
        public int EmployeeId { get; set; }
        public int JobTitleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual JobTitle JobTitle { get; set; }
    }
}
