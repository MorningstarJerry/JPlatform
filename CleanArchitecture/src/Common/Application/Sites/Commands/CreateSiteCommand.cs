using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Sites;
using Domain.Entities;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sites.Commands
{
    public class CreateSiteCommand : IRequestWrapper<SiteDto>
    {
        public string Name { get; set; }
    }

    public class CreateSiteCommandHandler : IRequestHandlerWrapper<CreateSiteCommand, SiteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSiteCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SiteDto>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            var entity = new Site(request.Name);

            await _context.Sites.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SiteDto>(entity));
        }
    }
}