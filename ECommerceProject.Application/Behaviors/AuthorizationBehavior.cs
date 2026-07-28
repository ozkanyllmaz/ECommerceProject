using ECommerceProject.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var userRoles = _currentUserService.Roles;

            bool IsAuthorize = request.Roles.Any(role => userRoles.Contains(role));

            if (!IsAuthorize)
            {
                throw new UnauthorizedAccessException("Bu işlemi gerçekleştirmek için yetkiniz bulunmamaktadır.");
            }

            return await next();
        }
    }
}
