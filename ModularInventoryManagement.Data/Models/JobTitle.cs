using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            EmployeeJobTitle = new HashSet<EmployeeJobTitle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalaryPerHour { get; set; }
        public decimal LatePenaltyPerMin { get; set; }

        public virtual ICollection<EmployeeJobTitle> EmployeeJobTitle { get; set; }
    }
}
