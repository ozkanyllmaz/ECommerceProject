using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
