using AutoMapper;
using ECommerceProject.Application.Features.Roles.Command.CreateRole;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleCommandRequest, Role>();
        }
    }
}
