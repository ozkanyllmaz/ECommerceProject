using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<IList<string>> GetRolesByUserIdAsync(Guid userId);

        IQueryable<User> GetLoginUser(string userId);

        Task<int> GetTotalUserCount();
    }
}
