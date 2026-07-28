using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Common
{
    public class PaginationParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }  

        public PaginationParameter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationParameter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
}
