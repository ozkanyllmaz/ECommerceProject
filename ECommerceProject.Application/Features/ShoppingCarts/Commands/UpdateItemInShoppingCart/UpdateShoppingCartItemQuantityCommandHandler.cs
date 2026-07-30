using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.UpdateItemInShoppingCart
{
    internal class UpdateShoppingCartItemQuantityCommandHandler : IRequestHandler<UpdateShoppingCartItemQuantityCommandRequest, CustomResponseDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateShoppingCartItemQuantityCommandHandler(ICurrentUserService currentUserService, IShoppingCartItemRepository shoppingCartItemRepository, IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(UpdateShoppingCartItemQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var cart = await _shoppingCartRepository
                .GetAsync(x => x.IsActive && x.UserId == Guid.Parse(userId), cancellationToken);

            if (cart == null)
                throw new NotFoundException("Sepet bulunamadı");

            var cartItem = await _shoppingCartItemRepository
                .GetAsync(x => x.ShoppingCartId == cart.Id && x.ProductId == Guid.Parse(request.ProductId), cancellationToken);

            if (cartItem == null)
                throw new NotFoundException("Sepette ürün bulunamadı");

            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
                throw new NotFoundException("Ürün bulunamadı");

            if (request.Quantity > product.Stock)
                throw new BadRequestException("Talep edilen ürün adedi stoktan fazla");

            cartItem.Quantity = request.Quantity;

            _shoppingCartItemRepository.Update(cartItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Adet güncelleme başarılı");
        }
    }
}
