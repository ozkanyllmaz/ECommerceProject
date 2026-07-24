using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace ECommerceProject.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestStatus = _httpContextAccessor.HttpContext;

            var requestMethod = requestStatus.Request.Method;
            var requestPath = requestStatus.Request.Path;
            var requestProtocol = requestStatus.Request.Protocol;
            var requestSheme = requestStatus.Request.Scheme;

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
                    "Request Tamamlandı:{@requestMethod} | {@requestSheme} | {@requestPath} | {@requestProtocol} | Süre: {@ElapseMilliseconds} ms",
                     requestMethod, requestSheme, requestPath, requestProtocol, stopwatch.ElapsedMilliseconds);

                return response;
            }
        }
    }
}
