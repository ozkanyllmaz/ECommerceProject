using AutoMapper;
using ECommerceProject.Application.Features.Categories.Commands.CreateCategory;
using ECommerceProject.Application.Features.Categories.Commands.UpdateCategory;
using ECommerceProject.Application.Features.Categories.Queries.ListCategory;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommandRequest, Category>();
            CreateMap<UpdateCategoryCommandRequest, Category>();

            CreateMap<Category, ListCategoryQueryResponse>();
        }
    }
}
