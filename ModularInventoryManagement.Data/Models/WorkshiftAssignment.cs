using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class WorkshiftAssignment
    {
        public WorkshiftAssignment()
        {
            Attendance = new HashSet<Attendance>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Multiplier { get; set; }
        public int EmployeeId { get; set; }
        public int WorkshiftId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Workshift Workshift { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
    }
}
