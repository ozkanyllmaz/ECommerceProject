using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Queries.GetLoginUser
{
    public class GetLoginUserQueryResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
