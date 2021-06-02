using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Facility
{
    public class FileStoresCreateDto
    {
        public string name { get; set; }
        public string fileType { get; set; }
        public string extname { get; set; }
        public string path { get; set; }
        public long size { get; set; }
        public string url { get; set; }
    }
}
