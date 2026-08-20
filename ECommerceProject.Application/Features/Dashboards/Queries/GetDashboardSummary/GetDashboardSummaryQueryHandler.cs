using ECommerceProject.Application.Abstractions.UnitOfWorks;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardSummary
{
    internal class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQueryRequest, CustomResponseDto<GetDashboardSummaryQueryResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        public GetDashboardSummaryQueryHandler(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CustomResponseDto<GetDashboardSummaryQueryResponse>> Handle(GetDashboardSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            var totalUsers = await _userRepository.GetTotalUserCount();

            var totalRevenue = await _orderRepository.GetTotalRevenue();

            var totalOrders = await _orderRepository.GetTotalOrderCount();

            var response = new GetDashboardSummaryQueryResponse
            {
                TotalOrders = totalOrders.ToString(),
                TotalRevenue = totalRevenue,
                TotalUsers = totalUsers.ToString()
            };

            return CustomResponseDto<GetDashboardSummaryQueryResponse>.Success(200, response, "Dashboard bilgileri çekildi");
                
        }
    }
}
