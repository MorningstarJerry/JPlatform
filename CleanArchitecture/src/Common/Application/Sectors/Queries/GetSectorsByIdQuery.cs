using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sectors;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sectors.Queries
{
    public class GetSectorsByIdQuery : IRequestWrapper<SectorDto>
    {
        public string SectorId { get; set; }
    }

    public class GetSectorsByIdQueryHandler : IRequestHandlerWrapper<GetSectorsByIdQuery, SectorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSectorsByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SectorDto>> Handle(GetSectorsByIdQuery request, CancellationToken cancellationToken)
        {
            SectorDto sector = await _context.Sectors
             .Where(x => x.Id == request.SectorId)
             .ProjectToType<SectorDto>(_mapper.Config)
             .FirstOrDefaultAsync(cancellationToken);

            return sector != null ? ServiceResult.Success(sector) : ServiceResult.Failed<SectorDto>(ServiceError.NotFount);
        }
    }
}