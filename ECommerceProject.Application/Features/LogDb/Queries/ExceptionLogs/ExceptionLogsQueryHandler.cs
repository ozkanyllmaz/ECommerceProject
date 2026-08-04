using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.LogDb.Queries.ExceptionLogs
{
    internal class ExceptionLogsQueryHandler : IRequestHandler<ExceptionLogsQueryRequest, CustomResponseDto<PaginationResult<ExceptionLogsQueryResponse>>>
    {
        private readonly IExceptionLogRepository _exceptionLogRepository;
        private readonly IMapper _mapper;

        public ExceptionLogsQueryHandler(IExceptionLogRepository exceptionLogRepository, IMapper mapper)
        {
            _exceptionLogRepository = exceptionLogRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<ExceptionLogsQueryResponse>>> Handle(ExceptionLogsQueryRequest request, CancellationToken cancellationToken)
        {
            var logs = await _exceptionLogRepository.GetAllAsQueryable()
                .ProjectTo<ExceptionLogsQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            return CustomResponseDto<PaginationResult<ExceptionLogsQueryResponse>>.Success(200, logs, "Exception loglar getirildi");
        }
    }
}
