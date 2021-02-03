using Application.ApplicationRole.Queries.GetRoles;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    public class RolesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<RolesResponse>>> GetAllRoles(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllRolesQuery(), cancellationToken));
        }
    }
}