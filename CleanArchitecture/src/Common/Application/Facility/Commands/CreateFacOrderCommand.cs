using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto.Facility;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Facility.Commands
{
    public class CreateFacOrderCommand : IRequestWrapper<FacOrderDto>
    {
        public CreateFacOrderCommand() { fileStores = new List<FileStoresCreateDto>(); }
        public string fix_location { get; set; }
        public int fix_location_id { get; set; }
        public string fix_type { get; set; }
        public int fix_type_id { get; set; }
        public string? fix_item { get; set; }
        public string fix_describe { get; set; }
        public string uid { get; set; }
        public int status { get; set; } = 0;
        public IList<FileStoresCreateDto> fileStores { get; set; }
    }

    public class CreateFacOrderCommandHandler : IRequestHandlerWrapper<CreateFacOrderCommand, FacOrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFacOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<FacOrderDto>> Handle(CreateFacOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new FacOrder
            {
                fix_no = DateTime.Now.Ticks.ToString(),
                fix_location_id = request.fix_location_id.ToString(),
                fix_location = request.fix_location,
                fix_type_id = request.fix_type_id.ToString(),
                fix_type = request.fix_type,
                fix_item = request.fix_item,
                remark = request.fix_describe,
                fix_describe = request.fix_describe,
                uid = request.uid,
                first_pic = GetfirstPics(request.fix_location_id, request.fix_type_id),
                status = request.status.ToString(),
                created_at =  DateTime.Now,
                FileStores = _mapper.Map<List<FileStores>>(request.fileStores)
            };

            await _context.FacOrders.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<FacOrderDto>(entity));
        }

        private string GetfirstPics(int fix_location_id, int fix_type_id)
        {
            var array = _context.ChargeMaps.AsNoTracking()
                 .Where(x => x.area_id == fix_location_id && x.type_id == fix_type_id).Select(x => x.sap_no).ToArray();

            var str = string.Join(",", array);
            return string.IsNullOrEmpty(str) ? "" : str;
        }
    }
}
