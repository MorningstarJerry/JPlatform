using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class FacilityUser 
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string sap_no { get; set; }
        public string phone { get; set; }
        public string factory_location { get; set; }
        public string wc { get; set; }
        public DateTime? deleted_at { get; set; }
        public string wechat_uid { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
