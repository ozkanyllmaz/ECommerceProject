using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDailyRevenue
{
    internal class GetDailyRevenueQueryHandler : IRequestHandler<GetDailyRevenueQueryRequest, CustomResponseDto<List<GetDailyRevenueQueryResponse>>>
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public GetDailyRevenueQueryHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<CustomResponseDto<List<GetDailyRevenueQueryResponse>>> Handle(GetDailyRevenueQueryRequest request, CancellationToken cancellationToken)
        {
            DateTime startDate = DateTime.Today.AddDays(-6);
            var values = await _orderItemRepository.GetWeeklyRevenueDtoAsync(startDate);

            var response = new List<GetDailyRevenueQueryResponse>();

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);

                values.TryGetValue(currentDate, out decimal dailyTotal);
                response.Add(new GetDailyRevenueQueryResponse
                {
                    DayName = currentDate.ToString("ddd", new System.Globalization.CultureInfo("tr-TR")),
                    TotalRevenue = dailyTotal,
                });
            }

            return CustomResponseDto<List<GetDailyRevenueQueryResponse>>.Success(200, response, "BarChart verileri getirildi");
        }
    }
}
