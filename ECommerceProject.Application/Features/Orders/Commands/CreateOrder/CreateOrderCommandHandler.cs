using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Domain.Entities.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Domain.Entities;
using System.Security.Cryptography;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.Orders.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CustomResponseDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRespository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public CreateOrderCommandHandler(ICurrentUserService currentUserService, IShoppingCartItemRepository shoppingCartItemRepository, IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, IOrderItemRepository orderItemRespository)
        {
            _currentUserService = currentUserService;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderItemRespository = orderItemRespository;
        }

        public async Task<CustomResponseDto> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var cart = await _shoppingCartRepository
                .GetAsync(x => x.UserId == Guid.Parse(userId), cancellationToken);

            if (cart == null)
                throw new NotFoundException("Sepet bulunamadı");

            var cartItems = await _shoppingCartItemRepository
                .GetListAsync(x => x.ShoppingCartId == cart.Id);

            if (!cartItems.Any())
                return CustomResponseDto.Success(200, "Sepet boş");

            var productIds = cartItems.Select(x => x.ProductId).Distinct().ToList();

            var productsList = await _productRepository.GetProductsByIdsAsync(productIds);

            // sepetteki ürünleri sipariş ürününe dönüştürme mapping 
            var orderItems = cartItems.Select(item =>
            {
                var currentProduct = productsList.FirstOrDefault(x => x.Id == item.ProductId);

                return new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = currentProduct?.Name ?? string.Empty,
                    UnitPrice = currentProduct?.Price ?? 0m,
                    TotalPrice = (currentProduct?.Price ?? 0m) * (item.Quantity)
                };
            }).ToList();

            var order = new Order
            {
                OrderItems = orderItems
            };

            order.UserId = Guid.Parse(userId);

            order.TotalAmount = orderItems.Sum(x => x.UnitPrice * x.Quantity);

            var num = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
            order.OrderNumber = (DateTime.UtcNow.ToString("yyyyMMdd") + num);

            order.OrderDate = DateTime.UtcNow;

            order.ShippingAddress = new Address
            {
                FirstName = request.ShippingAddress.FirstName,
                LastName = request.ShippingAddress.LastName,
                PhoneNumber = request.ShippingAddress.PhoneNumber,
                City = request.ShippingAddress.City,
                District = request.ShippingAddress.District,
                FullAddress = request.ShippingAddress.FullAddress,
                InvoiceType = request.ShippingAddress.InvoiceType,
                CompanyName = request.ShippingAddress.CompanyName,
                TaxOffice = request.ShippingAddress.TaxOffice,
                TaxNumber = request.ShippingAddress.TaxNumber,
            };

            order.BillingAddress = new Address
            {
                FirstName = request.BillingAddress.FirstName,
                LastName = request.BillingAddress.LastName,
                PhoneNumber = request.BillingAddress.PhoneNumber,
                City = request.BillingAddress.City,
                District = request.BillingAddress.District,
                FullAddress = request.BillingAddress.FullAddress,
                InvoiceType = request.BillingAddress.InvoiceType,
                CompanyName = request.BillingAddress.CompanyName,
                TaxOffice = request.BillingAddress.TaxOffice,
                TaxNumber = request.BillingAddress.TaxNumber,
            };

            // Stok güncelleme
            foreach (var orderItem in orderItems)
            {
                var product = productsList.FirstOrDefault(x => x.Id == orderItem.ProductId);
                if (product == null)
                    throw new NotFoundException($"Id = {orderItem.ProductId} olan  ürün bulunamadı");

                if (product.Stock < orderItem.Quantity)
                    throw new BusinessException($"{product.Name} isimli ürün için yeterli stok bulunmamaktadır");

                product.Stock -= orderItem.Quantity;
            }
            _productRepository.UpdateRange(productsList);

            await _orderRepository.AddAsync(order, cancellationToken);

            _shoppingCartItemRepository.RemoveRange(cartItems);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Sipariş başarıyla oluşturuldu");
        }
    }
}
