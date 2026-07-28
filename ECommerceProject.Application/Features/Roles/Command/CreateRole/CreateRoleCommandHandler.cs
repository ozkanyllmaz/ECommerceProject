using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Roles.Command.CreateRole
{
    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CustomResponseDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request);

            await _roleRepository.AddAsync(role);
            var affectedRows = await _roleRepository.SaveAsync();

            if(affectedRows > 0)
            {
                return CustomResponseDto.Success(201, "İşlem başarılı");
            }
            throw new NotFoundException("Ürün eklenirken hata oluştu");
        }
    }
}
