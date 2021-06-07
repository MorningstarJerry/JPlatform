using Application.Common.Models;
using Application.Common.Security;
using Application.Dto.Facility;
using Application.Facility.Commands;
using Application.Facility.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    public class FacilityController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<FacOrderCreateDto>>>> GetAllFacOrders([FromQuery] GetFacOrderQuery query,CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<FacOrderCreateDto>>> Create(CreateFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("confirm")]
        public async Task<ActionResult<ServiceResult<FacOrderCreateDto>>> Update(UpdateFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("finish")]
        public async Task<ActionResult<ServiceResult<FacOrderCreateDto>>> Finish(FinishFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
