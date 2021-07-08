using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Application.Dto.Facility;
using Domain.Entities;
using Domain.Enums;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
        private readonly IHttpClientHandler _httpClient;
        private string ClientApi { get; } = "uni-appSms-api";
        private readonly IIdentityService _identityService;

        public CreateFacOrderCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpClientHandler httpClient, IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
            _identityService = identityService;
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
                created_at = DateTime.Now,
                FileStores = _mapper.Map<List<FileStores>>(request.fileStores)
            };

            await _context.FacOrders.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            //string[] saps = 
            var createUser = await _identityService.GetUserById(request.uid);

            /*
            var facUsers = GetfirstPicsUsers(request.fix_location_id, request.fix_type_id);
            foreach (var user in facUsers)
            {
                SmsRequest reqSms = new SmsRequest(user.phone, user.name, createUser.wc, createUser.LastName);
                await _httpClient.GenericRequest<SmsRequest, SmsResponse>(ClientApi, string.Concat("https://30905186-20b1-4fe9-9a60-86846a85f7f6.bspapp.com/http/sendSms?", StringExtensions
                  .ParseObjectToQueryString(reqSms, true)), cancellationToken, MethodType.Get);
            }

            */
            //FOR TEST
            SmsRequest reqSms = new SmsRequest("18588850203", "Jerry Liu", createUser.wc, createUser.LastName);
            await _httpClient.GenericRequest<SmsRequest, SmsResponse>(ClientApi, string.Concat("https://30905186-20b1-4fe9-9a60-86846a85f7f6.bspapp.com/http/sendSms?", StringExtensions
              .ParseObjectToQueryString(reqSms, true)), cancellationToken, MethodType.Get);
            Log.Information("a new facility doc created");
            return ServiceResult.Success(_mapper.Map<FacOrderDto>(entity));
        }

        private string GetfirstPics(int fix_location_id, int fix_type_id)
        {
            var array = _context.ChargeMaps.AsNoTracking()
                 .Where(x => x.area_id == fix_location_id && x.type_id == fix_type_id).Select(x => x.sap_no).ToArray();

            var str = string.Join(",", array);
            return string.IsNullOrEmpty(str) ? "" : str;
        }

        private List<FacilityUser> GetfirstPicsUsers(int fix_location_id, int fix_type_id)
        {
            string firstPicIds = GetfirstPics(fix_location_id, fix_type_id);
            string[] sapIdAry = firstPicIds.Split(",");

            List<FacilityUser> list = new List<FacilityUser>();

            foreach (var charge in sapIdAry)
            {
                var facUser = _context.FacilityUsers.AsNoTracking().FirstOrDefault(x => x.sap_no == charge);
                if (facUser != null)
                {
                    list.Add(facUser);
                }
            }

            return list;
        }
    }

    public class SmsRequest
    {
        public string phone { get; set; }
        public string pic { get; set; }
        public string dep { get; set; }
        public string createby { get; set; }
        public SmsRequest(string phone, string pic, string dep, string createby)
        {
            this.phone = phone;
            this.pic = pic;
            this.dep = dep;
            this.createby = createby;
        }
    }

    public class SmsResponse
    {
        public string code { get; set; }
        public string errorCode { get; set; }
        public string success { get; set; }
    }
}