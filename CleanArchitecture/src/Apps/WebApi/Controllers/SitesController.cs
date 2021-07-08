using Application.Common.Models;
using Application.Dto.Sites;
using Application.Sites.Commands;
using Application.Sites.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    public class SitesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<SiteDto>>>> GetAllSites(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllSitesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<SiteDto>>> GetCityById(string id)
        {
            return Ok(await Mediator.Send(new GetSitesByIdQuery { SiteId = id }));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<SiteDto>>> Create(CreateSiteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<SiteDto>>> Update(UpdateSiteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<SiteDto>>> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteSiteCommand { Id = id }));
        }
    }
}