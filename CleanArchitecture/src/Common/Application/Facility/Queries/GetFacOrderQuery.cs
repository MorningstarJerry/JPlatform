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
    public class GetFacOrderQuery : IRequestWrapper<PaginatedList<FacOrderCreateDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Status { get; set; } = 0;
    }

    public class GetFacOrdersQueryHandler : IRequestHandlerWrapper<GetFacOrderQuery, PaginatedList<FacOrderCreateDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFacOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<FacOrderCreateDto>>> Handle(GetFacOrderQuery request, CancellationToken cancellationToken)
        {
            //var test = await _context.facOrders.AsNoTracking();
            /*
            PaginatedList<FacOrderCreateDto> list = await _context.facOrders.AsNoTracking()
                            .Where(x => x.status == 0)
                            .OrderByDescending(o => o.created_at)
                            .Include(d => d.FileStores)
                            .ProjectToType<FacOrderCreateDto>(_mapper.Config)
                            .PaginatedListAsync(request.PageNumber, request.PageSize);
            */
            PaginatedList<FacOrderCreateDto> list = await _context.facOrders.AsNoTracking()
                            .Where(x => x.status == request.Status.ToString())
                           .OrderByDescending(o => o.created_at)
                           .ProjectToType<FacOrderCreateDto>(_mapper.Config)
                           .PaginatedListAsync(request.PageNumber, request.PageSize);


            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<FacOrderCreateDto>>(ServiceError.NotFount);
        }
    }
}
