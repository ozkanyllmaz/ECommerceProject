using ECommerceProject.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
                // Handler'da bir hata fırlatılırsa ve kimse yakalamazsa buraya düşer.
                _logger.LogError(ex, "Uygulamada yakalanamayan bir hata oluştu");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // default olarak 500 dönüyoruz.
            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "Sunucu Hatası";

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

                case ValidationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Doğrulama Hatası";
                    break;

                case AuthenticationException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    title = "Giriş Başarısız";
                    break;

                case BusinessException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Giriş Başarısız";
                    break;
            }

            //response ayarları
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            // RRC 7807 hata standardı formatı
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message, // Handler'daki message
                Instance = context.Request.Path // Hatanın olduğu endpoint
                
            };

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(jsonResponse);

        }
    }
}
