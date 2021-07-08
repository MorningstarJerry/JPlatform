using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUser.Queries
{
    public class GetUserbyIdQuery : IRequestWrapper<ApplicationUserDto>
    {
        public string id { get; set; }
    }

    public class GetUserbyIdQueryHandler : IRequestHandlerWrapper<GetUserbyIdQuery, ApplicationUserDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public GetUserbyIdQueryHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<ApplicationUserDto>> Handle(GetUserbyIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserById(request.id);

            if (user == null)
                return ServiceResult.Failed<ApplicationUserDto>(ServiceError.ForbiddenError);

            return ServiceResult.Success(user);
        }

    }
}
