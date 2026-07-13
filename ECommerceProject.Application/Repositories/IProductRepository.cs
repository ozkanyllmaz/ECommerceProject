using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        // Product entity'sine özel metotlar buraya yazılabilir.
    }
}
