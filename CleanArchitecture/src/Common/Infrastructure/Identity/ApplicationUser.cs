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
    }
}
