using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardPieChart
{
    internal class GetDashboardPieChartQueryHandler : IRequestHandler<GetDashboardPieChartQueryRequest, CustomResponseDto<GetDashboardPieChartQueryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetDashboardPieChartQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<GetDashboardPieChartQueryResponse>> Handle(GetDashboardPieChartQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await _categoryRepository.GetProductSalesResultAsync();

            var response = new GetDashboardPieChartQueryResponse
            {
                ProductSalesResults = values
            };

            return CustomResponseDto<GetDashboardPieChartQueryResponse>.Success(200, response, "PieChart bilgileri getirildi");
        }
    }
}
