using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrder
{
    internal class ListOrderQueryHandler : IRequestHandler<ListOrderQueryRequest, CustomResponseDto<List<ListOrderQueryResponse>>>
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

        public async Task<CustomResponseDto<List<ListOrderQueryResponse>>> Handle(ListOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var query = _orderRepository.GetListWithFilterAsQueryable(x => x.UserId == Guid.Parse(userId))
                .ProjectTo<ListOrderQueryResponse>(_mapper.ConfigurationProvider);

            var orderItems = await _orderRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<List<ListOrderQueryResponse>>.Success(200, orderItems, "Siparişler başarıyla getirildi");
        }
    }
}
