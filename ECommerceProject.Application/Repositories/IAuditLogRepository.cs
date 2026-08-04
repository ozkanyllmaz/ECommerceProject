using ECommerceProject.Domain.Entities.LogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface IAuditLogRepository
    {
        IQueryable<AuditLog> GetAllAsQueryable();
    }
}
