using AutoMapper;
using ECommerceProject.Application.Features.Auth.Commands.Register;
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
