using Application.Dto;
using System.Collections.Generic;

namespace Application.ApplicationRole.Queries.GetRoles
{
    public class RolesResponse
    {
        public List<ApplicationRoleDto> Roles { get; set; }
    }
}