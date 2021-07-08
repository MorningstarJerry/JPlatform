using System;

namespace Domain.Entities
{
    public class FileStores
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? fileType { get; set; }
        public string? extname { get; set; }
        public string? path { get; set; }
        public string? size { get; set; }
        public string? url { get; set; }

        public Int64 FacOrderId { get; set; }
        public FacOrder FacOrder { get; set; }
    }
}
