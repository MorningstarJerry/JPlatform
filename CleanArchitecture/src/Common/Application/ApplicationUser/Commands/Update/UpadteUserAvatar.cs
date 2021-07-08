using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUser.Commands.Update
{
    public class UpadteUserAvatar : IRequestWrapper<ApplicationUserDto>
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
    }

    public class UpadteUserAvatarHandler : IRequestHandlerWrapper<UpadteUserAvatar, ApplicationUserDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public UpadteUserAvatarHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<ApplicationUserDto>> Handle(UpadteUserAvatar request, CancellationToken cancellationToken)
        {
            var task = await _identityService.UpdateUserAvatarAsync(request.Id, request.Avatar);

            if (task.Succeeded)
            {
                var user = await _identityService.GetUserById(request.Id);

                if (user == null)
                    return ServiceResult.Failed<ApplicationUserDto>(ServiceError.ForbiddenError);

                return ServiceResult.Success(user);
            }

            return null;
        }

    }
}
