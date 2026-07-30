using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.DeleteItemInShoppingCart
{
    internal class DeleteItemInShoppingCartCommandHandler : IRequestHandler<DeleteItemInShoppingCartCommandRequest, CustomResponseDto>
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteItemInShoppingCartCommandHandler(IShoppingCartItemRepository shoppingCartItemRepository, ICurrentUserService currentUserService, IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _currentUserService = currentUserService;
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(DeleteItemInShoppingCartCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var productId = request.ProductId;

            var cart = await _shoppingCartRepository
                .GetAsync(x => x.UserId == Guid.Parse(userId) && x.IsActive, cancellationToken);

            if (cart == null)
                throw new NotFoundException("Sepet bulunamadı");


            var cartItem = await _shoppingCartItemRepository
                .GetAsync(x => x.ShoppingCartId == cart.Id && x.ProductId == Guid.Parse(productId), cancellationToken);

            if (cartItem == null)
                throw new NotFoundException("Sepette böyle bir ürün bulunamadı");

            _shoppingCartItemRepository.Remove(cartItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Ürün sepetten silindi");

        }
    }
}
