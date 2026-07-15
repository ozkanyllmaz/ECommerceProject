using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerceProject.Application.DTOs.Common
{
    public class CustomResponseDto<T>
    {
        // Asıl verimiz (ürün listesi veya null)
        public T Data { get; set; }
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        // Sadece backend'in (BaseController) kullancağı statis kodu.
        // Frontend'e giden JSON paketinde Gizlenecek.
        [JsonIgnore]
        public int StatusCode { get; set; }

        // Başarılı geriye veri dönen (Listeleme, detay getirme)
        public static CustomResponseDto<T> Success(int statusCode, T data, string message = null)
        {
            return new CustomResponseDto<T>
            {
                Data = data,
                IsSuccessfull = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        // Başarılı ama geriye veri dönmeyen (Silmei Güncelleme)
        public static CustomResponseDto<T> Success(int statusCode, string message = null)
        {
            return new CustomResponseDto<T>
            {
                IsSuccessfull = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        // Başarısız ve geriye birden fazla hata dönen (Validation - doğrulama hataları)
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors, string message = "İşlem sırasında hatalar oluştu.")
        {
            return new CustomResponseDto<T>
            {
                IsSuccessfull = false,
                Message = message,
                Errors = errors,
                StatusCode = statusCode
            };
        }

        // Başarısız ve geriye tek bir hata dönen (Aranan ürün bulunamadı.)
        public static CustomResponseDto<T> Fail(int statusCode, string error, string message = "İşlem başarısız")
        {
            return new CustomResponseDto<T>
            {
                IsSuccessfull = false,
                Message = message,
                Errors = new List<string> { error },
                StatusCode = statusCode
            };
        }
    }
}
