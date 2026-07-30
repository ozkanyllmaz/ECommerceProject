using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.AddItemToShoppingCart
{
    internal class AddItemToShoppingCartCommandHandler : IRequestHandler<AddItemToShoppingCartCommandRequest, CustomResponseDto>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IShoppingCartItemRepository shoppingCartItemRepository, ICurrentUserService currentUserService, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _currentUserService = currentUserService;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(AddItemToShoppingCartCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            if (currentUser == null)
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var userId = Guid.Parse(currentUser);
            var productId = Guid.Parse(request.ProductId);

            var product = await _productRepository
                .GetAsync(x => x.Id == productId, cancellationToken);

            if (product == null)
                throw new NotFoundException("Ürün bulunamadı");

            if (request.Quantity > product.Stock)
                throw new BadRequestException("Talep edilen miktar ürün stoğundan fazla.");

            var activeCart = await _shoppingCartRepository
                .GetAsync(x => x.UserId == userId && x.IsActive == true, cancellationToken);

            if (activeCart == null)
            {
                activeCart = new ShoppingCart
                {
                    UserId = userId,
                    IsActive = true,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                };

                var cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = activeCart.Id,
                    ProductId = productId,
                    Quantity = request.Quantity,
                };
                activeCart.ShoppingCartItems.Add(cartItem);

                await _shoppingCartRepository.AddAsync(activeCart, cancellationToken);
            }

            var existingCartItem = await _shoppingCartItemRepository
                .GetAsync(x => x.ShoppingCartId == activeCart.Id && x.ProductId == productId, cancellationToken);

            if(existingCartItem != null)
            {
                existingCartItem.Quantity += request.Quantity;
                if (existingCartItem.Quantity > product.Stock)
                    throw new BadRequestException("Talep edilen miktar ürün stoğundan fazla.");
            }
            else
            {
                var cartItems = new ShoppingCartItem
                {
                    ShoppingCartId = activeCart.Id,
                    ProductId = productId,
                    Quantity = request.Quantity,
                };
                await _shoppingCartItemRepository.AddAsync(cartItems, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(201, "Ürün sepete eklendi");
        }
    }
}
