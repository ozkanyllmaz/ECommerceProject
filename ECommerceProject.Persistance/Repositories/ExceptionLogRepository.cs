using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities.LogEntities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly ECommerceDbContext _context;

        public ExceptionLogRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public IQueryable<ExceptionLog> GetAllAsQueryable()
        {
            return _context.ExceptionLogs
                .OrderByDescending(x => x.TimeStamp)
                .AsQueryable();
        }
    }
}
