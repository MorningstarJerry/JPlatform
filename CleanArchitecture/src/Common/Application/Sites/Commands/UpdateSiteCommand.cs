using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sites;
using Domain.Entities;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sites.Commands
{
    public class UpdateSiteCommand : IRequestWrapper<SiteDto>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateSiteCommandHandler : IRequestHandlerWrapper<UpdateSiteCommand, SiteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSiteCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SiteDto>> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cities.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SiteDto>(entity));
        }
    }
}