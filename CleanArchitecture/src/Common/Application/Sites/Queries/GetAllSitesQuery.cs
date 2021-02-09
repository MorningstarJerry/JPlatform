using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sites;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Sites.Queries
{
    public class GetAllSitesQuery : IRequestWrapper<List<SiteDto>>
    {
    }

    public class GetAllSitesQueryHandler : IRequestHandlerWrapper<GetAllSitesQuery, List<SiteDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSitesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<SiteDto>>> Handle(GetAllSitesQuery request, CancellationToken cancellationToken)
        {
            List<SiteDto> list = await _context.Sites.AsNoTracking()
                .ProjectToType<SiteDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<SiteDto>>(ServiceError.NotFount);
        }
    }
}