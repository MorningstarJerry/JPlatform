using Domain.Common;

namespace Domain.Entities
{
    public class Competency_Critira : AuditableEntity
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
        public Competency_Category category { get; set; }

        public string current { get; set; }
        public decimal score { get; set; }

    }
}
