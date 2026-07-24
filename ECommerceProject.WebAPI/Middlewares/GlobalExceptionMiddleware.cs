using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace ECommerceProject.WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // isteği pipeline'da bir sonraki middleware'e veya controller'a gönderiyoruz.
                await _next(context);
            }
            catch (Exception ex)
            {
                var userIdentityfier = context.User.Identity?.IsAuthenticated == true
                    ? context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                    : "Anonim";

                using (Serilog.Context.LogContext.PushProperty("User", userIdentityfier))
                {
                    // Handler'da bir hata fırlatılırsa ve kimse yakalamazsa buraya düşer.
                    _logger.LogError(ex, "İşlem sırasında bir kural ihlali oluştu: {exception}", ex.Message);

                }
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // default olarak 500 dönüyoruz.
            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "Sunucu Hatası";

            // RRC 7807 hata standardı formatı
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path // Hatanın olduğu endpoint
            };

            switch (exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    title = "Kaynak Bulunamadı";
                    break;

                case ArgumentNullException:
                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Geçersiz Argüman";
                    break;

                case InvalidOperationException:
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Geçersiz İşlem";
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    title = "Yetkisiz İşlem";
                    break;

                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Doğrulama Hataları";
                    problemDetails.Status = statusCode;
                    problemDetails.Title = "Doğrulama Hatası";
                    problemDetails.Detail = exception.Message;// Handler'daki message
                    problemDetails.Extensions.Add("errors", validationException.Errors);
                    break;

                case AuthenticationException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    title = "Giriş Başarısız";
                    break;

                case BusinessException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Giriş Başarısız";
                    break;

                default:
                    problemDetails.Status = statusCode;
                    problemDetails.Title = title;
                    problemDetails.Detail = exception.Message;
                    break;

            }

            //response ayarları
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            problemDetails.Status = statusCode;
            problemDetails.Title = title;
            problemDetails.Detail = exception.Message;// Handler'daki message

            

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(jsonResponse);

        }
    }
}
