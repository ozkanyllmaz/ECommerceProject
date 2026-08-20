using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;
using ECommerceProject.Application.DTOs.Common;

namespace ECommerceProject.Application.Features.Users.Commands.UpdateUserStatus
{
    internal class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommandRequest, CustomResponseDto<UpdateUserStatusCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserStatusCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<UpdateUserStatusCommandResponse>> Handle(UpdateUserStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("UserId bulunamadı");

            var user = await _userRepository.GetAsync(x => x.Id == Guid.Parse(userId));

            user.Status = request.status;

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto<UpdateUserStatusCommandResponse>.Success(200, "Kullanıcı durumu güncelleme başarılı");
            
        }
    }
}
