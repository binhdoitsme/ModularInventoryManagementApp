using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Attendance
    {
        public int EmployeeId { get; set; }
        public int WorkshiftAssignmentId { get; set; }
        public DateTimeOffset CheckTime { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual WorkshiftAssignment WorkshiftAssignment { get; set; }
    }
}
