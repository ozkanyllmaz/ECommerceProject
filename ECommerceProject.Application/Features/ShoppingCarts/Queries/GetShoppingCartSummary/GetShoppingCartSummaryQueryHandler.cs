using AutoMapper;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart;

namespace ECommerceProject.Application.Features.ShoppingCarts.Queries.GetShoppingCartSummary
{
    internal class GetShoppingCartSummaryQueryHandler : IRequestHandler<GetShoppingCartSummaryQueryRequest, CustomResponseDto<GetShoppingCartSummaryQueryResponse>>
    {
        private readonly IProductRepository _productRespository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRespository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartSummaryQueryHandler(IProductRepository productRespository, IMapper mapper, IShoppingCartItemRepository shoppingCartItemRespository, ICurrentUserService currentUserService, IShoppingCartRepository shoppingCartRepository)
        {

            _productRespository = productRespository;
            _mapper = mapper;
            _shoppingCartItemRespository = shoppingCartItemRespository;
            _currentUserService = currentUserService;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<CustomResponseDto<GetShoppingCartSummaryQueryResponse>> Handle(GetShoppingCartSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var cart = await _shoppingCartRepository
                .GetAsync(x => x.IsActive && x.UserId == Guid.Parse(userId), cancellationToken);

            if (cart == null)
                throw new NotFoundException("Sepet bulunamadı");

            var query = _shoppingCartItemRespository
                .GetListWithFilterAsQueryable(x => x.ShoppingCartId == cart.Id && !x.IsDeleted)
                .ProjectTo<ListItemInShoppingCartQueryResponse>(_mapper.ConfigurationProvider);

            var cartItems = await _shoppingCartItemRespository.ToListAsync(query, cancellationToken);

            if (cartItems.Count <= 0 || !cartItems.Any())
                return CustomResponseDto<GetShoppingCartSummaryQueryResponse>.Success(200, "Sepet boş");

            var response = new GetShoppingCartSummaryQueryResponse
            {
                CartItems = cartItems
            };

            response.SubTotal = cartItems.Sum(x => x.Price * x.Quantity);

            response.TaxAmount = response.SubTotal * 0.2m; // sabit bir kdv oranı 

            response.ShippingCost = response.SubTotal >= 1500m ? 0m : 500m;

            response.TotalPrice = response.SubTotal + response.TaxAmount + response.ShippingCost;

            return CustomResponseDto<GetShoppingCartSummaryQueryResponse>.Success(200, response, "Sipariş özeti hazır");
        }
    }
}
