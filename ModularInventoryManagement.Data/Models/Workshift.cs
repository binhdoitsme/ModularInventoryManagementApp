using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Workshift
    {
        public Workshift()
        {
            WorkshiftAssignment = new HashSet<WorkshiftAssignment>();
        }

        public int Id { get; set; }
        public byte DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SalaryPerHour { get; set; }

        public virtual ICollection<WorkshiftAssignment> WorkshiftAssignment { get; set; }
    }
}
