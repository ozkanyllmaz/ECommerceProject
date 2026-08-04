using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.LogDb.Queries.RequestLogs
{
    public class RequestLogsQueryRequest : IRequest<CustomResponseDto<PaginationResult<RequestLogsQueryResponse>>>, ISecuredRequest
    {
        public PaginationParameter paginationParameter { get; set; } = null!;
        public string[] Roles => ["Admin"];
    }
}
