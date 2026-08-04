using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities.LogEntities;
using ECommerceProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ECommerceDbContext _context;

        public AuditLogRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public IQueryable<AuditLog> GetAllAsQueryable()
        {
            return _context.AuditLogs
                .OrderByDescending(x => x.TimeStamp)
                .AsQueryable();
        }
    }
}
