using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<District> Districts { get; set; }
        DbSet<Village> Villages { get; set; }
        DbSet<Site> Sites { get; set; }
        DbSet<Sector> Sectors { get; set; }
        DbSet<Workcell> Workcells { get; set; }
        DbSet<FacilityUser> FacilityUsers { get; set; }
        DbSet<FacOrder> FacOrders { get; set; }
        DbSet<ChargeMap> ChargeMaps { get; set; }
        DbSet<Competency_Process> Competency_Process { get; set; }
        DbSet<Competency_Category> Competency_Category { get; set; }
        DbSet<Competency_Critira> Competency_Critira { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}