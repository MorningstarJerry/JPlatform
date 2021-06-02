using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FileStores
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fileType { get; set; }
        public string extname { get; set; }
        public string path { get; set; }
        public long size { get; set; }
        public string url { get; set; }

        public int FacOrderId { get; set; }
        public FacOrder FacOrder { get; set; }
    }
}
