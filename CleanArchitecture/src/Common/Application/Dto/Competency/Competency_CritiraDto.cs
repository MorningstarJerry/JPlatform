using System;

namespace Application.Dto.Competency
{
    public class Competency_CritiraDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string gradea { get; set; }
        public string gradeb { get; set; }
        public string gradec { get; set; }
        public string graded { get; set; }
        public string gradea_url { get; set; }
        public string gradeb_url { get; set; }
        public string gradec_url { get; set; }
        public string graded_url { get; set; }
        public int categoryId { get; set; }
        public string current { get; set; }
        public decimal score { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
