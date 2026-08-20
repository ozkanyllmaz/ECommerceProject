using AutoMapper;
using ECommerceProject.Application.DTOs.UserRole;
using ECommerceProject.Application.Features.Users.Commands.UpdateUser;
using ECommerceProject.Application.Features.Users.Queries.GetAllUsers;
using ECommerceProject.Application.Features.Users.Queries.GetLoginUser;
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

            CreateMap<User, GetLoginUserQueryResponse>();

            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<User, GetAllusersQueryResponse>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));
        }
    }
}
