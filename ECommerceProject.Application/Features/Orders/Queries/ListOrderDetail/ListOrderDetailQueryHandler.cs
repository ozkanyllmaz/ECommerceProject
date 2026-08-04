using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderDetail
{
    internal class ListOrderDetailQueryHandler : IRequestHandler<ListOrderDetailQueryRequest, CustomResponseDto<List<ListOrderDetailQueryResponse>>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ListOrderDetailQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<CustomResponseDto<List<ListOrderDetailQueryResponse>>> Handle(ListOrderDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.GetListWithFilterAsQueryable(x => x.Id == Guid.Parse(request.OrderId))
                .ProjectTo<ListOrderDetailQueryResponse>(_mapper.ConfigurationProvider);

            var order = await _orderRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<List<ListOrderDetailQueryResponse>>.Success(200, order, "Sipariş ürünler başarılı bir şekilde getirildi");

        }
    }
}
