using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FacOrder
    {
        public FacOrder()
        {
            FileStores = new List<FileStores>();
        }

        public int id { get; set; }
        public string fix_no { get; set; }
        public string fix_describe { get; set; }
        public string fix_location { get; set; }
        public int fix_location_id { get; set; }
        public string fix_type { get; set; }
        public int fix_type_id { get; set; }
        public string uid { get; set; }
        public string? fix_uid { get; set; }
        public int status { get; set; }
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

        public IList<FileStores> FileStores { get; set; }
    }
}
