using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sectors;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sectors.Queries
{
    public class GetAllSectorsQuery : IRequestWrapper<List<SectorDto>>
    {
    }

    public class GetAllSectorsQueryHandler : IRequestHandlerWrapper<GetAllSectorsQuery, List<SectorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSectorsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<SectorDto>>> Handle(GetAllSectorsQuery request, CancellationToken cancellationToken)
        {
            List<SectorDto> list = await _context.Sectors.AsNoTracking()
                .ProjectToType<SectorDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<SectorDto>>(ServiceError.NotFount);
        }
    }
}