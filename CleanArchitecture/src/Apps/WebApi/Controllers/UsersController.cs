using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.ApplicationUser.Queries;
using Application.Cities.Commands.Create;
using Application.Cities.Commands.Delete;
using Application.Cities.Commands.Update;
using Application.Cities.Queries.GetCities;
using Application.Cities.Queries.GetCityById;
using Application.Common.Models;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
