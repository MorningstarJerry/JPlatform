using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Facility;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Facility.Commands
{
    public class FinishFacOrderCommand : IRequestWrapper<FacOrderCreateDto>
    {
        public FinishFacOrderCommand() { fileStores = new List<CompleteFileStoresCreateDto>(); }
        public long Id { get; set; }
        public string finish_uid { get; set; }
        public string finish_describe { get; set; }
        public IList<CompleteFileStoresCreateDto> fileStores { get; set; }
    }

    public class FinishFacOrderCommandHandler : IRequestHandlerWrapper<FinishFacOrderCommand, FacOrderCreateDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FinishFacOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<FacOrderCreateDto>> Handle(FinishFacOrderCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.facOrders.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FacOrder), request.Id);
            }

            if (!string.IsNullOrEmpty(request.finish_uid))
                entity.finish_uid = request.finish_uid;

            if (!string.IsNullOrEmpty(request.finish_describe))
                entity.finish_describe = request.finish_describe;

            if (request.fileStores != null && request.fileStores.Count > 0)
            {
                entity.status = "2";
                entity.finished_at = DateTime.Now;
                entity.CompleteFileStores = _mapper.Map<List<CompleteFileStores>>(request.fileStores);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<FacOrderCreateDto>(entity));
        }
    }
}
