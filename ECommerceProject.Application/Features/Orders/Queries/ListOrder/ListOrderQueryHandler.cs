using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrder
{
    internal class ListOrderQueryHandler : IRequestHandler<ListOrderQueryRequest, CustomResponseDto<PaginationResult<ListOrderQueryResponse>>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ListOrderQueryHandler(ICurrentUserService currentUserService, IOrderRepository orderRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<ListOrderQueryResponse>>> Handle(ListOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var orderItems = await _orderRepository.GetListWithFilterAsQueryable(x => x.UserId == Guid.Parse(userId))
                .ProjectTo<ListOrderQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            //var orderItems = await _orderRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<PaginationResult<ListOrderQueryResponse>>.Success(200, orderItems, "Siparişler başarıyla getirildi");
        }
    }
}
