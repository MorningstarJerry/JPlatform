using System.Collections.Generic;
using System.Linq;
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
    public class GetWorkcellByIdQuery : IRequestWrapper<WorkcellDto>
    {
        public string WorkcellId { get; set; }
    }

    public class GetSitesByIdQueryHandler : IRequestHandlerWrapper<GetWorkcellByIdQuery, SiteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSitesByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SiteDto>> Handle(GetSitesByIdQuery request, CancellationToken cancellationToken)
        {
            SiteDto site = await _context.Sites
                .Where(x => x.Id == request.SiteId)
                .ProjectToType<SiteDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return site != null ? ServiceResult.Success(site) : ServiceResult.Failed<SiteDto>(ServiceError.NotFount);
        }
    }
}