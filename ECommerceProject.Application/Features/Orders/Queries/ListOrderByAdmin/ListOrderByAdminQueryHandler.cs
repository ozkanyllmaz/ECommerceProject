using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderByAdmin
{
    internal class ListOrderByAdminQueryHandler : IRequestHandler<ListOrderByAdminQueryRequest, CustomResponseDto<List<ListOrderByAdminQueryResponse>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ListOrderByAdminQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ListOrderByAdminQueryResponse>>> Handle(ListOrderByAdminQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.GetListWithFilterAsQueryable(x => x.Status == OrderStatus.Onaylandı)
                .ProjectTo<ListOrderByAdminQueryResponse>(_mapper.ConfigurationProvider);

            var orders = await _orderRepository.ToListAsync(query, cancellationToken);
            if (!orders.Any())
                return CustomResponseDto<List<ListOrderByAdminQueryResponse>>.Success(200, "Sipariş boş");

            return CustomResponseDto<List<ListOrderByAdminQueryResponse>>.Success(200, orders, "Manager onaylı siparişler listelendi");

        }
    }
}
