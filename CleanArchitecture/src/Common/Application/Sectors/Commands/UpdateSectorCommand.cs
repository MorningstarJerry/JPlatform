using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sectors;
using Domain.Entities;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sectors.Commands
{
    public class UpdateSectorCommand : IRequestWrapper<SectorDto>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateSectorCommandHandler : IRequestHandlerWrapper<UpdateSectorCommand, SectorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSectorCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SectorDto>> Handle(UpdateSectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cities.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SectorDto>(entity));
        }
    }
}