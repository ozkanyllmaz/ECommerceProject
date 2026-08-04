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
        string? DeviceId { get; }
        string? CreatedById { get; }
        List<string> Roles { get; }
    }
}
