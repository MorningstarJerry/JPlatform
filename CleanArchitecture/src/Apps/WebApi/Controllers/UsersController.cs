using Application.ApplicationUser.Commands.Update;
using Application.ApplicationUser.Queries;
using Application.Common.Models;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> GetUserById([FromQuery] string id)
        {
            return Ok(await Mediator.Send(new GetUserbyIdQuery { id = id }));
        }

        [HttpPut("avatar")]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> Update(UpadteUserAvatar command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
