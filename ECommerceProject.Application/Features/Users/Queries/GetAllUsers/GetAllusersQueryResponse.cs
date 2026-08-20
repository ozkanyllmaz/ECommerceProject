using ECommerceProject.Application.DTOs.UserRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllusersQueryResponse
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool Status { get; set; }
        public List<UserRoleDto>? UserRoles { get; set; }
    }
}
