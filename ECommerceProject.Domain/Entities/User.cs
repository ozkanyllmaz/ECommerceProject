using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Kriptografi kütüphaneleri byte dizisi ile çalışır.
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // Kullanıcı hesabı aktif mi değil mi
        public bool Status { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
