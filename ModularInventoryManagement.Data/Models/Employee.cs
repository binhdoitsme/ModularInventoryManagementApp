using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendance = new HashSet<Attendance>();
            EmployeeJobTitle = new HashSet<EmployeeJobTitle>();
            SupplierContract = new HashSet<SupplierContract>();
            User = new HashSet<User>();
            WorkshiftAssignment = new HashSet<WorkshiftAssignment>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Dob { get; set; }
        public int Gender { get; set; }
        public DateTime HireDate { get; set; }

        public virtual EmployeeContact EmployeeContact { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<EmployeeJobTitle> EmployeeJobTitle { get; set; }
        public virtual ICollection<SupplierContract> SupplierContract { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<WorkshiftAssignment> WorkshiftAssignment { get; set; }
    }
}
