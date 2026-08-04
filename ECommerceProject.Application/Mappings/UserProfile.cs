using AutoMapper;
using ECommerceProject.Application.Features.Users.Commands.UpdateUser;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserCommandRequest, User>();
        }
    }
}
