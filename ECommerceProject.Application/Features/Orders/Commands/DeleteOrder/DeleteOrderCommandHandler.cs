using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.Orders.Commands.DeleteOrder
{
    // CancelOrder bu
    internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, CustomResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Sipariş bulunamadı id={request.OrderId}");

            var orderId = Guid.Parse(request.OrderId);

            if (order.Status == OrderStatus.SiparisAlindi)
            {
                order.Status = OrderStatus.İptalEdildi;
                order.CancellationReason = request.CancellationReason;
                order.CancelledDate = DateTime.UtcNow;

                var orderItems = await _orderItemRepository.GetListAsync(x => x.OrderId == orderId);

                foreach (var orderItem in orderItems)
                {
                    var productId = orderItem.ProductId;
                    var product = await _productRepository.GetByIdAsync(productId.ToString());
                    if (product == null)
                        throw new NotFiniteNumberException("Ürün bulunamadı");
                    product.Stock += orderItem.Quantity;
                    _productRepository.Update(product);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return CustomResponseDto.Success(200, "Sipariş başarıyla iptal edildi");

            }
            else
            {
                throw new BusinessException($"{order.Status} aşamasında sipariş iptal edilemez.");
            }
        }
    }
}
