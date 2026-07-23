using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
    }
}
