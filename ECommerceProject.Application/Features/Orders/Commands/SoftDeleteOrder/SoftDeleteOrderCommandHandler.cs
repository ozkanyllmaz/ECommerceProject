using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;
using AutoMapper.QueryableExtensions;

namespace ECommerceProject.Application.Features.Orders.Commands.SoftDeleteOrder
{
    internal class SoftDeleteOrderCommandHandler : IRequestHandler<SoftDeleteOrderCommandRequest, CustomResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteOrderCommandHandler(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(SoftDeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"{request.OrderId} Id'li sipariş bulunamadı");

            order.IsDeleted = true;
            order.DeletedDate = DateTime.UtcNow;

            var orderItems = await _orderItemRepository.GetListAsync(x => x.OrderId == Guid.Parse(request.OrderId));

            foreach(var orderItem in orderItems)
            {
                _orderItemRepository.Remove(orderItem);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Sipariş başarıyla silindi. (soft delete)");

        }
    }
}
