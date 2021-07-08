using Domain.Common;

namespace Domain.Entities
{
    public class Competency_SmeDetails_History : AuditableEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string definition { get; set; }
        public string gradea { get; set; }
        public string gradeb { get; set; }
        public string gradec { get; set; }
        public string graded { get; set; }
        public decimal weight { get; set; }
        public decimal score { get; set; }
        public int year { get; set; }
        public int week { get; set; }

        public int dimensionId { get; set; }
        public Competency_SmeDimension dimension { get; set; }
    }
}
