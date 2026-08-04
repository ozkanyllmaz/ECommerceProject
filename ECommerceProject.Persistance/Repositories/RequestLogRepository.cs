using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities.LogEntities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class RequestLogRepository : IRequestLogRepository
    {
        private readonly ECommerceDbContext _context;

        public RequestLogRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public IQueryable<RequestLog> GetAllAsQueryable()
        {
            return _context.RequestLogs
                .OrderByDescending(x => x.TimeStamp)
                .AsQueryable();
        }
    }
}
