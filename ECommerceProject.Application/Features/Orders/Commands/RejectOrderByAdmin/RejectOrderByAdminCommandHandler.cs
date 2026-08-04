using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.Orders.Commands.RejectOrderByAdmin
{
    internal class RejectOrderByAdminCommandHandler : IRequestHandler<RejectOrderByAdminCommandRequest, CustomResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectOrderByAdminCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(RejectOrderByAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Sipariş bulunamadı : {request.OrderId}");

            order.Status = Domain.Entities.Enums.OrderStatus.Reddedildi;
            order.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Admin tarafından sipariş reddedildi");
        }
    }
}
