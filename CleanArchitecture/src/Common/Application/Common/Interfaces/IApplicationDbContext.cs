using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}