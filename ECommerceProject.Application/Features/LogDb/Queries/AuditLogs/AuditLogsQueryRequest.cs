using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.LogDb.Queries.AuditLogs
{
    public class AuditLogsQueryRequest : IRequest<CustomResponseDto<PaginationResult<AuditLogsQueryResponse>>>, ISecuredRequest
    {
        public PaginationParameter paginationParameter { get; set; } = null!;
        public string[] Roles => ["Admin"];
    }
}
