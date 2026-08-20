using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListAllOrder
{
    internal class ListAllOrderQueryHandler : IRequestHandler<ListAllOrderQueryRequest, CustomResponseDto<PaginationResult<ListAllOrderQueryResponse>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ListAllOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<ListAllOrderQueryResponse>>> Handle(ListAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetListAsQueryable()
                .ProjectTo<ListAllOrderQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            return CustomResponseDto<PaginationResult<ListAllOrderQueryResponse>>.Success(200, orders, "Tüm siparişler başarıyla getirildi");
        }
    }
}
