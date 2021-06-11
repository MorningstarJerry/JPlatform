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
    public class EvaluationFacOrderCommand : IRequestWrapper<FacOrderDto>
    {
        public long Id { get; set; }
        public string Rate { get; set; }
       
    }

    public class EvaluationFacOrderHandler : IRequestHandlerWrapper<EvaluationFacOrderCommand, FacOrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EvaluationFacOrderHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<FacOrderDto>> Handle(EvaluationFacOrderCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.FacOrders.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            if (!string.IsNullOrEmpty(request.Rate))
            {
                entity.rated = request.Rate;
                entity.rated_at = DateTime.Now;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<FacOrderDto>(entity));
        }
    }
}

