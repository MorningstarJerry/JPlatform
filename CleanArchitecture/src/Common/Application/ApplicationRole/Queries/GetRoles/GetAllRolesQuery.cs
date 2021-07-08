using Application.Common.Interfaces;
using Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationRole.Queries.GetRoles
{
    public class GetAllRolesQuery : IRequestWrapper<RolesResponse>
    {
    }

    public class GetAllRolesQueryHandler : IRequestHandlerWrapper<GetAllRolesQuery, RolesResponse>
    {
        private readonly IIdentityService _identityService;

        public GetAllRolesQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<RolesResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();

            if (roles == null)
                return ServiceResult.Failed<RolesResponse>(ServiceError.ForbiddenError);

            return ServiceResult.Success(new RolesResponse
            {
                Roles = roles
            });
        }
    }
}