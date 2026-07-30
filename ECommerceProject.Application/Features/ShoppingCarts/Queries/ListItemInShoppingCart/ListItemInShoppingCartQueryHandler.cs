using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Abstractions.UnitOfWorks;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart
{
    internal class ListItemInShoppingCartQueryHandler : IRequestHandler<ListItemInShoppingCartQueryRequest, CustomResponseDto<List<ListItemInShoppingCartQueryResponse>>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListItemInShoppingCartQueryHandler(ICurrentUserService currentUserService, IShoppingCartItemRepository shoppingCartItemRepository, IUnitOfWork unitOfWork, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _unitOfWork = unitOfWork;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ListItemInShoppingCartQueryResponse>>> Handle(ListItemInShoppingCartQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var cart = await _shoppingCartRepository
                .GetAsync(x => x.IsActive && x.UserId == Guid.Parse(userId));

            if (cart == null)
                throw new NotFoundException("Sepet bulunamadı");

            var query = _shoppingCartItemRepository
                .GetListWithFilterAsQueryable(x => x.ShoppingCartId == cart.Id && !x.IsDeleted)
                .ProjectTo<ListItemInShoppingCartQueryResponse>(_mapper.ConfigurationProvider);

            var cartItems = await _shoppingCartItemRepository.ToListAsync(query, cancellationToken);


            if (!cartItems.Any())
                throw new NotFoundException("Sepette ürün bulunamadı");

            return CustomResponseDto<List<ListItemInShoppingCartQueryResponse>>.Success(200, cartItems, "Sepetteki ürünleri listeleme başarılı");

        }
    }
}
