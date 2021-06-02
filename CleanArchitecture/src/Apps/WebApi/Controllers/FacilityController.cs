using Application.Common.Models;
using Application.Common.Security;
using Application.Dto.Facility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Facility.Commands;

namespace WebApi.Controllers
{
    [Authorize]
    public class FacilityController : BaseApiController
    {
        //[HttpGet("{id}")]
        //public async Task<FileResult> Get(int id)
        //{
        //    var vm = await Mediator.Send(new ExportDistrictsQuery { CityId = id });

        //    return File(vm.Content, vm.ContentType, vm.FileName);
        //}

        [HttpPost]
        public async Task<ActionResult<ServiceResult<FacOrderCreateDto>>> Create(CreateFacOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
