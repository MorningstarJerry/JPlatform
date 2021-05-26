using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Gsm { get; set; }

        public Department Department { get; set; }

        public Permission Permission { get; set; }

        public Workcell Workcell { get; set; }

        public Site Site { get; set; }

        public Sector Sector { get; set; }

        public int type { get; set; }

        public string sap_no { get; set; }

        public string phone { get; set; }

        public string factory_location { get; set; }

        public string wc { get; set; }
    }
}
