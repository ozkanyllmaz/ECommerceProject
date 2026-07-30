using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
