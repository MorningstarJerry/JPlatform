using Application.Dto.Sectors;
using Application.Dto.Sites;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Workcells
{
    public class WorkcellDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public string Location { get; set; }
        public String SiteId { get; set; }
        public String SectorId { get; set; }
    }
}