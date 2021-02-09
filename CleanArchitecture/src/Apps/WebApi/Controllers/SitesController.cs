using Application.Common.Models;
using Application.Dto.Sites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Sites.Queries;
using Application.Sites.Commands;

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