using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Abstractions.UnitOfWorks;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.ClearShoppingCart
{
    internal class ClearShoppingCartCommandHandler : IRequestHandler<ClearShoppingCartCommandRequest, CustomResponseDto>
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public ClearShoppingCartCommandHandler(IShoppingCartItemRepository shoppingCartItemRepository, IShoppingCartRepository shoppingCartRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(ClearShoppingCartCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var cart = await _shoppingCartRepository
                .GetAsync(x => x.UserId == Guid.Parse(userId) && x.IsActive, cancellationToken);

            if (cart == null)
                throw new NotFoundException("Sepet bulunamadı");

            var cartItem = await _shoppingCartItemRepository
                .GetListAsync(x => x.ShoppingCartId == cart.Id);

            if (cartItem == null || !cartItem.Any())
                return CustomResponseDto.Success(200, "Sepet zaten boş");

            foreach(var item in cartItem)
            {
                item.IsDeleted = true;
            }
            _shoppingCartItemRepository.UpdateRange(cartItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Sepet boşaltıldı");

        }
    }
}
