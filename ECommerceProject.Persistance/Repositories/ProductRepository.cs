using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
