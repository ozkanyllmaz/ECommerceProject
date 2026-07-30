using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string[] Roles => ["Admin", "Manager"];
    }
}
