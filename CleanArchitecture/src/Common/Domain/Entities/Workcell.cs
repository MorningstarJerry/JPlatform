using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Workcell : GenGUIDKey
    {
        public Workcell(string Name) : base(Name)
        {
        }
        
        public string Pic { get; set; }
        public string Location { get; set; }
        public string SiteId { get; set; }
        public string SectorId { get; set; }
    }
}