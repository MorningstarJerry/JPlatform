using Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApplicationRole.Queries.GetRoles
{
    public class RolesResponse
    {
        public List<ApplicationRoleDto> Roles { get; set; }
    }
}