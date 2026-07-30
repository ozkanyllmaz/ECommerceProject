using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string Name { get; set; } = null!;

        public string[] Roles => ["Admin", "Customer"];
    }
}
