using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sectors;
using Domain.Entities;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sectors.Commands
{
    public class CreateSectorCommand : IRequestWrapper<SectorDto>
    {
        public string Name { get; set; }
    }

    public class CreateSectorCommandHandler : IRequestHandlerWrapper<CreateSectorCommand, SectorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSectorCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SectorDto>> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
        {
            var entity = new Site(request.Name);

            await _context.Sites.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SectorDto>(entity));
        }
    }
}