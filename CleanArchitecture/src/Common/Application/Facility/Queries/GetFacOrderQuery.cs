﻿using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Application.Dto.Facility;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Facility.Queries
{
    public class GetFacOrderQuery : IRequestWrapper<PaginatedList<FacOrderDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Status { get; set; } = 0;
    }

    public class GetFacOrdersQueryHandler : IRequestHandlerWrapper<GetFacOrderQuery, PaginatedList<FacOrderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFacOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<FacOrderDto>>> Handle(GetFacOrderQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<FacOrderDto> list = await _context.FacOrders.AsNoTracking()
                            .Where(x => x.status == request.Status.ToString())
                           .OrderByDescending(o => o.created_at)
                           .ProjectToType<FacOrderDto>(_mapper.Config)
                           .PaginatedListAsync(request.PageNumber, request.PageSize);


            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<FacOrderDto>>(ServiceError.NotFount);
        }
    }
}
