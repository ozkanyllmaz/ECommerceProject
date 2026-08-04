using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderByManager
{
    internal class ListOrderByManagerQueryHandler : IRequestHandler<ListOrderByManagerQueryRequest, CustomResponseDto<List<ListOrderByManagerQueryResponse>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ListOrderByManagerQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ListOrderByManagerQueryResponse>>> Handle(ListOrderByManagerQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.GetListWithFilterAsQueryable(x => x.Status == OrderStatus.SiparisAlindi)
                .ProjectTo<ListOrderByManagerQueryResponse>(_mapper.ConfigurationProvider);

            var orders = await _orderRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<List<ListOrderByManagerQueryResponse>>.Success(200, orders, $"Statüsü : '{OrderStatus.SiparisAlindi}' olan siparişler listelendi");
        }
    }
}
