using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Dto.Facility
{
    public class FacOrderDto 
    {
        public Int64 id { get; set; }
        public string fix_no { get; set; }
        public string fix_describe { get; set; }
        public string fix_location { get; set; }
        public string fix_location_id { get; set; }
        public string fix_type { get; set; }
        public string fix_type_id { get; set; }
        public string uid { get; set; }
        public string? fix_uid { get; set; }
        public string status { get; set; }
        public string? rated { get; set; }
        public DateTime? confirmed_at { get; set; }
        public DateTime? finished_at { get; set; }
        public string? finish_img { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? finish_uid { get; set; }
        public string? finish_describe { get; set; }
        public string? first_pic { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime? rated_at { get; set; }
        public string remark { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? user_confirmed_at { get; set; }
        public string? fix_item { get; set; }

        public IList<FileStoresCreateDto> FileStores { get; set; }
        public IList<CompleteFileStoresCreateDto> CompleteFileStores { get; set; }
    }
}
