using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

    }
}
