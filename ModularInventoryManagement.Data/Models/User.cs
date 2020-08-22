using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        public int? EmployeeId { get; set; }
        public int RoleId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
