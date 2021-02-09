using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sectors;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sectors.Commands
{
    public class DeleteSectorCommand : IRequestWrapper<SectorDto>
    {
        public string Id { get; set; }
    }

    public class DeleteSectorCommandHandler : IRequestHandlerWrapper<DeleteSectorCommand, SectorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteSectorCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SectorDto>> Handle(DeleteSectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Sites
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            _context.Sites.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SectorDto>(entity));
        }
    }
}