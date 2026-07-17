using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Auth
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}
