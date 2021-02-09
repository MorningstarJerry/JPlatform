using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sites;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sites.Commands
{
    public class DeleteSiteCommand : IRequestWrapper<SiteDto>
    {
        public string Id { get; set; }
    }

    public class DeleteSiteCommandHandler : IRequestHandlerWrapper<DeleteSiteCommand, SiteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteSiteCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SiteDto>> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
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

            return ServiceResult.Success(_mapper.Map<SiteDto>(entity));
        }
    }
}