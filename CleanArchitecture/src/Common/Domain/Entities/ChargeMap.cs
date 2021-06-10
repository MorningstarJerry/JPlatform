using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ChargeMap
    {
		public int id { get; set; }
		public string sn { get; set; }
		public int area_id { get; set; }
		public int type_id { get; set; }
		public string name { get; set; }
		public string sap_no { get; set; }
		public byte status { get; set; }
		public byte[] created_at { get; set; }
		public byte[] updated_at { get; set; }
		public int rownum { get; set; }
	}
}
