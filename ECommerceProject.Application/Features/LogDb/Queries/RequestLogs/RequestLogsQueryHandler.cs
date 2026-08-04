using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.LogDb.Queries.RequestLogs
{
    internal class RequestLogsQueryHandler : IRequestHandler<RequestLogsQueryRequest, CustomResponseDto<PaginationResult<RequestLogsQueryResponse>>>
    {
        private readonly IRequestLogRepository _requestLogRepository;
        private readonly IMapper _mapper;

        public RequestLogsQueryHandler(IRequestLogRepository requestLogRepository, IMapper mapper)
        {
            _requestLogRepository = requestLogRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<RequestLogsQueryResponse>>> Handle(RequestLogsQueryRequest request, CancellationToken cancellationToken)
        {
            var logs = await _requestLogRepository.GetAllAsQueryable()
                .ProjectTo<RequestLogsQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            return CustomResponseDto<PaginationResult<RequestLogsQueryResponse>>.Success(200, logs, "Request logları başarıyla getirildi");
        }
    }
}
