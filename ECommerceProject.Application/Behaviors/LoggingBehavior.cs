using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ECommerceProject.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            var userIdentityfier = _currentUserService.IsAuthenticated
                ? (_currentUserService.Email ?? _currentUserService.UserId)
                : "Anonim";

            var logContext = new Dictionary<string, object>
            {
                { "User", userIdentityfier ?? "-" }
            };

            using (_logger.BeginScope(logContext))
            {
                _logger.LogInformation(
                                "Request Başladı: {RequestName} | Veri: {@ReqeustData}",
                                requestName, request);

                var stopwatch = Stopwatch.StartNew();

                var response = await next();

                stopwatch.Stop();

                _logger.LogInformation(
                    "Request Tamamlandı: {RequestName} | Süre: {ElapseMilliseconds} ms",
                    requestName, stopwatch.ElapsedMilliseconds);

                return response;
            }
        }
    }
}
