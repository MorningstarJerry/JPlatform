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
    public class UpdateFacOrderCommand : IRequestWrapper<FacOrderDto>
    {
        public long Id { get; set; }
        public string fix_uid { get; set; }
        public string remark { get; set; }
        public string status { get; set; }
        public string due_date { get; set; }
    }

    public class UpdateFacOrderCommandHandler : IRequestHandlerWrapper<UpdateFacOrderCommand, FacOrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFacOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<FacOrderDto>> Handle(UpdateFacOrderCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.FacOrders.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            if (!string.IsNullOrEmpty(request.fix_uid))
                entity.fix_uid = request.fix_uid;

            if (!string.IsNullOrEmpty(request.remark))
                entity.remark = request.remark;

            if (!string.IsNullOrEmpty(request.status))
                entity.status = request.status;

            if (!string.IsNullOrEmpty(request.due_date))
                entity.due_date = Convert.ToDateTime(request.due_date);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<FacOrderDto>(entity));
        }
    }
}

