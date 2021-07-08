using Application.Common.Models;
using Application.Common.Security;
using Application.Competency.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    public class ProcessController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<Process>>> GetProcessAllAsync(CancellationToken cancellationToken)
        {
            //return Ok(GetProcess());
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllCompetencyQuery(), cancellationToken));
        }

        private ServiceResult<List<Process>> GetProcess()
        {
            List<Process> list = new List<Process>() {
                new Process(1,"Heat Stake","Heat Stake",1,1.25,2.5),
                new Process(2,"Glue","Glue",2,1.25,2.5),
                new Process(3,"Label","Label",3,1.25,2.5),
                new Process(4,"Scan","Scan",4,1.25,2.5),
                new Process(5,"Packing","Packing",5,1.25,2.5),
                new Process(6,"螺丝","螺丝",6,1.25,2.5),
                new Process(7,"Soldering","Soldering",7,1.25,2.5),
                new Process(8,"Laser Marking","Laser Marking",8,1.25,2.5),
                new Process(9,"视觉检测","视觉检测",9,1.25,2.5),
                new Process(10,"Press fit","Press fit",10,1.25,2.5),
                new Process(11,"贴膜","贴膜",11,1.25,2.5),
                new Process(12,"线材装配","线材装配",12,1.25,2.5),
                new Process(13,"附件包装","附件包装",13,1.25,2.5),
                new Process(14,"成品自动包装","成品自动包装",14,1.25,2.5),
                new Process(15,"AGV","AGV",15,1.25,2.5),
                new Process(16,"堆垛","堆垛",16,1.25,2.5),
                new Process(17,"超声波焊接","超声波焊接",17,1.25,2.5),
                new Process(18,"ACF","ACF",18,1.25,2.5),
            };

            return ServiceResult.Success(list);
        }
    }

    public class Process
    {
        public Process(int id, string name, string description, int order, double capabilityForProcess, double capabilityForSme)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Order = order;
            this.CapabilityForProcess = capabilityForProcess;
            this.CapabilityForSme = capabilityForSme;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public double? CapabilityForProcess { get; set; }
        public double? CapabilityForSme { get; set; }
        public string Url { get; set; } = "https://vkceyugu.cdn.bspapp.com/VKCEYUGU-30905186-20b1-4fe9-9a60-86846a85f7f6/3dd5aa5e-5fe4-4b81-be1c-2b3003e311d8.jpg";
    }
}
