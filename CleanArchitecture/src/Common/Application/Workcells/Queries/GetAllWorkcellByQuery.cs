using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Workcells;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Workcells.Queries
{
    public class GetAllWorkcellByQuery : IRequestWrapper<List<WorkcellDto>>
    {
    }

    public class GetAllSitesQueryHandler : IRequestHandlerWrapper<GetAllWorkcellByQuery, List<WorkcellDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSitesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<WorkcellDto>>> Handle(GetAllWorkcellByQuery request, CancellationToken cancellationToken)
        {
            List<WorkcellDto> list = await _context.Sites.AsNoTracking()
                .ProjectToType<WorkcellDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<WorkcellDto>>(ServiceError.NotFount);
        }
    }
}