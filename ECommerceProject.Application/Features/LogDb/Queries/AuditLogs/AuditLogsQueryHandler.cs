using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.LogDb.Queries.AuditLogs
{
    internal class AuditLogsQueryHandler : IRequestHandler<AuditLogsQueryRequest, CustomResponseDto<PaginationResult<AuditLogsQueryResponse>>>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper _mapper;

        public AuditLogsQueryHandler(IAuditLogRepository auditLogRepository, IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<AuditLogsQueryResponse>>> Handle(AuditLogsQueryRequest request, CancellationToken cancellationToken)
        {
            var logs = await _auditLogRepository.GetAllAsQueryable()
                .ProjectTo<AuditLogsQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            return CustomResponseDto<PaginationResult<AuditLogsQueryResponse>>.Success(200, logs, "Audit logları getirildi");
        }
    }
}
