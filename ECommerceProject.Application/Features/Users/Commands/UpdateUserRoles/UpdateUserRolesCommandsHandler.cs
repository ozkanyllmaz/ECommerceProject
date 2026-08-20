using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.Users.Commands.UpdateUserRoles
{
    internal class UpdateUserRolesCommandsHandler : IRequestHandler<UpdateUserRolesCommandsRequest, CustomResponseDto<UpdateUserRolesCommandsResponse>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserRolesCommandsHandler(IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
        {
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<UpdateUserRolesCommandsResponse>> Handle(UpdateUserRolesCommandsRequest request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.UserId);
            var requestedRoleIds = request.RoleIds ?? new List<Guid>();

            var currentRoles = await _userRoleRepository.GetListAsync(x => x.UserId == Guid.Parse(request.UserId));
            var currentRoleIds = currentRoles.Select(x => x.RoleId).ToList();

            var rolesToRemove = currentRoles.Where(cr => !requestedRoleIds.Contains(cr.RoleId)).ToList();

            var roleIdsToAdd = requestedRoleIds.Where(id => !currentRoleIds.Contains(id)).ToList();
            var rolesToAdd = roleIdsToAdd.Select(roleId => new UserRole
            {
                UserId = userId,
                RoleId = roleId,
            }).ToList();

            if (rolesToRemove.Any())
            {
                _userRoleRepository.RemoveRange(rolesToRemove);
            }

            if (rolesToAdd.Any())
            {
                await _userRoleRepository.AddRangeAsync(rolesToAdd);
            }

            if(rolesToRemove.Any() || rolesToAdd.Any())
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return CustomResponseDto<UpdateUserRolesCommandsResponse>.Success(200, "Güncelleme başarılı");

        }
    }
}
