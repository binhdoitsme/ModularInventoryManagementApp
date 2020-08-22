using System;
using System.Collections.Generic;

namespace ModularInventoryManagement.Data.Models
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string View { get; set; }

        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
