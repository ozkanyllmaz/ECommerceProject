using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }


        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }

        // Hangi Ip den oluşturuldu (güvenlik ve loglama)
        public string CreatedByIp { get; set; }

        // Token'ı manuel iptal etmek(çıkış yapma)
        public DateTime? RevokedDate { get; set; }
        public string? RevokedByIp { get; set; }

        // Token yerine verilen yeni token
        public string? ReplacedByToken { get; set; }

        // İptal edilme sebebi.
        public string? ReasonRevoked {  get; set; }

        
        public bool IsExpired(DateTime currentTime) => currentTime >= ExpiresDate; // Token süresi doldu mu kontrol eder.
        public bool IsRevoked() => RevokedDate != null; // İptal edilmiş mi
        public bool IsActive(DateTime currentTime) => !IsRevoked() && !IsExpired(currentTime); // Aktif ve kullanılabilir.

    }
}
