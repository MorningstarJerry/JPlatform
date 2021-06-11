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
        public async Task<ActionResult<ServiceResult<List<FacOrderDto>>>> GetAllFacOrders([FromQuery] GetFacOrderQuery query,CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        [Route("user/orders")]
        public async Task<ActionResult<ServiceResult<List<FacOrderDto>>>> GetUserFacOrders([FromQuery] GetFacOrderByUserQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        [Route("myself/created/orders")]
        public async Task<ActionResult<ServiceResult<List<FacOrderDto>>>> GetMyCreateFacOrders([FromQuery] GetMyCreateFacOrderQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        [Route("myself/updated/orders")]
        public async Task<ActionResult<ServiceResult<List<FacOrderDto>>>> GetMyUpdateFacOrders([FromQuery] GetMyHandleFacOrderQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }


        [HttpGet]
        [Route("myself/finished/orders")]
        public async Task<ActionResult<ServiceResult<List<FacOrderDto>>>> GetMyFinishedFacOrders([FromQuery] GetMyFinishFacOrderQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<FacOrderDto>>> Create(CreateFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("confirm")]
        public async Task<ActionResult<ServiceResult<FacOrderDto>>> Update(UpdateFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("finish")]
        public async Task<ActionResult<ServiceResult<FacOrderDto>>> Finish(FinishFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("evaluation")]
        public async Task<ActionResult<ServiceResult<FacOrderDto>>> Evaluation(EvaluationFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
