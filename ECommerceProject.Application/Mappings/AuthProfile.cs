using AutoMapper;
using ECommerceProject.Application.DTOs.Auth;
using ECommerceProject.Application.Features.Auth.Commands;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            //CreateMap<Kaynak, Hedef>();
            CreateMap<RegisterCommandRequest, User>();
        }
    }
}
