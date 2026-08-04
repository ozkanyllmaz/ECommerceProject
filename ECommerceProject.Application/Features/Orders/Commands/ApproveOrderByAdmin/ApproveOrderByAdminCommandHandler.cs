using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.Orders.Commands.ApproveOrderByAdmin
{
    internal class ApproveOrderByAdminCommandHandler : IRequestHandler<ApproveOrderByAdminCommandRequest, CustomResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveOrderByAdminCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(ApproveOrderByAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Sipariş bulunamadı : {request.OrderId}");

            order.Status = Domain.Entities.Enums.OrderStatus.Tamamlandı;
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Admin siparişi onayladı");

        }
    }
}
