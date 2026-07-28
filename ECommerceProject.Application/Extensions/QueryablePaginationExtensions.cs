using ECommerceProject.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Extensions
{
    public static class QueryablePaginationExtensions
    {
        public static async Task<PaginationResult<T>> ToPaginatedResultAsync<T>(
            this IQueryable<T> query,
            PaginationParameter parameter,
            CancellationToken cancellationToken = default)
        {
            int pageNumber = parameter.PageNumber;
            int pageSize = parameter.PageSize;

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var count = query.Count();

            List<T> items = new List<T>();

            if (count > 0)
            {
                items = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            return new PaginationResult<T>(items, count, pageNumber, pageSize);

        }
    }
}
