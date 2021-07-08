using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Competency;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Competency.Queries
{
    public class GetAllCompetencyQuery : IRequestWrapper<List<Competency_ProcessDto>>
    {

    }

    public class GetAllCompetencyHandler : IRequestHandlerWrapper<GetAllCompetencyQuery, List<Competency_ProcessDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCompetencyHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<Competency_ProcessDto>>> Handle(GetAllCompetencyQuery request, CancellationToken cancellationToken)
        {

            //var task = await _context.Competency_Process
            //    .Include(x => x.Categorys)
            //    .ThenInclude(c => c.Critiras)
            //    .ToListAsync(cancellationToken);

            List<Competency_ProcessDto> list = await _context.Competency_Process
                .Include(x => x.Categorys)
                .ThenInclude(c => c.Critiras)
                .Include(x => x.Dimensions)
                .ThenInclude(c => c.smeDetails)
                .ProjectToType<Competency_ProcessDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<Competency_ProcessDto>>(ServiceError.NotFount);
        }
    }
}
